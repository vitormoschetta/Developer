using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Developer.Models;

namespace Developer.Controllers
{
    [ApiController]
    [Route("api/[action]")] //url => https://localhost:5001/api/menu
    public class WebAPIController: ControllerBase
    {
        private readonly Contexto _context;
        public WebAPIController(Contexto context)
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