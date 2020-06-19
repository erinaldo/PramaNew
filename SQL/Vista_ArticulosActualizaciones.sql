use PramaSQL
go
create view Vista_ArticulosActualizaciones
as
select AA.IdArticuloActualizacion, AA.Fecha, AA.IdRubroArticulo, R.RubroArticulo, AA.IdSubrubroArticulo, S.SubrubroArticulo,
AA.Aumento, AA.Descuento, AA.IdUsuario, u.Usuario
from ArticulosActualizaciones as AA, RubrosArticulos as R, SubrubrosArticulos as S, Usuarios as U
where AA.IdRubroArticulo = r.IdRubroArticulo
and AA.IdSubrubroArticulo = s.IdSubrubroArticulo
and aa.IdUsuario = u.IdUsuario
