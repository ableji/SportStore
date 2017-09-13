using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Model.Domain
{
    public class Product : BaseModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ImageId { get; set; }
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
        public int? ProductCagegoryID { get; set; }
       
        
        //refrence]Navigation property
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual IList<CartItem> CartItems { get; set; }

    }
}
