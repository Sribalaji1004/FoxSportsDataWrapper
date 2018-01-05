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
{   [Authorize]
    public class PlaylistGroupsController : ApiController
    {
        private FoxTickEntities db = new FoxTickEntities();

        public class PlaylistGroup
        {
            public Boolean Enabled { get; set; }
            public int EntryTypeID { get; set; }
            public int PlaylistGroupID { get; set; }
            public string Name { get; set; }
            public string TopicText { get; set; }
            public string TopicImage{get; set;}
            public string TopicColor { get; set; }
            public DateTime LastUpdated { get; set; }
            
        }

        public class ResultsGame
        {
            public string VisitorTeamAlias { get; set; }

            public string HomeTeamAlias { get; set; }

            public string Name { get; set; }

            public string Race { get; set; }

        }

        public class ScheduleGame
        {

            public VisitingTeam VisitingTeam { get; set; }

            public HomeTeam HomeTeam { get; set; }

            public string Race { get; set; }

            public string Name { get; set; }

        }

        public class CheckSportType
        {
            public string SportType { get; set; }


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

        public class PlaylistsGroup
        {
            public Boolean Enabled { get; set; }
            public List<Info> Info { get; set; }
            public PlaylistsGroup() { Info = new List<Info>(); }

        }

        public class Info
        {
            public Boolean Enabled { get; set; }
            public int GroupNoteID { get; set; }
            public GroupNote Notes { get; set; }
            public Info() { Notes = new GroupNote(); } 
        }

        public class GroupNote
        {
            public int ID { get; set; }
            public string HeaderImage { get; set; }
            public string HeaderText { get; set; }
            public string HeaderTextBGColor { get; set; }
            public string NoteText { get; set; }
        }
        // GET: api/PlaylistGroups
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PlaylistGroups/5
        public async Task<dynamic> Get(int ClientID,int PlaylistID)
        {
            var PlaylistDetails = db.PlaylistDetails.Where(a => a.PlaylistID == PlaylistID).ToList();
            List<PlaylistGroup> PG = new List<PlaylistGroup>();


            foreach (PlaylistDetail a in PlaylistDetails)
            {
                PlaylistGroup PGadding = new PlaylistGroup();
                
                    PGadding.Enabled = true;
                    PGadding.EntryTypeID = a.EntryTypeID;
                    PGadding.PlaylistGroupID = a.ID;                  
                    PGadding.Name = await getAliasName(a.HiveID, a.SDMLeagueCode,a.EntryTypeID,a.EntryID, Convert.ToInt32(a.NotesTypeID),a.OnAirName,a.Specifics1);
                if (a.EntryTypeID == 1 || a.EntryTypeID == 9)
                {
                    PGadding.TopicText = db.Groups.Where(b => b.ID == a.EntryID).Select(b => b.OnAirName).FirstOrDefault();
                }
                else
                {
                    PGadding.TopicText = a.OnAirName;
                }
                    PGadding.TopicImage = null;
                    PGadding.TopicColor = null;
                PG.Add(PGadding);
            }

            JsonSerializerSettings jsetting = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(PG, jsetting);
        }

        public async Task<string> getAliasName(string HiveId,string League,int EntryTypeID,int EntryId,int NotesTypeId,string onairname,string datetime)
        {
             if ((int)Enums.EntryType.Group == EntryTypeID) //1
            {
                string Group = db.Groups.Where(a=>a.ID == EntryId).Select(b=>b.Name).FirstOrDefault();
                return Group;
            }
            else if((int)Enums.EntryType.HiveGame == EntryTypeID) //6
            {
                //Finding the SportsType for the League *** Starts
                var sporttypename = " ";
                HttpClient SportsResults_client = new HttpClient();
                HttpResponseMessage SportsResults_response = await SportsResults_client.GetAsync(ConfigurationSettings.AppSettings["SDMUrl"] + @"/api/topic/" + League);
                if (SportsResults_response.IsSuccessStatusCode)
                {

                    string SportsType_responsebody = await SportsResults_response.Content.ReadAsStringAsync();
                    var SportsTypeObjs = JsonConvert.DeserializeObject<CheckSportType>(SportsType_responsebody);
                    sporttypename = SportsTypeObjs.SportType;

                }
                //Finding the SportsType for the League *** Ends
                HttpClient results_client = new HttpClient();
                HttpResponseMessage results_response = await results_client.GetAsync(ConfigurationSettings.AppSettings["DataHiveUrl"] + @"results/" + League + "/" + HiveId);
                var FinalResult = " ";
                if (results_response.IsSuccessStatusCode)
                {

                    string results_responseBody = await results_response.Content.ReadAsStringAsync();
                    var objs = JsonConvert.DeserializeObject<ResultsGame>(results_responseBody);
                    if (objs.Name != "undefined")
                    {
                        if (NotesTypeId == 3 && sporttypename.ToUpper() == "TENNIS")
                        {

                            FinalResult = datetime + " - " + League + " - " + objs.Name;

                        }
                        else
                        {

                            FinalResult = objs.Name;

                        }
                    }            
                    else if (objs.Race != "undefined")
                    {
                        FinalResult = objs.Race;
                    }
                    else
                    {
                        var visit = objs.VisitorTeamAlias == null ? "null" : objs.VisitorTeamAlias;
                        var home = objs.HomeTeamAlias == null ? "null" : objs.HomeTeamAlias;

                        FinalResult = visit + " @ " + home;
                    }
                    return FinalResult;
                }
                else
                {
                    HttpClient schedules_client = new HttpClient();
                    HttpResponseMessage schedules_response = await schedules_client.GetAsync(ConfigurationSettings.AppSettings["DataHiveUrl"] + @"Schedules/" + League + "/" + HiveId);
                    //response.EnsureSuccessStatusCode();
                    if (schedules_response.IsSuccessStatusCode)
                    {
                        string schedules_responseBody = await schedules_response.Content.ReadAsStringAsync();
                        var secobjs = JsonConvert.DeserializeObject<ScheduleGame>(schedules_responseBody);
                        if (secobjs.Name != "undefined")
                        {
                            if (NotesTypeId == 3 && sporttypename.ToUpper() == "TENNIS")
                            {

                                FinalResult = datetime + " - " + League + " - " + secobjs.Name;

                            }
                            else
                            {

                                FinalResult = secobjs.Name;

                            }
                        }
                        else if (secobjs.Race != "undefined")
                        {
                            FinalResult = secobjs.Race;
                        }
                        else
                        {
                            var visitname = secobjs.VisitingTeam.Alias == null ? "null" : secobjs.VisitingTeam.Alias;
                            var homename = secobjs.HomeTeam.Alias == null ? "null" : secobjs.HomeTeam.Alias;

                            FinalResult = visitname + " @ " + homename;
                        }



                        return FinalResult;
                    }


                }


            }
            else if ((int)Enums.EntryType.HiveTodaysGames == EntryTypeID) //7
            {
                var HiveTodaysGames = "Today's " +League;
                return HiveTodaysGames;
            }
            else if ((int)Enums.EntryType.HivePriorGames == EntryTypeID) //8
            {
                var HivePriorGames = "Previous " + League;
                return HivePriorGames;
            }
            else if ((int)Enums.EntryType.GroupOfGames == EntryTypeID) //9
            {
                var GroupOfGames = db.Groups.Where(a => a.ID == EntryId).Select(b => b.Name).FirstOrDefault();
                return GroupOfGames;
            }
            else if ((int)Enums.EntryType.GamesScheduleCurrent == EntryTypeID) //13
            {
                if ((int)Enums.EntryType.GamesScheduleCurrent == EntryTypeID && NotesTypeId == 2)
                {
                    var GamesScheduleCurrent = "Current Week's "+League+" - Quick Rip";
                    return GamesScheduleCurrent;
                }
                else
                {
                    var GamesScheduleCurrent = "Current Week's " +League ;
                    return GamesScheduleCurrent;
                }
            }
            else if ((int)Enums.EntryType.HiveTodaysGamesInProgressQuickRip == EntryTypeID) //14
            {
                if ((int)Enums.EntryType.HiveTodaysGamesInProgressQuickRip == EntryTypeID && NotesTypeId == 2)
                {
                    var HiveTodaysGamesInProgressQuickRip = "Current " + League + "Scores - Quick Rip";
                    return HiveTodaysGamesInProgressQuickRip;
                }
                else
                {
                    var HiveTodaysGamesInProgressQuickRip = "Current Scores - Quick Rip";
                    return HiveTodaysGamesInProgressQuickRip;
                }
            }
            else if ((int)Enums.EntryType.HivePreviousGamesFinalsQuickRip == EntryTypeID) //17
            {
                if ((int)Enums.EntryType.HivePreviousGamesFinalsQuickRip == EntryTypeID && (NotesTypeId == 1 || NotesTypeId == 2) )
                {
                    var HivePreviousGamesFinalsQuickRip = "Previous "+League+" Scores - Quick Rip";
                    return HivePreviousGamesFinalsQuickRip;
                }
                else
                {
                    var HivePreviousGamesFinalsQuickRip = "Previous  Scores - Quick Rip";
                    return HivePreviousGamesFinalsQuickRip;
                }
            }
            else if ((int)Enums.EntryType.APRankings == EntryTypeID) //20
            {
                var APRankings = "AP Pools for " + League;
                return APRankings;
            }
            else if ((int)Enums.EntryType.Top25 == EntryTypeID) //22
            {
                if ((int)Enums.EntryType.Top25 == EntryTypeID && NotesTypeId == 1)
                {
                    var Top25 = datetime + " - " + League + " - Top 25";
                    return Top25;
                }
                else if ((int)Enums.EntryType.Top25 == EntryTypeID && NotesTypeId == 2)
                {
                    var Top25 = "Top 25 " + League + " - Quick Rip";
                    return Top25;
                }
                else
                {
                    var Top25 = "Top 25 " + League ;
                    return Top25;
                }
            }
            else if ((int)Enums.EntryType.CFPRankings == EntryTypeID) //24
            {
                var CFPRank = "CFP Rankings" + League;
                return CFPRank;
            }
            else if ((int)Enums.EntryType.Top25GamesToday == EntryTypeID) //25
            {
                var Today25 = "Top 25 - Today" + League;
                return Today25;
            }
            else if ((int)Enums.EntryType.Top25GamesPrevious == EntryTypeID) //26
            {
                var Previous25 = "Top 25 - Previous" + League;
                return Previous25;
            }
            else if ((int)Enums.EntryType.GameStats == EntryTypeID) //27
            {
                var GameStats = "GameStats " + onairname;
                return GameStats;
            }
            else if ((int)Enums.EntryType.WeeklyLeadersPassing == EntryTypeID) //28
            {
                var WeeklyLeadersPassing = "Top " +EntryId+ "Weekly Leaders - Passing " +League;
                return WeeklyLeadersPassing;
            }
            else if ((int)Enums.EntryType.WeeklyLeadersRushing == EntryTypeID) //29
            {
                var WeeklyLeadersRushing = "Top " + EntryId + "Weekly Leaders - Rushing "+League;
                return WeeklyLeadersRushing;
            }
            else if ((int)Enums.EntryType.WeeklyLeadersRcvng == EntryTypeID) //30
            {
                var WeeklyLeadersRcvng = "Top " + EntryId + "Weekly Leaders - Receiving " +League ;
                return WeeklyLeadersRcvng;
            }
            else if ((int)Enums.EntryType.SpecificDate == EntryTypeID) //31
            {
                if ((int)Enums.EntryType.SpecificDate == EntryTypeID && NotesTypeId == 1)
                {
                    var SpecificDate = datetime+ " -" +League+ " - All Games";
                    return SpecificDate;
                }
                else if((int)Enums.EntryType.SpecificDate == EntryTypeID && NotesTypeId == 2)
                {
                    var SpecificDate = datetime + " -" + League + " - All Games Quick Rip";
                    return SpecificDate;
                }
            }
            else if ((int)Enums.EntryType.TodaysGamesQuickPlayout == EntryTypeID) //32
            {
                if ((int)Enums.EntryType.TodaysGamesQuickPlayout == EntryTypeID && NotesTypeId == 2)
                {
                    var TodaysGamesQuickPlayout = League + " -Today - Quick Playout";
                    return TodaysGamesQuickPlayout;
                }
                else
                {
                    var TodaysGamesQuickPlayout = League + " -Today - Quick Playout w/Notes";
                    return TodaysGamesQuickPlayout;
                }
            }
            else if ((int)Enums.EntryType.GamesScheduleCurrentQuickRip == EntryTypeID) //33
            {
                var GamesScheduleCurrentQuickRip = "Current Week's " +League+" - Quick Rip ";
                return GamesScheduleCurrentQuickRip;
            }
            else if ((int)Enums.EntryType.GamesScheduleCurrentQuickPlayoutWithNotes == EntryTypeID) //34
            {
                var GamesScheduleCurrentQuickPlayoutWithNotes = "Current Week's "+League+" - Quick Playout w/Notes ";
                return GamesScheduleCurrentQuickPlayoutWithNotes;
            }
            else if ((int)Enums.EntryType.Top25GamesQuickPlayoutWithNotes == EntryTypeID) //35
            {
                var Top25GamesQuickPlayoutWithNotes = "Top 25 "+League+" - Quick Playout w/Notes";
                return Top25GamesQuickPlayoutWithNotes;
            }



            return " ";

        }

        public async Task<dynamic> Get(int ClientID,int PlaylistID,int PlaylistGroupID) {

            PlaylistsGroup PlaylistGroup = new PlaylistsGroup();
            var infolist = db.Notes.Where(a => a.GroupID == PlaylistGroupID).ToList();
            foreach (var a in infolist)
            {
                //PlaylistGroup.Info.Add(new Info() { Enabled = true, GroupNoteID = a.ID, Notes = new GroupNote() { HeaderImage = null, HeaderText = null, HeaderTextBGColor =null, NoteText = a.Note1 }  });
                PlaylistGroup.Info.Add(new Info() { Enabled = true, GroupNoteID = a.ID});
            }
            PlaylistGroup.Enabled = true;
            return Json(PlaylistGroup);

        }

        // POST: api/PlaylistGroups
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PlaylistGroups/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PlaylistGroups/5
        public void Delete(int id)
        {
        }
    }
}
