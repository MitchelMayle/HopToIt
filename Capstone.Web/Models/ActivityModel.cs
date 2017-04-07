using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ActivityModel
    {
        public int ChildId { get; set; }
        public int Seconds { get; set; }
        public int Carrots { get; set; }
        public DateTime Date { get; set; }


        public DateTime todaysDate = DateTime.Now;
    }
}