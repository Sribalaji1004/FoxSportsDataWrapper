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
    public class GamesController : ApiController
    {
        public class Schedules_Obj
        {
            public string League { get; set; }

            public string DateTimeUtc { get; set; }

            public VisitingTeam VisitingTeam { get; set; }

            public HomeTeam HomeTeam { get; set; }

            public int DoubleHeaderGameNumber { get; set; }
        }


        public class Results_obj
        {
            public string League { get; set; }

            public string DateTimeUtc { get; set; }

            public string VisitorTeamAlias { get; set; }

            public string HomeTeamAlias { get; set; }

            public int DoubleHeaderGameNumber { get; set; }

        }


        public class psb
        {
            public Data data { get; set; }

        }

        public class Data
        {
            public string HomeTeamStatsDisplay { get; set; }

            public string VisitorTeamStatsDisplay { get; set; }

            public string HomeTeamHeader { get; set; }

            public string VisitorTeamHeader { get; set; }
        }

        public class VisitingTeam
        {
            public int Id { get; set; }
            public string Alias { get; set; }
            public string City { get; set; }
            public string Name { get; set; }
        }

        public class HomeTeam
        {
            public int Id { get; set; }
            public string Alias { get; set; }
            public string City { get; set; }
            public string Name { get; set; }
        }

        private FoxTickEntities db = new FoxTickEntities();
        // GET: api/Games
        [AcceptVerbs("GET")]
        [HttpGet]
        public async Task<dynamic> Get(int ClientID, string League,string Today)
        {
            HttpClient results_client = new HttpClient();
            HttpResponseMessage results_response = await results_client.GetAsync(ConfigurationSettings.AppSettings["DataHiveUrl"] + @"results/" + "/" + ClientID + "/" + League + "/" + Today);
            //HttpResponseMessage results_response = await results_client.GetAsync("https://jsonplaceholder.typicode.com/" + @"users");

            int doubleheader;
            var FinalResult = " ";
            int[] empty_array = new int[0];

            if (results_response.IsSuccessStatusCode)
            {
                string results_responseBody = await results_response.Content.ReadAsStringAsync();

                var objs = JsonConvert.DeserializeObject<Results_obj>(results_responseBody);
                var datetime = objs.DateTimeUtc;
                var timeUtc = Convert.ToDateTime(datetime); ;
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

                string date = easternTime.ToString("yyyyMMdd");

                var visit = objs.VisitorTeamAlias == null ? "null" : objs.VisitorTeamAlias;
                var home = objs.HomeTeamAlias == null ? "null" : objs.HomeTeamAlias;
                var leagues = objs.League;
                //objs[0].DoubleHeaderGameNumber;
                if (League.ToUpper() == "MLB")
                {
                    doubleheader = objs.DoubleHeaderGameNumber;
                    FinalResult = date + "_" + leagues + "_" + home + "_" + visit + "_" + doubleheader;
                }
                else
                {

                    FinalResult = date + "_" + leagues + "_" + home + "_" + visit;
                }



            }
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(empty_array, serializerSettings);

        }

        // GET: api/Games/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Games
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Games/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Games/5
        public void Delete(int id)
        {
        }
    }
}
