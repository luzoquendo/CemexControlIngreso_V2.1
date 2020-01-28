using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CemexControlIngreso_V2.Models
{
    public class SalidaViewModel
    {
        [Required]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Nombre")]

        public string Nombre { get; set; }

        public string Celular1 { get; set; }
        public int IdPlaca { get; set; }
        public int IdTrailer { get; set; }
        public int IdCorredor { get; set; }
        public int IdInstructor { get; set; }
        public string Alcohotest { get; set; }

        public string Observaciones { get; set; }

        public virtual PLACAS PLACAS { get; set; }

        public virtual TRAILER TRAILER { get; set; }

        public virtual CORREDOR CORREDOR { get; set; }

        public virtual INSTRUCTOR INSTRUCTOR { get; set; }
    }
}