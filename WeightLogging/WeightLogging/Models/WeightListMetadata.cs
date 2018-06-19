using BusServiceRequest.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace WeightLogging.Models
{
    [MetadataType(typeof(MetaData))]
    public partial class weight_list
    {
        public class MetaData
        {
            [Display(Name = "Weight List Id")]
            public short weight_list_id { get; set; }

            [Display(Name = "Date")]
            [Required]
            [Unique]
            [DataType(DataType.DateTime)]
            public DateTime record_date { get; set; }

            [Display(Name = "Max")]
            [Required]
            [RegularExpression("([1-9][0-9]*)")]
            [Range(1, int.MaxValue)]
            public int max_weight { get; set; }

            [Display(Name = "Min")]
            [Required]
            [RegularExpression("([1-9][0-9]*)")]
            [Range(1, int.MaxValue)]
            [LessThan(maxWeightProperty = "max_weight")]
            public int min_weight { get; set; }
        }
    }
}