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
    
    public partial class Playlist
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<bool> Locked { get; set; }
        public Nullable<bool> Staged { get; set; }
        public Nullable<byte> PlaylistTypeID { get; set; }
        public Nullable<int> ScoreboardID { get; set; }
    }
}
