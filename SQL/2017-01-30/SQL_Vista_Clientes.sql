USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Clientes]    Script Date: 01/30/2017 17:36:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vista_Clientes]
AS
SELECT     dbo.Clientes.IdCliente AS Codigo, dbo.Clientes.RazonSocial, dbo.Clientes.Cuit, dbo.Clientes.Direccion, dbo.Clientes.Barrio, dbo.Localidades.IdLocalidad, 
                      dbo.Localidades.Localidad, dbo.Clientes.CP, dbo.Provincias.IdProvincia, dbo.Provincias.Provincia, dbo.Clientes.Telefono, dbo.Clientes.Celular, dbo.Clientes.Fax, 
                      dbo.Clientes.Mail, dbo.Clientes.Web, dbo.Clientes.FechaNac AS Nacimiento, dbo.TiposClientes.IdTipoCliente AS IdTipo, dbo.TiposClientes.TipoCliente AS Tipo, 
                      dbo.TiposClientes.Descuento1, dbo.TiposClientes.Descuento2, dbo.Clientes.Observaciones, dbo.Clientes.Alta, dbo.Clientes.IdCondicionIva, dbo.Clientes.IdTransporte, 
                      dbo.Transportes.RazonSocial AS Transporte
FROM         dbo.Clientes INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.TiposClientes ON dbo.Clientes.IdTipoCliente = dbo.TiposClientes.IdTipoCliente INNER JOIN
                      dbo.Provincias ON dbo.Localidades.IdProvincia = dbo.Provincias.IdProvincia INNER JOIN
                      dbo.Transportes ON dbo.Clientes.IdTransporte = dbo.Transportes.IdTransporte

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[6] 4[62] 2[14] 3) )"
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
         Begin Table = "Clientes"
            Begin Extent = 
               Top = 25
               Left = 17
               Bottom = 144
               Right = 177
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Localidades"
            Begin Extent = 
               Top = 10
               Left = 616
               Bottom = 129
               Right = 776
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "TiposClientes"
            Begin Extent = 
               Top = 186
               Left = 469
               Bottom = 305
               Right = 629
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Provincias"
            Begin Extent = 
               Top = 83
               Left = 360
               Bottom = 172
               Right = 520
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Transportes"
            Begin Extent = 
               Top = 199
               Left = 209
               Bottom = 318
               Right = 369
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
      Begin ColumnWidths = 26
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
         Width = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Clientes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'500
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
         Column = 1440
         Alias = 2580
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Clientes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vista_Clientes'
GO

