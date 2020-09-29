INSERT INTO dbo.Empresas (Nombre, Direccion, Telefono, Web) VALUES ('Caamaño´s Corp','Avenida Siempreviva 742, CABA, Argentina','46263312','http://caamanocorp.com');
SELECT * FROM dbo.Empresas;


INSERT INTO dbo.Sectores (Nombre, EmpresaId) VALUES ('Recursos Humanos',1);
INSERT INTO dbo.Sectores (Nombre, EmpresaId) VALUES ('Contaduria',1);
INSERT INTO dbo.Sectores (Nombre, EmpresaId) VALUES ('Proveedores',1);
SELECT * FROM dbo.Sectores;

INSERT INTO dbo.Usuarios VALUES ('pcaamano','Pablo','Caamano','pcaamano@caamanocorp.com','5f4dcc3b5aa765d61d8327deb882cf99',1);
INSERT INTO dbo.Usuarios VALUES ('msflores','Maria Sol','Flores','msflores@caamanocorp.com','5f4dcc3b5aa765d61d8327deb882cf99',1);
SELECT * FROM dbo.Usuarios;