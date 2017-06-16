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
        private IScheduleDataLogic dataLogic;

        public ScheduleController()
        {
            this.dataLogic = new SqliteScheduleDataLogic();
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
                Id = c.Id,
                LocationName = content.Location,
                RequestDateTime = requestTime,
                ContentShortDescription = c.Title,
                Content = c.Value
            });
        }
    }
}
