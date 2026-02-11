using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreEF2.Data;
using MvcNetCoreEF2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MvcNetCoreEF2.Repositories
{
    #region
    //    create procedure SP_ALL_ENFERMOS
    //as
    //	select* from ENFERMO
    //go


    //create procedure SP_FIND_ENFERMO
    //(@inscripcion nvarchar(50))
    //as
    //	select* from ENFERMO where INSCRIPCION = @inscripcion
    //go


    //create procedure SP_DELETE_ENFERMO
    //(@inscripcion nvarchar(50))
    //as
    //	delete from ENFERMO where INSCRIPCION = @inscripcion
    //go


//    CREATE PROCEDURE SP_INSERT_ENFERMO
//    @inscripcion NVARCHAR(50),
//    @apellido NVARCHAR(50), 
//    @direccion NVARCHAR(50),
//    @fecha DATETIME
//AS
//BEGIN
//    INSERT INTO ENFERMO(INSCRIPCION, APELLIDO, DIRECCION, FECHA_NAC)
//    VALUES(@inscripcion, @apellido, @direccion, @fecha);
//    END
//    GO


    #endregion
    public class RepositoryEnfermos
    {
        private EnfermoContext context;
        public RepositoryEnfermos(EnfermoContext context)
        {
            this.context = context;
        }

        public async Task<List<Enfermo>> GetEnfermosAsync()
        {
            var enfermos = new List<Enfermo>();

            await using DbCommand com = this.context.Database.GetDbConnection().CreateCommand();
            com.CommandText = "SP_ALL_ENFERMOS";
            com.CommandType = CommandType.StoredProcedure;

            await this.context.Database.OpenConnectionAsync();

            await using DbDataReader reader = await com.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Enfermo enfermo = new Enfermo
                {
                    Inscripcion = reader["INSCRIPCION"].ToString(),
                    Apellido = reader["APELLIDO"].ToString(),
                    Direccion = reader["DIRECCION"].ToString(),
                    FechaNac = DateTime.Parse(reader["FECHA_NAC"].ToString()) // o GetDateTime(3)
                };

                enfermos.Add(enfermo);
            }

            await this.context.Database.CloseConnectionAsync();
            return enfermos;
        }


        public async Task<Enfermo> GetOneEnfermo(string inscripcion)
        {
            string sql = "SP_FIND_ENFERMO @inscripcion";
            SqlParameter pamIns = new SqlParameter("@inscripcion", inscripcion);
            // Await the Task<List<Enfermo>> before calling FirstOrDefault
            var consulta = await this.context.Enfermos.FromSqlRaw(sql, pamIns).ToListAsync();
            Enfermo enfermo = consulta.FirstOrDefault();
            return enfermo;

        }

        public async Task DeleteEnfermoAsync(string inscripcion)
        {
            string sql = "SP_DELETE_ENFERMO";
            SqlParameter pamIns = new SqlParameter("@inscripcion", inscripcion);
            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Parameters.Add(pamIns);
                await com.Connection.OpenAsync();
                await com.ExecuteNonQueryAsync();
                await com.Connection.CloseAsync();
                com.Parameters.Clear();
            }
        }

        public async Task DeleteEnfermoRawAsync(string inscripcion)
        {
            string sql = "SP_DELETE_ENFERMO @inscripcion";
            SqlParameter panIns = new SqlParameter("@inscripcion", inscripcion);
            await this.context.Database.ExecuteSqlRawAsync(sql, panIns);
            
        }

        public async Task InsertEnfermoAsync(string inscripcion,string apellido,string direccion,DateTime fecha)
        {
            string sql = "SP_INSERT_ENFERMO_CHAT @inscripcion,@apellido,@direccion,@fecha";
            SqlParameter pamIns = new SqlParameter("@inscripcion", inscripcion);
            SqlParameter pamApe = new SqlParameter("@apellido", apellido);
            SqlParameter pamDir = new SqlParameter("@direccion", direccion);
            SqlParameter pamFec = new SqlParameter("@fecha", fecha);

            await this.context.Database.ExecuteSqlRawAsync(sql, pamIns, pamApe, pamDir, pamFec);


        }






    }
}
