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
    public class HomeController : Controller
    {
        private readonly Contexto _context;

        public HomeController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            if (usuario == null)
                return RedirectToAction("Login", "Usuario");

            // ListaMenu de projetos no banco
            ViewBag.ListaProjetos = await _context.Projeto.ToListAsync();
                        
            return View();
        }

         public IActionResult Create()
        {           
            return View();
        }


         // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sigla,Nome")] Projeto projeto)
        {
            if (ModelState.IsValid){
                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        
    }
}
