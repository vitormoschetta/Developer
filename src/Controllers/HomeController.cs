using Developer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Developer.Data;

namespace Developer.Controllers
{

    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
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


        [HttpPost]
         public IActionResult Create()
        {           
            return PartialView("_Create");
        }


        [HttpPost]
        public async Task<IActionResult> CreateProjeto([Bind("Id,Sigla,Nome,Descricao,Linguagem,Framework")] Projeto projeto)
        {            
            if (ModelState.IsValid){
                _context.Add(projeto);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return PartialView("_Create");
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            ViewBag.Projeto = await _context.Projeto
                .SingleOrDefaultAsync(m => m.Id == id);
                
                
            if (ViewBag.Projeto == null)            
                return NotFound();            


            return PartialView("_Details");
            //return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int? id)
        {            
            if (id == null)
                return NotFound();        

            ViewBag.Projeto = await _context.Projeto.SingleOrDefaultAsync(m => m.Id == id);
            if (ViewBag.Projeto == null)            
                return NotFound();                   

            return PartialView("_Edit");
        }


         // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjeto(int id, [Bind("Id,Sigla,Nome,Descricao,Linguagem,Framework")] Projeto projeto)
        {
            if (id != projeto.Id)
                return NotFound();
            

            if (ModelState.IsValid){
                try{
                    _context.Update(projeto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!ProjetoExists(projeto.Id))                
                        return NotFound();                    
                    else                    
                        throw;                    
                }
                return RedirectToAction("Index");
            }

            return PartialView("_Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            ViewBag.Projeto = await _context.Projeto.SingleOrDefaultAsync(m => m.Id == id);
            if (ViewBag.Projeto == null)            
                return NotFound();            

            return PartialView("_Delete");
        }


        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            var projeto = await _context.Projeto.SingleOrDefaultAsync(m => m.Id == id);
            _context.Projeto.Remove(projeto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        private bool ProjetoExists(int id)
        {
            return _context.Projeto.Any(e => e.Id == id);
        }



        
    }
}
