using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Permiso
    {
        [Key]
        public int IdPermiso { set; get; }
        public Empresa Empresa { set; get; }
        public Sector Sector { set; get; }
        public Documento Documento { set; get; }
        public Usuario Usuario { set; get; }
    }
}