using BulkyBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Model.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        //public double CartTotal { get; set; } // hold cart total
        public OrderHeader OrderHeader { get; set; } // hold cart total

        //public OrderHeader OrderHeader { get; set; }
    }
}