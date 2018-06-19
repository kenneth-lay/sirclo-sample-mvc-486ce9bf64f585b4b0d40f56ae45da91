/* Reference: 
 * https://forums.asp.net/t/1731351.aspx
 */

using System;
using System.ComponentModel.DataAnnotations;
using WeightLogging.Models;
using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WeightLogging.Controllers;
using System.Web;

public sealed class LessThanAttribute : ValidationAttribute
{
    public string maxWeightProperty { get; set; }

    public LessThanAttribute()
    {
        this.ErrorMessage = "The minimum weight should be less than or equal with the maximum weight.";
    }

    public override bool IsValid(object value)
    {
        string maxWeightString = HttpContext.Current.Request[maxWeightProperty];
        if (string.IsNullOrEmpty(maxWeightString) || value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return true;
        }
        int maxWeight = int.Parse(maxWeightString);

        return (maxWeight >= int.Parse(value.ToString()));
    }
}