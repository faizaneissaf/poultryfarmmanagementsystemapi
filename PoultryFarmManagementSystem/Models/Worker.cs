//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PoultryFarmManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Worker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Worker()
        {
            this.BroilerBatches = new HashSet<BroilerBatch>();
            this.LayerBatches = new HashSet<LayerBatch>();
            this.Poultries = new HashSet<Poultry>();
        }
    
        public int worker_id { get; set; }
        public string worker_name { get; set; }
        public string worker_email { get; set; }
        public string worker_address { get; set; }
        public string worker_phoneno { get; set; }
        public string worker_password { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> pltry_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BroilerBatch> BroilerBatches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LayerBatch> LayerBatches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Poultry> Poultries { get; set; }
        public virtual Poultry Poultry { get; set; }
        public virtual User User { get; set; }
    }
}
