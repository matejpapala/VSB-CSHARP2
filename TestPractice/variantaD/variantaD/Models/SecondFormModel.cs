using System.ComponentModel.DataAnnotations;

namespace variantaD.Models
{
    public class SecondFormModel
    {
        [Required(ErrorMessage ="Povinne pole")]
        [RegularExpression(@"^[0-9]{5}$")]
        public string Psc { get; set; }
        [Required(ErrorMessage = "Povinne pole")]
        [RegularExpression(@"^\+420[0-9]{9}$")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Povinne pole")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Povinne pole")]
        public string Municipality { get; set; }
    }
}
