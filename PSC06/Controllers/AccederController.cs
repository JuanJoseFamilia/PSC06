using PSC06.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSC06.Controllers
{
    public class AccederController : Controller
    {
        // GET: Acceder
        public ActionResult Login()
        {
            ViewBag.OcultarFooter = true;
            return View();
        }

        public ActionResult Enter( string usuario, string password)
        {
            using(DBMVCEntities db = new DBMVCEntities())
            {
                //buscamos los registros en la tabla Users
                var read = from d in db.USERS
                           where d.Email == usuario
                           && d.Password == password
                           && d.idEstatus == 1
                           select d;
                
                if (read.Count() > 0)// verifica si existe el usuario si es mayor que 0
                {
                    
                    Session["Usuario"] = read.First();//se habre la seccion 
                    return Content("1");//enviamos valor al js de la vista
                }
                else
                {

                    //sms si no existe Usuario
                    return Content("Usuario no registrado");
                }
              
            }

        }
    }
}