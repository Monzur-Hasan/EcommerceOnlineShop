using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class TagName
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter tag name")]
        [Display(Name ="Tag Name")]
        public string Name { get; set; }
    }
}
