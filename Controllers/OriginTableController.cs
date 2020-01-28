using Developer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Developer.Controllers
{
    public class OriginTableController: Controller
    {
        private readonly Contexto _context;

        public OriginTableController(Contexto context)
        {
            _context = context;
            
        }
    }
}