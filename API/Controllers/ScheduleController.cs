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
    public class ScheduleController : Controller
    {
        private IDataLogic dataLogic;

        public ScheduleController()
        {
            this.dataLogic = DataGenerator.GenerateSqliteData();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ContentModel> Get(string locationId)
        {
            bool parsed = false;
            Guid beaconId = Guid.Empty;
            parsed = Guid.TryParse(locationId, out beaconId);
            if(!parsed)
            {
                return null;
            }

            DateTime requestTime = DateTime.Now;
            var content = this.dataLogic.GetScheduledContent(beaconId, requestTime);
            return content.Content.Select(c => new ContentModel()
            {
                LocationName = content.Location,
                RequestDateTime = requestTime,
                ContentShortDescription = c.Title,
                Content = c.Value
            });
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
