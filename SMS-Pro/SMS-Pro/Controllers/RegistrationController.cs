using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS_Pro.Models;

namespace SMS_Pro.Controllers
{
    public class RegistrationController : Controller
    {
        private abdullahaneesEntities db = new abdullahaneesEntities();

        // GET: Registration
        public ActionResult Index()
        {
            var registations = db.registations.Include(r => r.batch).Include(r => r.cours);
            return View(registations.ToList());
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.courses, "id", "course");
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno,address")] registation registation)
        {
            if (ModelState.IsValid)
            {
                db.registations.Add(registation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registation.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course", registation.course_id);
            return View(registation);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registation.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course", registation.course_id);
            return View(registation);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno,address")] registation registation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registation.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course", registation.course_id);
            return View(registation);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registation registation = db.registations.Find(id);
            db.registations.Remove(registation);
            db.SaveChanges(registation);
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
