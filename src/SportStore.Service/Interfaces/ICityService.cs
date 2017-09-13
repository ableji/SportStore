using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportStore.Service
{
    public interface ICityService:IService<City>
    {
        IQueryable<City> GetCitiesByProvinceID(int provinceID);

        string GetCityName(int cityID);

    }
}
