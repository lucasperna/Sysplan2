using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;

namespace CRUD_Biblioteca.Controllers
{
    public class AutorController : Controller
    {
        private readonly ISession _session;

        public AutorController(ISession session)
        {
            _session = session;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _session.Query<Autor>().ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Autor autor )
        {
            if(ModelState.IsValid)
            {
                using(ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(autor);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(autor);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _session.GetAsync<Autor>(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Autor autor)
        {
            if(autor.Id != id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                using(ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveOrUpdateAsync(autor);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _session.GetAsync<Autor>(id);

            using(ITransaction transaction = _session.BeginTransaction())
            {
                await _session.DeleteAsync(autor);
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}