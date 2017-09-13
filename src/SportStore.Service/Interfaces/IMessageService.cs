using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public interface IMessageService:IService<Message>
    {
        Task<int> Count();

        Task<IEnumerable<Message>> GetSome(int count);
    }
}
