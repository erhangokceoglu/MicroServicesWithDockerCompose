﻿using MicroService1.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProductsController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_configuration.GetSection("MicroServices")["Microservice2BaseUrl"]}/api/Products");
            var context = await response.Content.ReadAsStringAsync();
            return Ok(context);
        }

        [HttpPost]
        public IActionResult Save()
        {
            _dbContext.Products.Add(new Product() { Name = "Kereste" });
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
