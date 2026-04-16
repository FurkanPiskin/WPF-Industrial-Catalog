USE [FerraFilterDB]
GO

/****** Object:  Table [dbo].[Ferra_MuadilOrijinal] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ferra_MuadilOrijinal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[filtre_no_goster] [nvarchar](100) NOT NULL,
	[filtre_no_b] [nvarchar](100) NULL,
	[firma_adi] [nvarchar](150) NULL,
	[ferra_no_b] [nvarchar](100) NOT NULL,
	[orjinal_muadil] [nvarchar](100) NULL,
	[sabit_degisken] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Filtreler] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filtreler](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ferra_no_bosluksuz] [nvarchar](100) NULL,
	[filtre_tipi_tr] [nvarchar](255) NULL,
	[filtre_durumu] [nvarchar](100) NULL,
	[foto1] [nvarchar](max) NULL,
	[a] [nvarchar](50) NULL,
	[b] [nvarchar](50) NULL,
	[c] [nvarchar](50) NULL,
	[d] [nvarchar](50) NULL,
	[e] [nvarchar](50) NULL,
	[f] [nvarchar](50) NULL,
	[g] [nvarchar](50) NULL,
	[g1] [nvarchar](50) NULL,
	[h] [nvarchar](50) NULL,
	[i] [nvarchar](50) NULL,
	[j] [nvarchar](50) NULL,
	[k] [nvarchar](50) NULL,
	[bypass] [nvarchar](50) NULL,
	[anti_drain] [nvarchar](50) NULL,
	[anti_syphon] [nvarchar](50) NULL,
	[anti_syphon2] [nvarchar](50) NULL,
	[bypass2] [nvarchar](50) NULL,
	[bypass3] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- =========================================================
-- DUMMY DATA INSERTION FOR TESTING PURPOSES
-- =========================================================

SET IDENTITY_INSERT [dbo].[Filtreler] ON 

INSERT [dbo].[Filtreler] ([ID], [ferra_no_bosluksuz], [filtre_tipi_tr], [filtre_durumu], [foto1], [a], [b], [c], [d], [e], [f], [g], [g1], [h], [i], [j], [k], [bypass], [anti_drain], [anti_syphon], [anti_syphon2], [bypass2], [bypass3]) VALUES 
(1, N'DUMMY-1026', N'Yag filtresi', N'available', N'dummy_image_1.jpg', N'100', N'90', N'95', N'', N'', N'', N'1 - 1 / 8" - 16 ', N'', N'200', N'', N'', N'', N'', N'', N'', N'', N'', N''),
(2, N'DUMMY-1313', N'Hava kurutucu', N'available', N'dummy_image_2.jpg', N'130', N'95', N'105', N'', N'', N'', N'M 41 x 1.5', N'', N'140', N'', N'', N'', N'', N'', N'', N'', N'', N''),
(3, N'DUMMY-5133', N'Hava filtresi', N'available', N'dummy_image_3.jpg', N'400', N'220', N'', N'500', N'', N'', N'', N'', N'550', N'', N'', N'', N'', N'', N'', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[Filtreler] OFF

SET IDENTITY_INSERT [dbo].[Ferra_MuadilOrijinal] ON 

INSERT [dbo].[Ferra_MuadilOrijinal] ([id], [filtre_no_goster], [filtre_no_b], [firma_adi], [ferra_no_b], [orjinal_muadil], [sabit_degisken]) VALUES 
(1, N'ACME-9901', N'ACME9901', N'ACME CORP', N'DUMMY-1026', N'1', N'DD'),
(2, N'GLB-X100', N'GLBX100', N'GLOBAL FILTERS', N'DUMMY-1026', N'1', N'DD'),
(3, N'TECH-88', N'TECH88', N'TECH AUTOMOTIVE', N'DUMMY-1026', N'2', N'DD'),
(4, N'FAKE-13', N'FAKE13', N'FAKE INDUSTRIES', N'DUMMY-1313', N'1', N'DD'),
(5, N'MOCK-440', N'MOCK440', N'MOCK PARTS', N'DUMMY-1313', N'1', N'DD'),
(6, N'TEST-001', N'TEST001', N'TEST INC', N'DUMMY-1313', N'2', N'DD'),
(7, N'DEMO-55A', N'DEMO55A', N'DEMO BUS CO.', N'DUMMY-5133', N'1', N'DD'),
(8, N'TRK-900', N'TRK900', N'TRUCK PARTS LTD.', N'DUMMY-5133', N'1', N'DD'),
(9, N'AIR-MAX-1', N'AIRMAX1', N'AIR FLOW CO.', N'DUMMY-5133', N'2', N'DD'),
(10, N'OEM-XX99', N'OEMXX99', N'OEM MOTORS', N'DUMMY-5133', N'2', N'DD')
SET IDENTITY_INSERT [dbo].[Ferra_MuadilOrijinal] OFF
GO