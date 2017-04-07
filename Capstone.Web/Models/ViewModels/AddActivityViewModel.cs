using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class AddActivityViewModel
    {
        public int Child_Id { get; set; }
        public MascotModel Mascot { get; set; }
        public string UserName { get; set; }
        public int Steps { get; set; }
        public int Minutes { get; set; }
        public DateTime Date { get; set; }
        public string First_Name { get; set; }

    }
}