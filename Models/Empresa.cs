using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class Empresa {
        [Key]
        public int EmpresaId { set; get; }
        [Required]
        [StringLength(100)]
        public string Nombre { set; get; }
        [StringLength(50)]
        public string Direccion { set; get; }
        [StringLength(12)]
        public string Telefono { set; get; }
        [StringLength(50)]
        public string Web { set; get; }
        public List<Sector> Sectores { set; get; }
        public List<Documento> Documentos { set; get; }
        public List<Usuario> Usuarios { set; get; }

        public override string ToString(){
            return "{\"EmpresaId\": \"" + EmpresaId + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"Direccion\": \"" + Direccion + "\", "
            + "\"Telefono\": \"" + Telefono + "\", "
            + "\"Web\": \"" + Web + "\"}";
        }
    }
}