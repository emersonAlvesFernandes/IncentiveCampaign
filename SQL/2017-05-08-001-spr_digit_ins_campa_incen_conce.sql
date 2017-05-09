CREATE PROCEDURE [dbo].[spr_digit_ins_campa_incen_conce]
	@num_campa_incen int
	,@num_entid_conce int
AS
BEGIN
	
	insert into dbo.tbl_campa_incen_conce 
		(num_campa_incen, num_entid_conce)
	VALUES 
		(@num_campa_incen, @num_entid_conce)
END

