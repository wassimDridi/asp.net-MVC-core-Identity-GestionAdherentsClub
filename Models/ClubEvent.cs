namespace GestionAdherentsClub.Models
{
	public class ClubEvent
	{
		public int ClubEventId { get; set; }
		public string EventName { get; set; }
		public DateTime EventDate { get; set; }
		public string img { get; set; }
		public int ClubID { get; set; }
		public Club Club { get; set; }
	}
}
