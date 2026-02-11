using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEF2.Models;
using MvcNetCoreEF2.Repositories;

namespace MvcNetCoreEF2.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;
        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()

        {
            var especialidades = new Especialidades();
             especialidades = await this.repo.GetDoctoresAsync();


            return View(especialidades);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string especialidad,int aumento)
        {
            var especialidades = new Especialidades();
            especialidades = await this.repo.GetDoctoresAsync();
            await this.repo.UpdateDoctoresEspecialidadAsync(especialidad, aumento);
            especialidades.DoctoresList = await this.repo.GetDoctoresEspecialidadAsync(especialidad);
            return View(especialidades);
        }

    }
}
