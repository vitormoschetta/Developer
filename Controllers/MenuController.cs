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


        public async Task<IActionResult> Index(int id)
        {
            //Aqui estabelecemos a session do projeto selecionado na pagina inicial => Home/Index
            //Só muda a session se for cliado em um dos projetos na pagina inicial
            if (id != 0)
                HttpContext.Session.SetInt32("Projeto", id);            
            
            //ViewBag apenas para mostrar o projeto em uso na tela Menu/Index:
            ViewBag.projeto = HttpContext.Session.GetInt32("Projeto");

            var projeto = HttpContext.Session.GetInt32("Projeto");
            
            if (projeto != null){
                // Lista de Itens Menu com retorno padrão de acordo com o projeto em sessão
                ViewBag.ListaMenu = await _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id != 0 and Projeto_Id =" + projeto).ToListAsync();                    

                // Lista de menus Pai add a ViewBag.Lista para recuperar na View
                ViewBag.ListaMenuPai = await _context.Menu
                    .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = 0 and Projeto_Id =" + projeto).ToListAsync();

                    //Obs: Apenas Menus Pai tem MenuPai_Id igual a zero
            }
            else{
                //Se tentar navegar para Menu/Index sem selecionar um projeto, é redirecionado para Home/Index
                return RedirectToAction("Index", "Home");
            }
            
            return View(); 
        }


        //Cada onchange nos selects de filtro aciona a função js 'Atualiza()' -> site.js, que redireciona para esta action:
        [HttpPost]
        public async Task<IActionResult> ListaItensMenu(int filtroMenuPai, string filtroBack, string filtroFront, string filtroLayout)
        {
            //recuperando a session para saber em que projeto estamos trabalhando
            var projeto = HttpContext.Session.GetInt32("Projeto");

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu
                .FromSqlRaw("SELECT * FROM Menu where MenuPai_Id = 0 and Projeto_Id =" + projeto).ToListAsync();

            // Filtrando consulta pelos selects recebidos como parametro:
            string query = "SELECT * FROM Menu where MenuPai_Id != 0 and Projeto_Id = " + projeto;
            if (filtroMenuPai != 0)
                query = query + " and MenuPai_Id = " + filtroMenuPai;            
            if (filtroBack != "0")
                query = query + " and Back = '" + filtroBack + "'";            
            if (filtroFront != "0")
                query = query + " and Front = '" + filtroFront + "'";            
            if (filtroLayout != "0")
                query = query + " and Layout = '" + filtroLayout + "'";            

            ViewBag.ListaMenu = await _context.Menu.FromSqlRaw(query).ToListAsync();

            //Vai preencher os dados em uma partial que é chamada no formulario Menu/Index pelo arquivo site.js => javascript
            return PartialView("_TabelaIndex");
        }

        

        // GET: 
        public async Task<IActionResult> Create()
        {
            ViewBag.projeto = HttpContext.Session.GetInt32("Projeto");

            //ViewBag para gerar os valores no select MenuPai. Menu pai e igual a zero, e tem q ser menu pai do Projeto selecionado na pagina inicial, que está em session:
            ViewBag.ListaMenuPai = await _context.Menu
                .FromSqlRaw("select * from menu where MenuPai_Id = 0 and projeto_id = " + HttpContext.Session.GetInt32("Projeto"))
                .ToListAsync();
                
            return View();
        }


         // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Formulario,Funcao,Link,Back,Front,Layout,ObsApp,OriginTable,DestinTable,ObsTable,MenuPai_Id,Projeto_Id")] Menu menu)
        {
            if (ModelState.IsValid){
                _context.Add(menu);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(menu);
        }

         // GET: 
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.projeto = HttpContext.Session.GetInt32("Projeto");

            if (id == null)
                return NotFound();        

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)            
                return NotFound();

            return View(menu);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Formulario,Funcao,Link,Back,Front,Layout,ObsApp,OriginTable,DestinTable,ObsTable,MenuPai_Id, Projeto_Id")] Menu menu)
        {
            if (id != menu.Id)
                return NotFound();
            

            if (ModelState.IsValid){
                try{
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!MenuExists(menu.Id))                
                        return NotFound();                    
                    else                    
                        throw;                    
                }
                return RedirectToAction("Index");
            }

            return View(menu);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMenu(int? id)
        {
            if (id == null)            
                return NotFound();            

            ViewBag.Menu = await _context.Menu
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ViewBag.Menu == null)            
                return NotFound();            

            return PartialView("_Delete");
            //return View(menu);
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

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            ViewBag.Menu = await _context.Menu
                .SingleOrDefaultAsync(m => m.Id == id);
                
                
            if (ViewBag.Menu == null)            
                return NotFound();            

            // Lista de menus Pai add a ViewBag.Lista para recuperar na View
            ViewBag.ListaMenuPai = await _context.Menu.Where(m => m.MenuPai_Id == 0).ToListAsync();

            return PartialView("_Details");
            //return View();
        }






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