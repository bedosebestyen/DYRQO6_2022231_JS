using DYRQO6_HFT_2022231.Models;
using System.Linq;

namespace DYRQO6_HFT_2022231.Logic
{
    public interface ICustomerLogic
    {
        void Create(Customer item);
        void Delete(int id);
        Customer Read(int id);
        IQueryable<Customer> ReadAll();
        void Update(Customer item);
    }
}