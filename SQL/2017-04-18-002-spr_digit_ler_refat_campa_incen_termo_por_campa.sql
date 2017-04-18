create procedure [dbo].[spr_digit_ler_refat_campa_incen_termo_por_campa]
	@num_campa_incen int
as
begin
	select 
		termo.num_campa_incen_termo
		,termo.num_campa_incen
		,termo.nom_campa_incen_termo
		,termo.dat_publi
		,termo.tip_exten
		,termo.val_docum
		,termo.num_bytes
		,termo.cod_user
	from 
		dtb_bmbdigital..tbl_campa_incen_termo termo
	where 
		termo.num_campa_incen = @num_campa_incen
end
