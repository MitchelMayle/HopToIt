using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Mascot
{
    public interface IMascotDAL
    {
        void CreateMascot(MascotModel newMascot);
        MascotModel GetMascot(ChildModel child);
        void PurchaseItem(int childId, string itemName);
        void ChangeCurrentItem(int childId, string property, string itemName);
        List<ItemModel> GetHats();
        List<ItemModel> GetBackgrounds();
    }
}