using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }

}
