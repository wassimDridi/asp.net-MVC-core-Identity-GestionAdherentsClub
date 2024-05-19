using GestionAdherentsClub.Controllers;

namespace GestionAdherentsClub.Models
{
    public class Club
    {
        public int ClubID { get; set; }
        public string ClubName { get; set; }
        public string ClubNomResponsable { get; set; }
        public ICollection<Adherent> Adherents { get; set; }

        public string img { get; set; }
    }
}
