namespace GestionAdherentsClub.Models.Repositories
{
	public interface IClubEventRepository<ClubEvent>
	{
		IList<ClubEvent> GetAll();
		ClubEvent GetById(int id);
		void Add(ClubEvent a);
		void Edit(ClubEvent a);
		void Delete(ClubEvent a);
		IList<ClubEvent> GetClubEventByClubID(int? clubId);
		IList<ClubEvent> FindByName(string name);
	}
}
