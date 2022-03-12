using HShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HShopping.ModelView
{
    public class ProductsHomeVM
    {
        public List<Products> Products { get; set; }
        public Categories Categories { get; set; }
    }
}
