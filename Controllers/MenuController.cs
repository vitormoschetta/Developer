using Developer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http; 

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
            // ListaMenu com retorno padrão
            ViewBag.ListaMenu = await _context.Menu.Where(m => m.MenuPai_Id != 0).ToListAsync();

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> ListaItensMenu(int filtroMenuPai, string filtroBack, string filtroFront, string filtroLayout)
        {
            // ListaMenu com retorno padrão
            ViewBag.ListaMenu = await _context.Menu.Where(m => m.MenuPai_Id != 0).ToListAsync();

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            // Filtrando consulta pelos selects enviados
            string query = "SELECT * FROM Menu where MenuPai_Id != 0 ";
            if (filtroMenuPai != 0){
                query = query + " and MenuPai_Id = " + filtroMenuPai;
            }
            if (filtroBack != "0"){
                query = query + " and Back = '" + filtroBack + "'";
            }
            if (filtroFront != "0"){
                query = query + " and Front = '" + filtroFront + "'";
            }
            if (filtroLayout != "0"){
                query = query + " and Layout = '" + filtroLayout + "'";
            }
            ViewBag.ListaMenu = await _context.Menu.FromSqlRaw(query).ToListAsync();


            return PartialView("_TabelaIndex");
        }

        

        // GET: 
        public async Task<IActionResult> Create()
        {
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();
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

        /*
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Detalhes = await _context.Menu
                .SingleOrDefaultAsync(m => m.Id == id);
                
            if (ViewBag.Detalhes == null)
            {
                return NotFound();
            }

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            return PartialView("_Details");
        }
*/





        /*Se quiser manter a Session mesmo mudando de página
        if (HttpContext.Session.GetInt32("SessionMenuPai") != null){
            lista = await _context.Menu.Where(m => m.MenuPai_Id == HttpContext.Session.GetInt32("SessionMenuPai")).ToListAsync();
        }*/

        /*Se for necessário usar comandos SQL Bruto:        
        lista = await _context.Menu
            .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + filtroMenuPai)
            .ToListAsync();
        }
        
        Outro exemplo, misturando sql e linq:

        var blogs = context.Blogs
            .FromSqlInterpolated($"SELECT * FROM dbo.SearchBlogs({searchTerm})")
            .Where(b => b.Rating > 3)
            .OrderByDescending(b => b.Rating)
            .ToList();
        */
                    
      

    }
}