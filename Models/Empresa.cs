using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { set; get; }
        [StringLength(100)]
        public string Nombre { set; get; }
        public string Descripcion { set; get; }

    }
}