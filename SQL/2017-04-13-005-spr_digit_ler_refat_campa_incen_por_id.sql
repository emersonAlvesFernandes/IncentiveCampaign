CREATE PROCEDURE [dbo].[spr_digit_ler_refat_campa_incen_por_id]
	@num_campa_incen INT
AS
BEGIN	
	SELECT 
		campa.num_campa_incen
		,campa.nom_campa_incen
		,campa.dat_inici_vigen
		,campa.dat_final_vigen		
		,campa.ind_ativo
		,campa.dat_situa_regis
		,campa.cod_user
		,campa.ind_neces_carta_acord
	FROM 
		dtb_bmbdigital..tbl_campa_incen campa WITH(NOLOCK)
	WHERE
		campa.num_campa_incen = @num_campa_incen
		
END