using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product> RelatedItems { get; set; }
    }
}
