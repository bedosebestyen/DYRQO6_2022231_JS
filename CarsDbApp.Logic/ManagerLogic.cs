using DYRQO6_HFT_2022231.Models;
using DYRQO6_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Logic
{

    public class ManagerLogic : IManagerLogic
    {
        IRepository<Manager> manrepo;
        public ManagerLogic(IRepository<Manager> mrepo)
        {
            manrepo = mrepo;
        }
        public void Create(Manager item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Manager's name cannot be null!");
            }
            if (item.Age < 18 || item.Age > 90)
            {
                throw new ArgumentException("Minimum age is 18, Max age is 90. Please stay inside the scope!");
            }
            if (item.Name.Count() > 250)
            {
                throw new ArgumentException("Please use less characters!");
            }
            manrepo.Create(item);
        }

        public void Delete(int id)
        {
            var manager = this.manrepo.Read(id);
            if (manager == null)
            {
                throw new ArgumentException("Manager does not exist!");
            }
            manrepo.Delete(id);
        }

        public Manager Read(int id)
        {
            var manager = this.manrepo.Read(id);
            if (manager == null)
            {
                throw new ArgumentException("Manager does not exist!");
            }
            return manrepo.Read(id);
        }

        public IQueryable<Manager> ReadAll()
        {
            return manrepo.ReadAll();
        }

        public void Update(Manager item)
        {
            if (item.Name == null)
            {
                throw new NullReferenceException("Manager's name cannot be null");
            }
            if (item.Name.Length > 150)
            {
                throw new ArgumentException("Please use less characters!(max:150)");
            }
            manrepo.Update(item);
        }
    }
}
