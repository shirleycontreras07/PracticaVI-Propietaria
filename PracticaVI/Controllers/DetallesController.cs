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
    public class DetallesController : Controller
    {
        private FacturacionEntities db = new FacturacionEntities();

        // GET: Detalles
        public ActionResult Index()
        {
            var detalle = db.Detalle.Include(d => d.Articulo).Include(d => d.Factura);
            return View(detalle.ToList());
        }

        // GET: Detalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // GET: Detalles/Create
        public ActionResult Create()
        {
            ViewBag.IdArticulo = new SelectList(db.Articulo, "IdArticulo", "Descripcion");
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "IdFactura");
            return View();
        }

        // POST: Detalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalle,IdFactura,IdArticulo,PrecioTotal,Cantidad,Pago,Restante")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                detalle.Restante = detalle.PrecioTotal - detalle.Pago;
                db.Detalle.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArticulo = new SelectList(db.Articulo, "IdArticulo", "Descripcion", detalle.IdArticulo);
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "IdFactura", detalle.IdFactura);
            return View(detalle);
        }

        // GET: Detalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArticulo = new SelectList(db.Articulo, "IdArticulo", "Descripcion", detalle.IdArticulo);
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "IdFactura", detalle.IdFactura);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalle,IdFactura,IdArticulo,PrecioTotal,Cantidad,Pago,Restante")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArticulo = new SelectList(db.Articulo, "IdArticulo", "Descripcion", detalle.IdArticulo);
            ViewBag.IdFactura = new SelectList(db.Factura, "IdFactura", "IdFactura", detalle.IdFactura);
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            db.Detalle.Remove(detalle);
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
