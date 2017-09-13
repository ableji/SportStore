using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public interface IProductService : IService<Product>
    {
        //Here you can add custom methods

        Task<IEnumerable<Product>> GetAllForDropdown();

        Task<IEnumerable<Product>> Search(string key);

        Task<IEnumerable<Product>> GetAll(int categoryId);

        Task<int> Count();
    }
}
