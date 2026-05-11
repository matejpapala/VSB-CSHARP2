using System.ComponentModel.DataAnnotations;

namespace SmenarnaTest.Models
{
    public class ExchangeFormModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Enter correct email")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Choose currency")]
        public string Currency { get; set; }
        [Required(ErrorMessage ="Enter value")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Must be greater than zero")]
        public double Value { get; set; }
    }
}
