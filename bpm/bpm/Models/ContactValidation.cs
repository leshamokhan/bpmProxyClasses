using System.ComponentModel.DataAnnotations;

namespace bpm.Models
{
    public class ContactValidation
    {
        [Required(ErrorMessage = "Please enter your Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "3 to 30 characters required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter your MobilePhone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+*([0-9]{3})?[-]?\(?([0-9]{2})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number in the form '+XXX-XX-XXX-XXXX'")]
        public string MobilePhone { get; set; }


        [Required(ErrorMessage = "Please enter your Dear")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "3 to 30 characters required")]
        public string Dear { get; set; }


        [Required(ErrorMessage = "Please enter your JobTitle")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "3 to 30 characters required")]
        public string JobTitle { get; set; }


        [Required(ErrorMessage = "Please enter your BirthDate")]
        public string BirthDate { get; set; }
    }
}