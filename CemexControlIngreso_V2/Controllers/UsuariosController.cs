using CemexControlIngreso_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CemexControlIngreso_V2.Controllers
{
    public class UsuariosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Add(Models.Usuarios model)
        {
            using (CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3())
            {
                var nUsuario = new Models.Usuarios();
                nUsuario.CodigoUsuario = model.CodigoUsuario;
                nUsuario.Contraseña = model.Contraseña;
                nUsuario.Nombre = model.Nombre;
                db.Usuarios.Add(nUsuario);
                db.SaveChanges();
            }
            return Ok("Exito");
        }
    }
}
