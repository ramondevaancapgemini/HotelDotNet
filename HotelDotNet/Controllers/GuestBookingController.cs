using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelDotNet.Data;
using HotelDotNet.Models;
using HotelDotNet.Models.ViewModels;
using HotelDotNet.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelDotNet.Controllers
{
    [Authorize(Roles = "Guest")]
    public class GuestBookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GuestBookingController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Blocks
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var bookings = _context.Booking.Include(b => b.Room)
                .Where(b => b.UserId.Equals(user.Id))
                .Select(b => new BookingViewModel
                {
                    RoomId = b.RoomId,
                    From = b.From,
                    To = b.To,
                    Id = b.Id,
                    Room = b.Room
                });


            return View(await bookings.ToListAsync());
        }

        // GET: Blocks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Room)
                .SingleOrDefaultAsync(m => m.Id == id && m.UserId.Equals(user.Id));
            if (booking == null)
            {
                return NotFound();
            }
            var bookingView = new BookingViewModel()
            {
                From = booking.From,
                To = booking.To,
                Id = booking.Id,
                Room = booking.Room,
                RoomId = booking.RoomId
            };

            return View(bookingView);
        }

        // GET: Blocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,To,RoomSize,RoomType")] BookingCreateViewModel bookingView)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
                var availableRooms = BookingCheck.GetAvailableRooms(
                    _context, 
                    bookingView.RoomSize, 
                    bookingView.RoomType,
                    bookingView.From, 
                    bookingView.To
                ).ToList();

                if (!availableRooms.Any())
                {
                    ModelState.AddModelError("NoRoomsAvailable", "No rooms were available for that time period.");
                }
                else
                {
                    var room = availableRooms.First();
                    var booking = new Booking
                    {
                        Id = bookingView.Id,
                        From = bookingView.From,
                        To = bookingView.To,
                        RoomId = room.Id,
                        Room = room,
                        User = user,
                        Payment = null,
                    };
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(bookingView);
        }
    }
}