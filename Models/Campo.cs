using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models{
    public class Campo
    {
        [Key]
        public int idCampo { set; get; }
        public string Nombre { set; get; }
        public string Valor { set; get; }
    }
}