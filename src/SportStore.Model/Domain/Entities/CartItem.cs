using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Model.Domain
{
    public class CartItem : BaseModel
    {

        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get { return (Product.ProductPrice * Quantity); }
        }

    }
}
