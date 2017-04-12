using Capstone.Web.DAL.Child;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        public IChildDAL childDAL;
        public HomeController(IChildDAL childDAL)
        {
            this.childDAL = childDAL;

        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            if (Session["parent"] != null)
            {
                ParentModel parent = Session["parent"] as ParentModel;
                return PartialView("_ParentAuthenticated", parent);

            }
            if (Session["child"] != null)
            {
                ChildModel child = Session["child"] as ChildModel;
                return PartialView("_ChildAuthenticated", child);
            }

            return PartialView("_NotLoggedIn");
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Leaderboard()
        {
            LeaderboardViewModel leaderboard = new LeaderboardViewModel();

            if (Session["leaderboard"] != null)
            {
                leaderboard = Session["leaderboard"] as LeaderboardViewModel;
            }
            else
            {
                leaderboard.ChildList = childDAL.GetLeadersBySteps();
            }


            return View("LeaderBoard", leaderboard);
        }
        [HttpPost]
        public ActionResult Leaderboard(bool MinutesOption)
        {

            LeaderboardViewModel leaderboard = new LeaderboardViewModel();
            if (MinutesOption)
            {
                leaderboard.ChildList = childDAL.GetLeadersByCarrots();
            }
            else
            {
                leaderboard.ChildList = childDAL.GetLeadersBySteps();
            }
            leaderboard.MinutesOption = MinutesOption;
            Session["leaderboard"] = leaderboard;
            return RedirectToAction("LeaderBoard", leaderboard);
        }
    }
}