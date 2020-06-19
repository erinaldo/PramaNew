USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Remitos]    Script Date: 03/15/2017 21:14:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vista_Remitos]
AS
SELECT     dbo.Remitos.IdRemito, dbo.Remitos.Punto, dbo.Remitos.Nro, dbo.Remitos.NroRemito, dbo.Remitos.IdCliente, dbo.Remitos.CUIT, dbo.Clientes.RazonSocial, 
                      dbo.Clientes.Direccion, dbo.Clientes.CP, dbo.Clientes.IdLocalidad, dbo.Clientes.IdProvincia, dbo.Localidades.Localidad, dbo.Provincias.Provincia, 
                      dbo.Clientes.Telefono, dbo.Remitos.IdTransporte, dbo.Transportes.RazonSocial AS Transporte, dbo.Remitos.Comprobante, dbo.Remitos.IdFormaPago, 
                      dbo.Remitos.IdFormaPagoMerc, dbo.CondicionesCompra.CondicionCompra, CondicionesCompra_1.CondicionCompra AS CondicionCompraMerc, dbo.Remitos.Bultos, 
                      dbo.Remitos.Seguro, dbo.Remitos.Anulado, dbo.Remitos.Activo
FROM         dbo.Remitos INNER JOIN
                      dbo.Transportes ON dbo.Remitos.IdTransporte = dbo.Transportes.IdTransporte INNER JOIN
                      dbo.CondicionesCompra ON dbo.Remitos.IdFormaPago = dbo.CondicionesCompra.IdCondicionCompra INNER JOIN
                      dbo.Clientes ON dbo.Remitos.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.Provincias ON dbo.Clientes.IdProvincia = dbo.Provincias.IdProvincia INNER JOIN
                      dbo.CondicionesCompra AS CondicionesCompra_1 ON dbo.Remitos.IdFormaPagoMerc = CondicionesCompra_1.IdCondicionCompra

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[27] 2[18] 3) )"
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
         Top = -148
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Remitos"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 304
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Transportes"
            Begin Extent = 
               Top = 2
               Left = 357
               Bottom = 137
               Right = 576
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CondicionesCompra"
            Begin Extent = 
               Top = 146
               Left = 359
               Bottom = 303
               Right = 576
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Clientes"
            Begin Extent = 
               Top = 312
               Left = 359
               Bottom = 624
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Localidades"
            Begin Extent = 
               Top = 419
               Left = 51
               Bottom = 583
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Provincias"
            Begin Extent = 
               Top = 589
               Left = 49
               Bottom = 678
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CondicionesCompra_1"
            Begin Extent = 
               Top = 309
               Left = 52
               Bottom = 428
               Right = 244
            ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Remitos'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 25
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
         Column = 1710
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
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Remitos'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Remitos'
GO

