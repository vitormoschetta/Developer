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
            // Limpa session dos filtros 
            HttpContext.Session.Clear();

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
            
            // Instancia a lista com retorno padrão
            var lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0")
                .ToList();

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.Lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = 0")
                .ToList();


            //Seta a session MenuPai se ainda não existir
            if (HttpContext.Session.GetInt32("SessionMenuPai") == null){
                if (filtroMenuPai != null){
                    HttpContext.Session.SetInt32("SessionMenuPai", filtroMenuPai);
                }
            }

            //Seta a session Back se ainda não existir
            if (HttpContext.Session.GetString("SessionBack") == null){
                if (filtroBack != null){
                    HttpContext.Session.SetString("SessionBack", filtroBack);                
                }
            }

            //Retorna a lista pela session MenuPai se houver
            if (HttpContext.Session.GetInt32("SessionMenuPai") != null){
                lista = _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + HttpContext.Session.GetInt32("SessionMenuPai"))
                    .ToList();
            }

            /*

            //Retorna a lista pela session Back se houver
            if (HttpContext.Session.GetString("SessionBack") != null && HttpContext.Session.GetInt32("SessionMenuPai") == null){
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0 and back = '" + HttpContext.Session.GetString("SessionBack") + "'")
                .ToList();
            }

            //Retorna a lista pelas sessions já criadas
            if (HttpContext.Session.GetInt32("SessionMenuPai") != null && HttpContext.Session.GetString("SessionBack") != null){
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + HttpContext.Session.GetInt32("SessionMenuPai") + " and Back = '" + HttpContext.Session.GetString("SessionBack") + "'")
                .ToList();
            }


            /*Verifica se a session MenuPai ainda não existe e cria
            if (menuPai == null){
                if (filtroMenuPai != null){
                    HttpContext.Session.SetInt32("SessionMenuPai", filtroMenuPai);
                    menuPai = HttpContext.Session.GetInt32("SessionMenuPai");
                }
            }

            Verifica se a session Back ainda não existe e cria
            if (back == null){
                if (filtroBack != null){
                    HttpContext.Session.SetString("SessionBack", filtroBack);
                    back = HttpContext.Session.GetString("SessionBack");
                }
            }

            //Retorna a lista pelas sessions já criadas
            if (menuPai != null && back != null){
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + menuPai + " and Back = '" + back + "'")
                .ToList();
            }

            //Retorna a lista pela session MenuPai se houver
            if (menuPai != null && back == null){
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = " + menuPai)
                .ToList();
            }
            
             //Retorna a lista pela session Back se houver
            if (back != null && menuPai == null){
                lista = _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0 and back = '" + back + "'")
                .ToList();
            }*/
            
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