using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models.DTO {
    public class SectorDTO {
        public int SectorId { set; get; }
        public string Nombre { set; get; }
        public int EmpresaId { set; get; }

        public override string ToString(){
            return "{\"SectorId\": \"" + SectorId + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"EmpresaId\": \"" + EmpresaId + "\"}";
        }
    }
}