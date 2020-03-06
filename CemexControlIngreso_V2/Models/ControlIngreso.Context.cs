﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CONTROLINGRESOEntities3 : DbContext
    {
        public CONTROLINGRESOEntities3()
            : base("name=CONTROLINGRESOEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CHECKLIST> CHECKLIST { get; set; }
        public virtual DbSet<CONDUCTOR> CONDUCTOR { get; set; }
        public virtual DbSet<CONDUCTORESDESCANSANDO> CONDUCTORESDESCANSANDO { get; set; }
        public virtual DbSet<CORREDOR> CORREDOR { get; set; }
        public virtual DbSet<INSTRUCTOR> INSTRUCTOR { get; set; }
        public virtual DbSet<PLACAS> PLACAS { get; set; }
        public virtual DbSet<PRODUCTO> PRODUCTO { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TIEMPOENTRENODOS> TIEMPOENTRENODOS { get; set; }
        public virtual DbSet<TIEMPOSDEDESCANSO> TIEMPOSDEDESCANSO { get; set; }
        public virtual DbSet<TIPOTRAILER> TIPOTRAILER { get; set; }
        public virtual DbSet<TRAILER> TRAILER { get; set; }
        public virtual DbSet<VEHICULOSRUTA> VEHICULOSRUTA { get; set; }
        public virtual DbSet<VIAJE> VIAJE { get; set; }
        public virtual DbSet<VIAJECTRL> VIAJECTRL { get; set; }
        public virtual DbSet<VIAJESPORCONDUCTOR> VIAJESPORCONDUCTOR { get; set; }
        public virtual DbSet<Descanso> Descanso { get; set; }
        public virtual DbSet<ROLES> ROLES { get; set; }
        public virtual DbSet<USUARIOSROLE> USUARIOSROLE { get; set; }
        public virtual DbSet<BIOMETRICO> BIOMETRICO { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    
        public virtual int ActualizarViaje(Nullable<int> numeroViaje)
        {
            var numeroViajeParameter = numeroViaje.HasValue ?
                new ObjectParameter("NumeroViaje", numeroViaje) :
                new ObjectParameter("NumeroViaje", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarViaje", numeroViajeParameter);
        }
    
        public virtual ObjectResult<RepTiempoEntreNodos_Result> RepTiempoEntreNodos(Nullable<System.DateTime> fechaInicial, Nullable<System.DateTime> fechaFinal)
        {
            var fechaInicialParameter = fechaInicial.HasValue ?
                new ObjectParameter("FechaInicial", fechaInicial) :
                new ObjectParameter("FechaInicial", typeof(System.DateTime));
    
            var fechaFinalParameter = fechaFinal.HasValue ?
                new ObjectParameter("FechaFinal", fechaFinal) :
                new ObjectParameter("FechaFinal", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RepTiempoEntreNodos_Result>("RepTiempoEntreNodos", fechaInicialParameter, fechaFinalParameter);
        }
    
        public virtual ObjectResult<RepVehiculosEnRuta_Result> RepVehiculosEnRuta()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RepVehiculosEnRuta_Result>("RepVehiculosEnRuta");
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<TraerConductor_Result> TraerConductor(string cedula)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("Cedula", cedula) :
                new ObjectParameter("Cedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TraerConductor_Result>("TraerConductor", cedulaParameter);
        }
    
        public virtual ObjectResult<TraerConductorDisponible_Result> TraerConductorDisponible(string cedula)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("Cedula", cedula) :
                new ObjectParameter("Cedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TraerConductorDisponible_Result>("TraerConductorDisponible", cedulaParameter);
        }
    
        public virtual ObjectResult<TraerConductorId_Result> TraerConductorId(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TraerConductorId_Result>("TraerConductorId", idParameter);
        }
    
        public virtual ObjectResult<string> TraerInstructor(string id)
        {
            var idParameter = id != null ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("TraerInstructor", idParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> TraerViajeConductorId(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("TraerViajeConductorId", idParameter);
        }
    
        public virtual ObjectResult<TraerViajeId_Result> TraerViajeId(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TraerViajeId_Result>("TraerViajeId", idParameter);
        }
    
        public virtual ObjectResult<TraerViajePlaca_Result> TraerViajePlaca(string placa)
        {
            var placaParameter = placa != null ?
                new ObjectParameter("Placa", placa) :
                new ObjectParameter("Placa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TraerViajePlaca_Result>("TraerViajePlaca", placaParameter);
        }
    
        public virtual ObjectResult<string> TraerInstructorIdCond(string id)
        {
            var idParameter = id != null ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("TraerInstructorIdCond", idParameter);
        }
    }
}
