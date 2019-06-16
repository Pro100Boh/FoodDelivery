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
        private readonly IMapper _mapper;

        private readonly FoodDeliveryContext _dbContext;

        private readonly IHostingEnvironment _hostingEnv;

        private const string imageMimeType = "image/ief";

        public PizzaController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
        {
            _hostingEnv = env;
            this._mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (CachedData.Pizzas == null)
            {
                var query = _dbContext.Pizzas
                                .Include(g => g.PizzaIngradients)
                                .ThenInclude(gg => gg.Ingradient);

                var pizzas = await query.ToListAsync();

                CachedData.Pizzas = _mapper.Map<List<PizzaViewModel>>(pizzas);
            }

            return Ok(CachedData.Pizzas);
        }

        [HttpGet("{pizzaId:guid}/image")]
        public async Task<IActionResult> GetPizzaImage(Guid pizzaId)
        {
            if (!CachedData.Images.ContainsKey(pizzaId))
            {
                string imageFileName = await _dbContext.Pizzas
                                                .Where(p => p.Id == pizzaId)
                                                .Select(p => p.Image)
                                                .FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Pizza not found");

                string path = $"{_hostingEnv.ContentRootPath}/Images/Pizza/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.Images[pizzaId] = System.IO.File.ReadAllBytes(path);
            }

            return File(CachedData.Images[pizzaId], imageMimeType);
        }

    }
}
