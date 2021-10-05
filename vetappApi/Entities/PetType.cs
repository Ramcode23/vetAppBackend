using System.ComponentModel.DataAnnotations;

namespace vetappback.Entities
{
   public class PetType
	{
    	public int Id { get; set; }

    	[Display(Name = "Pet Type")]
    	[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
    	[Required(ErrorMessage = "The field {0} is mandatory.")]
    	public string Name { get; set; }
	}

}