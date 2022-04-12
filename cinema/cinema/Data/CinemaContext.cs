using cinema.Identity;
using Microsoft.EntityFrameworkCore;
using cinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace cinema.Data
{
    public class CinemaContext : IdentityDbContext<CinemaIdentityUser>
    {
        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
            
        }
        public DbSet<Theatre>? Theatres { get; set; }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<RoomTemplate>? RoomTemplates { get; set; }
        public DbSet<Show>? Shows { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<TicketOrder>? Orders { get; set; }
        public DbSet<Subscriber>? Subscribers { get; set; }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<FoundItem>? FoundItems { get; set; }
    }
}