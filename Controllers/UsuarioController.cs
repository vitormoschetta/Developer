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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace Developer.Controllers
{
    //Classe para criar o valor 'Hash' da senha do Usuario:
    public class Hash  
    {  
        public static string Create(string value, byte[] salt)  
        {  
            var hashed  = KeyDerivation.Pbkdf2(  
                                password: value,  
                                salt: salt,  
                                prf: KeyDerivationPrf.HMACSHA1,  
                                iterationCount: 10000,  
                                numBytesRequested: 256 / 8);  
    
            return Convert.ToBase64String(hashed);  
        }  
    
        public static bool Validate(string value, byte[] salt, string hash)  
            => Create(value, salt) == hash;  
    } 

    
    //Classe para criar o valor aleatório 'Salt' para concatenar e 'salgar' a Senha do Usuario:
    public class Salt  
    {  
        public static byte[] Create()  
        {  
            byte[] salt = new byte[128 / 8];  
            using (var generator = RandomNumberGenerator.Create())  
            {
                //Retorna o valor aletatorio 'generator' em Bytes:  
                generator.GetBytes(salt);  
                
                /* Converter byte para string:
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                var salt_string = enc.GetString(salt);*/
                return salt;
            }  
        }  
    } 
    

    public class UsuarioController: Controller
    {
        private readonly Contexto _context;
        public UsuarioController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {   
            var usuario = HttpContext.Session.GetString("Usuario"); 
            var usuarioPerfil = HttpContext.Session.GetInt32("UsuarioPerfil");

            if (usuario == null)
                return RedirectToAction("Login", "Usuario");
            
            if (usuarioPerfil != 2)
                return RedirectToAction("Index", "Home");
            

            var listaUsuarios = await _context.Usuario.ToListAsync();                        
            return View(listaUsuarios);
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
            if (ModelState.IsValid){   
                var salt = Salt.Create();//retorna em byte[]
                var hash = Hash.Create(usuario.Senha, salt);  // retorna em string

                if (Hash.Validate(usuario.Senha, salt, hash) == true){
                    usuario.Senha = hash;
                    usuario.Salt = Convert.ToBase64String(salt);//converte salt de byte[]  para string para salvar no banco

                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                
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
                    var salt = Salt.Create();//retorna em byte[]
                    var hash = Hash.Create(usuario.Senha, salt);  // retorna em string

                    if (Hash.Validate(usuario.Senha, salt, hash) == true){
                        usuario.Senha = hash;
                        usuario.Salt = Convert.ToBase64String(salt);//converte salt de byte[]  para string para salvar no banco

                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
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
            Usuario user = SaltUsuarioExists(Login); //verifica se o login(nome, cpf ou email) existe na tabela usuario

            if(user != null){
                var salt_tabela = user.Salt;//pega o valor salt da tabela

                byte[] salt = Convert.FromBase64String(salt_tabela); // Conver o valor salt que está no tipo string em byte[]
                var hash = Hash.Create(Senha, salt); // Gera o Hash para comparar com o hash da tabela do usuario

                // Agora sim verifica se o hash da senha é igual
                var usuario = await _context.Usuario.SingleOrDefaultAsync(u => ((u.Cpf == Login || u.Email == Login)|| u.Nome == Login) && u.Senha == hash); 

                if (usuario != null){
                    HttpContext.Session.SetString("Usuario", usuario.Cpf);
                    HttpContext.Session.SetInt32("UsuarioPerfil", usuario.Perfil_Id);
                    return RedirectToAction("Index", "Home");
                }
                else{
                    ViewBag.Mensagem = "Senha incorreta";
                }
            }
            else{
                ViewBag.Mensagem = "Usuario não encontrado";
            }                                                  
            
            return View();
        }

        public IActionResult Logout()
        {         
            HttpContext.Session.Remove("Usuario");
            HttpContext.Session.Remove("UsuarioPerfil");
            return View("Login","Usuario");
        }

        
        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        private Usuario SaltUsuarioExists(string Login)
        {
            Usuario usuario = _context.Usuario.SingleOrDefault(u => u.Cpf == Login || u.Email == Login || u.Nome == Login);                                        
            return usuario;
        }
    }

}


