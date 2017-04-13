CREATE PROCEDURE [dbo].[spr_digit_upd_refac_campa_incen]	
	@num_campa_incen		INT
	,@nom_campa_incen		VARCHAR(50)
	,@dat_inici_vigen		DATETIME
	,@dat_final_vigen		DATETIME
	,@ind_ativo				BIT		
	,@dat_situa_regis		DATETIME
	,@cod_user				VARCHAR(8)
	,@ind_neces_carta_acord BIT		
AS
BEGIN
	UPDATE 
		dtb_bmbdigital..tbl_campa_incen
	SET
		nom_campa_incen			= @nom_campa_incen,
		dat_inici_vigen			= @dat_inici_vigen,	
		dat_final_vigen			= @dat_final_vigen,	
		ind_ativo				= @ind_ativo,			
		dat_situa_regis			= @dat_situa_regis,
		cod_user				= @cod_user,	
		ind_neces_carta_acord	= @ind_neces_carta_acord 
	WHERE 
		num_campa_incen = @num_campa_incen		
END
