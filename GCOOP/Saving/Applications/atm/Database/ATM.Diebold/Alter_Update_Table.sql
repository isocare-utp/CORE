
-- DO After import SAVE ROW AS FILE
UPDATE amappstatus set coop_control=(select coop_control from cmcoopmaster where rownum=1) ;
UPDATE amsecwinsgroup set coop_control=(select coop_control from cmcoopmaster where rownum=1) ;
UPDATE amsecwins set coop_control=(select coop_control from cmcoopmaster where rownum=1) ;
UPDATE amsecuseapps set coop_control=(select coop_control from cmcoopmaster where rownum=1), coop_id=(select coop_control from cmcoopmaster where rownum=1) ;
UPDATE amsecpermiss set coop_control=(select coop_control from cmcoopmaster where rownum=1), coop_id=(select coop_control from cmcoopmaster where rownum=1) ;

