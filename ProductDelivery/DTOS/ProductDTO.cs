using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}