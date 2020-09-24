using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Sector
    {
        [Key]
        public int IdSector { set; get; }
        [Required]
        [StringLength(50)]
        public string Nombre { set; get; }
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { set; get; }
    }
}