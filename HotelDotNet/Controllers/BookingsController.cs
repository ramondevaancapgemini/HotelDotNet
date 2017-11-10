using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelDotNet.Data;
using HotelDotNet.Models;
using HotelDotNet.Models.ViewModels;
using HotelDotNet.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Localization;

namespace HotelDotNet.Controllers {
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var booking = _context.Booking.Include(b => b.Room).Include(b => b.User);
            return View(await booking.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Room)
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name");
            var users = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Name = string.Join(' ', u.GivenName, u.SurnamePrefix, u.Surname)
                });
            ViewData["UserId"] = new SelectList(users, "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,To,RoomId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var room = await _context.Room
                    .Include(r => r.Blocks)
                    .Include(r => r.Bookings)
                    .FirstAsync(r => r.Id == booking.RoomId);
                if (!BookingCheck.IsAvailable(room, booking.From, booking.To))
                {
                    ModelState.AddModelError("NotAvailable", "Room is not available for given period.");
                }
                else
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name", booking.RoomId);
            var users = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Name = string.Join(' ', u.GivenName, u.SurnamePrefix, u.Surname)
                });
            ViewData["UserId"] = new SelectList(users, "Id", "Name", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name", booking.RoomId);
            var users = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Name = string.Join(' ', u.GivenName, u.SurnamePrefix, u.Surname)
                });
            ViewData["UserId"] = new SelectList(users, "Id", "Name", booking.UserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,From,To,RoomId,UserId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var room = await _context.Room
                    .Include(r => r.Blocks)
                    .Include(r => r.Bookings)
                    .FirstAsync(r => r.Id == booking.RoomId);
                if (!BookingCheck.IsAvailable(room, booking.From, booking.To))
                {
                    ModelState.AddModelError("NotAvailable", "Room is not available for given period.");
                }
                else
                {
                    try
                    {
                        _context.Update(booking);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BookingExists(booking.Id))
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
            }
            var rooms = _context.Room
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Id + " - " + r.Description
                });
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name", booking.RoomId);
            var users = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Name = string.Join(' ', u.GivenName, u.SurnamePrefix, u.Surname)
                });
            ViewData["UserId"] = new SelectList(users, "Id", "Name", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Room)
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.Id == id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(long id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}
