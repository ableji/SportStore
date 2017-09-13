using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SportStore.Model.Domain;
using System.Linq;
using SportStore.Data;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Service
{
    public class ProvinceService : IProvinceService
    {

        #region Initial

        private ApplicationDBContext _context;
        public ProvinceService(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion


        public Task Delete(Province item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Province> Get(Province item)
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<Province>> GetAll() =>
           await _context.Provinces.ToListAsync();
                

        public Task<Province> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save(Province item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Province item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
