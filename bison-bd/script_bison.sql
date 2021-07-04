-- --------------------------------------------------
-- script_bison.sql
-- --------------------------------------------------

GO -- >>

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'bison')
BEGIN

	CREATE DATABASE bison

END

GO -- >>

USE bison

GO -- >>

-- --------------------------------------------------
-- schemas -- SELECT * FROM sys.schemas
-- --------------------------------------------------

GO -- >>

IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = 'seg')
BEGIN

	EXEC('CREATE SCHEMA seg')

END

GO -- >>

IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = 'cat')
BEGIN

	EXEC('CREATE SCHEMA cat')

END

GO -- >>

IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = 'log')
BEGIN

	EXEC('CREATE SCHEMA log')

END

GO -- >>

IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = 'bis')
BEGIN

	EXEC('CREATE SCHEMA bis')

END

GO -- >>

-- --------------------------------------------------
-- tables
-- --------------------------------------------------

GO -- >>

IF OBJECT_ID ('cat.rol') IS NOT NULL
	DROP TABLE cat.rol

GO -- >>

CREATE TABLE cat.rol
(
	id_rol			INT IDENTITY(1, 1) PRIMARY KEY,
	nombre			VARCHAR(50) NOT NULL,
	activo			BIT NOT NULL DEFAULT 1,
	fecha_alta		DATETIME NOT NULL DEFAULT GETDATE(),
	fecha_mod		DATETIME NOT NULL DEFAULT GETDATE()
)

GO -- >>

IF OBJECT_ID ('seg.usuario') IS NOT NULL
	DROP TABLE seg.usuario

GO -- >>

CREATE TABLE seg.usuario
(
	id_usuario		INT IDENTITY(1, 1) PRIMARY KEY,
	id_rol			INT NOT NULL,
	nick			VARCHAR(50) NOT NULL,
	pass			VARCHAR(50) NOT NULL,
	email			VARCHAR(150) NOT NULL,
	nombre			VARCHAR(50) NULL,
	ap_paterno		VARCHAR(50) NULL,
	ap_materno		VARCHAR(50) NULL,
	activo			BIT NOT NULL DEFAULT 1,
	ruta_imagen		VARCHAR(250) NOT NULL DEFAULT 'assets/user_image_default.png',
	theme			VARCHAR(30) NULL,
	fecha_alta		DATETIME NOT NULL DEFAULT GETDATE(),
	fecha_mod		DATETIME NOT NULL DEFAULT GETDATE()
)

GO -- >>

-- --------------------------------------------------
-- functions
-- --------------------------------------------------

-- --------------------------------------------------
-- views
-- --------------------------------------------------

-- --------------------------------------------------
-- sp
-- --------------------------------------------------

GO -- >>

IF OBJECT_ID ('cat.sp_rol_create') IS NOT NULL
	DROP PROCEDURE cat.sp_rol_create

GO -- >>

CREATE PROCEDURE cat.sp_rol_create -- cat.sp_rol_create ''
(
	@nombre		VARCHAR(50)
)
AS
BEGIN

	INSERT INTO cat.rol
	(
		nombre
	)
	VALUES
	(
		@nombre
	)

END

GO -- >>

-- --------------------------------------------------
-- inserts
-- --------------------------------------------------

GO -- >>

SET NOCOUNT ON

EXEC cat.sp_rol_create 'admin'
EXEC cat.sp_rol_create 'usuario'

-- SELECT * FROM cat.rol

SET NOCOUNT OFF


GO -- >>