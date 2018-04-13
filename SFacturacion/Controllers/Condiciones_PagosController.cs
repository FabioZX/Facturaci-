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
    public class Condiciones_PagosController : Controller
    {
        private SPfacturacionEntities db = new SPfacturacionEntities();

        // GET: Condiciones_Pagos
        public ActionResult Index(string Criterio = null)
        {
            var condicion = db.Condiciones_Pagos;
            return View(condicion.Where(p=> Criterio == null || p.Descripcion.StartsWith(Criterio)));
        }

        // GET: Condiciones_Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condiciones_Pagos condiciones_Pagos = db.Condiciones_Pagos.Find(id);
            if (condiciones_Pagos == null)
            {
                return HttpNotFound();
            }
            return View(condiciones_Pagos);
        }

        // GET: Condiciones_Pagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Condiciones_Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCondicion,Descripcion,Cantidad_Dias,Estado")] Condiciones_Pagos condiciones_Pagos)
        {
            if (ModelState.IsValid)
            {
                db.Condiciones_Pagos.Add(condiciones_Pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(condiciones_Pagos);
        }

        // GET: Condiciones_Pagos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condiciones_Pagos condiciones_Pagos = db.Condiciones_Pagos.Find(id);
            if (condiciones_Pagos == null)
            {
                return HttpNotFound();
            }
            return View(condiciones_Pagos);
        }

        // POST: Condiciones_Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCondicion,Descripcion,Cantidad_Dias,Estado")] Condiciones_Pagos condiciones_Pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condiciones_Pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(condiciones_Pagos);
        }

        // GET: Condiciones_Pagos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condiciones_Pagos condiciones_Pagos = db.Condiciones_Pagos.Find(id);
            if (condiciones_Pagos == null)
            {
                return HttpNotFound();
            }
            return View(condiciones_Pagos);
        }

        // POST: Condiciones_Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condiciones_Pagos condiciones_Pagos = db.Condiciones_Pagos.Find(id);
            db.Condiciones_Pagos.Remove(condiciones_Pagos);
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
