using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Child
{
    public interface IChildDAL
    {
        bool CreateChild(ChildModel newChild);
        ChildModel GetChild(string childUsername);
    }
}