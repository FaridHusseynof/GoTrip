using GoTrip.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoTrip.Data
{
    public class TripDbContext : IdentityDbContext<AppUser>
    {
        public TripDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tour> tours { get; set; }
    }
}
