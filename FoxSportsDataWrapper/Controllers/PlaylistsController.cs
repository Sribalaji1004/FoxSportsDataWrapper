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
    public class PlaylistsController : ApiController
    {
        private FoxTickEntities db = new FoxTickEntities();
        // GET: api/Playlists
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Playlists/5
        public async Task<dynamic> Get(int ClientID)
        {
            var PlaylistList = db.Playlists.Where(b=>b.ClientID == ClientID).Select(a => new { PlaylistID = a.ID, a.ClientID, a.Name }).ToList();
            JsonSerializerSettings jsonsetting = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(PlaylistList, jsonsetting);

        }

        // POST: api/Playlists
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Playlists/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Playlists/5
        public void Delete(int id)
        {
        }
    }
}
