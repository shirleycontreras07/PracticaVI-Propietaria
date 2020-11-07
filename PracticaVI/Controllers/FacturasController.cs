using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticaVI;

namespace PracticaVI.Controllers
{
    public class FacturasController : Controller
    {
        private FacturacionEntities db = new FacturacionEntities();

        // GET: Facturas
        public ActionResult Index()
        {
            var factura = db.Factura.Include(f => f.Cliente).Include(f => f.Vendedor);
            return View(factura.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "Cedula");
            ViewBag.IdVendedor = new SelectList(db.Vendedor, "IdVendedor", "Cedula");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFactura,IdCliente,IdVendedor,Fecha,Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                factura.Fecha = DateTime.Now;
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "Cedula", factura.IdCliente);
            ViewBag.IdVendedor = new SelectList(db.Vendedor, "IdVendedor", "Cedula", factura.IdVendedor);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "Cedula", factura.IdCliente);
            ViewBag.IdVendedor = new SelectList(db.Vendedor, "IdVendedor", "Cedula", factura.IdVendedor);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFactura,IdCliente,IdVendedor,Fecha,Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "Cedula", factura.IdCliente);
            ViewBag.IdVendedor = new SelectList(db.Vendedor, "IdVendedor", "Cedula", factura.IdVendedor);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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
