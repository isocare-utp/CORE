update amappstatus set coop_id=(select distinct coop_control from cmcoopmaster)
where coop_id='020001';

update amsecuseapps set coop_id=(select distinct coop_control from cmcoopmaster) ,
 coop_control=(select distinct coop_control from cmcoopmaster)
where coop_id='020001';

update amsecpermiss  set coop_id=(select distinct coop_control from cmcoopmaster)
where coop_id='020001';

update amsecwins  set coop_control=(select distinct coop_control from cmcoopmaster)
where coop_control='020001';

update amsecwinsgroup  set coop_control=(select distinct coop_control from cmcoopmaster)
where coop_control='020001';

commit;
