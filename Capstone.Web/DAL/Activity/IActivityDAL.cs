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
    }
}
