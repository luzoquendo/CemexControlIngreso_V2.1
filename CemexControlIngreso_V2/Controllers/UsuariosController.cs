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
        public IEnumerable<Usuarios> Get(DateTime fecha)
        {

                CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();
                var listado = db.Usuarios.ToList().Where(x => x.FechaCreacion >= fecha);
                return listado;
        }

        [HttpGet]
        public IEnumerable<Usuarios> Get()
        {
            CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();
            var listado = db.Usuarios.ToList();
            return listado;
        }
    }
}
