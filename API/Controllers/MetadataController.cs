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
    public class MetadataController : Controller
    {
        private IDataLogic dataLogic;

        public MetadataController()
        {
            this.dataLogic = DataGenerator.GenerateSqliteData();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get(string key)
        {
            return this.dataLogic.GetMetadata(key);
        }

        // POST api/values
        [HttpPost]
        public void Post(string key, string value)
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }

            this.dataLogic.AddMetadata(key, value);
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
