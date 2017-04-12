using Capstone.Web.Crypto;
using Capstone.Web.DAL.Child;
using Capstone.Web.DAL.Item;
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
        private readonly IItemsDAL itemsDAL;

        private readonly IChildDAL childDAL;
        public ChildController(IChildDAL childDAL, IMascotDAL mascotDAL, IItemsDAL itemsDAL)
        {
            this.childDAL = childDAL;
            this.mascotDAL = mascotDAL;
            this.itemsDAL = itemsDAL;
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
            if (Session["parent"] == null)
            {
                return RedirectToAction("Login", "Parent");
            }

            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Registration", viewModel);
            }

            ChildModel child = childDAL.GetChild(viewModel.User_Name);

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

                childDAL.CreateChild(child);
            }
            return RedirectToAction("Dashboard", "Parent");
        }

        [HttpPost]
        public ActionResult Login(ChildLoginModel model)
        {
            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            ChildModel child = childDAL.GetChild(model.UserName);

            HashProvider hash = new HashProvider();

            // check if child exists and passwords match
            if (child == null || !hash.VerifyPasswordMatch(child.Password, model.Password, child.Salt))
            {
                ModelState.AddModelError("invalid-credentials", "Invalid email password combination");
                return View("Login", model);
            }

            // check if child has time remaining
            if (child.Seconds <= 0)
            {
                ModelState.AddModelError("no-time-remaining", "You do not have any time remaining. You need more steps to earn more time.");
                return View("Login", model);
            }

            child.Mascot = mascotDAL.GetMascot(child);

            Session["child"] = child;

            // check if child needs to create mascot
            if (child.Mascot == null)
            {
                return RedirectToAction("ChooseMascot");
            }

            return RedirectToAction("Dashboard");
        }

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

        [HttpGet]
        public ActionResult ChooseMascot()
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            ChildModel child = Session["child"] as ChildModel;

            // check if child already has mascot
            if (child.Mascot != null)
            {
                ModelState.AddModelError("mascot-exists", "You have already chosen a mascot");
                return View("Dashboard", child);
            }

            return View("ChooseMascot");
        }

        [HttpPost]
        public ActionResult ChooseMascot(ChooseMascotModel chooseMascot)
        {
            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("ChooseMascot", chooseMascot);
            }

            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            // check if child already has mascot
            ChildModel getChild = Session["child"] as ChildModel;
            ChildModel checkChild = childDAL.GetChild(getChild.UserName);
            checkChild.Mascot = mascotDAL.GetMascot(checkChild);

            if (checkChild.Mascot != null)
            {
                ModelState.AddModelError("mascot-exists", "You have already chosen a mascot");
                return View("Dashboard");
            }

            // assign properties to new mascot
            MascotModel newMascot = new MascotModel();
            newMascot.Child_Id = checkChild.Child_Id;
            newMascot.Mascot_Image = chooseMascot.Mascot_Image;

            // create mascot
            mascotDAL.CreateMascot(newMascot);

            // retrieve new child model after mascot create
            ChildModel saveChild = childDAL.GetChild(checkChild.UserName);
            saveChild.Mascot = mascotDAL.GetMascot(saveChild);

            // save updated child in session
            Session["child"] = saveChild;

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Closet()
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            ChildModel child = Session["child"] as ChildModel;

            // check if child needs to create mascot
            if (child.Mascot == null)
            {
                return RedirectToAction("ChooseMascot");
            }

            return View("Closet", child);
        }

        [HttpPost]
        public ActionResult Closet(ChildModel model)
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            ChildModel child = Session["child"] as ChildModel;

            if (model.Mascot.CurrentHat != null)
            {
                mascotDAL.UpdateHat(child.Mascot.Mascot_Id, model.Mascot.CurrentHat);
            }

            if (model.Mascot.CurrentBackground != null)
            {
                mascotDAL.UpdateBackground(child.Mascot.Mascot_Id, model.Mascot.CurrentBackground);
            }

            child.Mascot = mascotDAL.GetMascot(child);
            Session["child"] = child;

            return RedirectToAction("Closet");
        }

        [HttpGet]
        public ActionResult Store()
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            ChildModel child = Session["child"] as ChildModel;

            // check if child needs to create mascot
            if (child.Mascot == null)
            {
                return RedirectToAction("ChooseMascot");
            }

            StoreViewModel viewModel = new StoreViewModel();
            viewModel.Mascot = child.Mascot;

            viewModel.Hats = itemsDAL.GetHats();
            viewModel.Backgrounds = itemsDAL.GetBackgrounds();

            Session["storeModel"] = viewModel;

            return View("Store", viewModel);
        }

        [HttpPost]
        public ActionResult Store(StoreViewModel storeModel)
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            ChildModel child = Session["child"] as ChildModel;  

            // check if child needs to create mascot
            if (child.Mascot == null)
            {
                return RedirectToAction("ChooseMascot");
            }

            StoreViewModel sessionModel = Session["storeModel"] as StoreViewModel;
            storeModel.Hats = sessionModel.Hats;
            storeModel.Mascot = sessionModel.Mascot;
            storeModel.Backgrounds = sessionModel.Backgrounds;

            // validation redirect
            if (!ModelState.IsValid)
            {
                return View("Store");
            }

            ItemModel purchasedItem = itemsDAL.GetItem(storeModel.ItemId);

            if (purchasedItem.Price > child.Carrots)
            {
                ModelState.AddModelError("insufficient-carrots", "You do not have enough carrots to purchase that item.");
                return View("Store", storeModel);
            }

            child.Mascot.OwnedItems = mascotDAL.GetListOfItems(child.Child_Id);
           
            if (child.Mascot.OwnedItems.Contains(purchasedItem.Item_Id))
            {
                ModelState.AddModelError("item-already-purchased", "You aready own this item!");
                return View("Store", storeModel);
            }

            mascotDAL.PurchaseItem(child.Child_Id, purchasedItem.Image, purchasedItem.Price);

            child = childDAL.GetChild(child.UserName);
            child.Mascot = mascotDAL.GetMascot(child);
            Session["child"] = child;

            return RedirectToAction("Closet", child);
        }

        public ActionResult Game()
        {
            // check if logged in
            if (Session["child"] == null)
            {
                return View("Login");
            }

            return View("Game");
        }

        [Route("OutOfTime")]
        public ActionResult OutOfTime()
        {
            ModelState.AddModelError("expired", "You do not have any time remaining. You need more steps to earn more time.");
            return View("OutOfTime");
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
