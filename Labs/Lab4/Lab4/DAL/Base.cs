using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DAL
{
    internal class Base
    {
        public int Id { get; set; }
        [Range()]
        public string Description { get; set; }
        
    }
}
