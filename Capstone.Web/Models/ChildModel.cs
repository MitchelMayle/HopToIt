using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ChildModel
    {
        public int Child_Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public int Steps { get; set; }
        public int Active_Minutes { get; set; }
    }
}