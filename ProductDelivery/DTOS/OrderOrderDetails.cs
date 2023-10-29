using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class OrderOrderDetails : OrderDTO
    {
        public List<OrderDetailDTO> OrderDetails { get; set; }
   
    public OrderOrderDetails()
    {
        OrderOrderDetails OrderDetails= new OrderOrderDetails();
    }
    }
}