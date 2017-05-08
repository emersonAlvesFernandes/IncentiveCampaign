CREATE TABLE [dbo].[tbl_campa_incen_usuar] (
    [num_campa_incen] INT      NOT NULL,
    [num_entid_usuar] INT      NOT NULL,
    [ind_aceit]       BIT      NULL,
    [dat_aceit]       DATETIME NULL,
    [ind_valid] BIT NULL, 
    CONSTRAINT [PK_tbl_campa_incen_usuar] PRIMARY KEY CLUSTERED ([num_campa_incen] ASC, [num_entid_usuar] ASC),
    CONSTRAINT [FK_tbl_campa_incen_tbl_campa_incen_usuar_Cascade] FOREIGN KEY ([num_campa_incen]) REFERENCES [dbo].[tbl_campa_incen] ([num_campa_incen]) ON DELETE CASCADE
);

