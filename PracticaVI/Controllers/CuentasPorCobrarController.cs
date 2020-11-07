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
    public class CuentasPorCobrarController : Controller
    {
        private FacturacionEntities db = new FacturacionEntities();

        // GET: CuentasPorCobrar
        public ActionResult Index()
        {
            return View(db.CuentasPorCobrar.ToList());
        }

        // GET: CuentasPorCobrar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasPorCobrar cuentasPorCobrar = db.CuentasPorCobrar.Find(id);
            if (cuentasPorCobrar == null)
            {
                return HttpNotFound();
            }
            return View(cuentasPorCobrar);
        }

        // GET: CuentasPorCobrar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentasPorCobrar/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCuenta,IdCliente,IdVendedor,IdFactura,PrecioTotal,Pago,Restante")] CuentasPorCobrar cuentasPorCobrar)
        {
            if (ModelState.IsValid)
            {
                db.CuentasPorCobrar.Add(cuentasPorCobrar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuentasPorCobrar);
        }

        // GET: CuentasPorCobrar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasPorCobrar cuentasPorCobrar = db.CuentasPorCobrar.Find(id);
            if (cuentasPorCobrar == null)
            {
                return HttpNotFound();
            }
            return View(cuentasPorCobrar);
        }

        // POST: CuentasPorCobrar/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCuenta,IdCliente,IdVendedor,IdFactura,PrecioTotal,Pago,Restante")] CuentasPorCobrar cuentasPorCobrar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentasPorCobrar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuentasPorCobrar);
        }

        // GET: CuentasPorCobrar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasPorCobrar cuentasPorCobrar = db.CuentasPorCobrar.Find(id);
            if (cuentasPorCobrar == null)
            {
                return HttpNotFound();
            }
            return View(cuentasPorCobrar);
        }

        // POST: CuentasPorCobrar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentasPorCobrar cuentasPorCobrar = db.CuentasPorCobrar.Find(id);
            db.CuentasPorCobrar.Remove(cuentasPorCobrar);
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
