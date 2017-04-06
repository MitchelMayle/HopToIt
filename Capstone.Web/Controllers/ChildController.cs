using Capstone.Web.Crypto;
using Capstone.Web.DAL.Child;
using Capstone.Web.DAL.Mascot;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class ChildController : Controller
    {
        private readonly IMascotDAL mascotDAL;

        private readonly IChildDAL dal;
        public ChildController(IChildDAL dal, IMascotDAL mascotDAL)
        {
            this.dal = dal;
            this.mascotDAL = mascotDAL;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            // check if logged in
            if (Session["parent"] == null)
            {
                return RedirectToAction("Login", "Parent", null);
            }
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Registration(ChildRegistrationModel viewModel)
        {
            // check if logged in
            if(Session["parent"] == null)
            {
                return RedirectToAction("Login", "Parent", null);
            }

            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Registration", viewModel);
            }

            ChildModel child = dal.GetChild(viewModel.User_Name);

            // check for duplicate username
            if (child != null)
            {
                ModelState.AddModelError("user name-exists", "That user name is already registered.");
                return View("Registration", viewModel);
            }
            else
            {
                
                child = new ChildModel
                {
                    First_Name = viewModel.First_Name,
                    UserName = viewModel.User_Name,

                };

                HashProvider hash = new HashProvider();
                child.Password = hash.HashPassword(viewModel.Password);
                child.Salt = hash.SaltValue;

                ParentModel parent = Session["parent"] as ParentModel;
                child.Parent_Id = parent.Parent_ID;

                dal.CreateChild(child);
            }
            return RedirectToAction("Dashboard", "Parent");
        }

        [HttpPost]
        [Route("ChildLogin")]
        public ActionResult Login(ChildLoginModel model)
        {
            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            ChildModel child = dal.GetChild(model.UserName);

            HashProvider hash = new HashProvider();

            // check if child exists and passwords match
            if (child == null || !hash.VerifyPasswordMatch(child.Password, model.Password, child.Salt))
            {
                ModelState.AddModelError("invalid-credentials", "Invalid email password combination");
                return View("Login", model);
            }

            child.Mascot = mascotDAL.GetMascot(child);

            Session["child"] = child;
            return RedirectToAction("Dashboard");
        }

        [Route("ChildDashboard/")]
        public ActionResult Dashboard()
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            ChildModel child = Session["child"] as ChildModel;
            return View("Dashboard", child);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.ChildId);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChildHeaderCarrot()
        {
            ChildModel child = Session["child"] as ChildModel;

            return PartialView("_ChildHeaderCarrot", child);
        }

        public ActionResult ChildHeaderTime()
        {
            ChildModel child = Session["child"] as ChildModel;

            return PartialView("_ChildHeaderTime", child);
        }
    }
}