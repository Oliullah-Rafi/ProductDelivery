using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDelivery.DTOS
{
    public class CategoryProductDTO : CategoryDTO
    {
        public List<ProductDTO> Products { get; set; }

        public CategoryProductDTO()
        {
            CategoryProductDTO Products = new CategoryProductDTO();
        }
    }
}