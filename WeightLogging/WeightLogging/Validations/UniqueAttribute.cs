/* Reference: 
 * https://stackoverflow.com/questions/18781027/regex-camel-case-to-underscore-ignore-first-occurrence
 */

using System;
using System.ComponentModel.DataAnnotations;
using WeightLogging.Models;
using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WeightLogging.Controllers;

namespace BusServiceRequest.Validations
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    sealed public class UniqueAttribute : ValidationAttribute
    {
        public UniqueAttribute()
        {
            this.ErrorMessage = "The date specified has already been used. The date must be unique.";
        }

        protected override ValidationResult IsValid(object date, ValidationContext validationContext)
        {
            if (date == null)
            {
                return null;
            }

            var property = validationContext.ObjectType.GetProperty("weight_list_id");
            var id = short.Parse(property.GetValue(validationContext.ObjectInstance, null).ToString());

            weight_list weight_list = WeightListController.GetWeightRecord(date.ToString());
            if (weight_list != null && weight_list.weight_list_id != id)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return null;
        }
    }
}