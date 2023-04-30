using DYRQO6_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Repository
{
    public class ManagerRepository : Repository<Manager>, IRepository<Manager>
    {
        public ManagerRepository(CarsDbContext ctx) : base(ctx)
        {

        }
        public override Manager Read(int id)
        {
            return ctx.Manager.FirstOrDefault(x => x.ManagerId == id);
        }

        public override void Update(Manager item)
        {
            var old = Read(item.ManagerId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(x => x.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
