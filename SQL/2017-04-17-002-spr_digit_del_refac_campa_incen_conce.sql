CREATE PROCEDURE [dbo].[spr_digit_del_refac_campa_incen_conce]
	@num_campa_incen INT,
	@num_entid_conce INT	
AS
BEGIN
	
	DELETE
		dtb_bmbdigital..tbl_campa_incen_conce		
	WHERE
		num_campa_incen = @num_campa_incen 
		AND num_entid_conce = @num_entid_conce

END