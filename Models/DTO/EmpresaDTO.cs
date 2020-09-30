using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models.DTO {
    public class EmpresaDTO {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Direccion { set; get; }
        public string Telefono { set; get; }
        public string Web { set; get; }

        public override string ToString(){
            return "{\"Id\": \"" + Id + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"Direccion\": \"" + Direccion + "\", "
            + "\"Telefono\": \"" + Telefono + "\", "
            + "\"Web\": \"" + Web + "\"}";
        }
    }
}