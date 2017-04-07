using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ChildModel
    {
        public int Child_Id { get; set; }
        public string Salt { get; set; }
        public int Parent_Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public int Carrots { get; set; }
        public int Seconds { get; set; }
        public MascotModel Mascot { get; set; }
        public List<ActivityModel> Activities { get; set; }
     
    }
}