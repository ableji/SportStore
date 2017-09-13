using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public interface IOrderService:IService<Order>
    {
        Task<IEnumerable<Order>> GetPendingOrders();
        Task<int> Count();
    }
}
