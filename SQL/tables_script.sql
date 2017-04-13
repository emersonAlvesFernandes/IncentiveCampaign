CREATE TABLE [dbo].[tbl_campa_incen](
	num_campa_incen int IDENTITY(1,1) NOT NULL,
	nom_campa_incen varchar(50) NOT NULL,
	dat_inici_vigen	datetime,
	dat_final_vigen	datetime,
	val_termo_aceit	varbinary(MAX),
	ind_ativo		bit,
	dat_situa_regis	datetime,
	cod_user		varchar(8),
	ind_neces_carta_acord bit

 CONSTRAINT [PK_tbl_campa_incen] PRIMARY KEY CLUSTERED 
(
	[num_campa_incen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


---------------------

CREATE TABLE [dbo].[tbl_campa_incen_termo](
	num_campa_incen_termo	int IDENTITY(1,1) NOT NULL,
	num_campa_incen			int NOT NULL,
	nom_campa_incen_termo	varchar(50) NOT NULL,
	dat_publi				datetime NOT NULL,
	tip_exten				varchar(10) NOT NULL,
	val_docum				varbinary(MAX) NOT NULL,
	num_bytes				int NOT NULL,
	cod_user				varchar(8) NOT NULL

 CONSTRAINT [PK_tbl_campa_incen_termo] PRIMARY KEY CLUSTERED 
(
	[num_campa_incen_termo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tbl_campa_incen_termo]  WITH CHECK ADD  CONSTRAINT [FK_tbl_campa_incen_tbl_campa_incen_termo_Cascade] FOREIGN KEY([num_campa_incen])
REFERENCES [dbo].[tbl_campa_incen] ([num_campa_incen])
ON DELETE CASCADE
GO

--------------------
CREATE TABLE [dbo].[tbl_campa_incen_usuar](
	num_campa_incen int NOT NULL,
	num_entid_usuar int NOT NULL,
	ind_aceit bit null,
	dat_aceit datetime null

 CONSTRAINT [PK_tbl_campa_incen_usuar] PRIMARY KEY CLUSTERED 
(
	[num_campa_incen] ASC
	,[num_entid_usuar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [dbo].[tbl_campa_incen_usuar]  WITH CHECK ADD  CONSTRAINT [FK_tbl_campa_incen_tbl_campa_incen_usuar_Cascade] FOREIGN KEY([num_campa_incen])
REFERENCES [dbo].[tbl_campa_incen] ([num_campa_incen])
ON DELETE CASCADE
GO

--------------------

CREATE TABLE [dbo].[tbl_campa_incen_conce](
	num_campa_incen int NOT NULL,
	num_entid_conce int NOT NULL,
	ind_carta_acord bit NOT NULL

 CONSTRAINT [PK_tbl_campa_conce] PRIMARY KEY CLUSTERED 
(
	[num_campa_incen] ASC,
	[num_entid_conce] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tbl_campa_incen_conce]  WITH CHECK ADD  CONSTRAINT [FK_tbl_campa_incen_tbl_campa_incen_conce_Cascade] FOREIGN KEY([num_campa_incen])
REFERENCES [dbo].[tbl_campa_incen] ([num_campa_incen])
ON DELETE CASCADE
GO


------------------------

CREATE TABLE [dbo].[tbl_campa_incen_pontu](
	num_campa_incen_pontu	INT IDENTITY(1,1) NOT NULL,
	num_campa_incen			INT NOT NULL,
	num_entid				INT NOT NULL,
	val_ponto				INT NOT NULL,
	des_ponto				VARCHAR(100) NULL,
	ind_ativo				BIT NOT NULL,
	dat_situa_regis			DATETIME NOT NULL,
	cod_user				VARCHAR(8) NOT NULL,
	cod_propo				VARCHAR(15) NULL,
	nm_contr				VARCHAR(10) NULL,
	cod_apoli				VARCHAR(20) NULL	

 CONSTRAINT [PK_tbl_campa_incen_pontu] PRIMARY KEY CLUSTERED 
(
	[num_campa_incen_pontu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[tbl_campa_incen_pontu]  WITH CHECK ADD  CONSTRAINT [FK_tbl_campa_incen_pontu_tbl_campa_incen] FOREIGN KEY([num_campa_incen])
REFERENCES [dbo].[tbl_campa_incen] ([num_campa_incen])

ALTER TABLE [dbo].[tbl_campa_incen_pontu] CHECK CONSTRAINT [FK_tbl_campa_incen_pontu_tbl_campa_incen]



