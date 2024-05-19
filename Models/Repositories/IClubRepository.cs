namespace GestionAdherentsClub.Models.Repositories
{
    public interface IClubRepository<Club>
    {
        IList<Club> GetAll();
        Club GetById(int id);
        void Add(Club c);
        void Edit(Club c);
        void Delete(Club c);
        //double StudentAgeAverage(int clubId);
        int ClubCount(int clubId);

    }
}
