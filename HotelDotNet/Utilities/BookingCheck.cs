using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelDotNet.Data;
using HotelDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDotNet.Utilities
{
    public static class BookingCheck
    {
        public static IEnumerable<Room> GetAvailableRooms(ApplicationDbContext context, RoomSize roomSize, RoomType roomType, DateTime from,
            DateTime to)
        {
            return context.Room
                .Include(r => r.Bookings)
                .Include(r => r.Blocks)
                .Where(r => r.RoomSize == roomSize && r.RoomType == roomType)
                .Where(r => IsAvailable(r, from, to));
        }

        public static IEnumerable<Room> GetAvailableRooms(ApplicationDbContext context, RoomSize roomSize, DateTime from, DateTime to)
        {
            return context.Room
                .Include(r => r.Bookings)
                .Include(r => r.Blocks)
                .Where(r => r.RoomSize == roomSize)
                .Where(r => IsAvailable(r, from, to));
        }

        public static IEnumerable<Room> GetAvailableRooms(ApplicationDbContext context, RoomType roomType, DateTime from, DateTime to)
        {
            return context.Room
                .Include(r => r.Bookings)
                .Include(r => r.Blocks)
                .Where(r => r.RoomType == roomType)
                .Where(r => IsAvailable(r, from, to));
        }

        public static IEnumerable<Room> GetAvailableRooms(ApplicationDbContext context, DateTime from, DateTime to)
        {
            return context.Room
                .Include(r => r.Bookings)
                .Include(r => r.Blocks)
                .Where(r => IsAvailable(r, from, to));
        }

        public static bool IsAvailable(Room r, DateTime from, DateTime to)
        {
            return !r.Bookings.Any(b => Overlap(b, from, to)) && !r.Blocks.Any(b => Overlap(b, from, to));
        }

        private static bool Overlap(Booking b, DateTime from, DateTime to)
        {
            return (from <= b.From && to > b.From) ||
                   (from < b.To && to >= b.To) ||
                   (from >= b.From && to <= b.To);
        }

        private static bool Overlap(Block b, DateTime from, DateTime to)
        {
            return (from <= b.From && to > b.From) ||
                   (from < b.To && to >= b.To) ||
                   (from >= b.From && to <= b.To);
        }
    }
}
