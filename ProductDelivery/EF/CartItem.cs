using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.EF
{
    public class CartItem
    {
        public int id { get; set; } // Product ID
        public string name { get; set; } // Product Name
      
        public float Price { get; set; } // Price of the product
        public float Quantity { get; set; } // Status of the product in the cart

        public string Status { get; set; }

        public int Count { get; set; }

        public int CustomerId { get; set; }
        public int CategoryId { get; set; } // Customer ID

        public DateTime Date { get; set; } // Date of the order



    }
}