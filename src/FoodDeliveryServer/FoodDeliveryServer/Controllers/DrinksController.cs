using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDeliveryServer.Infrastructure;
using FoodDeliveryServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly FoodDeliveryContext db;

        private readonly IHostingEnvironment hostingEnv;

        private const string imageMimeType = "image/ief";

        public DrinksController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
        {
            hostingEnv = env;
            this.mapper = mapper;
            db = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (CachedData.Drinks == null)
            {
                var drinks = await db.Drinks.ToListAsync();

                CachedData.Drinks = mapper.Map<List<DrinkViewModel>>(drinks);
            }

            return Ok(CachedData.Drinks);
        }

        [HttpGet("{drinkId:guid}/image")]
        public async Task<IActionResult> GetIngradientImage(Guid drinkId)
        {
            if (!CachedData.Images.ContainsKey(drinkId))
            {
                string imageFileName = await db.Drinks
                                                .Where(d => d.Id == drinkId)
                                                .Select(d => d.Image)
                                                .FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Drink not found");

                string path = $"{hostingEnv.ContentRootPath}/Images/Drinks/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.Images[drinkId] = System.IO.File.ReadAllBytes(path);
            }

            return File(CachedData.Images[drinkId], imageMimeType);
        }
    }
}