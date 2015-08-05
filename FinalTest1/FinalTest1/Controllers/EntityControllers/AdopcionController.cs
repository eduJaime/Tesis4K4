﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalTest1.Contextos;
using FinalTest1.Models.Entidades;
using FinalTest1.ViewModel;

namespace FinalTest1.Controllers.EntityControllers
{
    public class AdopcionController : Controller
    {
        private AerDb db = new AerDb();

        // GET: Adopcion
        public ActionResult Index()
        {
            var adopcions = db.Adopcions.Include(a => a.estadoAdopcion).Include(a => a.tipoAdopcion);
            return View(adopcions.ToList());
        }

        // GET: Adopcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopcion adopcion = db.Adopcions.Find(id);
            if (adopcion == null)
            {
                return HttpNotFound();
            }
            return View(adopcion);
        }

        // GET: Adopcion/Create
        public ActionResult Create()
        {
            //-------------------------------
            AdopcionViewModel modelo = new AdopcionViewModel();
            var animales = db.Animales.Include(a => a.raza).Include(a => a.tamanio);
            //var razas = db.Razas.Include(r => r.especie);
            //modelo.raza = razas;
            var tipoAdopcion = db.TipoAdopcions;
            modelo.tipoAdopcion = tipoAdopcion.ToList();
            modelo.animal = animales.ToList();

            //-------------------------------

            ViewBag.estadoAdopcionId = new SelectList(db.EstadoAdopcions, "Id", "nombre");
            ViewBag.tipoAdopcionId = new SelectList(db.TipoAdopcions, "Id", "nombre");
            return View(modelo);
        }

        // POST: Adopcion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,voluntarioId,admId,tipoAdopcionId,estadoAdopcionId,esTemporal,fechaAlta,fechaBaja,fechaConfirmacion,fechaCancelacion,fechaEntrega,fechaFin,importe,fechaFinColaboracion")] Adopcion adopcion)
        {
            if (ModelState.IsValid)
            {
                db.Adopcions.Add(adopcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estadoAdopcionId = new SelectList(db.EstadoAdopcions, "Id", "nombre", adopcion.estadoAdopcionId);
            ViewBag.tipoAdopcionId = new SelectList(db.TipoAdopcions, "Id", "nombre", adopcion.tipoAdopcionId);
            return View(adopcion);
        }

        // GET: Adopcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopcion adopcion = db.Adopcions.Find(id);
            if (adopcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.estadoAdopcionId = new SelectList(db.EstadoAdopcions, "Id", "nombre", adopcion.estadoAdopcionId);
            ViewBag.tipoAdopcionId = new SelectList(db.TipoAdopcions, "Id", "nombre", adopcion.tipoAdopcionId);
            return View(adopcion);
        }

        // POST: Adopcion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,voluntarioId,admId,tipoAdopcionId,estadoAdopcionId,esTemporal,fechaAlta,fechaBaja,fechaConfirmacion,fechaCancelacion,fechaEntrega,fechaFin,importe,fechaFinColaboracion")] Adopcion adopcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adopcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estadoAdopcionId = new SelectList(db.EstadoAdopcions, "Id", "nombre", adopcion.estadoAdopcionId);
            ViewBag.tipoAdopcionId = new SelectList(db.TipoAdopcions, "Id", "nombre", adopcion.tipoAdopcionId);
            return View(adopcion);
        }

        // GET: Adopcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopcion adopcion = db.Adopcions.Find(id);
            if (adopcion == null)
            {
                return HttpNotFound();
            }
            return View(adopcion);
        }

        // POST: Adopcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adopcion adopcion = db.Adopcions.Find(id);
            db.Adopcions.Remove(adopcion);
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
