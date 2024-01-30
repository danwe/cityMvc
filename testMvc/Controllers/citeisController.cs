using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using testMvc.Models;

namespace testMvc.Controllers
{
    public class citeisController : Controller
    {
         private masterEntities db = new masterEntities();
        BL bl = new BL();
        // GET: citeis

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            return View(bl.GetCitiesPageing(sortOrder, currentFilter, searchString, page, ViewBag, Request));
        }

        // GET: citeis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            citeis citeis = bl.find(id); 
            if (citeis == null)
            {
                return HttpNotFound();
            }
            return View(citeis);
        }

        // GET: citeis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: citeis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name")] citeis citeis)
        {
            if (ModelState.IsValid)
            {
                bl.Create(citeis);
               
                return RedirectToAction("Index");
            }

            return View(citeis);
        }

        // GET: citeis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            citeis citeis = db.citeis.Find(id);
            if (citeis == null)
            {
                return HttpNotFound();
            }
            return View(citeis);
        }

        // POST: citeis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] citeis citeis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citeis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(citeis);
        }

        // GET: citeis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            citeis citeis = db.citeis.Find(id);
            if (citeis == null)
            {
                return HttpNotFound();
            }
            return View(citeis);
        }

        // POST: citeis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            citeis citeis = db.citeis.Find(id);
            db.citeis.Remove(citeis);
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
