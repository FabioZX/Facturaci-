using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;



namespace UltimoIntento.Controllers
{
    public class FacturacionsController : Controller
    {
        private SPfacturacionEntities db = new SPfacturacionEntities();

        // GET: Facturacions
        public ActionResult Index(string Criterio = null)
        {
            var facturacion = db.Facturacion.Include(f => f.Articulos_Facturables).Include(f => f.Clientes).Include(f => f.Condiciones_Pagos).Include(f => f.Vendedores);
            return View(facturacion.Where(p=> Criterio == null || p.Forma_Pago.StartsWith(Criterio) || p.Comentario.StartsWith(Criterio) || p.Articulos_Facturables.Descripcion.StartsWith(Criterio) || p.Clientes.Nombre_Comercial.StartsWith(Criterio) || p.Condiciones_Pagos.Descripcion.StartsWith(Criterio) || p.Vendedores.Nombre.StartsWith(Criterio)));
        }

        // GET: Facturacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacion.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // GET: Facturacions/Create
        public ActionResult Create()
        {
            ViewBag.IdArticulo = new SelectList(db.Articulos_Facturables, "IdArticulo", "Descripcion");
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre_Comercial");
            ViewBag.IdCondicion = new SelectList(db.Condiciones_Pagos, "IdCondicion", "Descripcion");
            ViewBag.IdVendedor = new SelectList(db.Vendedores, "IdVendedor", "Nombre");
            return View();
        }

        // POST: Facturacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFacuracion,Forma_Pago,Fecha,Comentario,Cantidad,Precio_Unitario,IdArticulo,IdCliente,IdCondicion,IdVendedor")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Facturacion.Add(facturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArticulo = new SelectList(db.Articulos_Facturables, "IdArticulo", "Descripcion", facturacion.IdArticulo);
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre_Comercial", facturacion.IdCliente);
            ViewBag.IdCondicion = new SelectList(db.Condiciones_Pagos, "IdCondicion", "Descripcion", facturacion.IdCondicion);
            ViewBag.IdVendedor = new SelectList(db.Vendedores, "IdVendedor", "Nombre", facturacion.IdVendedor);
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacion.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArticulo = new SelectList(db.Articulos_Facturables, "IdArticulo", "Descripcion", facturacion.IdArticulo);
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre_Comercial", facturacion.IdCliente);
            ViewBag.IdCondicion = new SelectList(db.Condiciones_Pagos, "IdCondicion", "Descripcion", facturacion.IdCondicion);
            ViewBag.IdVendedor = new SelectList(db.Vendedores, "IdVendedor", "Nombre", facturacion.IdVendedor);
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFacuracion,Forma_Pago,Fecha,Comentario,Cantidad,Precio_Unitario,IdArticulo,IdCliente,IdCondicion,IdVendedor")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArticulo = new SelectList(db.Articulos_Facturables, "IdArticulo", "Descripcion", facturacion.IdArticulo);
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "Nombre_Comercial", facturacion.IdCliente);
            ViewBag.IdCondicion = new SelectList(db.Condiciones_Pagos, "IdCondicion", "Descripcion", facturacion.IdCondicion);
            ViewBag.IdVendedor = new SelectList(db.Vendedores, "IdVendedor", "Nombre", facturacion.IdVendedor);
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturacion facturacion = db.Facturacion.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        public ActionResult Report()
        {
            return View(db.Facturacion.ToList());
        }

        public ActionResult ExportReport()
        {
            string filename = "Facturación.csv";
            string filepath = @"C:\Users\Fabio Victor\Desktop\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("Facturacion, Forma de Pago, Cantidad, Precio Unitario, Cantidad X Precio, ID Articulo, Articulos, ID Cliente, Cliente, Cedula Cliente, Condicion de Pago, ID Vendedor, Vendedor"); //Encabezado 
            foreach (var i in db.Facturacion.ToList())
            {
                sw.WriteLine(i.IdFacuracion.ToString() + "," + i.Forma_Pago + "," + i.Cantidad + "," + i.Precio_Unitario + "," + i.Cantidad * i.Precio_Unitario  +","+ i.IdArticulo + "," + i.Articulos_Facturables.Descripcion +"," + i.IdCliente + "," + i.Clientes.Nombre_Comercial + "," + i.Clientes.Cedula + "," + i.Condiciones_Pagos.Descripcion + "," + i.IdVendedor + "," + i.Vendedores.Nombre);
            }
            sw.Close();

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);

        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturacion facturacion = db.Facturacion.Find(id);
            db.Facturacion.Remove(facturacion);
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
