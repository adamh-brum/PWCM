using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Models;
using API.DataLogic.Models;
using API.DataLogic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class BeaconController : Controller
    {
        private IDataLogic dataLogic;

        public BeaconController()
        {
            this.dataLogic = new InMemoryDataLogic();
            
            // Add stub data
            Guid beaconId = Guid.Parse("74278BDA-B644-4520-8F0C-720EAF059935");
            this.dataLogic.AddBeacon(beaconId, "33497", "White Beacon", "Reception");
            Guid contentId = this.dataLogic.AddContent("Welcome to @Pheonix", "<div style='color: darkcyan'><p>While you should feel free to enjoy the newly refurbished building, please note <b>contactors are still on site and not all areas are open</b>. For more details, visit the portal page <a href='www.google.com'>Accommodation Help</a></p></div>");
            this.dataLogic.ScheduleContent(contentId, new Guid[1]{beaconId}, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Beacon> Get()
        {
            return this.dataLogic.GetBeacons();
        }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
