using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Quantity { get; set; }
    }
}