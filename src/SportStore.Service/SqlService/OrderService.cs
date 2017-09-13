using SportStore.Service;
using System;
using System.Collections.Generic;
using System.Text;
using SportStore.Model.Domain;
using System.Threading.Tasks;
using SportStore.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Service
{
    public class OrderService : IOrderService
    {
        #region Initial

        private ApplicationDBContext _context;
        public OrderService(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion


        public Task<int> Count() =>
           _context.Orders.CountAsync();


        public Task Delete(Order item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Get(Order item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAll() =>

              await _context.Orders
                    .Where(x => x.IsDeleted == false)
                    .Where(x=>x.Shiped==true)
                    .Include(x => x.CartItems)
                    .ThenInclude(i => i.Product)
                    .ToListAsync();
        

        public async Task<Order> GetByID(int id) =>

            await _context.Orders.FindAsync(id);
        

        public async Task<IEnumerable<Order>> GetPendingOrders() =>
              await _context.Orders
                    .Where(x => x.IsDeleted == false)
                    .Where(x => x.Shiped == false)
                    .Include(x => x.CartItems)
                    .ThenInclude(i => i.Product)
                    .ToListAsync();

        public async Task Save(Order item)
        {
            item.InsertedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            _context.AttachRange(item.CartItems.Select(x => x.Product));
            await _context.Orders.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
