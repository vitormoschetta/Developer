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
            HttpContext.Session.Clear();
            // ListaMenu com retorno padrão
            ViewBag.listaMenu = await _context.Menu.Where(m => m.MenuPai_Id != 0).ToListAsync();

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> ListaTodos(int filtroMenuPai, string filtroBack)
        {
            // ListaMenu com retorno padrão
            ViewBag.listaMenu = await _context.Menu.Where(m => m.MenuPai_Id != 0).ToListAsync();

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            ViewBag.MenuPai = "Vazio";
            ViewBag.Back = "Vazio";
            ViewBag.Dois = "Vazio";
      
            // Renova a session MenuPai ao mudar o select
            if (filtroMenuPai != 0 && filtroBack == "0"){
                ViewBag.listaMenu = await _context.Menu.Where(m => m.MenuPai_Id == filtroMenuPai).ToListAsync();
                //ViewBag.MenuPai = "Adiciona session MenuPai / " + filtroMenuPai;
            }

            //Renova a session Back ao mudar o select
            if (filtroBack != "0" && filtroMenuPai == 0){
                ViewBag.listaMenu = await _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where Back = '" + filtroBack + "' and MenuPai_Id != 0")
                    .ToListAsync();
                //ViewBag.Back = "Adiciona session Back / " + filtroBack;
            }

            if (filtroMenuPai != 0 && filtroBack != "0"){
                ViewBag.listaMenu = await _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where Back = '" + filtroBack + "' and MenuPai_Id = " + filtroMenuPai)
                    .ToListAsync();
                //ViewBag.Dois = "Entrou nos dois";
            }

            return PartialView("_partialTabela");
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Index(int filtroMenuPai, string filtroBack)
        {
            // ListaMenu com retorno padrão
            ViewBag.listaMenu = await _context.Menu.Where(m => m.MenuPai_Id != 0).ToListAsync();

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            ViewBag.MenuPai = "Vazio";
            ViewBag.Back = "Vazio";
            ViewBag.Dois = "Vazio";
      
            // Renova a session MenuPai ao mudar o select
            if (filtroMenuPai != 0){
                HttpContext.Session.SetInt32("SessionMenuPai", filtroMenuPai);
                //ViewBag.MenuPai = "Adiciona session MenuPai / " + filtroMenuPai;
            }

            //Renova a session Back ao mudar o select
            if (filtroBack != "0"){
                HttpContext.Session.SetString("SessionBack", filtroBack);   
                //ViewBag.Back = "Adiciona session Back / " + filtroBack;
            }



           
            
            //Retorna a lista pela session MenuPai se houver
            if (HttpContext.Session.GetInt32("SessionMenuPai") != 0 && HttpContext.Session.GetString("SessionBack") == "0"){
                ViewBag.listaMenu = await _context.Menu.Where(m => m.MenuPai_Id == HttpContext.Session.GetInt32("SessionMenuPai")).ToListAsync();
                
                ViewBag.MenuPai = "Entrou no SessionMenuPai / " + HttpContext.Session.GetInt32("SessionMenuPai");
            }

            //Retorna a lista pela session Back se houver
            if (HttpContext.Session.GetString("SessionBack") != "0" && HttpContext.Session.GetInt32("SessionMenuPai") == 0){
                ViewBag.listaMenu = await _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where Back = '" + HttpContext.Session.GetString("SessionBack") + "' and MenuPai_Id != 0")
                    .ToListAsync();
                ViewBag.Back = "Entrou no SessionBack / " + HttpContext.Session.GetString("SessionBack");
            }

            if (HttpContext.Session.GetInt32("SessionMenuPai") != 0 && HttpContext.Session.GetString("SessionBack") != "0")
            {
                ViewBag.listaMenu = await _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where Back = '" + HttpContext.Session.GetString("SessionBack") + "' and MenuPai_Id = " + HttpContext.Session.GetInt32("SessionMenuPai"))
                    .ToListAsync();
                
                ViewBag.Dois = "Entrou no Session dos dois / " + HttpContext.Session.GetInt32("SessionMenuPai") + " / " +  HttpContext.Session.GetString("SessionBack");
            }
           
            

            return View();        
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






        /*Se quiser manter a Session mesmo mudando de página
        if (HttpContext.Session.GetInt32("SessionMenuPai") != null){
            lista = await _context.Menu.Where(m => m.MenuPai_Id == HttpContext.Session.GetInt32("SessionMenuPai")).ToListAsync();
        }*/

        /*Se for necessário usar comandos SQL Bruto:        
        lista = await _context.Menu
            .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + HttpContext.Session.GetInt32("SessionMenuPai"))
            .ToListAsync();
        }*/
                    
      

    }
}