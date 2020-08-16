using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nothing_Fancy.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string contact{ get; set; }
        public int roomSize { get; set; }
        public string roomName { get; set; }
    }
}
