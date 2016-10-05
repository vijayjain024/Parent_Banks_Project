using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parent_Bank.Models;
using System.Threading.Tasks;

namespace Parent_Bank.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

 

        

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        
        public ActionResult Create()
        {
            if (User.IsInRole("Owner"))
            {
                return View();
            }
            else
                return View("~/Views/Accounts/UnauthorizedError.cshtml");
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Create([Bind(Include = "AccountId,Owner,Recepient,Name,InterestRate,Balance")] Account account)
        {

            if (ModelState.IsValid)
            {
                // check if owner is already a recepient
                int recepientInstances = db.Accounts.Where(p => p.Owner == account.Recepient).Count();

                // if we found 1 (or more) existing ssn's then this is validation error
                if (recepientInstances > 0)
                {
                    ModelState.AddModelError("Owner", "Owner already exists as a recepient and hence cannot be added");
                }

                //check if a recepient already exists as a owner
                int ownerInstances = db.Accounts.Where(p => p.Recepient == account.Owner).Count();
                if (ownerInstances > 0)
                {
                    ModelState.AddModelError("Recepient", "Recepient already exists as a owner and hence cannot be added");
                }

                int recepientRepeats = db.Accounts.Where(p => p.Recepient == account.Recepient).Count();
                if (recepientRepeats > 0)
                {
                    ModelState.AddModelError("Recepient", "Recepient already exists");
                }

                // test again that the model is valid before saving
                if (ModelState.IsValid)
                {
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            return View(account);
        }

        // GET: Accounts/Edit/5
        [Authorize(Roles = "Owner")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Edit([Bind(Include = "AccountId,Owner,Recepient,Name,InterestRate,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Owner")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Owner")]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            if (account.Balance > 0)
            {
                ModelState.AddModelError("Balance", "Account cannot be deleted if balance is greater than zero.");
            }

                db.Accounts.Remove(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
