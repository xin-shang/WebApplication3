using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsWebAPIController : ControllerBase
    {
        private readonly AmazonOrdersContext _context;

        public ItemsWebAPIController(AmazonOrdersContext context)
        {
            _context = context;
        }

        /* *
         * GET: 用于从Web服务检索数据
         * **/
        // GET: api/ItemsWebAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems(string? SearchText, int? CategoryID)
        {
            if (_context.Items == null) 
            {
                return NotFound();
            }

            var query = _context.Items.Select(i => i);
            if (!string.IsNullOrWhiteSpace(SearchText)) 
            {
                query = query.Where(i => i.ItemName.StartsWith(SearchText));
            }

            if (CategoryID.HasValue) 
            {
                query = query.Where(i => i.Category.ParentCategoryId == CategoryID);
            }

            query = query.OrderBy(i => i.ItemName);

            return await query.ToListAsync();
        }

        // GET: api/ItemsWebAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


        /* *
         * PUT: 用于更新Web服务上的数据
         * **/
        // PUT: api/ItemsWebAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.ItemId)
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


        /* *
         * POST: 用于在Web服务上创建新的数据项
         * **/
        // POST: api/ItemsWebAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        /* *
         * DELETE: 用于删除Web服务上的一项数据
         * **/
        // DELETE: api/ItemsWebAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
