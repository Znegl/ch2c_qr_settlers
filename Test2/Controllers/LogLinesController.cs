using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Controllers
{
    public class LogLinesController : Controller
    {
        private readonly Ch2CContext _context;

        public LogLinesController(Ch2CContext context)
        {
            _context = context;
        }

        // GET: LogLines
        public async Task<IActionResult> Index()
        {
            var lines = _context.Logs.OrderByDescending(x => x.LogTime).ToListAsync();
            return View(await lines);
        }

        private bool LogLineExists(int id)
        {
            return _context.Logs.Any(e => e.LogLineID == id);
        }
    }
}
