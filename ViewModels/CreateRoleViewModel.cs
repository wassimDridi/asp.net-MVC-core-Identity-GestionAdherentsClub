using System.ComponentModel.DataAnnotations;

namespace GestionAdherentsClub.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		[Display(Name = "Role")]
		public string RoleName { get; set; }
	}
}
