using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Mascot
{
    public interface IMascotDAL
    {
        void CreateMascot(ChildModel child);
        MascotModel GetMascot(ChildModel child);
    }
}