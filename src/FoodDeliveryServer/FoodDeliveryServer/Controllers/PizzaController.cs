using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDeliveryServer.Entities;
using FoodDeliveryServer.Infrastructure;
using FoodDeliveryServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly FoodDeliveryEFCoreContext db;

        public PizzaController(FoodDeliveryEFCoreContext context, IMapper mapper)
        {
            this.mapper = mapper;
            db = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pizzas = await db.Pizzas.AsNoTracking().
                            Include(g => g.PizzaIngradients).
                            ThenInclude(gg => gg.Ingradient).
                            AsNoTracking().
                            ToListAsync();

            var pizzasView = mapper.Map<IEnumerable<PizzaViewModel>>(pizzas);

            return Ok(pizzasView);
        }

        /*
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
