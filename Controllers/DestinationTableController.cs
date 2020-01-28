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
    public class DestinationTableController: Controller
    {
        private readonly Contexto _context;

        public DestinationTableController(Contexto context)
        {
            _context = context;
        }
    }
}