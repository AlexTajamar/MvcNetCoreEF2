using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEF2.Data;
using MvcNetCoreEF2.Models;
using System;
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
                    Inscripcion = reader.GetInt32(0),
                    Apellido = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    FechaNac = reader.GetString(3) // o GetDateTime(3)
                };

                enfermos.Add(enfermo);
            }

            await this.context.Database.CloseConnectionAsync();
            return enfermos;
        }

    
}
}
