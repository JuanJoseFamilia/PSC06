using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSC06.Models;
using PSC06.Models.ViewModels;

namespace PSC06.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Query()
        {
            List<QueryViewModels> lsit = null;
            using (DBMVCEntities db = new DBMVCEntities())
            {
                //SELECT * FROM USERS WHERE IDESTATUS == 1
                lsit = (from d in db.USERS
                        where d.idEstatus == 1
                        orderby d.Nombre

                        select new QueryViewModels
                        {
                            Id = d.ID,
                            Nombre = d.Nombre,
                            Usuario = d.Usuario,
                            Email = d.Email,
                            Password = d.Password,
                            Edad= d.Edad,
                            idEstatus = d.idEstatus,
                            
                        }).ToList();
            }
            return View(lsit);//aqui retornamos la data a la vista llamada Query
        }

        public ActionResult Delete(int Id)
        {
            using (var db = new DBMVCEntities())
            {
                var oUser = db.USERS.Find(Id);
                oUser.idEstatus = 3;
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content( "1" );
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddUserViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new DBMVCEntities())
            {
                USERS oUser = new USERS();

                oUser.idEstatus = 1;
                oUser.Nombre = model.Nombre;
                oUser.Usuario = model.Usuario;
                oUser.Email = model.Email;
                oUser.Password = model.Password;
                oUser.Edad = model.Edad;

                db.USERS.Add(oUser);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User/Query"));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditUserViewModels model = new EditUserViewModels();

            using(var db = new DBMVCEntities())
            {
                var oUser = db.USERS.Find(id);

                model.Id = id;
                model.Nombre = oUser.Nombre;
                model.Usuario= oUser.Usuario;
                model.Email = oUser.Email;
                model.Password = oUser.Password;  
                model.Edad = oUser.Edad;
                
                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new DBMVCEntities())
            {
                var oUser = db.USERS.Find(model.Id);

                oUser.Nombre= model.Nombre;
                oUser.Usuario = model.Usuario;
                oUser.Email = model.Email;
                oUser.Edad = model.Edad;

                if (model.Password != null || model.Password != "")
                {
                    oUser.Password = model.Password;
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User/Query"));
        }
        


    }
}