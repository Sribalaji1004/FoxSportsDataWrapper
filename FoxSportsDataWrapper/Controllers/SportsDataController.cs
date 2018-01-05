using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.Collections.Specialized;
using static FoxSportsDataWrapper.Controllers.GamesController;

namespace FoxSportsDataWrapper.Controllers
{    [Authorize]
    public class GameEvents
    {
        public string SportsType { get; set; }
        public string LeagueCode { get; set; }
        public List<Events> Events { get; set; }
        public GameEvents() { Events = new List<Events>();}

    }

    public class Events
    {
        public string ID { get; set; }
        public GameData EventData { get; set; }


    }

    public class GameData
    {
        public int Quarter { get; set; } 
        public TimeSpan Clock { get; set; }
        public string VisitorTeamCity { get; set; }
        public string VisitorTeamAlias { get; set; }
        public string VisitorTeamName { get; set; }
        public int VisitorScore { get; set; }
        public int  VisitorWins { get; set; }
        public int VisitorLosses { get; set; }
        public int VisitorTies { get; set; }
        public string HomeTeamCity { get; set; }
        public string HomeTeamAlias { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeScore { get; set; }
        public int HomeWins { get; set; }
        public int HomeLosses { get; set; }
        public int HomeTies { get; set; }
        public string Status { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string League { get; set; }
        public int Season { get; set; }
        public string CorrelationId { get; set; }
        public string SourceType { get; set; }
        public int DoubleHeaderGameNumber { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public List<Notes> Notes { get; set; }
        public GameData() { Notes = new List<Notes>(); }

    }

    public class Notes
    {

        public bool Enabled { get; set; }
        public int ID { get; set; }
        public string HeaderImage { get; set; }
        public string HeaderText { get; set; }
        public string HeaderTextBGColor { get; set; }
        public string NoteText { get; set; }

    }

    public class Overrides_main
    {
        public Model Model { get; set; }

    }

    public class Model
    {
        public int VisitorScore { get; set; }
        public int VisitorWins { get; set; }
        public int VisitorLosses { get; set; }
        public int VisitorTies { get; set; }
        public int HomeScore { get; set; }
        public int HomeWins { get; set; }
        public int HomeLosses { get; set; }
        public int HomeTies { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string Status { get; set; }
        public TimeSpan Clock { get; set; }
        public int Quarter { get; set; }
    }


    public class SportsDataController : ApiController
    {
        private FoxTickEntities db = new FoxTickEntities(); 
        [HttpGet]
        public async Task<dynamic> Get(string Day, string SportType, string LeagueCode,int ClientID)
        {
            HttpClient results_client = new HttpClient();
            HttpResponseMessage results_response = await results_client.GetAsync(ConfigurationSettings.AppSettings["DataHiveUrl"] + @"results/" + LeagueCode + "/" + Day);
            if (results_response.IsSuccessStatusCode)
            {
                GameEvents Gameevent = new GameEvents();
                Gameevent.SportsType = SportType;
                Gameevent.LeagueCode = LeagueCode;
                string results_responseBody = await results_response.Content.ReadAsStringAsync();
                var objs = JsonConvert.DeserializeObject<List<GameData>>(results_responseBody);

                foreach (GameData a in objs)
                {
                    var hiveId = a.CorrelationId;
                    var league = a.League;
                    HttpClient GameOver_Rides = new HttpClient();
                    HttpResponseMessage GameOver_RidesResponse = await GameOver_Rides.GetAsync(ConfigurationSettings.AppSettings["DataHiveUrl"] + @"results/" + league + "/overrides?id=" + hiveId);
                    if (GameOver_RidesResponse.IsSuccessStatusCode)
                    {
                        var Over = JsonConvert.DeserializeObject<Overrides_main>(results_responseBody);
                        a.Clock = Over.Model.Clock;
                        a.DateTimeUtc = Over.Model.DateTimeUtc;
                        a.HomeWins = Over.Model.HomeWins;
                        a.HomeTies = Over.Model.HomeTies;
                        a.HomeLosses = Over.Model.HomeLosses;
                        a.VisitorWins = Over.Model.VisitorWins;
                        a.VisitorTies = Over.Model.VisitorTies;
                        a.VisitorLosses = Over.Model.VisitorLosses;
                        a.HomeScore = Over.Model.HomeScore;
                        a.VisitorScore = Over.Model.VisitorScore;
                        a.Status = Over.Model.Status;
                        a.Quarter = Over.Model.Quarter;
                    }

                    /// For Getting the Game details based on CorrelationID from DataHive URL ***Start
                    //HttpClient GameDetails_Client = new HttpClient();
                    //HttpResponseMessage GameDetails_ClientResponse = await GameDetails_Client.GetAsync(ConfigurationSettings.AppSettings["DataHiveUrl"] + @"results/" + a.League + "/" + a.CorrelationId);

                    int doubleheader;
                    var FinalResult = " ";
                    //int[] empty_array = new int[0];

                    //if (GameDetails_ClientResponse.IsSuccessStatusCode)
                    //{
                    //    string GameDetails_ClientResponseBody = await GameDetails_ClientResponse.Content.ReadAsStringAsync();

                    //    var ResultObjs = JsonConvert.DeserializeObject<Results_obj>(GameDetails_ClientResponseBody);
                    var datetime = a.DateTimeUtc;
                    var timeUtc = Convert.ToDateTime(datetime); ;
                    TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

                    string date = easternTime.ToString("yyyyMMdd");

                    var visit = a.VisitorTeamAlias == null ? "null" : a.VisitorTeamAlias;
                    var home = a.HomeTeamAlias == null ? "null" : a.HomeTeamAlias;
                    var leagues = a.League;
                    if (a.League.ToUpper() == "MLB")
                    {
                        doubleheader = a.DoubleHeaderGameNumber;
                        FinalResult = date + "_" + leagues + "_" + home + "_" + visit + "_" + doubleheader;
                    }
                    else
                    {

                        FinalResult = date + "_" + leagues + "_" + home + "_" + visit;
                    }
                    //}
                    //Utilities.LoadClientShares(db, ClientID);
                    //List<GameHiveNote> lstGameHiveNote = new List<GameHiveNote>();
                    //foreach (Client c in db.Clients.Single(s => s.ID == ClientID).Clients) lstGameHiveNote = lstGameHiveNote.Concat(db.GameHiveNotes.Where(gw => gw.GameDetails == FinalResult && gw.ClientID == c.ID)).ToList();
                    var GameNotesList = db.GameHiveNotes.Where(ab => ab.GameDetails == FinalResult && ab.ClientID == ClientID).ToList();
                    foreach (GameHiveNote abc in GameNotesList)
                    {
                        a.Notes.Add(new Notes { Enabled = true, HeaderImage = null, HeaderText = null, HeaderTextBGColor = null, ID = abc.ID, NoteText = abc.Note });
                    }
                    /// For Getting the Game details based on CorrelationID from DataHive URL ***END


                    Gameevent.Events.Add(new Events { ID= hiveId, EventData = a});
                    
                }
                JsonSerializerSettings JsonSetting = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(Gameevent, JsonSetting);

            }
            return " ";
        }
        [HttpGet]
        public async Task<dynamic> Get(string Day, string SportType, string LeagueCode,DateTime StartUtc,DateTime EndUtc)
        {

            //string fullname1 = Request["fullname"];
            return "Date";

        }


        [HttpGet]
        [Route("api/SportsData/v1/Events/{SportType}/{LeagueCode}/{GameID}")]
        public async Task<dynamic> Get(string SportType, string LeagueCode, int GameID,int ClientID,int PlaylistID,int PlaylistGroupID)
        {

            //string fullname1 = Request["fullname"];
            return "SingleGame";

        }


        //public static void LoadClientShares(FoxTickEntities db, int cid)
        //{
        //    db.Clients.Single(s => s.ID == cid).Clients.Load();
        //}
    }
}
