//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CemexControlIngreso_V2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TIEMPOSDEDESCANSO
    {
        public int Id { get; set; }
        public string Conductor { get; set; }
        public int NumeroViaje { get; set; }
        public System.DateTime FechaEntradaDescanso { get; set; }
        public System.DateTime FechaSalidaDescanso { get; set; }
        public string CorredorVial { get; set; }
    }
}
