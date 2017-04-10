using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.Models.ViewModels
{
    public class ActivityHistoryViewModel
    {
        public List<ActivityModel> ActivityList {get;set;}
        public int ChildId { get; set; }
 
    }
}