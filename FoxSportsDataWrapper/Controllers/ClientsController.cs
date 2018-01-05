using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;

namespace FoxSportsDataWrapper.Controllers
{
    [Authorize]
    public class ClientsController : ApiController
    {
        private FoxTickEntities db = new FoxTickEntities();

        // GET: api/Clients
        public async Task<dynamic> Get()
        {
            var Clientslist = db.Clients.Where(a=>a.DataHiveEnabled == true).Select(n => new { n.ID, n.Abbreviation, n.Description }).ToList();           
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(Clientslist, serializerSettings);
        }

        // GET: api/Clients/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Clients
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clients/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clients/5
        public void Delete(int id)
        {
        }
    }
}
