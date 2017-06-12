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
    public class ContentController : Controller
    {
        private IDataLogic dataLogic;

        public ContentController()
        {
            this.dataLogic = DataGenerator.GenerateData();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ContentModel> Get(string locationId)
        {
            bool parsed = false;
            Guid beaconId = Guid.Empty;
            parsed = Guid.TryParse(locationId, out beaconId);
            if (!parsed)
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

        // POST api/values
        [HttpPost]
        public void Post(string shortDescription, string content, string[] locationIds)
        {
            // Create content
            int contentId = this.dataLogic.AddContent(shortDescription, content);

            // If any location Id's provided
            if (locationIds != null)
            {
                List<Guid> beacons = new List<Guid>();
                foreach (var locationId in locationIds)
                {
                    bool parsed = false;
                    Guid beaconId = Guid.Empty;
                    parsed = Guid.TryParse(locationId, out beaconId);
                    if (parsed)
                    {
                        beacons.Add(beaconId);
                    }
                    else
                    {
                        // Add validation error
                    }
                }

                this.dataLogic.ScheduleContent(contentId, beacons.ToArray(), DateTime.Now, DateTime.Now.AddDays(5));
            }
        }

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
