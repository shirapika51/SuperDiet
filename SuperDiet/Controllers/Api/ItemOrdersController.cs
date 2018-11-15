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
    [Route("Api/ItemOrders")]
    [ApiController]
    public class ItemOrdersController : ControllerBase
    {
        private readonly SuperDietContext _context;

        public ItemOrdersController(SuperDietContext context)
        {
            _context = context;
        }

        // GET: api/ItemOrders
        [HttpGet("GetItemOrder/{userID}")]
        public IActionResult GetItemOrder([FromRoute] int userID)
        {
            var items = from itemOrder in _context.ItemOrder
                        join item in _context.Item on itemOrder.ItemID equals item.ID
                        where itemOrder.OrderID == userID
                        select new
                        {
                            id = item.ID,
                            name = item.Name,
                            price = item.Price,
                            quantity = itemOrder.Quantity
                        };
            return Ok(items);
        }

        // PUT: api/ItemOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemOrder([FromRoute] int id, [FromBody] ItemOrder itemOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemOrder.ItemID)
            {
                return BadRequest();
            }

            _context.Entry(itemOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemOrderExists(id))
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

        // POST: api/ItemOrders
        [HttpPost]
        public async Task<IActionResult> PostItemOrder([FromBody] ItemOrder itemOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemOrder.Add(itemOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemOrderExists(itemOrder.ItemID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItemOrder", new { id = itemOrder.ItemID }, itemOrder);
        }

        // DELETE: api/ItemOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemOrder = await _context.ItemOrder.FindAsync(id);
            if (itemOrder == null)
            {
                return NotFound();
            }

            _context.ItemOrder.Remove(itemOrder);
            await _context.SaveChangesAsync();

            return Ok(itemOrder);
        }

        private bool ItemOrderExists(int id)
        {
            return _context.ItemOrder.Any(e => e.ItemID == id);
        }

        // POST: Api/ItemOrders/RemoveFromCart
        [HttpPost("RemoveFromCart/{ItemId}/{UserID}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int ItemId, [FromRoute] int UserID)
        {
            var itemOrder = _context.ItemOrder.Where(m => m.ItemID == ItemId && m.OrderID == UserID).First();
            if (itemOrder == null)
            {
                return NotFound();
            }
            itemOrder.Quantity--;
            var item = await _context.Item.SingleOrDefaultAsync(p => p.ID == itemOrder.ItemID);
            if (item != null)
            {
                item.Quantity++;
            }
            if (itemOrder.Quantity == 0)
            {
                _context.ItemOrder.Remove(itemOrder);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}