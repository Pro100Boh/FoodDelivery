using System;
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
        private readonly FoodDeliveryContext _dbContext;

        private readonly IHostingEnvironment _hostingEnv;

        private const string imageMimeType = "image/ief"; 

        public IngradientsController(IHostingEnvironment env, FoodDeliveryContext dbContext)
        {
            _hostingEnv = env;
            _dbContext = dbContext;
        }

        [HttpGet("{ingradientId:guid}/image")]
        public async Task<IActionResult> GetIngradientImage(Guid ingradientId)
        {
            if (!CachedData.Images.ContainsKey(ingradientId))
            {
                string imageFileName = await _dbContext.Ingradients
                                                .Where(i => i.Id == ingradientId)
                                                .Select(i => i.Image)
                                                .FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(imageFileName))
                    return NotFound("Ingradient not found");

                string path = $"{_hostingEnv.ContentRootPath}/Images/Ingradients/{imageFileName}";

                if (!System.IO.File.Exists(path))
                    return NotFound("Image not found");

                CachedData.Images[ingradientId] = System.IO.File.ReadAllBytes(path);
            }

            return File(CachedData.Images[ingradientId], imageMimeType);
        }
    }
}