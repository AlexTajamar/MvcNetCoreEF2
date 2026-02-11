using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreEF2.Models;
using MvcNetCoreEF2.Repositories;

namespace MvcNetCoreEF2.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;

        public EnfermosController(RepositoryEnfermos repo)
        {
            this.repo = repo;
        }

        // GET: Enfermos
        public async Task<ActionResult> Index()
        {
            List<Enfermo> enfermos = await this.repo.GetEnfermosAsync();
            return View(enfermos);
        }

        public async Task<ActionResult> Details(string inscripcion)
        {
            Enfermo enfermo = await this.repo.GetOneEnfermo(inscripcion);
            return View(enfermo);
        }

        public async Task<ActionResult> Delete(string inscripcion)
        {
            await this.repo.DeleteEnfermoAsync(inscripcion);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteRaw(string inscripcion)
        {
            await this.repo.DeleteEnfermoRawAsync(inscripcion);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Enfermo e)
        {
            
            await this.repo.InsertEnfermoAsync(e.Inscripcion, e.Apellido, e.Direccion, e.FechaNac);
            return RedirectToAction("Index");
        }

    }
}