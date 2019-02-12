using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Schedule_It_2.Models;

namespace Schedule_It_2.Controllers
{
    public class TimeOffEventController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeOffEvent
        // Makes sure the user is an Admin before allowing them to view the Index
        [Authorize]
        public ActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                ViewBag.headerData = new TimeOffEvent();
                return View(db.TimeOffEvents.OrderBy(x => x.Start).ToList());
            }
            else return View("~/Views/Shared/AdminError.cshtml");
        }

        // GET: TimeOffEvent/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else return View("~/Views/Account/Login.cshtml");
        }
        // POST: TimeOffEvent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventId,Start,End,Note,Title,ActiveSchedule,ApproverId,Submitted")] TimeOffEvent timeOffEvent)
        {
            if (ModelState.IsValid)
            {
                // Setting the submitted time to now
                timeOffEvent.Submitted = DateTime.Now;
                // Setting the EventId to a new unique GUID, without this, it is all 0s
                timeOffEvent.EventId = Guid.NewGuid();
                db.TimeOffEvents.Add(timeOffEvent);
                //timeOffEvent.UserId = HttpContext.User.Identity.GetUserId();
                db.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else return View("Confirm");
            }

            return View(timeOffEvent);
        }
        // hi
        // GET: TimeOffEvent/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOffEvent timeOffEvent = (TimeOffEvent)db.TimeOffEvents.Find(id);
            if (timeOffEvent == null)
            {
                return HttpNotFound();
            }
            return View(timeOffEvent);
        }

        // POST: TimeOffEvent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventId,Start,End,Note,Title,ActiveSchedule,ApproverId,Submitted")] TimeOffEvent timeOffEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeOffEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeOffEvent);
        }

        // GET: TimeOffEvent/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOffEvent timeOffEvent = db.TimeOffEvents.Find(id);
            if (timeOffEvent == null)
            {
                return HttpNotFound();
            }
            return View(timeOffEvent);
        }

        // POST: TimeOffEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TimeOffEvent timeOffEvent = db.TimeOffEvents.Find(id);
            db.TimeOffEvents.Remove(timeOffEvent);
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

        //Function takes in a string message title and a string message Content 
        //  and sends the message to all users in current database
        private void MessageAll(string messageTitle, string content)
        {
            Message m = new Message
            {
                MessageId = new Guid(),
                MessageTitle = messageTitle,
                Content = content,
                DateSent = DateTime.Now,
                Sender = db.Users.Find("be59571d - 359d - 449e-b540 - ac529c09b9f0") //Sender is Admin
            };

            foreach (var user in db.Users)
            {
                //m.Recipient = user;
                db.Messages.Add(m);
                db.SaveChanges();
            }


        }



                //Get events from database context
        public JsonResult GetEvents()
        {
            var events = db.TimeOffEvents.ToList();
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        //Save an event to database, whether new or update
        [HttpPost]
        public JsonResult SaveEvent(TimeOffEvent u)
        {
            var status = false;

            //If EventId exists, replace current event c with updated event u.
            //Otherwise, add new event n to database
            if (u.EventId == Guid.Empty)
            {
                u.EventId = Guid.NewGuid();
                db.TimeOffEvents.Add(u);
            }
            else
            {
                var c = db.TimeOffEvents.Where(a => a.EventId == u.EventId).FirstOrDefault();
                if (c != null)
                {
                    c.Title = u.Title;
                    c.Start = u.Start;
                    c.End = u.End;
                    c.Note = u.Note;
                }
            }

            db.SaveChanges();
            status = true;

            return new JsonResult { Data = new { status = status, eventID = u.EventId } };
        }


        //Delete existing event with provided EventId
        [HttpPost]
        public JsonResult DeleteEvent(Guid EventId)
        {
            var status = false;

            var c = db.TimeOffEvents.Where(a => a.EventId == EventId).FirstOrDefault();
            if (c != null)
            {
                db.TimeOffEvents.Remove(c);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Approve(Guid id)
        {
            TimeOffEvent timeOffEvent = db.TimeOffEvents.Find(id);
            timeOffEvent.ApproverId = new Guid(User.Identity.GetUserId());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
