using DYRQO6_HFT_2022231.Models;
using System.Linq;

namespace DYRQO6_HFT_2022231.Logic
{
    public interface IManagerLogic
    {
        void Create(Manager item);
        void Delete(int id);
        Manager Read(int id);
        IQueryable<Manager> ReadAll();
        void Update(Manager item);
    }
}