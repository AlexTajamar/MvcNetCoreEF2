using Microsoft.EntityFrameworkCore;
using MvcNetCoreEF2.Data;
using MvcNetCoreEF2.Models;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcNetCoreEF2.Repositories
{
    public class RepositoryDept
    {
        private DeptContext context;

        public RepositoryDept(DeptContext context)
        {
            this.context = context;
        }

        public async Task<List<Departamento>> GetDeptAsync()
        {
            var consulta = from datos in this.context.Departamentos select datos;

            return await consulta.ToListAsync();
        }

        public async Task<Departamento> FindDEPTByIdAsync(int dept)
        {
            var consulta = from datos in this.context.Departamentos where datos.Dept == dept select datos;

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task InsertDEPTAsync(int dept, string nombre, string loc )
        {
            Departamento d = new Departamento()
            {
                Dept = dept,
                Nombre = nombre,
                Localizacion = loc,
              
            };

            await this.context.AddAsync(d);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateHospitalAsync(int dept, string nombre, string loc)
        {
            Departamento departamento = await this.FindDEPTByIdAsync(dept);

            departamento.Dept = dept;
            departamento.Nombre = nombre;
            departamento.Localizacion = loc;
           

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteHospitalByIdAsync(int dept)
        {
            Departamento departamento = await FindDEPTByIdAsync(dept);

            // Eliminamos temporalmente de la colección
            this.context.Departamentos.Remove(departamento);

            // Confirmamos los cambios en la BBDD
            await this.context.SaveChangesAsync();
        }
    }
}
