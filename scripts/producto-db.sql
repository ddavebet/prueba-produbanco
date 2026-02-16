create database [producto-db] collate Modern_Spanish_CI_AS
go

use [producto-db]
go

grant connect on database :: [producto-db] to dbo
go

grant view any column encryption key definition, view any column master key definition on database :: [producto-db] to [public]
go

create table dbo.Productos
(
    Id          uniqueidentifier                           not null
        constraint PK_Productos
            primary key,
    Nombre      nvarchar(100) collate Modern_Spanish_CI_AS not null,
    Descripcion nvarchar(200) collate Modern_Spanish_CI_AS,
    Categoria   int                                        not null,
    Imagen      nvarchar(500) collate Modern_Spanish_CI_AS,
    Precio      decimal(18, 2)                             not null,
    Stock       int                                        not null
)
go

create table dbo.__EFMigrationsHistory
(
    MigrationId    nvarchar(150) collate Modern_Spanish_CI_AS not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32) collate Modern_Spanish_CI_AS  not null
)
go