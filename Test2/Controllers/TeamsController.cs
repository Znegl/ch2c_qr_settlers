using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Controllers
{
    public class BuyingItem
    {
        public string Name { get; set; }
        public int Metal { get; set; }
        public int Wood { get; set; }
        public int Cloth { get; set; }
        public int Plastic { get; set; }
    }

    public class TeamsController : Controller
    {
        private readonly Ch2CContext _context;
        private string TEAMID = "TeamID";

        private Dictionary<string, BuyingItem> ThingsToBuy = new Dictionary<string, BuyingItem>();

        public TeamsController(Ch2CContext context)
        {
            _context = context;
            var c = (from item in _context.ItemsToBuys
                     select item).Count() == 0;
            if (c)
            {
                var resourceNames = new List<string> { "Metal", "Wood", "Plastic", "Cloth" };
                for(var i = 0; i < 60; i++)
                {
                    foreach (var item in resourceNames)
                    {
                        _context.ItemsToBuys.Add(new ItemsToBuy { Guid = Guid.NewGuid().ToString(), Name = item });
                    }
                }
                _context.SaveChanges();
            }
            ThingsToBuy.Add("Lille Hammer", new BuyingItem
            {
                Name = "Lille Hammer",
                Metal = 3,
                Wood = 2
            });
            ThingsToBuy.Add("Lille Sav", new BuyingItem
            {
                Name = "Lille Sav",
                Metal = 2,
                Plastic = 2
            });
            ThingsToBuy.Add("Saks", new BuyingItem
            {
                Name = "Saks",
                Metal = 3,
                Plastic = 3
            });
            ThingsToBuy.Add("Træplade A4", new BuyingItem
            {
                Name = "Træplade A4",
                Wood = 4
            });
            ThingsToBuy.Add("Søm", new BuyingItem
            {
                Name = "Søm",
                Metal = 1
            });
            ThingsToBuy.Add("Grillspyd", new BuyingItem
            {
                Wood = 1,
                Name = "Grillspyd"
            });
            ThingsToBuy.Add("Tape", new BuyingItem
            {
                Plastic = 10,
                Name = "Tape"
            });
            ThingsToBuy.Add("Piberender", new BuyingItem
            {
                Name = "Piberenser",
                Cloth = 2,
                Metal = 1
            });
            ThingsToBuy.Add("Snor", new BuyingItem
            {
                Name = "Snor",
                Cloth = 3
            });
            ThingsToBuy.Add("Papir A4", new BuyingItem
            {
                Name = "Papir A4",
                Cloth = 1
            });
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        public async Task<IActionResult> BuyManyItems(int number, string itemName)
        {
            var (team, done) = await TestTeam();
            if (team == null)
                return NotFound();

            if (done)
                return RedirectToAction("TimesUp");

            if (!ThingsToBuy.ContainsKey(itemName))
                return NotFound();

            ViewData["ItemName"] = itemName;

            var itemToBuy = ThingsToBuy[itemName];
            var canBuy = false;
            if ((itemToBuy.Metal*number) <= team.Metal && (itemToBuy.Wood*number) <= team.Wood && (itemToBuy.Plastic*number) <= team.Plastic && (itemToBuy.Cloth*number) <= team.Cloth)
                canBuy = true;

            if (canBuy)
            {
                team.Metal -= (itemToBuy.Metal*number);
                team.Wood -= (itemToBuy.Wood*number);
                team.Cloth -= (itemToBuy.Cloth*number);
                team.Plastic -= (itemToBuy.Plastic*number);
                team.NumberOfActions += 1;
                _context.Teams.Update(team);
                _context.TeamHasBoughts.Add(new TeamHasBought { TeamID = team.TeamID, Team = team, Bought = itemName });
            }
            var logLine = new LogLine { LogTime = DateTime.Now, Message = $"{team.TeamName} tried to buy {number} of {itemName} with success: {canBuy}" };
            _context.Logs.Add(logLine);
            await _context.SaveChangesAsync();

            ViewData["Bought"] = canBuy;
            ViewData["amount"] = number;
            return View("BuyItem", team);
        }

        public IActionResult BuyItem(string itemName)
        {
            //TODO Rewirte this as a call to the other one with 1 as parameter

            return RedirectToAction("BuyManyItems", new { number=1, itemName = itemName});

            // var (team, done) = await TestTeam();
            // if (team == null)
            //     return NotFound();

            // if (done)
            //     return RedirectToAction("TimesUp");

            // if (!ThingsToBuy.ContainsKey(itemName))
            //     return NotFound();

            // ViewData["ItemName"] = itemName;

            // var itemToBuy = ThingsToBuy[itemName];
            // var canBuy = false;
            // if (itemToBuy.Metal <= team.Metal && itemToBuy.Wood <= team.Wood && itemToBuy.Plastic <= team.Plastic && itemToBuy.Cloth <= team.Cloth)
            //     canBuy = true;

            // if (canBuy)
            // {
            //     team.Metal -= itemToBuy.Metal;
            //     team.Wood -= itemToBuy.Wood;
            //     team.Cloth -= itemToBuy.Cloth;
            //     team.Plastic -= itemToBuy.Plastic;
            //     team.NumberOfActions += 1;
            //     _context.Teams.Update(team);
            // }
            // var logLine = new LogLine { LogTime = DateTime.Now, Message = $"{team.TeamName} tried to buy {itemName} with success: {canBuy}" };
            // _context.Logs.Add(logLine);
            // await _context.SaveChangesAsync();

            // ViewData["Bought"] = canBuy;
            // return View(team);
        }

        public async Task<IActionResult> Status()
        {
            var (team, done) = await TestTeam();
            if (team == null)
                return NotFound();

            if (done)
                return RedirectToAction("TimesUp");

            var logLine = new LogLine { LogTime = DateTime.Now, Message = $"{team.TeamName} asked for status" };
            _context.Logs.Add(logLine);
            await _context.SaveChangesAsync();

            return View(team);
        }

        public IActionResult TimesUp() => View();
        public IActionResult Cheater() => View();

        public async Task<IActionResult> AddResource(int resourceType, string resourceUUID)
        {
            var (team, done) = await TestTeam();
            if (team == null)
                return NotFound();

            if (done)
                return RedirectToAction("TimesUp");

            var validResource = (from res in _context.ItemsToBuys
                                 where res.Guid == resourceUUID
                                 select res).Count() == 1;

            if (!validResource)
                return RedirectToAction("Cheater");

            var rrs = (from readr in _context.ResourceReads
                       where readr.TeamID == team.TeamID && readr.ResourceUUID == resourceUUID
                       select readr).Count();

            if (rrs > 0)
                return RedirectToAction("Cheater");
            var type = "";
            switch (resourceType)
            {
                case 0:
                    team.Metal += 1;
                    type = "Metal";
                    break;
                case 1:
                    team.Wood += 1;
                    type = "Wood";
                    break;
                case 2:
                    team.Cloth += 1;
                    type = "Cloth";
                    break;
                case 3:
                    team.Plastic += 1;
                    type = "Plastic";
                    break;
                default:
                    break;
            }
            team.NumberOfActions += 1;
            var rr = new ResourceRead { ResourceUUID = resourceUUID, TeamID = team.TeamID };
            _context.ResourceReads.Add(rr);
            _context.Teams.Update(team);
            var logLine = new LogLine { LogTime = DateTime.Now, Message = $"{team.TeamName} add a resource of type {type}" };
            _context.Logs.Add(logLine);
            await _context.SaveChangesAsync();

            return View(team);
        }

        public async Task<IActionResult> StartTeam(string teamName)
        {
            if (teamName == null)
                return NotFound();

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamName == teamName);
            if (team == null)
            {
                var newTeam = new Team
                {
                    TeamName = teamName,
                    StarTtime = DateTime.Now
                };
                _context.Teams.Add(newTeam);
                await _context.SaveChangesAsync();

                team = newTeam;

                var logLine = new LogLine { LogTime = DateTime.Now, Message = $"New phone added to {teamName} starting NOW" };
                _context.Logs.Add(logLine);
                await _context.SaveChangesAsync();
            }
            else
            {
                var logLine = new LogLine { LogTime = DateTime.Now, Message = $"New phone added to {teamName}. Time is already running" };
                _context.Logs.Add(logLine);
                await _context.SaveChangesAsync();
            }

            HttpContext.Session.SetInt32(TEAMID, team.TeamID);

            return View(team);
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamID == id);
        }

        private async Task<(Team team, bool teamIsDone)> TestTeam()
        {
            var teamId = HttpContext.Session.GetInt32(TEAMID);
            if (teamId == null)
                return (null, true);
            var team = await _context.Teams.FirstOrDefaultAsync(m => m.TeamID == teamId);
            if (team == null)
                return (null, true);

            if (team.StarTtime.Add(TimeSpan.FromMinutes(15)) < DateTime.Now)
                return (team, true);

            return (team, false);
        }
    }
}
