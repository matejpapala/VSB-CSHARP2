using System.ComponentModel.DataAnnotations;

namespace variantaF.Models
{
    public class FormViewModel
    {
        [Required(ErrorMessage ="Povinne jmeno")]
        [MinLength(3)]
        public string Name { get; set; }
        public string AltId { get; set; }
        public string Date { get; set;  }
    }
}
