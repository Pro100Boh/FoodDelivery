using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryServer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngradientsController : ControllerBase
    {
        private readonly FoodDeliveryContext db;

        private readonly IHostingEnvironment hostingEnv;

        private const string imageMimeType = "image/ief"; 

        public IngradientsController(IHostingEnvironment env, FoodDeliveryContext dbContext)
        {
            hostingEnv = env;
            db = dbContext;
        }

        [HttpGet("{ingradientId:guid}/image")]
        public async Task<IActionResult> GetIngradientImage(Guid ingradientId)
        {
            if (!CachedData.Images.ContainsKey(ingradientId))
            {
                string imageFileName = await db.Ingradients
                                                .Where(i => i.Id == ingradientId)
                                                .Select(i => i.Image)
                                                .FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Ingradient not found");

                string path = $"{hostingEnv.ContentRootPath}/Images/Ingradients/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.Images[ingradientId] = System.IO.File.ReadAllBytes(path);
            }

            return File(CachedData.Images[ingradientId], imageMimeType);
        }
    }
}