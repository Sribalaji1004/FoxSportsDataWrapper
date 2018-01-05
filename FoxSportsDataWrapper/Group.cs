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
    
    public partial class Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            this.GroupGames = new HashSet<GroupGame>();
            this.Notes = new HashSet<Note>();
        }
    
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string OnAirName { get; set; }
        public string CreatedName { get; set; }
        public string Type { get; set; }
        public Nullable<int> TeamID { get; set; }
        public string Header { get; set; }
        public int GroupAnimationTypeID { get; set; }
        public Nullable<bool> Editable { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string SDMTeamCode { get; set; }
        public string SDMLeagueCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupGame> GroupGames { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }
    }
}