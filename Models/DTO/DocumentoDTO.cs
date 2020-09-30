using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models.DTO {
    public class DocumentoDTO {
        public int DocumentoId { set; get; }
        public string Numero { set; get; }
        public string Fecha { set; get; }
        public string ImgPath { set; get; }
        public int EmpresaId { set; get; }
        public int SectorId { set; get; }

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