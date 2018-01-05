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
    
    public partial class spTICK_Games_Get_Automation_Result
    {
        public int ID { get; set; }
        public long StatsIncID { get; set; }
        public string Matchup { get; set; }
        public int VisitorsID { get; set; }
        public int HomeID { get; set; }
        public Nullable<int> VisitorsRanking { get; set; }
        public Nullable<int> HomeRanking { get; set; }
        public string VisitorsAbbreviation { get; set; }
        public string HomeAbbreviation { get; set; }
        public string VisitorsCityName { get; set; }
        public string HomeCityName { get; set; }
        public string VisitorsNickName { get; set; }
        public string HomeNickName { get; set; }
        public string VisitorsCity { get; set; }
        public string HomeCity { get; set; }
        public int VisitorsScore { get; set; }
        public int HomeScore { get; set; }
        public string VisitorsPrimaryColor { get; set; }
        public string VisitorsSecondaryColor { get; set; }
        public string HomePrimaryColor { get; set; }
        public string HomeSecondaryColor { get; set; }
        public Nullable<int> LastVisitorsScore { get; set; }
        public Nullable<int> LastHomeScore { get; set; }
        public Nullable<byte> ScoreAlertID { get; set; }
        public string Clock { get; set; }
        public Nullable<int> GameStatusID { get; set; }
        public string Status { get; set; }
        public string Network { get; set; }
        public string NetworkDescription { get; set; }
        public int Editable { get; set; }
        public Nullable<System.DateTime> GameDateTime { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string Details { get; set; }
        public string DetailsPostScore { get; set; }
        public string DetailsPreScore { get; set; }
        public string StatusPreScore { get; set; }
        public string SportType { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool BlockData { get; set; }
        public string ScoreDescription { get; set; }
        public int VisitorWins { get; set; }
        public int VisitorLoss { get; set; }
        public int VisitorTie { get; set; }
        public int HomeWins { get; set; }
        public int HomeLoss { get; set; }
        public int HomeTie { get; set; }
    }
}