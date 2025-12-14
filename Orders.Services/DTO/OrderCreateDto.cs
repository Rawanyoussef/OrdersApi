using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Services.DTO
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "Customer Name Is Required")]
        [StringLength(100, ErrorMessage = "Customer Name cannot exceed 100 characters")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product Is Required")]
        public string Product { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
