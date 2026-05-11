using System.ComponentModel.DataAnnotations;

namespace variantaF.Models
{
    public class InputViewModel
    {
        [Required(ErrorMessage ="Musi byt zadano")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        public string Date { get; set;  }
    }
}
