using Developer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Developer.Controllers
{
    public class MenuController: Controller
    {
        private readonly Contexto _context;

        public MenuController(Contexto context)
        {
            _context = context;
        }


         public async Task<IActionResult> Index()
        {
            var lista = _context.Menu
            .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0")
            .ToList();

            var listaMenuPai = _context.Menu
            .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = 0")
            .ToList();

            ViewBag.Lista = listaMenuPai;
                    
            return View(lista);

        }


        //PÓST
        [HttpPost]
        public async Task<IActionResult> Index(int filtroMenuPai, string filtroBack)
        {
            var lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0")
                .ToList();

            if (filtroMenuPai != 0 && filtroBack != "0"){
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + filtroMenuPai + "and Back = '" + filtroBack + "'")
                .ToList();
            }
            if (filtroMenuPai != 0)
            {
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + filtroMenuPai)
                .ToList();
            }
            if (filtroBack != "0")
            {
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0 and Back = '" + filtroBack + "'")
                .ToList();
            }

            var listaMenuPai = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = 0")
                .ToList();

            ViewBag.Lista = listaMenuPai;

            return View(lista);        
            
        }


        // GET: 
        public async Task<IActionResult> Create()
        {
            var lista = await _context.Menu.ToListAsync();
            ViewBag.Lista = lista;
            return View();
        }


         // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Formulario,Funcao,Link,Back,Front,Layout,ObsApp,OriginTable,DestinTable,ObsTable,MenuPai_Id")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

         // GET: 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Formulario,Funcao,Link,Back,Front,Layout,ObsApp,OriginTable,DestinTable,ObsTable,MenuPai_Id")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(menu);
        }


        // GET:
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }


        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.Id == id);
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }



        /*public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + id)
                .ToList();

                return View(lista);
            }
            else{
                var lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0")
                .ToList();

                var listaMenuPai = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = 0")
                .ToList();

                ViewBag.Lista = listaMenuPai;
                       
                return View(lista);
            }
        }*/

    }
}