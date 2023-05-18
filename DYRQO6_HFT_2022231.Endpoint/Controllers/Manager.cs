﻿using DYRQO6_HFT_2022231.Endpoint.Services;
using DYRQO6_HFT_2022231.Logic;
using DYRQO6_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DYRQO6_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        IManagerLogic logic;
        IHubContext<SignalRHub> hub;
        public ManagerController(IManagerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Manager> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        public Manager Read(int id)
        {
            return this.logic.Read(id);

        }

        [HttpPost]
        public void Create([FromBody] Manager value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("ManagerCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Manager value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ManagerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var managerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ManagerDeleted", managerToDelete);
        }
    }
}
