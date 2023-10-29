using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class ProductOrderDetailsDTO : ProductDTO
    {
      
            public List<OrderDetailDTO> OrderDetails { get; set; }

            public ProductOrderDetailsDTO()
            {
            ProductOrderDetailsDTO OrderDetails = new ProductOrderDetailsDTO();
            }
        
    }
}