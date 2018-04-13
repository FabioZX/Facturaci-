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
    public class Articulos_FacturablesController : Controller
    {
        private SPfacturacionEntities db = new SPfacturacionEntities();

        // GET: Articulos_Facturables
        public ActionResult Index(string Criterio = null)
        {
            var articulo = db.Articulos_Facturables;
            return View(articulo.Where(p=> Criterio == null || p.Descripcion.StartsWith(Criterio)));
        }

        // GET: Articulos_Facturables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos_Facturables articulos_Facturables = db.Articulos_Facturables.Find(id);
            if (articulos_Facturables == null)
            {
                return HttpNotFound();
            }
            return View(articulos_Facturables);
        }

        // GET: Articulos_Facturables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articulos_Facturables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArticulo,Descripcion,Precio,Estado")] Articulos_Facturables articulos_Facturables)
        {
            if (ModelState.IsValid)
            {
                db.Articulos_Facturables.Add(articulos_Facturables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articulos_Facturables);
        }

        // GET: Articulos_Facturables/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos_Facturables articulos_Facturables = db.Articulos_Facturables.Find(id);
            if (articulos_Facturables == null)
            {
                return HttpNotFound();
            }
            return View(articulos_Facturables);
        }

        // POST: Articulos_Facturables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArticulo,Descripcion,Precio,Estado")] Articulos_Facturables articulos_Facturables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulos_Facturables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articulos_Facturables);
        }

        // GET: Articulos_Facturables/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos_Facturables articulos_Facturables = db.Articulos_Facturables.Find(id);
            if (articulos_Facturables == null)
            {
                return HttpNotFound();
            }
            return View(articulos_Facturables);
        }

        // POST: Articulos_Facturables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulos_Facturables articulos_Facturables = db.Articulos_Facturables.Find(id);
            db.Articulos_Facturables.Remove(articulos_Facturables);
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
