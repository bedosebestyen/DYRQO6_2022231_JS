using DYRQO6_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Repository
{
    public class CustomerRepository : Repository<Customer>, IRepository<Customer> 
    {
        public CustomerRepository(CarsDbContext ctx) : base(ctx)
        {
        }

        public override Customer Read(int id)
        {
            return ctx.Customer.FirstOrDefault(x => x.CustomerId == id);
        }

        public override void Update(Customer item)
        {
            var old = Read(item.CustomerId);
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
