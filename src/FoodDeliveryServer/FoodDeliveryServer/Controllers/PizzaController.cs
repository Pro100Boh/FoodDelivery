using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDeliveryServer.Entities;
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

        public PizzaController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
        {
            hostingEnv = env;
            this.mapper = mapper;
            db = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = db.Pizzas
                            .Include(g => g.PizzaIngradients)
                            .ThenInclude(gg => gg.Ingradient);

            var pizzas = await query.ToListAsync();

            var pizzasView = mapper.Map<IEnumerable<PizzaViewModel>>(pizzas);

            return Ok(pizzasView);
        }

        [HttpGet("{pizzaId:guid}/image")]
        public async Task<IActionResult> GetPizzaImage(Guid pizzaId)
        {
            // If an entity is being tracked by the context, then it is returned 
            // immediately without making a request to the database
            var pizza = await db.Pizzas.FindAsync(pizzaId);

            if (pizza == null)
                return NotFound("Pizza not found");

            string imageFileName = pizza.Image;

            string path = $"{hostingEnv.ContentRootPath}/Images/Pizza/{imageFileName}";

            if (!System.IO.File.Exists(path))
                return NotFound("Image not found");

            string type = "image/ief"; // mime type for image file

            return await Task.Run(() => PhysicalFile(path, type, imageFileName));
        }

    }
}
