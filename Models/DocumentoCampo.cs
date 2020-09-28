using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class DocumentoCampo {
        [ForeignKey("DocumentoId")]
        public int DocumentoId { set; get; }
        public Documento Documento { set; get; }
        [ForeignKey("CampoId")]
        public int CampoId { set; get; }
        public Campo Campo { set; get; }

        public override string ToString(){
            return "{\"DocumentoId\": \"" + DocumentoId + "\", "
            + "\"Documento\": \"" + Documento.ToString() + "\", "
            + "\"CampoId\": \"" + CampoId + "\", "
            + "\"Campo\": \"" + Campo.ToString() + "\"}";
        }
    }
}