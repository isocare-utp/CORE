using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DataLibrary;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Net.Mail;
using System.Text;
using CoreSavingLibrary;
using System.Net;
using System.Collections.Specialized;

namespace Saving.Applications.admin
{
    public partial class w_sheet_sms : PageWebSheet, WebSheet
    {
        public string SMS_LON_STAT_DATA = "", SMS_DEP_STAT_DATA = "";

        public void init()
        {

            try
            {
                String send_date = Request["d"];
                String operdate_code = Request["o"];
                String send_flag = Request["s"];
            }
            catch { }
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

                String datasource = state.SsConnectionString;// "Data Source=rac/gcoop;Persist Security Info=True;User ID=ifsct;Password=ifsct;Unicode=True;";
                datasource = (Request["d"] != null) ? Request["d"] : datasource;
                if (this.TbConnectionString.Text == null || TbConnectionString.Text.Trim() == "")
                {
                    TbConnectionString.Text = datasource;
                }
                this.Txb_send_date.Text = DateTime.Now.ToString("yyyyMMdd", WebUtil.EN);
                LbServerMessage.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();
                string sql = "";
                try
                {
                    sql = @"CREATE TABLE smsnewsmessage (
                                    MSG_ID NUMBER(20) NOT NULL
                                    ,msg_title VARCHAR2(30) NOT NULL
                                    , msg_detail VARCHAR2(150) NOT NULL
                                    , SEND_DATE date NOT NULL
                                    , operate_date date NOT NULL
                                    , send_to VARCHAR2(20) default 'ALL' NOT NULL 
                                    , send_flag number(1) default 0 NOT NULL 
                                    , entry_id VARCHAR2(50) not NULL
                                    , entry_date date  default sysdate not NULL) ";
                    WebUtil.Query(sql);
                    sql = @"ALTER TABLE smsnewsmessage ADD ( CONSTRAINT smsnewsmessage_pk PRIMARY KEY ( MSG_ID ))";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"CREATE TABLE SMSCONFIG (
                                    SENDER_NUMBER VARCHAR2(20) NOT NULL
                                    ,SENDER_CODE VARCHAR2(30) NOT NULL
                                    , PHONENETWORK VARCHAR2(30) NOT NULL
                                    , ENABLE_FLAG NUMBER(1,0) DEFAULT 1 NOT NULL
                                    , USER_NAME VARCHAR2(20) NOT NULL
                                    , USER_PWD VARCHAR2(20) NOT NULL
                                    , URL VARCHAR2(150) NULL
                                    , URL_PARAMS VARCHAR2(255) NULL) ";
                    WebUtil.Query(sql);
                    sql = @"ALTER TABLE SMSCONFIG ADD ( CONSTRAINT smsconfig_pk PRIMARY KEY ( SENDER_NUMBER,SENDER_CODE, PHONENETWORK ))";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"insert into SMSCONFIG (SENDER_NUMBER,SENDER_CODE, PHONENETWORK, ENABLE_FLAG, USER_NAME, USER_PWD, URL, URL_PARAMS)
                            values('DTACTEST','COOP','DTAC','0','ISOCARE','PASSWORD','http://corpsms.dtac.co.th/servlet/com.iess.socket.SmsCorplink','&RefNo={0}&Sender={1}&Msn={2}&Sno={3}&MsgType=H&Msg={4}&Encoding=25&User={5}&Password={6}')";
                    WebUtil.Query(sql);
                    }
                catch { }
                try
                {
                    sql = @"insert into SMSCONFIG (SENDER_NUMBER,SENDER_CODE, PHONENETWORK, ENABLE_FLAG, USER_NAME, USER_PWD, URL, URL_PARAMS)
                            values('AISTEST','COOP','AIS','0','ISOCARE','PASSWORD','http://corpsms.ais.co.th/servlet/com.iess.socket.SmsCorplink','&CMD=SENDMSG&FROM={0}&TO={1}&CODE={2}&REPORT=Y&CHARGE=Y&CTYPE=LUNICODE&CONTENT={3}')";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"CREATE TABLE SMSPATTERNCONFIG (
                                    SMS_TRANS_CODE VARCHAR2(10) NOT NULL
                                    , SMS_TRANS_DESC VARCHAR2(50) NOT NULL
                                    , SMS_PATTERN VARCHAR2(500) NOT NULL
                                    , FROM_SYSTEM VARCHAR2(500) NOT NULL
                                    , ENABLE_FLAG NUMBER(1,0) DEFAULT 1 NOT NULL
                                    , SMS_TRANS_SQL VARCHAR2(500) NOT NULL
                                    )";
                    WebUtil.Query(sql);
                    sql = @"ALTER TABLE SMSPATTERNCONFIG ADD ( CONSTRAINT smspatternconfig_pk PRIMARY KEY ( SMS_TRANS_CODE, FROM_SYSTEM ))";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"CREATE TABLE SMSTRANSACTION (
                                    REF_NO VARCHAR2(100) NOT NULL
                                    , SEQ_NO NUMBER(6,0) NOT NULL
                                    , MEMBER_NO VARCHAR2(10) NOT NULL
                                    , TELEPHONE_NUMBER VARCHAR2(20) NOT NULL
                                    , MESSAGE_TEXT VARCHAR2(255) NOT NULL
                                    , CREATE_DATE DATE NOT NULL
                                    , SEND_DATE DATE NOT NULL
                                    , MESSAGE_STATUS NUMBER(1,0) DEFAULT 1 NOT NULL
                                    , FROM_SYSTEM VARCHAR2(10)
                                    , SMS_TRANS_CODE VARCHAR2(10)
                                    , POST_FLAG NUMBER(1,0) DEFAULT 1 NOT NULL) ";
                    WebUtil.Query(sql);
                    sql = @"ALTER TABLE SMSTRANSACTION ADD ( CONSTRAINT SMSTRANSACTION_PK PRIMARY KEY ( REF_NO, SEQ_NO ))";
                    WebUtil.Query(sql);
                }
                catch { }

                try
                {
                    sql = @"ALTER TABLE SMSTRANSACTION ADD(SEND_STATUS_MSG VARCHAR2(255)) ";
                    WebUtil.Query(sql);
                }
                catch { }

                try
                {
                    sql = @"ALTER TABLE SMSTRANSACTION ADD(SEND_PRODATE DATE DEFAULT sysdate NOT NULL ,SEND_PROTIME VARCHAR2(5) DEFAULT '00:00' NOT NULL ) ";
                    //SEND_PRODATE = วันที่ประมวลผล
                    //SEND_PROTIME = เวลาที่ประมวลผล 
                    WebUtil.Query(sql);
                }
                catch { }
                //1.แจ้งข่าวสารประสาสัมพันธ์ 
                try
                {
                    sql = @"create or replace view SMS_PATTERN_N01 as 
                                select q.* from (
                                select s.msg_title as msg_title,s.msg_detail as msg_detail
                                ,s.operate_date as send_date
                                ,to_char( s.operate_date,'dd/MM/')||(to_number(to_char( s.operate_date,'yyyy'))+543) as send_date_str
                                ,m.member_no as member_no
                                ,replace(NVL(m.addr_mobilephone,m.addr_phone),'-') as phone_number
                                from smsnewsmessage s ,mbmembmaster m
                                where
                                 m.resign_status <> 1  and m.member_status  = 1 
							     and s.send_flag = 0
                                ) q where q.phone_number is not null and length(q.phone_number) = 10
                                order by member_no asc";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"insert into SMSPATTERNCONFIG (SMS_TRANS_CODE,SMS_TRANS_DESC, SMS_PATTERN, FROM_SYSTEM, ENABLE_FLAG , SMS_TRANS_SQL)
                            values('N01','แจ้งข่าวสารสมาชิก','msg_detail','NEW','1','SMS_PATTERN_N01 where to_char(send_date,''yyyyMMdd'')=?')";
                    WebUtil.Query(sql);
                }
                catch { }


                //2.แจ้งการอนุมัติสัญญาเงินกู้ฉุกเฉิน , สามัญ , พิเศษ
                try
                {
                    sql = @"create or replace view SMS_PATTERN_L02 as 
                                select q.* from (
                                select ln.loancontract_no as loancontract_no
                                ,ln.loanapprove_date as approve_date
                                ,ln.loanapprove_date as send_date
                                ,to_char( ln.loanapprove_date,'dd/MM/')||(to_number(to_char( ln.loanapprove_date,'yyyy'))+543) as loanapprove_date_str
                                ,ln.member_no as member_no
                                ,replace(NVL(m.addr_mobilephone,m.addr_phone),'-') as phone_number
                                ,ln.loanapprove_amt as loanapprove_amt,replace(to_char(ln.loanapprove_amt,'999,999,990.00'),' ','') as loanapprove_amt_str 
                                from lncontmaster ln ,mbmembmaster m
                                where
                                 m.member_no = ln.member_no and
                                 ln.contract_status = 1 
                                ) q where q.phone_number is not null and length(q.phone_number) = 10
                                order by member_no asc";
                    WebUtil.Query(sql);
                }
                catch { }

                try
                {
                    sql = @"insert into SMSPATTERNCONFIG (SMS_TRANS_CODE,SMS_TRANS_DESC, SMS_PATTERN, FROM_SYSTEM, ENABLE_FLAG , SMS_TRANS_SQL)
                            values('L02','แจ้งการอนุมัติสัญญาเงินกู้','''อนุมัติเงินกู้ ''||loanapprove_amt_str||'' เลขที่ ''||loancontract_no||'' ณ.วันที่ ''||loanapprove_date_str','LON','1','SMS_PATTERN_L02 where to_char(send_date,''yyyyMMdd'')=?')";
                    WebUtil.Query(sql);
                }
                catch { }


                //L91.แจ้งการอนุมัติจ่ายสัญญาเงินกู้
                try
                {
                    sql = @"create or replace view SMS_PATTERN_L91 as 
                                select q.* from (
                                select l.loancontract_no as loancontract_no
                                ,sl.slip_date as send_date
                                ,sl.slip_date as slip_date
                                ,to_char(sl.slip_date,'dd/MM/')||(to_number(to_char(sl.slip_date,'yyyy'))+543) as slip_date_str
                                ,l.member_no as member_no
                                ,replace(NVL(m.addr_mobilephone,m.addr_phone),'-') as phone_number
                                ,l.loanapprove_amt as loanapprove_amt,replace(to_char(l.loanapprove_amt,'999,999,990.00'),' ','') as loanapprove_amt_str 
                                ,sl.payoutnet_amt as payoutnet_amt,replace(to_char(sl.payoutnet_amt,'999,999,990.00'),' ','') as payoutnet_amt_str  
                                from slslippayout sl ,lncontmaster l ,mbmembmaster m
                                where
                                 l.loancontract_no = sl.loancontract_no and 
                                 m.member_no = l.member_no and
                                 sl.payoutnet_amt > 0 and 
                                 l.contract_status = 1 
                                ) q where q.phone_number is not null and length(q.phone_number) = 10
                                order by member_no asc ";
                    WebUtil.Query(sql);
                }
                catch { }

                try
                {
                    sql = @"insert into SMSPATTERNCONFIG (SMS_TRANS_CODE,SMS_TRANS_DESC, SMS_PATTERN, FROM_SYSTEM, ENABLE_FLAG , SMS_TRANS_SQL)
                            values('L91','อนุมัติจ่ายเงินกู้','''อนุมัติจ่ายเงินกู้ ''||loanapprove_amt_str||'' บ.รับสุทธิ ''||payoutnet_amt_str||'' บ.วันที่ ''||slip_date_str','LON','0','SMS_PATTERN_L91 where to_char(send_date,''yyyyMMdd'')=?')";
                    WebUtil.Query(sql);
                }
                catch { }

                //L92.แจ้งการอนุมัติสัญญาเงินกู้
                try
                {
                    sql = @"create or replace view SMS_PATTERN_L92 as 
                            select q.* from (
                            select l.loancontract_no as loancontract_no
                            ,l.loanapprove_date as send_date
                            ,l.loanapprove_date as loanapprove_date
                            ,to_char(l.loanapprove_date,'dd/MM/')||(to_number(to_char(l.loanapprove_date,'yyyy'))+543) as loanapprove_date_str
                            ,l.member_no as member_no
                            ,replace(NVL(m.addr_mobilephone,m.addr_phone),'-') as phone_number
                            ,l.loanapprove_amt as loanapprove_amt,replace(to_char(l.loanapprove_amt,'999,999,990.00'),' ','') as loanapprove_amt_str 
                            from lncontmaster l ,mbmembmaster m
                            where
                             m.member_no = l.member_no and
                             l.contract_status = 1 
                            ) q where q.phone_number is not null and length(q.phone_number) = 10
                            order by member_no asc ";
                    WebUtil.Query(sql);
                }
                catch { }

                try
                {
                    sql = @"insert into SMSPATTERNCONFIG (SMS_TRANS_CODE,SMS_TRANS_DESC, SMS_PATTERN, FROM_SYSTEM, ENABLE_FLAG , SMS_TRANS_SQL)
                            values('L92','สัญญาเงินกู้','''สัญญาเงินกู้ ''||loancontract_no||'' ได้รับการอนุมัติแล้ว ''||loanapprove_amt_str||'' บ.วันที่ ''||loanapprove_date_str','LON','0','SMS_PATTERN_L92 where to_char(send_date,''yyyyMMdd'')=?')";
                    WebUtil.Query(sql);
                }
                catch { }


                //D91.แจ้งการรับเงินฝาก
                try
                {
                    sql = @"create or replace view SMS_PATTERN_D91 as 
                            select q.* from (
                            select d.deptaccount_no
                            ,d.deptslip_date as send_date
                            ,d.deptslip_date as deptslip_date
                            ,to_char(d.deptslip_date,'dd/MM/')||to_char(to_number(to_char(d.deptslip_date,'yyyy'))+543) as deptslip_date_str
                            ,dm.member_no as member_no
                            ,replace(NVL(m.addr_mobilephone,m.addr_phone),'-') as phone_number
                            ,d.deptslip_amt ,replace(to_char(d.deptslip_amt,'999,999,990.00'),' ','') as deptslip_amt_str 
                            ,dc.recppaytype_desc 
                            from dpdeptslip d , dpucfrecppaytype dc , dpdeptmaster dm ,mbmembmaster m
                            where
                             d.deptaccount_no = dm.deptaccount_no and 
                             dc.recppaytype_code =d.recppaytype_code and 
                             m.member_no = dm.member_no and
                             dc.recppaytype_flag >0 
                            ) q where q.phone_number is not null and length(q.phone_number) = 10
                            order by member_no asc ";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"insert into SMSPATTERNCONFIG (SMS_TRANS_CODE,SMS_TRANS_DESC, SMS_PATTERN, FROM_SYSTEM, ENABLE_FLAG , SMS_TRANS_SQL)
                            values('D91','รับฝากเงิน','''รับฝากเงิน ''||deptslip_amt_str||'' บ.เข้าบัญชีเลขที่ ''||deptaccount_no||'' วันที่ ''||deptslip_date_str','DEP','0','SMS_PATTERN_D91 where to_char(send_date,''yyyyMMdd'')=?')";
                    WebUtil.Query(sql);
                }
                catch { }

                //D92.แจ้งการถอนเงินฝาก
                try
                {
                    sql = @"create or replace view SMS_PATTERN_D92 as 
                            select q.* from (
                            select d.deptaccount_no
                            ,d.deptslip_date as send_date
                            ,d.deptslip_date as deptslip_date
                            ,to_char(d.deptslip_date,'dd/MM/')||to_char(to_number(to_char(d.deptslip_date,'yyyy'))+543) as deptslip_date_str
                            ,dm.member_no as member_no
                            ,replace(NVL(m.addr_mobilephone,m.addr_phone),'-') as phone_number
                            ,d.deptslip_amt ,replace(to_char(d.deptslip_amt,'999,999,990.00'),' ','') as deptslip_amt_str 
                            ,dc.recppaytype_desc 
                            from dpdeptslip d , dpucfrecppaytype dc , dpdeptmaster dm ,mbmembmaster m
                            where
                             d.deptaccount_no = dm.deptaccount_no and 
                             dc.recppaytype_code =d.recppaytype_code and 
                             m.member_no = dm.member_no and
                             dc.recppaytype_flag <0 
                            ) q where q.phone_number is not null and length(q.phone_number) = 10
                            order by member_no asc ";
                    WebUtil.Query(sql);
                }
                catch { }
                try
                {
                    sql = @"insert into SMSPATTERNCONFIG (SMS_TRANS_CODE,SMS_TRANS_DESC, SMS_PATTERN, FROM_SYSTEM, ENABLE_FLAG , SMS_TRANS_SQL)
                            values('D92','ถอนเงินฝาก','''ถอนเงินฝาก ''||deptslip_amt_str||'' บ.บัญชีเลขที่ ''||deptaccount_no||'' วันที่ ''||deptslip_date_str','DEP','0','SMS_PATTERN_D92 where to_char(send_date,''yyyyMMdd'')=?')";
                    WebUtil.Query(sql);
                }
                catch { }

                try
                {
                    WebUtil.Query("create or replace view v_current_login as select USERNAME,application,CREATE_TIME,LAST_TRY from ssotoken order by LAST_TRY desc ");
                    WebUtil.Query("create or replace view v_SMSTRANSACTION_FLAG_8 as select REF_NO,SEQ_NO as SQ,MEMBER_NO as MNO,TELEPHONE_NUMBER as NO ,MESSAGE_TEXT as SMS,CREATE_DATE as C,SEND_DATE as S,MESSAGE_STATUS as MS,FROM_SYSTEM as SC,SMS_TRANS_CODE as code,POST_FLAG as flag from SMSTRANSACTION where post_flag=8 order by REF_NO ASC,SEQ_NO ASC");
                    WebUtil.Query("create or replace view v_SMSTRANSACTION_FLAG_0 as select REF_NO,SEQ_NO as SQ,MEMBER_NO as MNO,TELEPHONE_NUMBER as NO ,MESSAGE_TEXT as SMS,CREATE_DATE as C,SEND_DATE as S,MESSAGE_STATUS as MS,FROM_SYSTEM as SC,SMS_TRANS_CODE as code,POST_FLAG as flag from SMSTRANSACTION where post_flag=0 order by REF_NO ASC,SEQ_NO ASC");
                    WebUtil.Query("create or replace view v_SMSTRANSACTION_FLAG_1 as select REF_NO,SEQ_NO as SQ,MEMBER_NO as MNO,TELEPHONE_NUMBER as NO ,MESSAGE_TEXT as SMS,CREATE_DATE as C,SEND_DATE as S,MESSAGE_STATUS as MS,FROM_SYSTEM as SC,SMS_TRANS_CODE as code,POST_FLAG as flag from SMSTRANSACTION where post_flag=1 order by REF_NO ASC,SEQ_NO ASC");
                    WebUtil.Query("create or replace view v_SMSTRANSACTION_FLAG_F as select REF_NO,SEQ_NO as SQ,MEMBER_NO as MNO,TELEPHONE_NUMBER as NO ,MESSAGE_TEXT as SMS,CREATE_DATE as C,SEND_DATE as S,MESSAGE_STATUS as MS,FROM_SYSTEM as SC,SMS_TRANS_CODE as code,POST_FLAG as flag from SMSTRANSACTION where post_flag=-9 order by REF_NO ASC,SEQ_NO ASC"); 
                }
                catch { }
                try
                {
                    sql = @"create or replace view smstransaction_dep_sum_mmyyyy as
                                    select CNT_01||', '||CNT_02||', '||CNT_03||', '||CNT_04||', '||CNT_05||', '||CNT_06||', '
                                        ||CNT_07||', '||CNT_08||', '||CNT_09||', '||CNT_10||', '||CNT_11||', '||CNT_12||', ' as CNT from (
                                    select 
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='01' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_01 ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='02' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_02  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='03' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_03  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='04' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_04  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='05' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_05  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='06' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_06  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='07' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_07  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='08' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_08  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='09' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_09  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='10' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_10  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='11' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_11  ,
                                     (select count(*) from smstransaction where from_system='DEP' and to_char(send_date,'mm') ='12' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_12 
                                    from dual ) ";
                    WebUtil.Query(sql);
                    sql = @"create or replace view smstransaction_lon_sum_mmyyyy as
                                    select CNT_01||', '||CNT_02||', '||CNT_03||', '||CNT_04||', '||CNT_05||', '||CNT_06||', '
                                        ||CNT_07||', '||CNT_08||', '||CNT_09||', '||CNT_10||', '||CNT_11||', '||CNT_12||', ' as CNT from (
                                    select 
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='01' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_01 ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='02' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_02  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='03' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_03  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='04' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_04  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='05' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_05  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='06' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_06  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='07' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_07  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='08' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_08  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='09' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_09  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='10' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_10  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='11' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_11  ,
                                     (select count(*) from smstransaction where from_system='LON' and to_char(send_date,'mm') ='12' and to_char(send_date,'yyyy')= to_char(sysdate,'yyyy') ) as CNT_12 
                                    from dual ) ";
                    WebUtil.Query(sql);
                }
                catch { }
            }
            else
            {
            }

            try
            {
                string connectionString = TbConnectionString.Text;
                Sta ta = new Sta(connectionString);
                string  sql = "select * from smstransaction_lon_sum_mmyyyy ";
                Sdt dt = ta.Query(sql);
                if (dt.Next()) {
                    this.SMS_LON_STAT_DATA = dt.GetString("CNT");
                } 
                sql = "select * from smstransaction_dep_sum_mmyyyy ";
                dt = ta.Query(sql);
                if (dt.Next())
                {
                    this.SMS_DEP_STAT_DATA = dt.GetString("CNT");
                }
                ta.Close();
            }
            catch { }
        }

        public void InitJsPostBack()
        {
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearch":
                    break;

            }
        }

        public string ExecuteCmd(string Arguments, string user, string password, string domain)
        {
            return ExecuteCommand("cmd", Arguments, user, password, domain);
        }

        public string ExecuteCommand(string command, string Arguments, string user, string password, string domain)
        {
            string output = null;

            Process p = new Process();

            ProcessStartInfo s = new ProcessStartInfo();
            if (domain != null || domain != "") s.Domain = domain;
            if (user != null || user != "") s.UserName = user;
            if (password != null || password != "")
            {
                s.Password = new SecureString();
                char[] passwords = password.ToCharArray();
                for (int i = 0; i < password.Length; i++)
                {
                    s.Password.AppendChar(passwords[i]);
                }
            }
            s.FileName = command;
            s.UseShellExecute = false;
            s.RedirectStandardOutput = true;
            s.RedirectStandardError = true;
            s.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            if (Arguments != null && Arguments != "") s.Arguments = "/C \"" + Arguments+"\"";
            p.StartInfo = s;
            p.EnableRaisingEvents = true;
            try
            {
                p.Start();

                while (!p.HasExited)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                //check to see what the exit code was
                if (p.ExitCode != 0)
                {
                    output = "Exitcode: " + p.ExitCode + "  - Err1: " + p.StandardError + " - Executor: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
                }
                else
                {
                    output = "Command Result: " + p.StandardOutput.ReadToEnd();
                }
            }
            catch (Exception ex)
            { output += ex.Message; }

            return output;
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public static int sendGMail(string fromAddress, string fromPassword, string[] toAddress, string subject, string body)
        {
            //กรณีผู้ส่งเป็น gmail ต้อง Set lesssecure = on ที่ Link https://www.google.com/settings/security/lesssecureapps
            return sendMail(fromAddress, fromPassword, toAddress, subject, body, "smtp.gmail.com", 587);
        }
        public static int sendMail(string fromAddress, string fromPassword, string[] toAddress, string subject, string body, string host, int port)
        {

            string your_id = fromAddress;
            string your_password = fromPassword;
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(your_id, your_password),
                    Timeout = 10000,
                };
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(your_id);
                for (int i = 0; i < toAddress.Length; i++)
                {
                    mm.To.Add(toAddress[i]);
                }
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.IsBodyHtml = true;
                //mm. = MailFormat.Html;
                mm.Subject = subject;
                mm.Body = body;
                client.Send(mm);
                Console.WriteLine("Email Sent");

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not end email\n\n" + e.ToString());
                return -1;
            }
        }

        protected void Btn_Retreive_Click(object sender, EventArgs e)
        {

            this.TbSQL.Text = this.DropDownList1.SelectedValue;
            LbServerMessage.Text = "";
            LbOutput.Text = "";
            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
           // ta.Transection();
            try
            {
                string sql = TbSQL.Text.Trim();
                if (sql.ToLower().IndexOf("select") >= 0)
                {
                    DataTable dt = ta.QueryDataTable(sql);
                    if (dt != null)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        LbServerMessage.Text = DateTime.Now + " ข้อมูล = " + dt.Rows.Count + " row";
                        LbServerMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        LbServerMessage.Text = DateTime.Now + " ไม่พบข้อมูล";
                        LbServerMessage.ForeColor = Color.Red;
                    }
                }
                else if (sql.ToUpper().IndexOf("GETDATASMS") >= 0)
                {
                    sql = "select * from SMSPATTERNCONFIG where enable_flag=1";
                    Sdt dt = ta.Query(sql);
                    string msg = "";
                    while (dt.Next())
                    {
                        string SMS_TRANS_CODE=dt.GetString("SMS_TRANS_CODE");
                        string SMS_TRANS_SQL = dt.GetString("SMS_TRANS_SQL");
                        string SMS_PATTERN = dt.GetString("SMS_PATTERN");
                        string FROM_SYSTEM = dt.GetString("FROM_SYSTEM");
                        string curDate = DateTime.Now.ToString("yyyyMMdd");
                        string send_date = this.Txb_send_date.Text.Trim();
                        try
                        {
                            sql = "select ss.* , length(SMS) as sms_len from ( select s.*," + SMS_PATTERN + " as SMS from ( select * from " + SMS_TRANS_SQL + " ) s ) ss ";
                            if (send_date.Length <= 0) send_date = curDate;
                            sql = sql.Replace("?", "'" + send_date + "'");
                            msg += "<br/>" + SMS_TRANS_CODE;
                            Sdt dt_ = ta.Query(sql);
                            string REF_NO = DateTime.Now.Ticks.ToString().Substring(0,15), MEMBER_NO = "", TELEPHONE_NUMBER = "", MESSAGE_TEXT = "";
                            Decimal SEQ_NO = 0;
                            msg+=" ข้อมูล "+dt_.Rows.Count;
                            while(dt_.Next()){
                                ++SEQ_NO;
                                MESSAGE_TEXT=dt_.GetString("SMS");
                                TELEPHONE_NUMBER = dt_.GetString("phone_number");
                                MEMBER_NO = dt_.GetString("MEMBER_NO");
                                sql = "insert into SMSTRANSACTION (REF_NO,SEQ_NO,MEMBER_NO,TELEPHONE_NUMBER,MESSAGE_TEXT,CREATE_DATE,SEND_DATE,MESSAGE_STATUS,FROM_SYSTEM,SMS_TRANS_CODE,POST_FLAG)values";
                                sql += "('" + REF_NO + "','" + SEQ_NO + "','" + MEMBER_NO + "','" + TELEPHONE_NUMBER + "','" + MESSAGE_TEXT + "',sysdate,to_date('" + send_date + "','yyyyMMdd'),0,'" + FROM_SYSTEM + "','" + SMS_TRANS_CODE + "',8)";

                                ta.Exe(sql);
                            }

                            if (SMS_TRANS_CODE.IndexOf("N")>=0)
                            {
                                sql = "update smsnewsmessage set send_flag=1,send_date=sysdate where to_char(operate_date,'yyyyMMdd')='" + send_date + "'";
                                WebUtil.ExeSQL(sql);
                            }
                        }
                        catch { }
                    }
                    LbServerMessage.Text = DateTime.Now + " ทำรายการสำเร็จ " + msg;
                    LbServerMessage.ForeColor = Color.Green;
                }
                else if (sql.ToUpper().IndexOf("GETCONFIRMSMS") >= 0)
                {

                    try
                    {
                        string RefNo = DateTime.Now.Ticks.ToString();
                        string Sender_code = "";
                        Decimal seq_no = 0;
                        string Sender_number = "";
                        string Target_number = "";
                        string Url = "";
                        string User = "";
                        string Password = "";

                        sql = "select * from SMSCONFIG where enable_flag=1";
                        Sdt dt = ta.Query(sql);
                        if (dt.Next())
                        {
                            Sender_code = dt.GetString("SENDER_CODE");
                            Sender_number = dt.GetString("SENDER_NUMBER");
                            Url = dt.GetString("URL");
                            User = dt.GetString("USER_NAME");
                            Password = dt.GetString("USER_PWD");

                            sql = "select * from SMSTRANSACTION where post_flag=8 order by REF_NO asc,seq_no asc";
                            dt = ta.Query(sql);
                            if (dt != null)
                            {
                                for (int i = 0; dt.Next(); i++)
                                {
                                    RefNo = dt.GetString("REF_NO");
                                    seq_no = dt.GetDecimal("seq_no");
                                    Target_number = dt.GetString("TELEPHONE_NUMBER");

                                    sql = "update SMSTRANSACTION set post_flag=0,send_date=sysdate where REF_NO='" + RefNo + "' and seq_no='" + seq_no + "' and post_flag=8";
                                    ta.Exe(sql);
                                }
                                LbServerMessage.Text = "ประมวลยืนยันรายการรอส่ง SMS " + DateTime.Now + " ข้อมูล = " + dt.Rows.Count + " row";
                                LbServerMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                LbServerMessage.Text = "ประมวลยืนยันรายการรอส่ง SMS " + DateTime.Now + " ไม่พบข้อมูล";
                                LbServerMessage.ForeColor = Color.Red;
                            }

                            //ta.Commit();
                            ta.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            ta.RollBack();
                        }
                        catch { }
                        ta.Close();
                        LbServerMessage.Text = ex.Message;
                        LbServerMessage.ForeColor = Color.Red;
                    }
                }
                else
                {

                    ta.Exe(sql);
                    LbServerMessage.Text = DateTime.Now + " ทำรายการสำเร็จ";
                    LbServerMessage.ForeColor = Color.Green;
                }
              //  ta.Commit();
                ta.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }
        private string convertToUTF8(string strFrom)
        {
            byte[] bytSrc;
            byte[] bytDestination;
            string strTo = String.Empty;

            bytSrc = Encoding.ASCII.GetBytes(strFrom);
            bytDestination = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, bytSrc);
            strTo = Encoding.UTF8.GetString(bytDestination);

            return strTo;
        }
        protected void Btn_Send_Click(object sender, EventArgs e)
        {

            WebUtil.StartSMSBuilder(state.SsConnectionIndex);

            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            //ta.Transection();
            try
            {
                string RefNo = DateTime.Now.Ticks.ToString();
                string Sender_code = "";
                Decimal seq_no =0;
                string Sender_number = "";
                string Target_number = "";
                string Url = "";
                string User = "";
                string Password = "";
                string send_status = "false";

                string sql = "select * from SMSCONFIG where enable_flag=1";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    Sender_code = dt.GetString("SENDER_CODE");
                    Sender_number = dt.GetString("SENDER_NUMBER");
                    Url = dt.GetString("URL");
                    User = dt.GetString("USER_NAME");
                    Password = dt.GetString("USER_PWD");

                    sql = "select * from SMSTRANSACTION where post_flag=0 order by REF_NO asc,seq_no asc";
                         dt = ta.Query(sql);
                            if (dt != null)
                            {
                                for (int i = 0; dt.Next();i++ )
                                {
                                    RefNo=dt.GetString("REF_NO");
                                    seq_no = dt.GetDecimal("seq_no");
                                    Target_number = dt.GetString("TELEPHONE_NUMBER");
                                    //DTAC http://dtacsmsapi4.dtac.co.th/servlet/com.iess.socket.SmsCorplink
                                    //&RefNo={0}&Sender={1}&Msn={2}&Sno={3}&MsgType=H&Msg={4}&Encoding=25&User={5}&Password={6}
                                    String Parameters = "";
                                    NameValueCollection values = new NameValueCollection();
                                    values["RefNo"] = RefNo; 
                                    Parameters += "&RefNo=" + values["RefNo"];

                                    values["Sender"] = Sender_code; 
                                    Parameters += "&Sender=" + values["Sender"];

                                    //values["Msn"] = Sender_number;
                                    //Parameters += "&Msn=" +values["Msn"];

                                    values["Msn"] = Target_number; 
                                    Parameters += "&Msn=" + values["Msn"];

                                    //values["Sno"] = Target_number; 
                                    //Parameters += "&Sno=" + values["Sno"];

                                    values["MsgType"] = "T"; 
                                    Parameters += "&MsgType=" + values["MsgType"];

                                    values["Msg"] = dt.GetString("MESSAGE_TEXT");
                                    Parameters += "&Msg=" + values["Msg"];

                                    //values["Encoding"] = "25";
                                    //Parameters += "&Encoding=" + values["Encoding"];

                                    values["User"] = User; 
                                    Parameters += "&User=" + values["User"];

                                    values["Password"] = Password; 
                                    Parameters += "&Password=" + values["Password"];

                                    //send_status=this.sendSMS(Url, values);
                                    //send_status = this.HttpPost(Url, Parameters);

                                    //List<string> cmdList = new List<string>();
                                    //string batchfilename = values["RefNo"] + values["User"] + values["Msn"] + ".bat";
                                    //String java_sendsms = "java sendsms " + values["User"] + " " + values["Password"] + " " + values["Sender"] + " " + values["Msn"] + " " + values["RefNo"] + " " + values["Msg"] + "";
                                    //System.Environment.SetEnvironmentVariable("CLASSPATH", "D:\\GCOOP_ALL\\CORE\\GCOOP\\Saving\\Applications\\admin\\;D:\\GCOOP_ALL\\CORE\\GCOOP\\iReport\\ReportBuilderCORE\\dist\\lib\\ojdbc6.jar;%CLASSPATH%");
                                    //cmdList.Add("D:");
                                    //cmdList.Add("SET CLASSPATH=D:\\GCOOP_ALL\\CORE\\GCOOP\\Saving\\Applications\\admin\\");
                                    //cmdList.Add(java_sendsms);
                                    //WebUtil.RunCommand(cmdList, batchfilename);
                                    //try { System.Diagnostics.Process.Start(java_sendsms); }
                                    //catch { }
                                    //send_status = "success";

                                    /*
                                    sql = "update SMSTRANSACTION set message_status=" + (send_status.ToLower().IndexOf("success") >= 0 ? 1 : -1) + ",post_flag=1,send_date=sysdate where REF_NO='" + RefNo + "' and seq_no='" + seq_no + "'";
                                    ta.Exe(sql);
                                    try
                                    {
                                        sql = "update SMSTRANSACTION set send_status_msg='" + send_status + "' where REF_NO='" + RefNo + "' and seq_no='" + seq_no + "'";
                                        ta.Exe(sql);
                                    }
                                    catch { }
                                     */
                                }
                                LbServerMessageSender.Text = "ประมวลส่ง SMS "+DateTime.Now + " ข้อมูล = " + dt.Rows.Count + " row <br/> กรุณากลับไปตรวจสอบสถานะการส่ง SMS อีกครั้งหากพบว่ารอนานเกิน 1 นาทีแล้วรายการยังไม่ถูกประมวลส่ง ให้ประมวลส่ง SMS อีกครั้ง";
                                LbServerMessageSender.ForeColor = Color.Green;
                            }
                            else
                            {
                                LbServerMessageSender.Text = "ประมวลส่ง SMS " + DateTime.Now + " ไม่พบข้อมูล";
                                LbServerMessageSender.ForeColor = Color.Red;
                            }
                
                        //ta.Commit();
                        ta.Close();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }

        public string HttpPost(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. 
            //Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr =
                  new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public String sendSMS(String url,NameValueCollection values)
        {
            bool status = false;
            String responseString = "false";
            try
            {
                var client = new WebClient();
                //var values = new NameValueCollection();
                //values["thing1"] = "hello";
                //values["thing2"] = "world";

                var response = client.UploadValues(url, values);

                 responseString = Encoding.Default.GetString(response);

                //status = true;
            }
            catch {
                //status = false;
            }
            return responseString;
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {

            string connectionString = TbConnectionString.Text;
            Sta ta = new Sta(connectionString);
            //ta.Transection();
            try
            {
                string RefNo = DateTime.Now.Ticks.ToString();
                string Sender_code = "";
                Decimal seq_no = 0;
                string Sender_number = "";
                string Target_number = "";
                string Url = "";
                string User = "";
                string Password = "";
                bool send_status = false;

                string sql = "select * from SMSCONFIG where enable_flag=1";
                Sdt dt = ta.Query(sql);
                if (dt.Next())
                {
                    Sender_code = dt.GetString("SENDER_CODE");
                    Sender_number = dt.GetString("SENDER_NUMBER");
                    Url = dt.GetString("URL");
                    User = dt.GetString("USER_NAME");
                    Password = dt.GetString("USER_PWD");

                    sql = "select * from SMSTRANSACTION where post_flag=0 order by REF_NO asc,seq_no asc";
                    dt = ta.Query(sql);
                    if (dt != null)
                    {
                        for (int i = 0; dt.Next(); i++)
                        {
                            RefNo = dt.GetString("REF_NO");
                            seq_no = dt.GetDecimal("seq_no");
                            Target_number = dt.GetString("TELEPHONE_NUMBER");
                            //DTAC http://corpsms.dtac.co.th/servlet/com.iess.socket.SmsCorplink
                            //&RefNo={0}&Sender={1}&Msn={2}&Sno={3}&MsgType=H&Msg={4}&Encoding=25&User={5}&Password={6}
                            NameValueCollection values = new NameValueCollection();
                            values["RefNo"] = RefNo;
                            values["Sender"] = Sender_code;
                            values["Msn"] = Sender_number;
                            values["Sno"] = Target_number;
                            values["MsgType"] = "H";
                            values["Msg"] = dt.GetString("MESSAGE_TEXT");
                            values["Encoding"] = "25";
                            values["User"] = User;
                            values["Password"] = Password;

                            //send_status = this.sendSMS(Url, values);

                            sql = "update SMSTRANSACTION set post_flag=-9,send_date=sysdate where REF_NO='" + RefNo + "' and seq_no='" + seq_no + "'";
                            ta.Exe(sql);
                        }
                        LbServerMessage.Text = "ประมวลยกเลิกรายการรอส่ง SMS " + DateTime.Now + " ข้อมูล = " + dt.Rows.Count + " row";
                        LbServerMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        LbServerMessage.Text = "ประมวลยกเลิกรายการรอส่ง SMS " + DateTime.Now + " ไม่พบข้อมูล";
                        LbServerMessage.ForeColor = Color.Red;
                    }

                    //ta.Commit();
                    ta.Close();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    ta.RollBack();
                }
                catch { }
                ta.Close();
                LbServerMessage.Text = ex.Message;
                LbServerMessage.ForeColor = Color.Red;
            }
        }

        protected void Btn_Reset_Trans_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = TbConnectionString.Text;
                Sta ta = new Sta(connectionString);
                string sql = "delete from smstransaction ";
                ta.Query(sql);
                ta.Close();
                LbServerMessage.Text = "ประมวลล้างรายการ SMS " + DateTime.Now + " สำเร็จ";
                LbServerMessage.ForeColor = Color.Green;
            }
            catch {
                LbServerMessage.Text = "ประมวลล้างรายการ SMS " + DateTime.Now + " ไม่สำเร็จ";
                LbServerMessage.ForeColor = Color.Red;
            }
        }
    }

   
}