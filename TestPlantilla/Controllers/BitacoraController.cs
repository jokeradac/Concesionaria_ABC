using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPlantilla.Models;

namespace TestPlantilla.Controllers
{
    public class BitacoraController : Controller
    {
        private AgenciaContext db;
        // GET: Bitacora
        public ActionResult Index()
        {
            try
            {
                using (Conection())
                {
                    return View(db.Bitacora.ToList());
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error encontrado: " + e.Message);
                return View();
            }
        }//Fin del index
        
        [HttpGet]
        public  ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Bitacora b)
        {
            try
            {
                using (Conection())
                {
                    db.Bitacora.Add(b);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error encontrado: " + e.Message);
                return View();
            }
        }//Fin Crear-POST

        //Listar los lotes de una solicitud
        public ActionResult ListarSolicitudes()
        {
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
        }

        //Listar la tabla de solicitudes
        public ActionResult TablaSolicitudes()
        {
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
        }

        //Listar automoviles
        public ActionResult ListarAutomoviles()
        {
            try
            {
                using (Conection())
                {
                    return PartialView(db.Automovil.ToList());
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error encontrado: " + e.Message);
                return View();
            }
        }//Fin ListarAutomoviles

        //Listar tabla de automoviles
        public ActionResult TablaAutomoviles()
        {
            try
            {
                using (Conection())
                {
                    return PartialView(db.Automovil.ToList());
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error encontrado: " + e.Message);
                return View();
            }
        }

        //Editar una bitacora
        public ActionResult Editarb(int bitacora)
        {
            try
            {
                using (var db = Conection())
                {
                    Bitacora bita = db.Bitacora.Find(bitacora);
                    return View(bita);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }// Fin get-editar

        public ActionResult Editarb(Bitacora bt)
        {
            try
            {
                using (var db = Conection())
                {
                    Bitacora aux = db.Bitacora.Find(bt.idBitacora);
                    aux.Automovil = bt.Automovil;
                    aux.Solicitud = bt.Solicitud;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }// Fin de Editar


        public ActionResult Detalles(int bitacora)
        {
            try
            {
                using (var db = Conection())
                {
                    Bitacora a1 = db.Bitacora.Find(bitacora);
                    return View(a1);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }//Fin Detalles

        public ActionResult Eliminar(int bitacora)
        {
            try
            {
                using (var db = Conection())
                {
                    Bitacora a1 = db.Bitacora.Find(bitacora);
                    db.Bitacora.Remove(a1);
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

        public AgenciaContext Conection()
        {
            this.db = new AgenciaContext();
            return db;
        }
    }//Fin clase BitacoraController
}