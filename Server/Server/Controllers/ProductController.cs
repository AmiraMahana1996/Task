using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DBContext _context;
        public ProductController(DBContext context)
        {
            _context = context;
        }

        // get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }


        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //add product
        [HttpPost]
        public async Task<ActionResult<Product>> postProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> updateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok();
        }
        private bool productAvailable(int id)
        {
            return (_context.Products?.Any(p => p.Id == id)).GetValueOrDefault();
        }
        //delete product
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();


                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();

            }
        }


    }
}
