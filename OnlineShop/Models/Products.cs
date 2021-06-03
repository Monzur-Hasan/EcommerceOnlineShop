using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        [Display(Name= "Product Color")]
        public string ProductColor { get; set; }

        [Required]
        [Display(Name="Available")]
        public bool IsAvailable { get; set; }

        [Display(Name="Product Type")]
        [Required]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }

        [Display(Name = "Special Tag")]
        [Required]
        public int SpecialTagId { get; set; }

        [ForeignKey("SpecialTagId")]
        public TagName SpecialTag { get; set; }  
      
    }
}


