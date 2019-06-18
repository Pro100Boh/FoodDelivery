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
    public class DessertsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly FoodDeliveryContext _dbContext;

        private readonly IHostingEnvironment _hostingEnv;

        private const string imageMimeType = "image/ief";

        public DessertsController(IHostingEnvironment env, IMapper mapper, FoodDeliveryContext dbContext)
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
                var drinks = _dbContext.Desserts.ToList();

                CachedData.Desserts = _mapper.Map<List<DessertViewModel>>(drinks);
            }

            return Ok(CachedData.Desserts);
        }

        [HttpGet("{dessertId:guid}/image")]
        public IActionResult GetDessertImage(Guid dessertId)
        {
            if (!CachedData.Images.ContainsKey(dessertId))
            {
                string imageFileName = _dbContext.Desserts
                                                .Where(d => d.Id == dessertId)
                                                .Select(d => d.Image)
                                                .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Dessert not found");

                string path = $"{_hostingEnv.ContentRootPath}/Images/Desserts/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.SetImageCache(dessertId, System.IO.File.ReadAllBytes(path));
            }

            return File(CachedData.Images[dessertId], imageMimeType);
        }
    }
}