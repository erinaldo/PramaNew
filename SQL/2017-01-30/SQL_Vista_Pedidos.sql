USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Pedidos]    Script Date: 01/30/2017 17:36:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vista_Pedidos]
AS
SELECT     dbo.Pedidos.IdPedido, dbo.Pedidos.IdFormaPago, dbo.FormasPago.FormaPago, dbo.Pedidos.IdCliente, dbo.Clientes.Cuit, dbo.Clientes.ApeNom, 
                      dbo.Clientes.IdCondicionCompra, dbo.Pedidos.Punto, dbo.Pedidos.Nro, dbo.Pedidos.PuntoNro, dbo.Pedidos.Entrada, dbo.Pedidos.IdTransporte, 
                      dbo.Transportes.RazonSocial, dbo.Pedidos.Fecha, dbo.Pedidos.Comentario, dbo.Pedidos.Finalizado, dbo.Pedidos.Dto, dbo.Pedidos.Flete, dbo.Pedidos.Cerrado, 
                      dbo.Pedidos.Activo
FROM         dbo.Pedidos INNER JOIN
                      dbo.Transportes ON dbo.Pedidos.IdTransporte = dbo.Transportes.IdTransporte INNER JOIN
                      dbo.Clientes ON dbo.Pedidos.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.FormasPago ON dbo.Pedidos.IdFormaPago = dbo.FormasPago.IdFormaPago

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[58] 4[18] 2[6] 3) )"
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
         Begin Table = "Pedidos"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 307
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Transportes"
            Begin Extent = 
               Top = 188
               Left = 321
               Bottom = 307
               Right = 506
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Clientes"
            Begin Extent = 
               Top = 0
               Left = 336
               Bottom = 122
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FormasPago"
            Begin Extent = 
               Top = 162
               Left = 583
               Bottom = 264
               Right = 743
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
         Column = 3225
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
         Or = 1350
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Pedidos'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Pedidos'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Pedidos'
GO

