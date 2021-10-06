using System.ComponentModel.DataAnnotations;

namespace vetappApi.DTOs
{
    public class ServiceTypeCreateDTO
    {

        [Display(Name = "Service Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }
    }

}
