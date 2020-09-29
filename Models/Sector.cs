using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models
{
    public class Sector
    {
        [Key]
        public int SectorId { set; get; }
        [Required]
        [StringLength(50)]
        public string Nombre { set; get; }
        [ForeignKey("EmpresaId")]
        public int EmpresaId { set; get; }
        public Empresa Empresa { set; get; }
        public List<UsuarioSector> UsuarioSectores { set; get; }
        public List<Documento> Documentos { set; get; }

        public override string ToString(){
            return "{\"SectorId\": \"" + SectorId + "\", "
            + "\"Nombre\": \"" + Nombre + "\", "
            + "\"EmpresaId\": \"" + EmpresaId + "\"}";
        }
    }
}