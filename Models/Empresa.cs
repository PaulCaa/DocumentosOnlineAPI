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
        [Column("Descripcion", TypeName = "nText")]
        public string Descripcion { set; get; }

        public override string ToString(){
            return "{\"EmpresaId\": \"" + EmpresaId + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"Descripcion\": \"" + Descripcion + "\"}";
        }
    }
}