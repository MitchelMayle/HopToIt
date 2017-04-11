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
        void PurchaseItem(int childId, string itemName, int itemPrice);
        void UpdateHat(int mascot_Id, string currentHat);
        void UpdateBackground(int mascot_Id, string currentBackground);
        List<int> GetListOfItems(int childId); 
    }
}