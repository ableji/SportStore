using System;
using System.Collections.Generic;
using System.Linq;
using SportStore.Model.Domain;
using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public class ProductService : IProductService
    {
        #region Initial

        private ApplicationDBContext _context;
        public ProductService(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion


        public Task<int> Count() =>
           _context.Products
            .Where(x => x.Quantity > 0)
            .CountAsync();

        public async Task Delete(Product item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteById(int id)
        {
            Product pro =await _context.Products.Where(x => x.ID == id).FirstOrDefaultAsync();
            pro.IsDeleted = true;
            await Update(pro);
        }

        public async Task<Product> Get(Product item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAll(int categoryId)
        {
            if (categoryId == 0)
                return await _context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x=>x.Quantity>0)
                .Include(p => p.ProductCategory)
                .ToListAsync();

            
            else
                return await _context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Quantity > 0)
                .Where(x => x.ProductCategory.ID == categoryId)
                .Include(p => p.ProductCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAll() =>
                await _context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Quantity > 0)
                .Include(p => p.ProductCategory)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetAllForDropdown() =>
            await _context.Products
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Quantity > 0)
            .ToListAsync();

        public async Task<Product> GetByID(int id) =>
             await _context.Products.FindAsync(id);
        
        public async Task Save(Product item)
        {
            item.InsertedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product item)
        {
           _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> Search(string key) =>
            await _context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.ProductName.Contains(key))
                .Where(x => x.Quantity > 0)
                .ToListAsync();


    }
}
