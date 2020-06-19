USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Detalle_Presu_ABM]    Script Date: 02/09/2017 10:41:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vista_Detalle_Presu_ABM]
AS
SELECT     dbo.DetallePresupuestos.IdArticulo, dbo.DetallePresupuestos.Codigo_Articulo AS CodigoArticulo, dbo.DetallePresupuestos.Cantidad, 
                      dbo.DetallePresupuestos.Descripcion AS Articulo, dbo.Productos.IdProducto AS IdPropio, 'PRODUCTOS' AS Tabla, dbo.DetallePresupuestos.PrecioUnitario AS Precio, 
                      dbo.Articulos.PorcentajeIva AS IVA, dbo.Productos.IdCoeficienteArticulo AS Coeficiente, dbo.CoeficientesArticulos.CoeficientePublico AS Pub, 
                      dbo.CoeficientesArticulos.CoeficienteDistribuidor AS Dist, dbo.CoeficientesArticulos.CoeficienteRevendedor AS Rev, dbo.DetallePresupuestos.IdDetPresupuesto, 
                      dbo.DetallePresupuestos.IdPresupuesto
FROM         dbo.DetallePresupuestos INNER JOIN
                      dbo.Productos ON dbo.DetallePresupuestos.IdArticulo = dbo.Productos.IdArticulo INNER JOIN
                      dbo.CoeficientesArticulos ON dbo.Productos.IdCoeficienteArticulo = dbo.CoeficientesArticulos.IdCoeficienteArticulo INNER JOIN
                      dbo.Articulos ON dbo.DetallePresupuestos.IdArticulo = dbo.Articulos.IdArticulo

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[15] 2[25] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DetallePresupuestos"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Productos"
            Begin Extent = 
               Top = 125
               Left = 240
               Bottom = 244
               Right = 429
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CoeficientesArticulos"
            Begin Extent = 
               Top = 6
               Left = 467
               Bottom = 125
               Right = 669
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Articulos"
            Begin Extent = 
               Top = 6
               Left = 707
               Bottom = 125
               Right = 890
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
    ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Detalle_Presu_ABM'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Detalle_Presu_ABM'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Detalle_Presu_ABM'
GO

