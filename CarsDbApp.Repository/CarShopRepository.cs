using DYRQO6_HFT_2022231.Models;
using Castle.DynamicProxy.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Repository
{
    public class CarShopRepository : Repository<CarShop>, IRepository<CarShop>
    {
        public CarShopRepository(CarsDbContext ctx) : base(ctx) 
        {
        }

        public override CarShop Read(int id)
        {
            return ctx.Carshop.FirstOrDefault(x => x.ShopId == id);
        }
        public override void Update(CarShop item)
        {
            var old = Read(item.ShopId);
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
