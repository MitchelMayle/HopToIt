using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.DAL.Parent
{
    public interface IParentDAL
    {
        void CreateParent(ParentModel newParent);
        ParentModel GetParent(string emailAddress);
        List<ChildModel> GetChildren(int parent_Id);

    }
}