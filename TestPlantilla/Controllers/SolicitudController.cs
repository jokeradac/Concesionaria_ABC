using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPlantilla.Models;

namespace TestPlantilla.Controllers
{
    public class SolicitudController : Controller
    {
        private AgenciaContext db;
        // GET: Solicitud
        public ActionResult Index()
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (Conection())
                {
                    return PartialView(db.Solicitud.ToList());
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error encontrado: " + e.Message);
                return View();
            }
        }//Fin index

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Solicitud s)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (Conection())
                {
                    db.Solicitud.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }//Fin Crear sobrecargado

        [HttpGet]
        public ActionResult Editar(int idSolicitud)
        {
            try
            {
                using (Conection())
                {
                    Solicitud au1 = db.Solicitud.Find(idSolicitud);
                    return View(au1);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }//Fin Get-editar
        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Editar(Solicitud s)
        {
            try
            {
                using (Conection())
                {
                    Solicitud aux = db.Solicitud.Find(s.idSolicitud);
                    aux.Fecha = s.Fecha;
                    aux.Lote = s.Lote;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error encontrado: " + e.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Detalles(int idSolicitud)
        {
            try
            {
                using (Conection())
                {
                    Solicitud s1 = db.Solicitud.Find(idSolicitud);
                    return View(s1);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }//Fin de los detalles

        public ActionResult Eliminar(int idSolicitud)
        {
            try
            {
                using (Conection())
                {
                    Solicitud s1 = db.Solicitud.Find(idSolicitud);
                    db.Solicitud.Remove(s1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }

        //Conectarse al contexto
        public AgenciaContext Conection()
        {
            this.db = new AgenciaContext();
            return db;
        }
    }
}