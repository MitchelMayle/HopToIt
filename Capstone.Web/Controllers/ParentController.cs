using Capstone.Web.DAL.Parent;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capstone.Web.Crypto;
using Capstone.Web.Models.ViewModels;
using Capstone.Web.DAL.Mascot;
using Capstone.Web.DAL.Child;
using Capstone.Web.DAL.Activity;

namespace Capstone.Web.Controllers
{

    public class ParentController : Controller
    {
        private readonly IParentDAL parentDAL;
        private readonly IMascotDAL mascotDAL;
        private readonly IChildDAL childDAL;
        private readonly IActivityDAL activityDAL;
        public ParentController(IParentDAL parentDAL, IMascotDAL mascotDAL, IChildDAL childDAL, IActivityDAL activityDAL)
        {
            this.parentDAL = parentDAL;
            this.mascotDAL = mascotDAL;
            this.childDAL = childDAL;
            this.activityDAL = activityDAL;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(ParentLoginModel model)
        {
            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            ParentModel parent = parentDAL.GetParent(model.Email);

            HashProvider hash = new HashProvider();

            // check if parent exists and passwords match
            if (parent == null || !hash.VerifyPasswordMatch(parent.Password, model.Password, parent.Salt))
            {
                ModelState.AddModelError("invalid-credentials", "Invalid email password combination");
                return View("Login", model);
            }
        
            Session["parent"] = parent;
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Registration(ParentRegistrationModel viewModel)
        {
            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Registration", viewModel);
            }

            ParentModel newParent = parentDAL.GetParent(viewModel.Email);

            // check for duplicate email
            if (newParent != null)
            {
                ModelState.AddModelError("email-exists", "That email address is already registered.");
                return View("Registration", viewModel);
            }
            else
            {
                newParent = new ParentModel
                {
                    First_Name = viewModel.First_Name,
                    Last_Name = viewModel.Last_Name,
                    Email = viewModel.Email,
                };

                HashProvider hash = new HashProvider();
                newParent.Password = hash.HashPassword(viewModel.Password);
                newParent.Salt = hash.SaltValue;

                parentDAL.CreateParent(newParent);
            }

            // get saved parent model
            ParentModel parent = parentDAL.GetParent(newParent.Email);

            Session["parent"] = parent;
            return RedirectToAction("Dashboard");
        }

        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            // not logged in redirect
            if(Session["parent"] == null)
            {
                return View("Login");
            }

            ParentModel parent = Session["parent"] as ParentModel;

            parent.Children = parentDAL.GetChildren(parent.Parent_ID);
            
            // add mascots to child list
            foreach (ChildModel child in parent.Children)
            {
                child.Mascot = mascotDAL.GetMascot(child);
            }

            return View("Dashboard", parent);
        }
        //[Route("AddActivity")]
        [HttpGet]
        public ActionResult AddActivity(string userName)
        {
            ChildModel child = childDAL.GetChild(userName);
            child.Mascot = mascotDAL.GetMascot(child);
            return View("AddActivity", child);
        }
        [HttpPost]
        public ActionResult AddActivity(ChildModel child, string userName, int childId)
        {
            
            ActivityModel activity = new ActivityModel();
            activity.Carrots = child.Carrots;
            activity.Date = DateTime.Now.Date; //<--***Will need to be replaced by actual Date taken in from form!!!!***
            activity.ChildId = childId;

            activityDAL.AddActivity(activity);
                               
            return RedirectToAction("Dashboard");
        }

    }
}