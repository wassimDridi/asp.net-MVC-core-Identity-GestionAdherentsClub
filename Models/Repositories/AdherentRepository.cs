using Microsoft.EntityFrameworkCore;

namespace GestionAdherentsClub.Models.Repositories
{
    public class AdherentRepository : IAdherentRepository<Adherent>
    {
		
		readonly AdherentContext context;
        public AdherentRepository(AdherentContext context)
        {
            this.context = context;
        }
        public IList<Adherent> GetAll()
        {
            return context.Adherents.OrderBy(x => x.AdherentName).Include(x => x.Club).ToList();
        }
        public Adherent GetById(int id)
        {
            return context.Adherents.Where(x => x.AdherentId ==
            id).Include(x => x.Club).SingleOrDefault();
        }
        public void Add(Adherent a)
        {
            context.Adherents.Add(a);
            context.SaveChanges();
        }
        public void Edit(Adherent a)
        {
            Adherent s1 = context.Adherents.Find(a.AdherentId);
            if (s1 != null)
            {
                s1.AdherentName = a.AdherentName;
                s1.AdherentAdresse = a.AdherentAdresse;
                s1.AdherentDateNaissance = a.AdherentDateNaissance;
                s1.ClubID = a.ClubID;
                s1.img = a.img;
                context.SaveChanges();
            }
        }
        public void Delete(Adherent a)
        {
            Adherent s1 = context.Adherents.Find(a.AdherentId);
            if (s1 != null)
            {
                context.Adherents.Remove(s1);
                context.SaveChanges();
            }
        }
        public IList<Adherent> GetAdherentsByClubID(int? clubId)
        {
            return context.Adherents.Where(s =>
            s.ClubID.Equals(clubId))
            .OrderBy(s => s.AdherentName)
            .Include(std => std.Club).ToList();
        }
        public IList<Adherent> FindByName(string name)
        {
            return context.Adherents.Where(s =>
            s.AdherentName.Contains(name)).Include(std => std.Club).ToList();
                
        }
    }
}
