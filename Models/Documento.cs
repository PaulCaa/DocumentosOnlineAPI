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
        [StringLength(30)]
        public string Nombre { set; get; }
        public Empresa Empresa { set; get; }
        public Sector Sector { set; get; }
        public List<Campo> Campos { set; get; }
    }
}