CREATE PROCEDURE [dbo].[spr_digit_ler_refat_campa_incen_conce_por_campa]
	@num_campa_incen INT
AS
BEGIN
	
	SELECT 
		entid_conce.num_entid,
		entid_conce.nom_entid,
		entid_conce.num_cnpj_cpf,
		campa_incen.ind_neces_carta_acord		
	FROM
		dtb_bmbdigital..tbl_campa_incen_conce conce WITH(NOLOCK)
			
			INNER JOIN dtb_bmbdigital..tbl_campa_incen campa_incen WITH(NOLOCK)
				ON conce.num_campa_incen = campa_incen.num_campa_incen
						
			INNER JOIN dtb_corporativo..tbl_entidade entid_conce WITH ( NOLOCK )
				ON entid_conce.num_entid = conce.num_entid_conce
	WHERE 
		campa_incen.num_campa_incen = @num_campa_incen

END