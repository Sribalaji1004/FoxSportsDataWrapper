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
    
    public partial class spTICK_ScoreAlerts_All_Result
    {
        public int ID { get; set; }
        public Nullable<byte> ScoreAlertID { get; set; }
        public string VisitorsAbbreviation { get; set; }
        public string HomeAbbreviation { get; set; }
        public int VisitorsScore { get; set; }
        public int HomeScore { get; set; }
        public Nullable<int> LastVisitorsScore { get; set; }
        public Nullable<int> LastHomeScore { get; set; }
        public string ScoreDescription { get; set; }
        public Nullable<System.DateTime> ScoreLastUpdated { get; set; }
    }
}
