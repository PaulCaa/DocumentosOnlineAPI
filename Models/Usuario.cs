using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class Usuario {
        [Key]
        public int UsuarioId { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        [Required]
        [StringLength(100)]
        public string Email { set; get; }
        [Required]
        [StringLength(100)]
        public string HashPwd { set; get; }
        [ForeignKey("EmpresaId")]
        public Empresa Empresa { set; get; }
        public List<Permiso> Permisos { set; get; }

        public override string ToString(){
            return "{\"UsuarioId\": \"" + UsuarioId + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"Apellido\": \"" + Apellido + "\", "
            + "\"Email\": \"" + Email + "\", "
            + "\"HashPwd\": \"" + HashPwd + "\", "
            + "\"Empresa\": \"" + Empresa.ToString() + "\", "
            + "\"Permisos\": \"" + Permisos.ToString() + "\"}";
        }
    }
}