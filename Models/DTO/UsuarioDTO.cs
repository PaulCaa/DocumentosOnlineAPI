using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models.DTO {
    public class UsuarioDTO {
        public string UsuarioId { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Email { set; get; }
        public string HashPwd { set; get; }
        public int EmpresaId { set; get; }

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