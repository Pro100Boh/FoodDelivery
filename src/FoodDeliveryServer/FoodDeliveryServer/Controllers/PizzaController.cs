using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDeliveryServer.Infrastructure;
using FoodDeliveryServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly FoodDeliveryContext db;

        private readonly IHostingEnvironment hostingEnv;

        private const string imageMimeType = "image/ief";

        public PizzaController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
        {
            hostingEnv = env;
            this.mapper = mapper;
            db = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (CachedData.Pizzas == null)
            {
                var query = db.Pizzas
                                .Include(g => g.PizzaIngradients)
                                .ThenInclude(gg => gg.Ingradient);

                var pizzas = await query.ToListAsync();

                CachedData.Pizzas = mapper.Map<List<PizzaViewModel>>(pizzas);
            }

            return Ok(CachedData.Pizzas);
        }

        [HttpGet("{pizzaId:guid}/image")]
        public async Task<IActionResult> GetPizzaImage(Guid pizzaId)
        {
            if (!CachedData.Images.ContainsKey(pizzaId))
            {
                string imageFileName = await db.Pizzas
                                                .Where(p => p.Id == pizzaId)
                                                .Select(p => p.Image)
                                                .FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Pizza not found");

                string path = $"{hostingEnv.ContentRootPath}/Images/Pizza/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.Images[pizzaId] = System.IO.File.ReadAllBytes(path);
            }

            return File(CachedData.Images[pizzaId], imageMimeType);
        }

    }
}
