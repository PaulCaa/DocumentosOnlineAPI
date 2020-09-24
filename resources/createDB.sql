USE master;
CREATE DATABASE DocumentosOnline
ON PRIMARY(
	NAME='DOCUMENTOS_ONLINE_PRIMARY',
	FILENAME='D:\Program files\MSSQL\DocumentosOnline.mdf',
	SIZE=10MB,
	MAXSIZE=100MB,
	FILEGROWTH=1MB
)LOG ON(
    NAME='Documentos_Online_log',
    FILENAME='D:\Program files\MSSQL\DocumentosOnline.ldf',
	SIZE=10MB,
	MAXSIZE=100MB,
	FILEGROWTH=1MB
);
GO