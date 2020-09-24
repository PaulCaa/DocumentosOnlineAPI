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
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { set; get; }
        [ForeignKey("IdSector")]
        public Sector Sector { set; get; }
        [ForeignKey("IdDocumento")]
        public Documento Documento { set; get; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { set; get; }
    }
}