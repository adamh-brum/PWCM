using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<ContentModel> Get(string locationId)
        {
            switch(locationId)
            {
                case "1":
                {
                    return new ContentModel[] 
                    { 
                        new ContentModel()
                        {
                            LocationName = "West Midlands, UK",
                            RequestDateTime = DateTime.Now,
                            ContentShortDescription = "Notification 1",
                            Content = "<h1>BOO</H1>"
                        }
                    };
                }
                default:
                {
                    // No content
                    return new ContentModel[0];
                }
            }
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
