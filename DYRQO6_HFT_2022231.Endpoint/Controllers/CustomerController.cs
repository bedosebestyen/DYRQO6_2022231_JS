using DYRQO6_HFT_2022231.Endpoint.Services;
using DYRQO6_HFT_2022231.Logic;
using DYRQO6_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DYRQO6_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerLogic logic;
        IHubContext<SignalRHub> hub;
        public CustomerController(ICustomerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Customer> ReadAll()
        {
            return this.logic.ReadAll();
        }

        
        [HttpGet("{id}")]
        public Customer Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Customer value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CustomerCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Customer value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CustomerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CustomerDeleted", customerToDelete);
        }
    }
}
