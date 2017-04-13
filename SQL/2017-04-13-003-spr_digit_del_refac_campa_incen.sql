CREATE PROCEDURE [dbo].[spr_digit_del_refac_campa_incen]	
	@num_campa_incen INT	
AS
BEGIN
	UPDATE 
		dtb_bmbdigital..tbl_campa_incen 
	SET 
		ind_ativo = 0 
	WHERE 
		num_campa_incen = @num_campa_incen	
END
