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
        [StringLength(50)]
        public string Nombre { set; get; }
        public Empresa Empresa { set; get; }
    }
}