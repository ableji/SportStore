using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public interface IService<X>
    {

        Task<X> Get(X item);

        Task<X> GetByID(int id);

        Task<IEnumerable<X>> GetAll();

        Task Save(X item);

        Task Delete(X item);

        Task DeleteById(int id);

        Task Update(X item);

        Task UpdateById(int id);

    }
}
