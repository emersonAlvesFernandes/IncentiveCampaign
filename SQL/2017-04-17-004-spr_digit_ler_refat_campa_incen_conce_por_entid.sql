CREATE PROCEDURE [dbo].[spr_digit_ler_refat_campa_incen_conce_por_entid]
	@num_entid_usuar INT
AS 
BEGIN
	--SELECT
	--	entid_usuar.num_entid AS num_entid_usuar
	--	,entid_usuar.nom_entid AS nom_entid_usuar	
	--	,entid_conce.nom_entid AS nom_conce
	--	,entid_conce.num_cnpj_cpf 
	--	,equip_conce.num_entid_conce
	--	--,regio.nom_regio
	
	--FROM 
	--	dtb_corporativo..tbl_entidade entid_usuar WITH ( NOLOCK )
					
	--	INNER JOIN dtb_corporativo..tbl_equipe_concessionaria equip_conce WITH ( NOLOCK )
	--	ON equip_conce.num_entid = entid_usuar.num_entid
	
	--	INNER JOIN dtb_corporativo..tbl_entidade entid_conce WITH ( NOLOCK )
	--		ON entid_conce.num_entid = equip_conce.num_entid_conce

	--	INNER JOIN dtb_corporativo..tbl_regional regio WITH ( NOLOCK )
	--		ON entid_conce.num_regio = regio.num_regio

	--WHERE 	
	--	equip_conce.ind_situa_regis = 'A'		
	--	and entid_usuar.num_entid = @num_entid_usuar

	--GROUP BY
	--	entid_usuar.num_entid
	--	,entid_usuar.nom_entid
	--	,equip_conce.num_entid_conce
	--	,entid_conce.nom_entid 
	--	,entid_conce.num_cnpj_cpf 
	--	--,regio.nom_regio

	select	
		equip_conce.num_entid_conce
		,entid_conce.num_entid
		,entid_conce.nom_entid
		,entid_conce.num_cnpj_cpf
		,campa_conce.ind_carta_acord
		,campa_usuar.num_entid_usuar		
		--equip_conce.*
	from 
		dtb_bmbdigital..tbl_campa_incen_usuar campa_usuar  WITH ( NOLOCK )
			
			INNER JOIN dtb_corporativo..tbl_equipe_concessionaria equip_conce WITH ( NOLOCK )
				ON equip_conce.num_entid = campa_usuar.num_entid_usuar
			
			INNER JOIN dtb_bmbdigital..tbl_campa_incen_conce campa_conce  WITH ( NOLOCK )
				ON campa_conce.num_entid_conce = equip_conce.num_entid_conce

			INNER JOIN dtb_corporativo..tbl_entidade entid_conce WITH ( NOLOCK )
				ON entid_conce.num_entid = campa_conce.num_campa_incen
	where 
		campa_usuar.num_entid_usuar = 65494
END