using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int Id { get; set; }

        [Display(Name= "Order No")]
        public string OrderNo { get; set; }

        [Required(ErrorMessage ="Please enter your name")]
        public string Name { get; set; }

        [Display(Name = "Phone No")]
        [Required(ErrorMessage = "Please enter valid phone no")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please enter valid email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
