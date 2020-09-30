INSERT INTO dbo.Empresas (Nombre, Direccion, Telefono, Web) VALUES ('Caamaño´s Corp','Avenida Siempreviva 742, CABA, Argentina','46263312','http://caamanocorp.com');
SELECT * FROM dbo.Empresas;


INSERT INTO dbo.Sectores (Nombre, EmpresaId) VALUES ('Recursos Humanos',1);
INSERT INTO dbo.Sectores (Nombre, EmpresaId) VALUES ('Contaduria',1);
INSERT INTO dbo.Sectores (Nombre, EmpresaId) VALUES ('Proveedores',1);
SELECT * FROM dbo.Sectores;

INSERT INTO dbo.Usuarios VALUES ('pcaamano','Pablo','Caamano','pcaamano@caamanocorp.com','5f4dcc3b5aa765d61d8327deb882cf99',1);
INSERT INTO dbo.Usuarios VALUES ('msflores','Maria Sol','Flores','msflores@caamanocorp.com','5f4dcc3b5aa765d61d8327deb882cf99',1);
SELECT * FROM dbo.Usuarios;

INSERT INTO dbo.UsuarioSectores VALUES ('pcaamano',1);
INSERT INTO dbo.UsuarioSectores VALUES ('pcaamano',2);
INSERT INTO dbo.UsuarioSectores VALUES ('pcaamano',3);
INSERT INTO dbo.UsuarioSectores VALUES ('msflores',1);
SELECT * FROM dbo.UsuarioSectores


INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('R038000950135','2020-04-22','/sistemas/resources/E1S3R038000950135.pdf',1,3);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('L009903','2020-04-22','/sistemas/resources/E1S1L009903.pdf',1,1);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('00721532K','2020-04-29','/sistemas/resources/E1S200721532K.pdf',1,2);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('N039225','2020-05-01','/sistemas/resources/E1S2N039225.pdf',1,2);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('L009904','2020-05-03','/sistemas/resources/E1S1L009904.pdf',1,1);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('R033000909301','2020-05-11','/sistemas/resources/E1S3R033000909301.pdf',1,3);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('R039000930991','2020-05-24','/sistemas/resources/E1S3R039000930991.pdf',1,3);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('L002239','2020-05-25','/sistemas/resources/E1S1L002239.pdf',1,1);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('R038000950116','2020-05-30','/sistemas/resources/E1S3R038000950116.pdf',1,3);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('N037490','2020-06-03','/sistemas/resources/E1S2N037490.pdf',1,2);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('00759221K','2020-06-03','/sistemas/resources/E1S200759221K.pdf',1,2);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('R03800096610','2020-06-20','/sistemas/resources/E1S3R03800096610.pdf',1,3);
INSERT INTO dbo.Documentos (Numero,Fecha,ImgPath,EmpresaId,SectorId) VALUES ('L006945','2020-06-22','/sistemas/resources/E1S1L006945.pdf',1,1);
SELECT * FROM dbo.Documentos;




