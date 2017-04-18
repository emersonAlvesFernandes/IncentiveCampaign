
USE [dtb_bmbdigital]
GO

/****** Object:  StoredProcedure [dbo].[spr_digit_ler_refat_campa_incen_conce_por_campa]    Script Date: 18/04/2017 14:14:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spr_digit_ler_refat_campa_incen_conce_por_campa]
	@num_campa_incen INT
AS
BEGIN
	
	SELECT 			
		entid_conce.num_entid as num_entid_conce,
		entid_conce.nom_entid as nom_campa_incen,
		entid_conce.num_cnpj_cpf as cnpj_cpf_entid_conce,
		--campa_incen.ind_neces_carta_acord,
		conce.ind_carta_acord

	FROM
		dtb_bmbdigital..tbl_campa_incen_conce conce WITH(NOLOCK)
			
			INNER JOIN dtb_bmbdigital..tbl_campa_incen campa_incen WITH(NOLOCK)
				ON conce.num_campa_incen = campa_incen.num_campa_incen
						
			INNER JOIN dtb_corporativo..tbl_entidade entid_conce WITH ( NOLOCK )
				ON entid_conce.num_entid = conce.num_entid_conce	
	WHERE 
		campa_incen.num_campa_incen = @num_campa_incen

END
GO
	