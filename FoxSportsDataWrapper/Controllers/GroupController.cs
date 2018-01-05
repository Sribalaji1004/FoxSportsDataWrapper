using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace FoxSportsDataWrapper.Controllers
{
    [Authorize]
    public class GroupController : ApiController
    {
        public class SingleGroupNote {

            public bool Enabled { get; set; }
            public int ID { get; set; }
            public string HeaderImage { get; set; }
            public string HeaderText { get; set; }
            public string HeaderTextBGColor { get; set; }
            public string NoteText { get; set; }

        }

        public class ListOfGroupNotes {
            public List<SingleGroupNotes> Note { get; set; }
            public ListOfGroupNotes() { Note = new List<SingleGroupNotes>(); }
        }

        public class SingleGroupNotes {
            public int ID { get; set; }
            public string HeaderImage { get; set; }
            public string HeaderText { get; set; }
            public string HeaderTextBGColor { get; set; }
            public string NoteText { get; set; }
        }

        private FoxTickEntities db = new FoxTickEntities();
        // GET: api/Group
        [Route("api/Ticker/v1/Notes/Group")]
        public async Task<dynamic> Get(int ClientID, int PlaylistID, int PlaylistGroupID)
        {
            var CheckClientAndPlaylistID = db.Playlists.Where(a => a.ClientID == ClientID && a.ID == PlaylistID).FirstOrDefault();
            if (CheckClientAndPlaylistID != null)
            {
                ListOfGroupNotes Lists = new ListOfGroupNotes();
                var FindEntryID = db.PlaylistDetails.Where(ab => ab.PlaylistID == PlaylistID && ab.ID == PlaylistGroupID).FirstOrDefault();
                if (FindEntryID != null)
                {

                    var GetGroupNotes = db.Notes.Where(gg => gg.GroupID == FindEntryID.EntryID).ToList();
                    List<SingleGroupNotes> Single = new List<SingleGroupNotes>();

                    foreach (var a in GetGroupNotes)
                    {
                        Single.Add(new SingleGroupNotes { ID = a.ID, NoteText = a.Note1 });
                    }


                    Lists.Note = Single;
                }

                return Json(Lists);
            }
            return new int[0];
        }

        // GET: api/Group/5
        [HttpGet]
        public async Task<dynamic> Get(int NotesID, int ClientID, int PlaylistID, int PlaylistGroupID)
        {
            SingleGroupNote SingleNote = new SingleGroupNote();
            var SingleGroupNote = db.Notes.Where(a => a.ID == NotesID).FirstOrDefault();
            SingleNote.Enabled = true;
            SingleNote.ID = SingleGroupNote.ID;
            SingleNote.HeaderImage = null;
            SingleNote.HeaderText = null;
            SingleNote.HeaderTextBGColor = null;
            SingleNote.NoteText = SingleGroupNote.Note1;
            JsonSerializerSettings JsonSetting = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(SingleNote, JsonSetting);

        }

        // POST: api/Group
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Group/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Group/5
        public void Delete(int id)
        {
        }
    }
}
