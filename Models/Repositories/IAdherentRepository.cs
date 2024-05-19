namespace GestionAdherentsClub.Models.Repositories
{
    public interface IAdherentRepository<Adherent>
    {
        IList<Adherent> GetAll();
        Adherent GetById(int id);
        void Add(Adherent a);
        void Edit(Adherent a);
        void Delete(Adherent a);
        IList<Adherent> GetAdherentsByClubID(int? clubId);
        IList<Adherent> FindByName(string name);
    }
}
