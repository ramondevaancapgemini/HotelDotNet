using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelDotNet.Models;
using HotelDotNet.Models.ViewModels;

namespace HotelDotNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId);
        }

        public DbSet<HotelDotNet.Models.Room> Room { get; set; }

        public DbSet<HotelDotNet.Models.Booking> Booking { get; set; }

        public DbSet<HotelDotNet.Models.Block> Block { get; set; }

        public DbSet<HotelDotNet.Models.Payment> Payment { get; set; }

        public DbSet<HotelDotNet.Models.ViewModels.BookingViewModel> BookingViewModel { get; set; }

        public DbSet<HotelDotNet.Models.BookingCreateViewModel> BookingCreateViewModel { get; set; }
    }
}
