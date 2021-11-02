using System.ComponentModel.DataAnnotations;

namespace vetappApi.DTOs
{
    public class RecoverPassword
    {
        [Required]
    	[EmailAddress]
    	public string Email { get; set; }

    }
}