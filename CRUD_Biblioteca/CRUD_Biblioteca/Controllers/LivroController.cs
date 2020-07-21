using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHibernate;
using NHibernate.Linq;

namespace CRUD_Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        private readonly ISession _session;

        public LivroController(ISession session)
        {
            _session = session;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _session.Query<Livro>().ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Autores"] = new SelectList( _session.CreateQuery("select s from Autor s").List<Autor>().ToList(),
                "Id",
                "Nome"
            );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Livro livro)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(livro);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(livro);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewData["Autores"] = new SelectList(_session.CreateQuery("select s from Autor s").List<Autor>().ToList(),
                   "Id",
                   "Nome"
               );
            return View(await _session.GetAsync<Livro>(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Livro livro)
        {
            if (livro.Id != id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveOrUpdateAsync(livro);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _session.GetAsync<Livro>(id);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                await _session.DeleteAsync(livro);
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}