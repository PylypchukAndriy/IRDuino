using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IRDuino.Models.DeviceHive.HiveClient;
using IRDuino.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace IRDuino.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Device>> Get()
        {
            IHiveClient client = new HiveClient();
            var devices = await client.GetDevices();

            await client.SendDeviceCommand(devices.First().ID);
            return devices;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
