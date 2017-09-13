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
    public class MessageService : IMessageService
    {
        #region Initial

        private ApplicationDBContext _context;
        public MessageService(ApplicationDBContext context)
        {
            _context = context;
        }



        #endregion


        public Task<int> Count() =>

            _context.Messages.CountAsync();
        

        public Task Delete(Message item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Message> Get(Message item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetAll() =>
            await _context.Messages.ToListAsync();

        public Task<Message> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetSome(int count) =>
                await _context.Messages
                .Take(count)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        public async Task Save(Message item)
        {
            await _context.Messages.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public Task Update(Message item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
