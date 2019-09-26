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

        public IActionResult ShowTeams()
        {
            var teams = _context.Teams;
            return View(teams);
        }

        public IActionResult ShowTeamBuys(int teamId)
        {
            var buys = _context.TeamHasBoughts.Where(t => t.TeamID == teamId);
            return View(buys);
        }

        public IActionResult ShowResourceLinks()
        {
            var resources = _context.ItemsToBuys;
            return View(resources);
        }

        private bool LogLineExists(int id)
        {
            return _context.Logs.Any(e => e.LogLineID == id);
        }
    }
}
