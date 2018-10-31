using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperDiet.Models;

namespace SuperDiet.Controllers
{
    public class ItemsController : Controller
    {
        private readonly SuperDietContext _context;

        public ItemsController(SuperDietContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.userID = id;
            return View(await _context.Item.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/AddToCart
        [HttpPost("AddToCart/{ItemId}")]
        public async Task<IActionResult> AddToCart([FromRoute] int ItemId, int UserID)
        {
            var item = await _context.Item.SingleOrDefaultAsync(m => m.ID == ItemId);
            if (item == null || item.Quantity == 0)
            {
                return NotFound();
            }
            var itemOrder = await _context.ItemOrder.SingleOrDefaultAsync(m => m.OrderID == UserID && m.ItemID == ItemId);
            if (itemOrder == null)
            {
                itemOrder = new ItemOrder
                {
                    OrderID = UserID,
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

        // GET: Items
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var item = await _context.Item.SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
