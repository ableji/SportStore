using SportStore.Model.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetForTreeView();

        IQueryable<ProductCategory> LoadChilds(int id);

        Task<IEnumerable<ProductCategory>> GetAllForDropdown();

        Task<IEnumerable<ProductCategory>> GetRootCategories();
    }
}
