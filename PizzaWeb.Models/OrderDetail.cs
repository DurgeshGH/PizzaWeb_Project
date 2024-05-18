using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWeb.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderId { get; set; }

        public double Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        [Required]
        public string? PostalCode { get; set; }



        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]

        public Product Product { get; set; }


    }
}
