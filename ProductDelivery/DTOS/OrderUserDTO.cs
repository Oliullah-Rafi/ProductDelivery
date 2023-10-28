using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class OrderUserDTO : UserDTO
    {
        public List<OrderDTO> Orders { get; set; }

        public OrderUserDTO()
        {
            OrderDTO order = new OrderDTO();
        }
    }
}