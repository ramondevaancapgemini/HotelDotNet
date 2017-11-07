using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelDotNet.Data;
using HotelDotNet.Models;

namespace HotelDotNet.Controllers
{
    public class BlocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Block.Include(b => b.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Blocks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _context.Block
                .Include(b => b.Room)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        // GET: Blocks/Create
        public IActionResult Create()
        {
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name");
            return View();
        }

        // POST: Blocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,To,Description,RoomId")] Block block)
        {
            if (ModelState.IsValid)
            {
                _context.Add(block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name", block.RoomId);
            return View(block);
        }

        // GET: Blocks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _context.Block.SingleOrDefaultAsync(m => m.Id == id);
            if (block == null)
            {
                return NotFound();
            }
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name", block.RoomId);
            return View(block);
        }

        // POST: Blocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,From,To,Description,RoomId")] Block block)
        {
            if (id != block.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(block);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockExists(block.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name", block.RoomId);
            return View(block);
        }

        // GET: Blocks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _context.Block
                .Include(b => b.Room)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }

        // POST: Blocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var block = await _context.Block.SingleOrDefaultAsync(m => m.Id == id);
            _context.Block.Remove(block);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockExists(long id)
        {
            return _context.Block.Any(e => e.Id == id);
        }
    }
}
