using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class UsuarioDocumento
    {
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { set; get; }
        [ForeignKey("IdDocumento")]
        public Documento Documento { set; get; }
    }
}