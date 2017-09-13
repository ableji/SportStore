using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportStore.Model.Domain;
using SportStore.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Service
{
    public class ProductCategoryService : IProductCategoryService
    {

        #region Initial

        private ApplicationDBContext _context;
        public ProductCategoryService(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion


        public async  Task Delete(ProductCategory item)
        {
           throw new NotImplementedException();
        }

        public async Task DeleteById(int id)
        {
            var result = await _context.ProductCategories.FindAsync(id);
            result.IsDeleted = true;
            await Update(result);
        }

        public async Task<ProductCategory> Get(ProductCategory item)
        {
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<ProductCategory>> GetAll() =>

            await _context.ProductCategories
                  .Where(x => x.IsDeleted == false)
                  .Where(x => x.ParentID == null)
                  .ToListAsync();

        public async Task<IEnumerable<ProductCategory>> GetAllForDropdown() =>
              await _context.ProductCategories
                    .Where(x => x.IsDeleted == false)
                    .ToListAsync();

        public async Task<ProductCategory> GetByID(int id) =>
            await _context.ProductCategories.FindAsync(id);

        public async Task<IEnumerable<ProductCategory>> GetForTreeView() =>

             await  _context.ProductCategories
                    .Where(x => !x.IsDeleted)
                    .Where(x => x.ParentID == null)
                    .ToListAsync();

        public async Task<IEnumerable<ProductCategory>> GetRootCategories() =>

           await _context.ProductCategories
                .Where(x => x.ParentID == null).ToListAsync();
      

        public IQueryable<ProductCategory> LoadChilds(int id) =>

                 _context.ProductCategories
                .Where(x => x.ID == id)
                .Include(x => x.Childs);

        public async Task Save(ProductCategory item)
        {
            item.InsertedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductCategory item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateById(int id)
        {
           var category = await _context.ProductCategories.FindAsync(id);
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
