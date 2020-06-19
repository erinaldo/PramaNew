create table MigrarCodigos
(
IdMigrarCodigo int identity (1,1) not null,
IdArticulo int,
CodigoNuevo varchar(10),
CodigoAnt int,

constraint PK_IdMigrarCodigo primary key (IdMigrarCodigo)
)