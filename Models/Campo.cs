using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models{
    public class Campo
    {
        [Key]
        public int idCampo { set; get; }
        [Required]
        [StringLength(30)]
        public string Nombre { set; get; }
        [Required]
        [StringLength(50)]
        public string Valor { set; get; }
    }
}