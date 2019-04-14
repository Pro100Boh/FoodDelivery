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
        public async Task<IActionResult> Get(string sort = null,
                                            [Range(1, int.MaxValue)]int page = 1,
                                            [Range(2, 10)]int pageSize = 4)
        {
            IQueryable<Pizza> query = db.Pizzas.
                            Include(g => g.PizzaIngradients).
                            ThenInclude(gg => gg.Ingradient);

            // sorting
            switch (sort)
            {
                case null:
                    break;
                case "name":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case "name-desc":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "price-desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    return BadRequest(sort);
            }

            // pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var pizzas = await query.ToListAsync();

            var pizzasView = mapper.Map<IEnumerable<PizzaViewModel>>(pizzas);

            return Ok(pizzasView);
        }

        [HttpGet("{pizzaId:guid}/image")]
        public async Task<IActionResult> GetPizzaImage(Guid pizzaId)
        {
            var pizza = await db.Pizzas.FindAsync(pizzaId);

            if (pizza == null)
                return NotFound("Pizza not found");

            string imageFileName = pizza.Image;

            string path = $"{hostingEnv.ContentRootPath}/Images/Pizza/{imageFileName}";

            if (!System.IO.File.Exists(path))
                return NotFound("Image not found");

            string type = "	image/ief"; // mime type for image file

            return await Task.Run(() => PhysicalFile(path, type, imageFileName));
        }

    }
}
