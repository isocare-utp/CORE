
--setup ATM application 
insert into amappstatus values(
'020001','atm',trunc(sysdate),trunc(sysdate),'0','admin',sysdate,'0','admin',sysdate,'0','admin',sysdate,'','','','','','','','2560','0','ระบบ ATM','ATM','COM','0','000','200','1','020001','0','1'
);
insert into amsecuseapps values(
'020001','admin','atm','020001'
);
insert into amsecwins values(
'atm','WATM-OFF02','w_sheet_atmoffline_exp','ATM Offline ส่งออกข้อมูลธนาคาร ','','ATM Offline ส่งออกข้อมูลธนาคาร ','','1','','','1','A','10','','','020001','1'
);
insert into amsecwins values(
'atm','WATM-OFF01','w_sheet_atmoffline_imp','ATM Offline นำเข้าข้อมูลธนาคาร ','','ATM Offline นำเข้าข้อมูลธนาคาร ','','1','','','1','A','20','','','020001','1'
);
insert into amsecwinsgroup values(
'atm','A','ATM Offline ประจำวัน','10','020001'
);
insert into amsecpermiss values(
'020001','admin','atm','WATM-OFF02','1','1','020001'
);
insert into amsecpermiss values(
'020001','admin','atm','WATM-OFF01','1','1','020001'
);
commit;


-- DO After import SAVE ROW AS FILE
UPDATE amappstatus set coop_control=(select coop_control from cmcoopmaster where rownum=1), coop_id=(select coop_control from cmcoopmaster where rownum=1)  where coop_control='020001' or coop_id='020001' ;
UPDATE amsecwinsgroup set coop_control=(select coop_control from cmcoopmaster where rownum=1) where coop_control='020001' ;
UPDATE amsecwins set coop_control=(select coop_control from cmcoopmaster where rownum=1) where coop_control='020001';
UPDATE amsecuseapps set coop_control=(select coop_control from cmcoopmaster where rownum=1), coop_id=(select coop_control from cmcoopmaster where rownum=1) where coop_control='020001' or coop_id='020001';
UPDATE amsecpermiss set coop_control=(select coop_control from cmcoopmaster where rownum=1), coop_id=(select coop_control from cmcoopmaster where rownum=1) where coop_control='020001' or coop_id='020001';
commit;
