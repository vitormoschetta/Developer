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
    public class UsuarioController: Controller
    {
        private readonly Contexto _context;
        public UsuarioController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {         
            return View();
        }

         public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Cpf,Senha,Ativo,Perfil_Id")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }


        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Cpf,Senha,Ativo,Perfil_Id")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string Login, string Senha)
        {
            var usuario = await _context.Usuario.SingleOrDefaultAsync(u => (u.Cpf == Login || u.Email == Login) && u.Senha == Senha); 
                                            //.SingleOrDefault pois pode retornar nulo 
            if (usuario != null){
                HttpContext.Session.SetString("Usuario", usuario.Cpf);     
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensagem = "Usuario e/ou Senha incorretos.";
            return View();
        }

        public IActionResult Logout()
        {         
            HttpContext.Session.Remove("Usuario");
            return View("Login","Usuario");
        }

        
        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}


