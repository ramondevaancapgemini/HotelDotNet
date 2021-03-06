﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelDotNet.Data;
using HotelDotNet.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelDotNet.Controllers
{
    [Authorize(Roles = "Receptionist")]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payment.Include(p => p.Booking);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Booking)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            var bookings = _context.Booking
                .Select(b => new
                {
                    Id = b.Id,
                    Name = string.Join(' ', b.Id, b.User.GivenName, b.User.SurnamePrefix, b.User.Surname)
                });
            ViewData["BookingId"] = new SelectList(bookings, "Id", "Name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,BookingId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                var booking = await _context.Booking.Include(b => b.Payment).FirstAsync(b => b.Id == payment.BookingId);

                if (booking.Payment != null)
                {
                    ModelState.AddModelError("AlreadyPaid", "Booking was already paid for.");
                } else { 
                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var bookings = _context.Booking
                .Select(b => new
                {
                    Id = b.Id,
                    Name = string.Join(' ', b.Id, b.User.GivenName, b.User.SurnamePrefix, b.User.Surname)
                });
            ViewData["BookingId"] = new SelectList(bookings, "Id", "Name", payment.BookingId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Booking)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var payment = await _context.Payment.SingleOrDefaultAsync(m => m.Id == id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(long id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}
