CREATE PROCEDURE [dbo].[spr_digit_ler_refat_campa_incen]
AS
BEGIN
	SELECT 
		num_campa_incen
		,nom_campa_incen
		,dat_inici_vigen
		,dat_final_vigen		
		,ind_ativo
		,dat_situa_regis
		,cod_user
		,ind_neces_carta_acord
	FROM dtb_bmbdigital..tbl_campa_incen
END


