using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Documento
    {
        [Key]
        public int IdDocumento { set; get; }
        [StringLength(50)]
        public string Nombre { set; get; }
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { set; get; }
        [ForeignKey("IdSector")]
        public Sector Sector { set; get; }
        [Required]
        public List<Campo> Campos { set; get; }
    }
}