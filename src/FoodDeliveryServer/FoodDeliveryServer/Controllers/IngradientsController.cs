using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDeliveryServer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngradientsController : ControllerBase
    {
        private readonly FoodDeliveryContext db;

        private readonly IHostingEnvironment hostingEnv;

        public IngradientsController(IHostingEnvironment env, FoodDeliveryContext dbContext)
        {
            hostingEnv = env;
            db = dbContext;
        }

        [HttpGet("{ingradientId:guid}/image")]
        public async Task<IActionResult> GetIngradientImage(Guid ingradientId)
        {
            // If an entity is being tracked by the context, then it is returned 
            // immediately without making a request to the database
            var ingradient = await db.Ingradients.FindAsync(ingradientId);

            if (ingradient == null)
                return NotFound("Ingradient not found");

            string imageFileName = ingradient.Image;

            string path = $"{hostingEnv.ContentRootPath}/Images/Ingradients/{imageFileName}";

            if (!System.IO.File.Exists(path))
                return NotFound("Image not found");

            string type = "image/ief"; // mime type for image file

            return await Task.Run(() => PhysicalFile(path, type, imageFileName)); 
        }
    }
}