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
    public class DessertsController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly FoodDeliveryContext db;

        private readonly IHostingEnvironment hostingEnv;

        private const string imageMimeType = "image/ief";

        public DessertsController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
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
                var drinks = await db.Desserts.ToListAsync();

                CachedData.Desserts = mapper.Map<List<DessertViewModel>>(drinks);
            }

            return Ok(CachedData.Desserts);
        }

        [HttpGet("{dessertId:guid}/image")]
        public async Task<IActionResult> GetDessertImage(Guid dessertId)
        {
            if (!CachedData.Images.ContainsKey(dessertId))
            {
                string imageFileName = await db.Desserts
                                                .Where(d => d.Id == dessertId)
                                                .Select(d => d.Image)
                                                .FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Dessert not found");

                string path = $"{hostingEnv.ContentRootPath}/Images/Desserts/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.Images[dessertId] = System.IO.File.ReadAllBytes(path);
            }

            return File(CachedData.Images[dessertId], imageMimeType);
        }
    }
}