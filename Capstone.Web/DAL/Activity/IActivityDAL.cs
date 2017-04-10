using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Activity
{
    public interface IActivityDAL
    {
        
        void AddActivity(ActivityModel activity);
        int GetSteps(int child_Id);
        int GetMinutes(int child_Id);
        bool IdExists(int child_Id);
        List<ActivityModel> GetActivities(int child_Id);
    }
}
