using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace SportStore.Model.Domain
{
    //its "Principal entity" because it contains primary/alternative key(in relationship with Product)
    public class ProductCategory : BaseModel
    {
        public ProductCategory()
        {

        }

        public string Name { get; set; }

        //Collection navigation property
        public virtual IEnumerable<Product> Products { get; set; }


        //self refrence for hierarchy structure 
        public int? ParentID { get; set; }
        public virtual ProductCategory Parent { get; set; }
        public virtual IList<ProductCategory> Childs { get; set; }
    }
}

