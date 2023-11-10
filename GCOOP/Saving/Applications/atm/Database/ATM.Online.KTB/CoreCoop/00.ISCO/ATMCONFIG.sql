--------------------------------------------------------
--  File created - Tuesday-June-21-2016   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table ATMCONFIG
--------------------------------------------------------

  CREATE TABLE "ATMCONFIG" 
   (	"X" CHAR(1)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into ATMCONFIG
SET DEFINE OFF;
Delete from ATMCONFIG;
Insert into ATMCONFIG (X) values ('Y');
