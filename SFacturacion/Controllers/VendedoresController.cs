using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UltimoIntento.Controllers
{
    public class VendedoresController : Controller
    {
        private SPfacturacionEntities db = new SPfacturacionEntities();

        // GET: Vendedores
        public ActionResult Index(string Criterio = null)
        {
            var vendedor = db.Vendedores;
            return View(vendedor.Where(p=> Criterio == null || p.Nombre.StartsWith(Criterio)));
        }

        // GET: Vendedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // GET: Vendedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVendedor,Nombre,Porciento_Comision,Estado")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vendedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendedores);
        }

        // GET: Vendedores/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVendedor,Nombre,Porciento_Comision,Estado")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendedores);
        }

        // GET: Vendedores/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendedores vendedores = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedores);
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
