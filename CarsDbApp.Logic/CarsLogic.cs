using DYRQO6_HFT_2022231.Models;
using DYRQO6_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DYRQO6_HFT_2022231.Logic
{
    public class CarsLogic : ICarsLogic
    {
        IRepository<Cars> carsrepo;
        IRepository<Customer> custrepo;
        IRepository<CarShop> shoprepo;

        public CarsLogic(IRepository<Cars> carsrep, IRepository<Customer> custrep, IRepository<CarShop> shoprep)
        {
            carsrepo = carsrep;
            custrepo = custrep;
            shoprepo = shoprep;
        }

        public void Create(Cars item)
        {
            if (item.CarType == null)
            {
                throw new NullReferenceException("Car's brand cannot be null!");
            }
            if (item.CarType.Count() >=250)
            {
                throw new ArgumentException("Please use less characters!");
            }
            carsrepo.Create(item);
        }

        public void Delete(int id)
        {
            var car = this.carsrepo.Read(id);
            if (car == null)
            {
                throw new ArgumentException("Car does not exist!");
            }
            carsrepo.Delete(id);
        }

        public Cars Read(int id)
        {
            var car = this.carsrepo.Read(id);
            if (car == null)
            {
                throw new ArgumentException("Car does not exist!");
            }
            return carsrepo.Read(id);
        }

        public IQueryable<Cars> ReadAll()
        {
            return carsrepo.ReadAll(); ;
        }

        public void Update(Cars item)
        {
            if (item.CarType == null)
            {
                throw new NullReferenceException("Car's name cannot be null");
            }
            if (item.CarType.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            if (item.CarColor.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            carsrepo.Update(item);
        }
        public IEnumerable<object> GetCustomerWithMostExpensiveCar()
        {
            var query = from x in carsrepo.ReadAll()
                        let MostExpensiveCar = carsrepo.ReadAll().Max(x => x.Price)
                        where x.Price == MostExpensiveCar && x.Customer.CustomerId == x.CustomerId
                        select new
                        {
                            Name = x.Customer.Name
                        };

            return query;
        }

        public IEnumerable<object> GetCarPurchaseDateOfOldestCar()
        {
            var oldest = carsrepo.ReadAll().Min(t => t.PurchaseDate);
            return from x in carsrepo.ReadAll()
                   where oldest.Date == x.PurchaseDate
                   && x.CustomerId == x.Customer.CustomerId
                   select new
                   {
                       Date = x.PurchaseDate.Year + "." + x.PurchaseDate.Month + "." + x.PurchaseDate.Day,
                       Name = x.Customer.Name,
                       CarType = x.CarType
                   };
        }

        public IEnumerable<CustomerInfo> GetYoungestWithCar()
        {
            var youngest = custrepo.ReadAll().Min(t => t.Age);
            return from x in carsrepo.ReadAll()
                   where youngest == x.Customer.Age && x.Customer.CustomerId == x.CustomerId && x.ShopId == x.Shop.ShopId
                   select new CustomerInfo
                   {
                       Name = x.Customer.Name,
                       Age = x.Customer.Age,
                       CarType = x.CarType,
                       CarShop = x.Shop.Name
                   };
        }

        public IEnumerable<object> ShopWithBmw()
        {
            return from x in carsrepo.ReadAll()
                   where x.CarType == "BMW" && x.ShopId == x.Shop.ShopId
                   select new
                   {
                       CarType = x.CarType,
                       Price = x.Price,
                       Shop = x.Shop.Name
                   };
        }
    }
    public class CustomerInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string CarType { get; set; }
        public string CarShop { get; set; }


        public override bool Equals(object obj)
        {
            Customer b = obj as Customer;
            Cars c = obj as Cars;
            CarShop r = obj as CarShop;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Name == b.Name
                    && this.Age == b.Age
                    && this.CarType == c.CarType
                    && this.CarShop == r.Name;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Age, this.CarType, this.CarShop);
        }
    }
}
