using System.ComponentModel.DataAnnotations;

namespace GestionAdherentsClub.Models
{
    public class Adherent
    {
        public int AdherentId { get; set; }
        [Required]
        public string AdherentName { get; set; }
        [Required]
        public string AdherentAdresse { get; set; }
        //[Range(1, 100)]
        //public int Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime AdherentDateNaissance { get; set; }
        public int ClubID { get; set; }
        public Club Club { get; set; }

        public string img { get; set; }

        public string UserID { get; set; }
    }
}
