using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpsServer.Models;

namespace RpsServer.Controllers
{
    [Route("api/debug/[controller]")]
    public class StatusController : Controller
    {
        private readonly GameContext context;

        public StatusController(GameContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Game> GetAll()
        {
            return this.context.Games;
        }
    }
}
