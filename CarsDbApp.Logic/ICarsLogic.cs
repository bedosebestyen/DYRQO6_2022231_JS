using DYRQO6_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace DYRQO6_HFT_2022231.Logic
{
    public interface ICarsLogic
    {
        void Create(Cars item);
        void Delete(int id);
        Cars Read(int id);
        IQueryable<Cars> ReadAll();
        void Update(Cars item);
        IEnumerable<object> GetCustomerWithMostExpensiveCar();
        IEnumerable<object> GetCarPurchaseDateOfOldestCar();
        IEnumerable<CustomerInfo> GetYoungestWithCar();
        IEnumerable<object> ShopWithBmw();
    }
}