using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class Documento {
        [Key]
        public int DocumentoId { set; get; }
        [StringLength(50)]
        public string Numero { set; get; }
        public DateTime Fecha { set; get; }
        public string ImgPath { set; get; }
        [ForeignKey("EmpresaId")]
        public int EmpresaId { set; get; }
        public Empresa Empresa { set; get; }
        [ForeignKey("SectorId")]
        public int SectorId { set; get; }
        public Sector Sector { set; get; }

        public override string ToString(){
            return "{\"DocumentoId\": \"" + DocumentoId + "\", "
            + "\"Numero\": \"" + Numero + "\", "
            + "\"Fecha\": \"" + Fecha + "\", "
            + "\"ImgPath\": \"" + ImgPath + "\", "
            + "\"EmpresaId\": \"" + EmpresaId + "\", "
            + "\"SectorId\": \"" + SectorId + "\"}";
        }
    }
}