using DYRQO6_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace DYRQO6_HFT_2022231.Logic
{
    public interface ICarShopLogic
    {
        void Create(CarShop item);
        void Delete(int id);
        CarShop Read(int id);
        IQueryable<CarShop> ReadAll();
        void Update(CarShop item);
        public IEnumerable<object> GetHighestPaidManager();
    }
}