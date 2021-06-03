using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Developer.Models;
using Developer.Data;

namespace Developer.Controllers
{
    [ApiController]
    [Route("api/[action]")] //url => https://localhost:5001/api/menu
    public class WebAPIController: ControllerBase
    {
        private readonly AppDbContext _context;
        public WebAPIController(AppDbContext context)
        {
            _context = context;
        }

        //url => https://localhost:5001/api/menu        
        public async Task<ActionResult<IEnumerable<Menu>>> Menu()
        {                        
            return await _context.Menu.ToListAsync();              
        }

        //url => https://localhost:5001/api/menu/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> Menu(int id)
        {                        
            return await _context.Menu
                .FromSqlRaw("Select * From Menu where id = " + id)
                .ToListAsync();     
            //SingleOrDefaultAsync(m => m.id == id);                    
        }

    }
}