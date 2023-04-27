using NetExam.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetExam.Models
{
    public class Location : ILocation
    {
        public string Name { get; set; }
        public string Neighborhood { get; set; }
    }
}
