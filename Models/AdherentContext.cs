using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionAdherentsClub.Models
{
    public class AdherentContext : IdentityDbContext
    {
        public AdherentContext(DbContextOptions<AdherentContext> options) : base(options)
        {
        }
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Club> Clubs { get; set; }
		public DbSet<ClubEvent> ClubEvents { get; set; }
	}
}
