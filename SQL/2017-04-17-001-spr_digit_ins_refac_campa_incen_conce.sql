CREATE PROCEDURE [dbo].[spr_digit_ins_refac_campa_incen_conce]
	@num_campa_incen INT,
	@num_entid_conce INT,
	@ind_carta_acord INT

AS
BEGIN
	
	INSERT INTO 
		dtb_bmbdigital..tbl_campa_incen_conce
		(num_campa_incen, num_entid_conce, ind_carta_acord)
	VALUES
		(@num_campa_incen, @num_entid_conce, @ind_carta_acord)

END