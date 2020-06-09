using System.ComponentModel.DataAnnotations;

namespace Camera_Shop.Models
{
	public class LoginViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
	}
}