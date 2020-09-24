using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        [Required]
        [StringLength(100)]
        public string Email { set; get; }
        [Required]
        [StringLength(100)]
        public string HashPwd { set; get; }
        [Required]
        public Empresa Empresa { set; get; }
    }
}