using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DAL
{
    public class Base
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string BaseName { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        public decimal RoomPrice { get; set; }
        public double SeaDistance { get; set; }
    }
}
