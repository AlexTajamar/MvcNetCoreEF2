using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreEF2.Data;
using MvcNetCoreEF2.Models;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;


#region

//Create procedure SP_DOCTORES_ESP
//(@especialidad nvarchar(50))
//as
//select * from doctor
//where ESPECIALIDAD=@especialidad
//go

//alter procedure SP_UPDATE_DOCTORES_ESPECIALIDAD
//(@especialidad varchar(50), @salario int )
//as
//update DOCTOR set SALARIO=SALARIO+@salario
//where ESPECIALIDAD=@especialidad;
//go

//alter procedure SP_ALL_DOCTORES
//as
//	select DISTINCT ESPECIALIDAD from DOCTOR

//go
#endregion
namespace MvcNetCoreEF2.Repositories
{
    public class RepositoryDoctores
    {
        private EnfermoContext context;
        public RepositoryDoctores(EnfermoContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetEspecialidadesAsync()
        {
            return await this.context.Doctor
                .Select(d => d.ESPECIALIDAD)
                .Distinct()
                .ToListAsync();    
        }

        public async Task<Especialidades> GetDoctoresAsync()
        {
            await using DbCommand com = this.context.Database.GetDbConnection().CreateCommand();
            com.CommandText = "SP_ALL_DOCTORES";
            com.CommandType = CommandType.StoredProcedure;
            await this.context.Database.OpenConnectionAsync();

            await using DbDataReader reader = await com.ExecuteReaderAsync();
            Especialidades especialidades = new Especialidades();
            especialidades.EspecialidadesList = new List<string>();

            while (await reader.ReadAsync())
            {
                especialidades.EspecialidadesList.Add(reader["ESPECIALIDAD"].ToString());
            }

            await this.context.Database.CloseConnectionAsync();

            return especialidades;

        }

        public async Task UpdateDoctoresEspecialidadAsync(string especialidad,int aumento)
        {
            string sql = "SP_UPDATE_DOCTORES_ESPECIALIDAD @especialidad, @salario";
            SqlParameter pamEsp = new SqlParameter("@especialidad", especialidad);
            SqlParameter pamSal = new SqlParameter("@salario", aumento);

            await this.context.Database.ExecuteSqlRawAsync(sql,pamEsp,pamSal);
        }

        public async Task UpdateDoctoresEspecialidadAsyncSinRaw(string especialidad, int aumento)
        {
            var doctores = await this.context.Doctor
                .Where(d => d.ESPECIALIDAD == especialidad)
                .ToListAsync();

            foreach (var doc in doctores)
            {
                doc.SALARIO += aumento;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> GetDoctoresEspecialidadAsync(string especialidad)
        {
            string sql = "SP_DOCTORES_ESP @especialidad";
            SqlParameter pamEsp = new SqlParameter("@especialidad", especialidad);

            return await this.context.Doctor
                .FromSqlRaw(sql, pamEsp)
                .ToListAsync();
        }

    }
}
