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
    
    public partial class spTICK_Notes_GetByGroup_Result
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string Note { get; set; }
        public string NoteColor { get; set; }
        public Nullable<int> SportID { get; set; }
        public int TeamID { get; set; }
        public int NoteTeamID { get; set; }
        public int GroupTeamID { get; set; }
        public string Header { get; set; }
        public string NoteHeader { get; set; }
        public string GroupHeader { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> Imported { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public Nullable<int> UserID { get; set; }
        public string TeamAbbreviation { get; set; }
        public string TeamCityName { get; set; }
        public string TeamNickName { get; set; }
        public string TeamPrimaryColor { get; set; }
        public string TeamSecondaryColor { get; set; }
        public string SportDescription { get; set; }
        public string SportShortDisplay { get; set; }
    }
}
