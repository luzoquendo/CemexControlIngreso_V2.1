using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios.Controllers
{
    public class UsuariosController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Add(Models.Request.UsuarioRequest model)
        {
            using (Models.CONTROLINGRESOEntities db = new Models.CONTROLINGRESOEntities())
            {
                var nUsuario = new Models.AspNetUsers();
                nUsuario.UserName = model.Nombre;
                nUsuario.Email = model.Email;
                db.AspNetUsers.Add(nUsuario);
                db.SaveChanges();
            }
            return Ok("Exito");
        }
    }
}
