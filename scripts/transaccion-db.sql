create database [transaccion-db]
go

use [transaccion-db]
go

grant connect on database :: [transaccion-db] to dbo
go

grant view any column encryption key definition, view any column master key definition on database :: [transaccion-db] to [public]
go

create table dbo.Transacciones
(
    Id             uniqueidentifier not null
        constraint PK_Transacciones
            primary key,
    Fecha          datetime2        not null,
    Tipo           int              not null,
    ProductoId     uniqueidentifier not null,
    Cantidad       int              not null,
    PrecioUnitario decimal(18, 2)   not null,
    PrecioTotal    decimal(18, 2)   not null,
    Detalle        nvarchar(500) collate Modern_Spanish_CI_AS
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