using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nothing_Fancy.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string reserverName { get; set; }
        public string nameOfRoom { get; set; }
        public DateTime reserveDate { get; set; }
        public double cost { get; set; }
    }
}