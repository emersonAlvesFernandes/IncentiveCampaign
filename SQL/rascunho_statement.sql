select 
	dealer.num_entid_usuar
	, sum(ponto.val_ponto) as total
from 
	tbl_campa_incen_usuar dealer
		left join tbl_campa_incen_pontu ponto on dealer.num_entid_usuar = ponto.num_entid
group by 
	dealer.num_entid_usuar

------

select 
	ponto.num_entid 
	,ponto.val_ponto
	,campanha.num_campa_incen	
	,campanha.ind_neces_carta_acord
	,concessionaria.num_entid_conce
	,concessionaria.ind_carta_acord
	,dealer.ind_aceit
	
from 
	tbl_campa_incen_pontu ponto
		left join tbl_campa_incen_conce concessionaria 
			on ponto.num_campa_conce = concessionaria.num_entid_conce			
			and ponto.num_campa_incen = concessionaria.num_campa_incen
		
		join tbl_campa_incen campanha 
			on ponto.num_campa_incen = campanha.num_campa_incen

		join tbl_campa_incen_usuar dealer 
			on ponto.num_entid = dealer.num_entid_usuar
			and ponto.num_campa_incen = dealer.num_campa_incen

where 
	ponto.ind_ativo = 1