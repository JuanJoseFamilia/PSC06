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
            //abrimos la base de datos 
            using (var db = new DBMVCEntities())
            {
                var oUser = db.USERS.Find(Id);//Aqui se busca mediante la id el registro a eliminar
                oUser.idEstatus = 3;
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content( "1" );//retornamos el contenido "1" 
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
            //abrimos la base de datos
            using (var db = new DBMVCEntities())
            {
                USERS oUser = new USERS();
                // se asignan los valores que tomamos del modelo a oUSer
                oUser.idEstatus = 1;
                oUser.Nombre = model.Nombre;
                oUser.Usuario = model.Usuario;
                oUser.Email = model.Email;
                oUser.Password = model.Password;
                oUser.Edad = model.Edad;

                db.USERS.Add(oUser);//le dicimos que agrege esos valores a la tabla
                db.SaveChanges();// guardamos cambios
            }
            return Redirect(Url.Content("~/User/Query"));//redirigimos a la tabla principal
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditUserViewModels model = new EditUserViewModels();
            //abrimos la base de datos
            using (var db = new DBMVCEntities())
            {
                var oUser = db.USERS.Find(id);
                // se asignan los valores que tomamos del oUser al modelo
                model.Id = id;
                model.Nombre = oUser.Nombre;
                model.Usuario= oUser.Usuario;
                model.Email = oUser.Email;
                model.Password = oUser.Password;  
                model.Edad = oUser.Edad;
                
                return View(model);//devolvemos la vista con el modelo

            }
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModels model)
        {
            //se valiad si el estado del modelo es valiodo y se retorna
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //abrimos la base de datos
            using (var db = new DBMVCEntities())
            {
                var oUser = db.USERS.Find(model.Id);
                // se asignan los valores que tomamos del modelo al oUser
                oUser.Nombre= model.Nombre;
                oUser.Usuario = model.Usuario;
                oUser.Email = model.Email;
                oUser.Edad = model.Edad;
                //validamos si la password es nula o si esta vacia
                if (model.Password != null || model.Password != "")
                {
                    oUser.Password = model.Password;
                }
                //usamos modified para modificar los campos de nuestro registro
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();//guardamos cambios
            }
            return Redirect(Url.Content("~/User/Query"));//redirigimos a la tabla principal
        }
        


    }
}