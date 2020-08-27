using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nothing_Fancy.Models
{
    public class Reservation : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string reserverName { get; set; }
        public string nameOfRoom { get; set; }
        public DateTime reserveDateBegin { get; set; }
        public DateTime reserveDateEnd { get; set; }
        public double cost { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (reserveDateEnd < reserveDateBegin)
            {
                yield return new ValidationResult("The end Date must be greater than start Date");
            }
        }
    }
}