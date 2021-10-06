using System.ComponentModel.DataAnnotations;

namespace vetappback.DTOs
{
    public class OwnerResponse

    {
        public int Id { get; set; }
        [Display(Name = "Document")]
    	public string Document { get; set; }

    	[Display(Name = "First Name")]
    	public string FirstName { get; set; }

    	[Display(Name = "Last Name")]
    	public string LastName { get; set; }

    	[MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
    	public string Address { get; set; }

    	[Display(Name = "Full Name")]
    	public string FullName => $"{FirstName} {LastName}";

    	[Display(Name = "Full Name")]
    	public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
        
    }
}