using DYRQO6_HFT_2022231.Models;
using DYRQO6_HFT_2022231.Repository;
using System;
using System.Linq;
using System.Numerics;

namespace DYRQO6_HFT_2022231.Logic
{
    public class CustomerLogic : ICustomerLogic
    {
        IRepository<Customer> repo;

        public CustomerLogic(IRepository<Customer> repo)
        {
            this.repo = repo;
        }

        public void Create(Customer item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Customers 's name cannot be null!");
            }
            if (item.Age < 17 || item.Age > 100)
            {
                throw new ArgumentException("Minimum age is 17, Max age is 100. Please stay inside the scope!");
            }
            if (item.Name.Count() > 250)
            {
                throw new ArgumentException("Please use less characters!");
            }
            if (item.Address.Count() > 250)
            {
                throw new ArgumentException("Please use less characters!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            var customer = this.repo.Read(id);
            //if (customer == null)
            //{
            //    throw new ArgumentException("Customer does not exist!");
            //}
            this.repo.Delete(id);
        }

        public Customer Read(int id)
        {
            var customer = this.repo.Read(id);
            //if (customer == null)
            //{
            //    throw new ArgumentException("Customer does not exist!");
            //}
            return this.repo.Read(id);
        }

        public IQueryable<Customer> ReadAll()
        {
            return this.repo.ReadAll(); ;
        }

        public void Update(Customer item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Customer's name cannot be null");
            }
            if (item.Name.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            if (item.Address.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            if (item.Age > 100 || item.Age < 17)
            {
                throw new ArgumentException("Minimum age is 17, Max age is 100. Please stay inside the scope!");
            }
            this.repo.Update(item);
        }
    }
}
