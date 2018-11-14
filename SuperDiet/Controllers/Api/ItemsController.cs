using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperDiet.Models;

namespace SuperDiet.Controllers.Api
{
    [Route("Api/Items")]
    public class ItemsController : ControllerBase
    {
        private readonly SuperDietContext _context;

        public ItemsController(SuperDietContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> GetItem()
        {
            return _context.Item;
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Item.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Items
        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ID }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ID == id);
        }

        // POST: Api/Items/AddToCart
        [HttpPost("AddToCart/{ItemId}/{UserID}")]
        public async Task<IActionResult> AddToCart([FromRoute] int ItemId,[FromRoute] int UserID)
        {
            var item = await _context.Item.SingleOrDefaultAsync(m => m.ID == ItemId);
            if (item == null || item.Quantity == 0)
            {
                return NotFound();
            }
            var itemOrder = await _context.ItemOrder.SingleOrDefaultAsync(m => m.OrderID == UserID && m.ItemID == ItemId);
            var Orderuser = await _context.Order.SingleOrDefaultAsync(m => m.ID == UserID);
            if (itemOrder == null)
            {
                itemOrder = new ItemOrder
                {
                    OrderID = UserID,
                    Order = Orderuser,
                    ItemID = item.ID,
                    Quantity = 1
                };
                _context.ItemOrder.Add(itemOrder);
            }
            else
            {
                itemOrder.Quantity += 1;
            }
            item.Quantity--;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}