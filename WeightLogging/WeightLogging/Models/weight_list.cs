//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeightLogging.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class weight_list
    {
        public short weight_list_id { get; set; }
        public System.DateTime record_date { get; set; }
        public int max_weight { get; set; }
        public int min_weight { get; set; }
    }
}