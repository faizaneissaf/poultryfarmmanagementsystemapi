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
    
    public partial class Sale
    {
        public int sale_id { get; set; }
        public Nullable<int> bb_id { get; set; }
        public Nullable<int> lb_id { get; set; }
        public string b_name { get; set; }
        public Nullable<int> bb_totalqty { get; set; }
        public Nullable<int> bb_avgbodyweight { get; set; }
        public Nullable<int> lb_totaleggsintrays { get; set; }
        public Nullable<int> pltry_id { get; set; }
        public string c_name { get; set; }
        public string c_address { get; set; }
        public Nullable<int> c_qty { get; set; }
        public Nullable<int> s_status { get; set; }
        public Nullable<int> bb_priceperchick { get; set; }
        public Nullable<int> lb_pricepertryofeggs { get; set; }
    }
}