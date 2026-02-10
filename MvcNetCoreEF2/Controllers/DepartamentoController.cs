using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEF2.Models;
using MvcNetCoreEF2.Repositories;
using System.Threading.Tasks;

namespace MvcNetCoreEF2.Controllers
{
    public class DepartamentoController : Controller
    {
        private RepositoryDept repo;

        public DepartamentoController(RepositoryDept repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Departamento> hospitales = await this.repo.GetDeptAsync();
            return View(hospitales);
        }

        public async Task<IActionResult> Details(int dept)
        {
            Departamento d = await this.repo.FindDEPTByIdAsync(dept);
            return View(d);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int dept, string nombre, string loc)
        {
            await this.repo.InsertDEPTAsync(dept, nombre, loc);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int dept)
        {
            await this.repo.DeleteHospitalByIdAsync(dept);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int dept)
        {
            Departamento d = await this.repo.FindDEPTByIdAsync(dept);

            return View(d);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idhospital, string nombre, string loc)
        {
            await this.repo.UpdateHospitalAsync(idhospital, nombre, loc);

            return RedirectToAction("Index");
        }
    }
}
