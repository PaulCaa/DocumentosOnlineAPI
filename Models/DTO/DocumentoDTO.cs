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
        public string Empresa { set; get; }
        public string Sector { set; get; }

        public override string ToString(){
            return "{\"DocumentoId\": \"" + DocumentoId + "\", "
            + "\"Numero\": \"" + Numero + "\", "
            + "\"Fecha\": \"" + Fecha + "\", "
            + "\"ImgPath\": \"" + ImgPath + "\", "
            + "\"Empresa\": \"" + Empresa + "\", "
            + "\"Sector\": \"" + Sector + "\"}";
        }
    }
}