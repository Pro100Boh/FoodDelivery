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
    public class DrinksController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly FoodDeliveryContext _dbContext;

        private readonly IHostingEnvironment _hostingEnv;

        private const string imageMimeType = "image/ief";

        public DrinksController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
        {
            _hostingEnv = env;
            this._mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (CachedData.Drinks == null)
            {
                var drinks = _dbContext.Drinks.ToList();

                CachedData.Drinks = _mapper.Map<List<DrinkViewModel>>(drinks);
            }

            return Ok(CachedData.Drinks);
        }

        [HttpGet("{drinkId:guid}/image")]
        public IActionResult GetIngradientImage(Guid drinkId)
        {
            if (!CachedData.Images.ContainsKey(drinkId))
            {
                string imageFileName = _dbContext.Drinks
                                                .Where(d => d.Id == drinkId)
                                                .Select(d => d.Image)
                                                .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Drink not found");

                string path = $"{_hostingEnv.ContentRootPath}/Images/Drinks/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.SetImageCache(drinkId, System.IO.File.ReadAllBytes(path));
            }

            return File(CachedData.Images[drinkId], imageMimeType);
        }
    }
}