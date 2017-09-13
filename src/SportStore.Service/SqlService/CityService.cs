using SportStore.Service;
using System;
using System.Collections.Generic;
using System.Text;
using SportStore.Model.Domain;
using System.Threading.Tasks;
using System.Linq;
using SportStore.Data;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Service
{
    public class CityService : ICityService
    {
        #region Initial

        private ApplicationDBContext _context;
        public CityService(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion

        public Task Delete(City item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<City> Get(City item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<City>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<City> GetByID(int id)
        {
            throw new NotImplementedException();
        }

      
    

        public string GetCityName(int cityID)
        {
            throw new NotImplementedException();
        }

        public Task Save(City item)
        {
            throw new NotImplementedException();
        }

        public Task Update(City item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<City> ICityService.GetCitiesByProvinceID(int provinceID) =>
             _context.Cities
              .Where(x => x.ProvinceID == provinceID);
    }
}
