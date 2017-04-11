using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Child
{
    public interface IChildDAL
    {
        void CreateChild(ChildModel newChild);
        ChildModel GetChild(string userName);
        void UpdateSeconds(string userName, int secondsToSubtract);
        void AddCarrot(string userName);
        List<ChildModel> GetLeadersBySteps();
        List<ChildModel> GetLeadersByCarrots();
    }
}