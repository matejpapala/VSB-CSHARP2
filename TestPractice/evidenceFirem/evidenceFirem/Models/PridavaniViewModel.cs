using System.ComponentModel.DataAnnotations;

namespace evidenceFirem.Models
{
    public class PridavaniViewModel
    {
        [Required(ErrorMessage ="Povinne pole")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Povinne pole")]
        public string Dic { get; set; }
        [Required(ErrorMessage ="Povinne pole")]
        [Range(1, int.MaxValue, ErrorMessage ="Musi byt kladne cislo")]
        public int PocetZamestnancu { get; set; }
        [Required(ErrorMessage ="Povinny vyber")]
        public int PravniForma { get; set; }
        [MaxLength(500)]
        public string Poznamka { get; set; }

    }
}
