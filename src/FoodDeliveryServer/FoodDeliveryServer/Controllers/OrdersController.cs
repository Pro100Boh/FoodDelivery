using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDeliveryServer.Entities;
using FoodDeliveryServer.Infrastructure;
using FoodDeliveryServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly FoodDeliveryContext _dbContext;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public OrdersController(UserManager<User> userManager, SignInManager<User> signInManager,
            FoodDeliveryContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrdersHistory()
        {
            string currentUserId = await GetCurrentUserId();

            var orders = await _dbContext.Orders.AsNoTracking()
                .Include(o => o.OrderedProducts)
                .Where(o => o.UserId == currentUserId)
                .ToListAsync();

            var ordersView = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

            return Ok(ordersView);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MakeOrder([FromBody] IEnumerable<Guid> productsIds)
        {
            var pizzas = await _dbContext.Pizzas.Where(p => productsIds.Contains(p.Id)).ToListAsync();
            var drinks = await _dbContext.Drinks.Where(p => productsIds.Contains(p.Id)).ToListAsync();
            var desserts = await _dbContext.Desserts.Where(p => productsIds.Contains(p.Id)).ToListAsync();

            var products = new List<IProduct>();
            products.AddRange(pizzas);
            products.AddRange(drinks);
            products.AddRange(desserts);

            var orderedProducts = new List<OrderedProduct>();
            foreach (var product in products)
            {
                orderedProducts.Add(new OrderedProduct
                {
                    ProductName = product is Pizza ? $"Pizza {product.Name}" : product.Name,
                    ProductPrice = product.Price
                });
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderedProducts = orderedProducts,
                UserId = await GetCurrentUserId()
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private async Task<string> GetCurrentUserId()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            return currentUser.Id;
        }
    }
}
