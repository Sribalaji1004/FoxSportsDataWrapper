//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoxSportsDataWrapper
{
    using System;
    using System.Collections.Generic;
    
    public partial class GameNote
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int ClientID { get; set; }
        public string Note { get; set; }
        public string NoteColor { get; set; }
        public Nullable<int> TeamID { get; set; }
        public string Header { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool Imported { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> InGameStatID { get; set; }
    }
}
