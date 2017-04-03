using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL.Parent
{
    public interface IParentDAL
    {
        void CreateParent(ParentModel newParent);
        ParentModel GetParent(ParentModel searchParent);
        List<ChildModel> GetChildren(int parent_Id);

    }
}