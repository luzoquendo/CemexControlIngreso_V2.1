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
    
    public partial class TRAILER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRAILER()
        {
            this.VIAJE = new HashSet<VIAJE>();
            this.VIAJE1 = new HashSet<VIAJE>();
            this.VIAJECTRL = new HashSet<VIAJECTRL>();
        }
    
        public int IdTrailer { get; set; }
        public string PlacaTrailer { get; set; }
        public bool Estado { get; set; }
        public int IdTipoTrailer { get; set; }
    
        public virtual TIPOTRAILER TIPOTRAILER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIAJE> VIAJE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIAJE> VIAJE1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIAJECTRL> VIAJECTRL { get; set; }
    }
}
