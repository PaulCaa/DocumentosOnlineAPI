using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class Usuario {
        [Key]
        public string UsuarioId { set; get; }
        [StringLength(50)]
        public string Nombre { set; get; }
        [StringLength(50)]
        public string Apellido { set; get; }
        [Required]
        [StringLength(100)]
        public string Email { set; get; }
        [Required]
        [StringLength(50)]
        public string HashPwd { set; get; }
        [ForeignKey("EmpresaId")]
        public int EmpresaId { set; get; }
        public Empresa Empresa { set; get; }
        public List<UsuarioSector> UsuarioSectores { set; get; }

        public Usuario() {
            this.HashPwd = "5f4dcc3b5aa765d61d8327deb882cf99";
        }

        public override string ToString(){
            return "{\"UsuarioId\": \"" + UsuarioId + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"Apellido\": \"" + Apellido + "\", "
            + "\"Email\": \"" + Email + "\", "
            + "\"HashPwd\": \"" + HashPwd + "\", "
            + "\"EmpresaId\": \"" + EmpresaId + "\"}";
        }
    }
}