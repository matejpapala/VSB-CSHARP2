using System.ComponentModel.DataAnnotations;

namespace financakTest.Models
{
    public class DetailViewModel
    {
        [Required(ErrorMessage ="Povinne")]
        public string Name { get; set;}
        [Required(ErrorMessage ="Povinny email")]
        [EmailAddress(ErrorMessage ="neplatny email")]
        public string Email { get; set;}
        [Required(ErrorMessage ="Vyberte pohlavi")]
        public string Sex { get; set; }
        [Required(ErrorMessage ="Poznamka je povinna")]
        [MaxLength(500)]
        public string Note { get; set;}
        public string CHodnota { get; set; }
    }
}
