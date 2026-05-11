using System.ComponentModel.DataAnnotations;

namespace variantaD.Models
{
    public class FormModel
    {
        [Required(ErrorMessage ="Povinne pole")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage ="Spatne psc")]
        public string Psc { get; set; }
    }
}
