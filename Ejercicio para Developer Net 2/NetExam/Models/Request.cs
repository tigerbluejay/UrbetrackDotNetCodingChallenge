using NetExam.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetExam.Models
{
    public class Request : IBooking
    {
        public string LocationName { get; set; }
        public string OfficeName { get; set; }
        public DateTime DateTime { get; set; }
        public int Hours { get; set; }
        public string UserName { get; set; }
    }
}
