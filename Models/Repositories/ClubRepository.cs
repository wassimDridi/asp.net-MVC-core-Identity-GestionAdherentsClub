namespace GestionAdherentsClub.Models.Repositories
{
    public class ClubRepository : IClubRepository<Club>
    {
        readonly AdherentContext context;
        public ClubRepository(AdherentContext context)
        {
            this.context = context;
        }
        public IList<Club> GetAll()
        {
            return context.Clubs.OrderBy(c => c.ClubName).ToList();
        }
        public Club GetById(int id)
        {
            return context.Clubs.Find(id);
        }
        public void Add(Club c)
        {
            context.Clubs.Add(c);
            context.SaveChanges();
        }
        public void Edit(Club c)
        {
            Club c1 = context.Clubs.Find(c.ClubID);
            if (c1 != null)
            {
                c1.ClubName = c.ClubName;
                c1.ClubNomResponsable = c.ClubNomResponsable;
                c1.img = c.img;
                context.SaveChanges();
            }
        }
        public void Delete(Club c)
        {
            Club c1 = context.Clubs.Find(c.ClubID);
            if (c1 != null)
            {
                context.Clubs.Remove(c1);
                context.SaveChanges();
            }
        }

        public int ClubCount(int clubId)
        {
            return context.Clubs.Where(c => c.ClubID ==
            clubId).Count();
        }
    }
}
