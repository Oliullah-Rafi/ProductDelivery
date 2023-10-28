using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public System.DateTime Date { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}