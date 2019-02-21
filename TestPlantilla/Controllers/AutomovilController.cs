using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPlantilla.Models;

namespace TestPlantilla.Controllers
{
    public class AutomovilController : Controller
    {
        // GET: Automovil
        public ActionResult Index()
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = Conection())
                {
                    /*Utilizamos una vista parcial por la plantilla*/
                    return PartialView(db.Automovil.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }

        [HttpGet]
        //Metodo post para crear el usuario
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Recibe el usuario y lo ingresa a la BD por medio de ORM
        public ActionResult Agregar(Automovil a)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = Conection())
                {
                    db.Automovil.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }//Fin de Agregas post...

        [HttpGet]
         public ActionResult Editar(int Folio)
        {
            try
            {
                using (var db = Conection())
                {
                    Automovil au1 = db.Automovil.Find(Folio);
                    return View(au1);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }//Fin Get-editar

        public ActionResult Editar(Automovil au)
        {
            try
            {
                using (var db = Conection())
                {
                    Automovil aux = db.Automovil.Find(au.Folio);
                    aux.Marca = au.Marca;
                    aux.Modelo = au.Modelo;
                    aux.TipoTransmision = au.TipoTransmision;
                    aux.Color = au.Color;
                    aux.Estetica = au.Estetica;
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

        public ActionResult Detalles(int Folio)
        {
            try
            {
                using (var db = Conection())
                {
                    Automovil a1 = db.Automovil.Find(Folio);
                    return View(a1);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error encontrado: " + ex.Message);
                return View();
            }
        }

        public ActionResult Eliminar(int Folio)
        {
            try
            {
                using (var db = Conection())
                {
                    Automovil a1 = db.Automovil.Find(Folio);
                    db.Automovil.Remove(a1);
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

        //Retorna una conexion a la base de datos
        public AgenciaContext Conection()
        {
            return new AgenciaContext();
        }
    }
}