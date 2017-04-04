﻿using Capstone.Web.DAL.Parent;
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
            else
            {
                FormsAuthentication.SetAuthCookie(parent.Email, true);
                Session[SessionKeys.ParentId] = parent.Parent_ID;
            }

            return RedirectToAction("Dashboard", "Parent", parent);
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

            ParentModel parent = dal.GetParent(viewModel.Email);

            if (parent != null)
            {
                ModelState.AddModelError("email-exists", "That email address is already registered.");
                return View("Registration", viewModel);
            }
            else
            {
                parent = new ParentModel
                {
                    First_Name = viewModel.First_Name,
                    Last_Name = viewModel.Last_Name,
                    Email = viewModel.Email,
                };

                HashProvider hash = new HashProvider();
                parent.Password = hash.HashPassword(viewModel.Password);
                parent.Salt = hash.SaltValue;

                dal.CreateParent(parent);
            }
            return RedirectToAction("Dashboard", "Parent", parent);
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.ParentId);

            return RedirectToAction("Index", "Home");
        }
    }
}