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

namespace Capstone.Web.Controllers
{

    public class ParentController : Controller
    {
        private readonly IParentDAL dal;
        public ParentController(IParentDAL dal)
        {
            this.dal = dal;
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
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            ParentModel parent = dal.GetParent(model.Email);

            HashProvider hash = new HashProvider();

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
            if (!ModelState.IsValid)
            {
                return View("Registration", viewModel);
            }

            ParentModel newParent = dal.GetParent(viewModel.Email);

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

                dal.CreateParent(newParent);
            }

            ParentModel parent = dal.GetParent(newParent.Email);

            Session["parent"] = parent;
            return RedirectToAction("Dashboard");
        }

        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            if(Session["parent"] == null)
            {
                return View("Login");
            }
            ParentModel parent = Session["parent"] as ParentModel;
            
            return View("Dashboard", parent);
        }



    }
}