using Microsoft.AspNetCore.Mvc;
using ProductAPI3.Application.Interfaces;
using ProductAPI3.Domain.Entities;

namespace ProductAPI3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    { 

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await productService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetByIdAsync(id);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var created = await productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            throw new Exception("Simulated exception from Update(...)");
            var existing = await productService.GetByIdAsync(id);
            if (existing is null) return NotFound();

            var updated = product with { Id = id };
            await productService.UpdateAsync(updated);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await productService.GetByIdAsync(id);
            if (existing is null) return NotFound();

            await productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("simulate-exception")]
        public IActionResult SimulateException()
        {
            throw new NullReferenceException("Test Exception: Product not found!");
        }

        [HttpGet("simulate-division-by-zero")]
        public IActionResult SimulateDivideByZero()
        {
            int x = 0;
            int y = 1 / x; // 🧨 throw DivideByZeroException
            return Ok();
        }

    }

}