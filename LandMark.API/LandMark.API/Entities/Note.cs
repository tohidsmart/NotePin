using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LandMark.API.Entities
{

    /// <summary>
    /// Entity class representing a single note. This class is used by entity framework to create a SQL server table
    /// </summary>
    public class Note
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string User { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }
}
