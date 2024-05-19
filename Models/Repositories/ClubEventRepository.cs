using GestionAdherentsClub.Models;
namespace GestionAdherentsClub.Models.Repositories
{

	public class ClubEventRepository : IClubEventRepository<ClubEvent>
	{
		readonly AdherentContext context;
		public ClubEventRepository(AdherentContext context)
		{
			this.context = context;
		}
		public IList<ClubEvent> GetAll()
		{
			return context.ClubEvents.OrderBy(x => x.EventName).ToList();
		}
		public ClubEvent GetById(int id)
		{
			return context.ClubEvents.FirstOrDefault(x => x.ClubEventId == id);
		}
		public void Add(ClubEvent a)
		{
			context.ClubEvents.Add(a);
			context.SaveChanges();
		}
		public void Edit(ClubEvent a)
		{
			var existingEvent = context.ClubEvents.FirstOrDefault(x => x.ClubEventId == a.ClubEventId);
			if (existingEvent != null)
			{
				existingEvent.EventName = a.EventName;
				existingEvent.EventDate = a.EventDate;
				existingEvent.ClubID = a.ClubID;
				existingEvent.img = a.img;
				context.SaveChanges();
			}
		}
		public void Delete(ClubEvent a)
		{
			var existingEvent = context.ClubEvents.FirstOrDefault(x => x.ClubEventId == a.ClubEventId);
			if (existingEvent != null)
			{
				context.ClubEvents.Remove(existingEvent);
				context.SaveChanges();
			}
		}
		public IList<ClubEvent> GetClubEventByClubID(int? clubId)
		{
			return context.ClubEvents.Where(s =>
			s.ClubID.Equals(clubId))
			.OrderBy(s => s.EventName).ToList();
		}
		public IList<ClubEvent> FindByName(string name)
		{
			return context.ClubEvents.Where(s =>
			s.EventName.Contains(name)).ToList();
		}
	}


}
