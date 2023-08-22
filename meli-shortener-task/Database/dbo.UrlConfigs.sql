CREATE TABLE [dbo].[UrlConfigs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UrlCorta] NCHAR(10) NULL,
    [UrlLarga] NCHAR(3000) NULL,
    [FechaCreacion] DATETIME NULL,
    [FechaExpira] DATETIME NULL,
    [Habilitado] TINYINT NULL,
    [NumVisitas] BIGINT NULL
)
