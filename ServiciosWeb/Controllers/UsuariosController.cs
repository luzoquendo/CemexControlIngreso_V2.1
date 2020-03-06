using ServiciosWeb.Models;
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
        CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();
        
        public IEnumerable<Usuarios> Get()
        {
                var nUsuario = db.Usuarios.ToList();
            return nUsuario; ;
        }
    }
}
