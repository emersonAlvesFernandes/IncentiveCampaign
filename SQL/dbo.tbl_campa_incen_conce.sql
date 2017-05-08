CREATE TABLE [dbo].[tbl_campa_incen_conce] (
    [num_campa_incen] INT NOT NULL,
    [num_entid_conce] INT NOT NULL,
    [ind_carta_acord] BIT NOT NULL,
    [val_carta_acord] VARBINARY(MAX) NULL, 
    [dat_upl_carta_acord] DATETIME NULL, 
    CONSTRAINT [PK_tbl_campa_conce] PRIMARY KEY CLUSTERED ([num_campa_incen] ASC, [num_entid_conce] ASC),
    CONSTRAINT [FK_tbl_campa_incen_tbl_campa_incen_conce_Cascade] FOREIGN KEY ([num_campa_incen]) REFERENCES [dbo].[tbl_campa_incen] ([num_campa_incen]) ON DELETE CASCADE
);

