﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoxSportsDataWrapper.Controllers
{
    [Authorize]
    public class NotesController : ApiController
    {
        public class SingleGameNotes
        {
            public int ID { get; set; }
            public string HeaderImage { get; set; }
            public string HeaderText { get; set; }
            public string HeaderTextBGColor { get; set; }
            public string NoteText { get; set; }

        }


        public class CIDGames {

            public string ID{ get; set; }
            public List<SingleNotes> Notes { get; set; }
            public CIDGames() { Notes = new List<SingleNotes>(); }


        }


        public class SingleNotes {
            public int ID { get; set; }
            public string HeaderImage { get; set; }
            public string HeaderText { get; set; }
            public string HeaderTextBGColor { get; set; }
            public string NoteText { get; set; }
        }

        private FoxTickEntities db = new FoxTickEntities();
        // GET: api/Notes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Notes/5
        [Route("api/Ticker/V1/Notes/Game")]
        public async Task<dynamic> Get(string ID)
        {
            string[] values = ID.Split(',');
            List<CIDGames> NumberofNotes = new List<CIDGames>();
            foreach (string cid in values) {
                CIDGames SingleCID = new CIDGames();
                SingleCID.ID = cid;
                var GameDetailsFromCID = db.GameHiveNotes.Where(a => a.HiveID == cid).Select(b => b.GameDetails).FirstOrDefault();
                if (GameDetailsFromCID != null)
                {
                    var NumberOfNotes = db.GameHiveNotes.Where(cd => cd.GameDetails == GameDetailsFromCID).ToList();
                    foreach(GameHiveNote gn in NumberOfNotes)
                    {
                        SingleNotes ListOfNotes = new SingleNotes();
                        ListOfNotes.ID = gn.ID;
                        ListOfNotes.HeaderImage = null;
                        ListOfNotes.HeaderText = null;
                        ListOfNotes.HeaderTextBGColor = null;
                        ListOfNotes.NoteText = gn.Note;

                        SingleCID.Notes.Add(ListOfNotes);
                    }


                }

                NumberofNotes.Add(SingleCID);
            }
            return Json(NumberofNotes);
        }

        // POST: api/Notes
        public async Task<dynamic> Get(int ID, int ClientID, int PlaylistID, int PlaylistGroupID)
        {
            int[] empty_array = new int[0];
            SingleGameNotes SingleGame = new SingleGameNotes();
            var SingleNote = db.GameHiveNotes.Where(ab => ab.ID == ID).FirstOrDefault();
            if (SingleNote != null)
            {
                SingleGame.ID = SingleNote.ID;
                SingleGame.HeaderImage = null;
                SingleGame.HeaderText = null;
                SingleGame.HeaderTextBGColor = null;
                SingleGame.NoteText = SingleNote.Note;
                return Json(SingleGame);
            }
            else {
                
                return Json(empty_array);
            }
        }

        // PUT: api/Notes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Notes/5
        public void Delete(int id)
        {
        }
    }
}
