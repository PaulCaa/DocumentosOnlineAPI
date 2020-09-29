using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class UsuarioSector {

        [Key]
        public string UsuarioId { set; get; }
        public Usuario Usuario { set; get; }
        [Key]
        public int SectorId { set; get; }
        public Sector Sector { set; get; }

        public override string ToString(){
            return "{\"UsuarioId\": \"" + UsuarioId + "\", "
            + "\"SectorId\": \"" + SectorId + "\"}";
        }
    }
}