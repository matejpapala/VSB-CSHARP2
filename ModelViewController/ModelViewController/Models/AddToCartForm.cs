using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelViewController.Models;

public class AddToCartForm
{
    public int Id {get;set;}
    [Required]
    [Range(1,10)]
    [DisplayName("Pocet kusu:")]
    public int? Quantity {get;set;}
}
