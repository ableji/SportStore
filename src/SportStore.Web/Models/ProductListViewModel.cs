using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaginationViewModel PagingInfo { get; set; }
        public ProductCategory Category { get; set; }


    }
}

