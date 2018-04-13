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
    public class ClientesController : Controller
    {
        private SPfacturacionEntities db = new SPfacturacionEntities();

        // GET: Clientes
        public ActionResult Index(string Criterio = null)
        {
            var cliente = db.Clientes;
            return View(cliente.Where(p => Criterio == null || p.Nombre_Comercial.StartsWith(Criterio) || p.Cedula.StartsWith(Criterio)));
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,Nombre_Comercial,Cedula,Estado")] Clientes clientes)
        {
            if (!ValidaCedula(clientes.Cedula))
            {
                ModelState.AddModelError("Error", "La cedula esta mal escrita");
            }

            else 
            {
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //return View(clientes);
            
            return View();


        }

        // GET: Clientes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,Nombre_Comercial,Cedula,Estado")] Clientes clientes)
        {
            if (!ValidaCedula(clientes.Cedula))
            {
                ModelState.AddModelError("Error", "La cedula esta mal escrita");
            }

            else
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                return View();

           /* if (ModelState.IsValid)
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientes);*/
        }

        // GET: Clientes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
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

        public static bool ValidaCedula(string cedula)
        {
            if (cedula.Any(c => char.IsLetter(c)))
                return false;

            if (cedula.All(c => c == '0'))
                return false;

            cedula = cedula.Replace("-", "").Trim();
            string temp = string.Empty;
            if (cedula.Length == 11)
            {
                for (int i = 0; i < cedula.Length - 1; i++)
                {
                    temp += (byte)char.GetNumericValue(cedula[i]) * (i % 2 + 1);
                }
                int sigma = temp.Select(x => (byte)char.GetNumericValue(x)).Sum(x => x);
                return Math.Abs(10 - sigma % 10) == char.GetNumericValue(cedula.Last());
            }
            return false;
        }

        //public static bool ValidaCedula(string Cedula)
        //{
        //    int vnTotal = 0;
        //    string vcCedula = Cedula.Replace("-", "");
        //    int pLongCed = vcCedula.Trim().Length;
        //    int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

        //    if (pLongCed < 11 || pLongCed > 11)
        //        return false;

        //    for (int vDig = 1; vDig <= pLongCed; vDig++)
        //    {
        //        int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
        //        if (vCalculo < 10)
        //            vnTotal += vCalculo;
        //        else
        //            vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
        //    }

        //    if (vnTotal % 10 == 0)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
