using DYRQO6_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Repository
{
    public class CarsRepository : Repository<Cars>, IRepository<Cars>
    {
        public CarsRepository(CarsDbContext ctx) : base(ctx)
        {

        }
        public override Cars Read(int id)
        {
            return ctx.Cars.FirstOrDefault(x => x.CarId == id);
        }

        public override void Update(Cars item)
        {
            var old = Read(item.CarId);  
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
