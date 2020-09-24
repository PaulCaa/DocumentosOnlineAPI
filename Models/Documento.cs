using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Documento
    {
        [Key]
        public int DocumentoId { set; get; }
        [StringLength(50)]
        public string Nombre { set; get; }
        [ForeignKey("EmpresaId")]
        public Empresa Empresa { set; get; }
        [ForeignKey("SectorId")]
        public Sector Sector { set; get; }
        [Required]
        public List<DocumentoCampo> DocumentoCampos { set; get; }
    }
}