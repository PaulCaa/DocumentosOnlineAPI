using System;
using System.Collections.Generic;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;

namespace DocumentosOnlineAPI.Utils {
    public class ModelMapper {

        public static EmpresaDTO Map(Empresa m) {
            EmpresaDTO dto = new EmpresaDTO();
            dto.Id = m.EmpresaId;
            if(m.Nombre != null) dto.Nombre = m.Nombre;
            if(m.Direccion != null) dto.Direccion = m.Direccion;
            if(m.Telefono != null) dto.Telefono = m.Telefono;
            if(m.Web != null) dto.Web = m.Web;
            return dto;
        }
        public static Empresa Map(EmpresaDTO dto) {
            Empresa m = new Empresa();
            if(dto.Nombre != null) m.Nombre = dto.Nombre;
            if(dto.Direccion != null) m.Direccion = dto.Direccion;
            if(dto.Telefono != null) m.Telefono = dto.Telefono;
            if(dto.Web != null) m.Web = dto.Web;
            return m;
        }

        public static Sector Map(SectorDTO dto) {
            Sector m = new Sector();
            m.EmpresaId = dto.EmpresaId;
            if(dto.Nombre != null) m.Nombre = dto.Nombre;
            return m;
        }

        public static SectorDTO Map(Sector s) {
            SectorDTO dto = new SectorDTO();
            dto.SectorId = s.SectorId;
            dto.EmpresaId = s.EmpresaId;
            if(s.Nombre != null) dto.Nombre = s.Nombre;
            return dto;
        }

        public static DocumentoDTO Map(Documento d) {
            DocumentoDTO dto = new DocumentoDTO();
            dto.DocumentoId = d.DocumentoId;
            if(d.Numero != null) dto.Numero = d.Numero;
            if(d.Fecha != null) dto.Fecha = d.Fecha.ToString();
            if(d.ImgPath != null) dto.ImgPath = d.ImgPath;
            return dto;
        }

        public static Documento Map(DocumentoDTO dto) {
            Documento d = new Documento();
            if(dto.Numero != null) d.Numero = dto.Numero;
            if(dto.ImgPath != null) d.ImgPath = dto.ImgPath;
            d.Fecha = DateTime.Now;
            return d;
        }

        public static UsuarioDTO Map(Usuario u, string empresa) {
            UsuarioDTO dto = new UsuarioDTO();
            if(u.UsuarioId != null) dto.UsuarioId = u.UsuarioId;
            if(u.Nombre != null) dto.Nombre = u.Nombre;
            if(u.Apellido != null) dto.Apellido = u.Apellido;
            if(u.Email != null) dto.Email = u.Email;
            dto.HashPwd = "#########";
            dto.EmpresaId = u.EmpresaId;
            dto.NombreEmpresa = empresa;
            return dto;
        }

    }
}