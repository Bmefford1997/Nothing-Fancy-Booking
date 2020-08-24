using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nothing_Fancy.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string reviewerName { get; set; }
        public double reviewRate { get; set; }
        public DateTime reviewDate { get; set; }
        public string reviewContent { get; set; }
    }
}
