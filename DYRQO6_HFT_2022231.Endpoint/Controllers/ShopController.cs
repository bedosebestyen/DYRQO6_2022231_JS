using DYRQO6_HFT_2022231.Logic;
using DYRQO6_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DYRQO6_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        ICarShopLogic logic;

        public ShopController(ICarShopLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<CarShop> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        public CarShop Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] CarShop value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] CarShop value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
