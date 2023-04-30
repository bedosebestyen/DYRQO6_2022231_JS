using DYRQO6_HFT_2022231.Models;
using DYRQO6_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Logic
{
    public class CarShopLogic : ICarShopLogic
    {
        IRepository<CarShop> shoprepo;
        IRepository<Manager> manrepo;
        public CarShopLogic(IRepository<CarShop> repo, IRepository<Manager> mrepo)
        {
            shoprepo = repo;
            manrepo = mrepo;
        }

        public void Create(CarShop item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Shop's name cannot be null!");
            }
            if (item.Name.Count() > 250)
            {
                throw new ArgumentException("Please use less characters!");
            }
            shoprepo.Create(item);
        }

        public void Delete(int id)
        {
            var shop = this.shoprepo.Read(id);
            if (shop == null)
            {
                throw new ArgumentException("Shop does not exist!");
            }
            shoprepo.Delete(id);
        }
        public CarShop Read(int id)
        {
            var shop = this.shoprepo.Read(id);
            if (shop == null)
            {
                throw new ArgumentException("Shop does not exist!");
            }
            return shoprepo.Read(id);
        }

        public IQueryable<CarShop> ReadAll()
        {
            return shoprepo.ReadAll(); ;
        }

        public void Update(CarShop item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Shop's name cannot be null");
            }
            if (item.Name.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            if (item.Address.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            shoprepo.Update(item);
        }
        public IEnumerable<object> GetHighestPaidManager()
        {
            var highest_salary = manrepo.ReadAll().Max(t => t.Salary);
            var query = from x in shoprepo.ReadAll()
                        where highest_salary == x.Manager.Salary && x.Manager.ManagerId == x.ManagerId
                        select new
                        {
                            Name = x.Manager.Name,
                            Salary = x.Manager.Salary,
                            Shop = x.Name
                        };
            return query;
        }
    }
}
