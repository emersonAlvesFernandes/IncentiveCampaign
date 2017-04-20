select * from tbl_campa_incen
select * from tbl_campa_incen_conce
select * from tbl_campa_incen_usuar
select * from tbl_campa_incen_pontu

alter table tbl_campa_incen_pontu
add num_campa_conce int 

alter table tbl_campa_incen_usuar
add ind_chk_valid bit


insert into tbl_campa_incen values ('Campanha do Metalica', SYSDATETIME(), SYSDATETIME(), null, 1,  SYSDATETIME(),	'RBRONZO',	1)

insert into tbl_campa_incen_conce values (1, 30510, 1)
insert into tbl_campa_incen_conce values (4, 30510, 1)

insert into tbl_campa_incen_usuar values (1, 708844, 1, SYSDATETIME())
insert into tbl_campa_incen_usuar values (4, 708844, 1, SYSDATETIME())
insert into tbl_campa_incen_usuar values (1, 708843, 1, SYSDATETIME())

delete tbl_campa_incen_conce