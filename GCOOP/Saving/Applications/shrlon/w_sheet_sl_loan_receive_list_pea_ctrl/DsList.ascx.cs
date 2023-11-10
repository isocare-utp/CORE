using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_pea_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNREQLOANDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQLOAN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_detail");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void RetrieveList()
        {
            String sql = @"SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.COOP_ID,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
         ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
         ( ( LNCONTMASTER.COOP_ID = {0} ) AND 
          
         ( ( lncontmaster.withdrawable_amt > 0 ) AND  
         ( lncontmaster.contract_status > 0 ) ) AND  
         ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT SLSLIPPAYOUT.LOANCONTRACT_NO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
   UNION   
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
          
         ( ( lnreqloan.loanrequest_status = 11 ) ) AND  
         ( LNREQLOAN.LOANREQUEST_DOCNO not in ( SELECT SLSLIPPAYOUT.LOANREQUEST_DOCNO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )    
 ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveTypeGroup(string group)
        {
            String sql = @"SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.COOP_ID,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
         ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
         ( ( LNCONTMASTER.COOP_ID = {0} ) AND 
           (LNLOANTYPE.LOANGROUP_CODE={1}  ) AND 
         ( ( lncontmaster.withdrawable_amt > 0 ) AND  
         ( lncontmaster.contract_status > 0 ) ) AND  
         ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT SLSLIPPAYOUT.LOANCONTRACT_NO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
   UNION   
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
           (LNLOANTYPE.LOANGROUP_CODE = {1}  ) AND
         ( ( lnreqloan.loanrequest_status = 11 ) ) AND  
         ( LNREQLOAN.LOANREQUEST_DOCNO not in ( SELECT SLSLIPPAYOUT.LOANREQUEST_DOCNO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )    
 ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, group);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


        //ใช้งานอยู่
        public void RetrieveListEntryGroup(string group, string entry)
        {
            String sql = @"SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.COOP_ID,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  ,
         0 as loanrequest_status,
null as slip_date
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
         ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
         ( ( LNCONTMASTER.COOP_ID = {0} ) AND 
           ( LNLOANTYPE.LOANGROUP_CODE like {1}  ) AND
           ( LNCONTMASTER.APPROVE_ID like {2}  ) AND
         ( ( lncontmaster.withdrawable_amt > 0 ) AND  
         ( lncontmaster.contract_status > 0 ) ) AND  
         ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT SLSLIPPAYOUT.LOANCONTRACT_NO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
   UNION   
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  ,
        lnreqloan.loanrequest_status,
        lnreqloan.printpay_date as slip_date
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
           ( LNLOANTYPE.LOANGROUP_CODE like {1}  ) AND
           ( LNREQLOAN.ENTRY_ID like {2}  ) AND
         ( ( lnreqloan.loanrequest_status in (11,12) ) ) AND  
         ( LNREQLOAN.LOANREQUEST_DOCNO not in ( SELECT SLSLIPPAYOUT.LOANREQUEST_DOCNO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )    
 ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, group, entry);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }



        public void RetrieveSearch(String sqlext_con, String sqlext_req)
        {
            String sql = @"SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.COOP_ID,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,  
		 LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNCONTMASTER.APPROVE_ID,
         LNCONTMASTER.LOANAPPROVE_DATE,
         LNLOANTYPE.PREFIX,  
         LNLOANTYPE.LOANTYPE_CODE, 
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE, 

	
         0 as operate_flag  
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
         ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
         ( ( LNCONTMASTER.COOP_ID =  {0} ) AND  
         ( ( lncontmaster.withdrawable_amt > 0 )  
" + sqlext_con + @" AND

         ( lncontmaster.contract_status > 0 ) ) AND  
         ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT SLSLIPPAYOUT.LOANCONTRACT_NO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
   UNION   
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT, 
         LNREQLOAN.APPROVE_ID,
         LNREQLOAN.APPROVE_DATE,

		 LNLOANTYPE.LOANTYPE_CODE,  
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID =  {0} ) 
" + sqlext_req + @" AND
         ( ( lnreqloan.loanrequest_status = 11 ) ) AND  
         ( LNREQLOAN.LOANREQUEST_DOCNO not in ( SELECT SLSLIPPAYOUT.LOANREQUEST_DOCNO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )    

";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            //this.DATA[0].NOTICEDUE_DATE = state.SsWorkDate;

        }

        //ใช้งานอยู่ ค้นหาข้อมูลตามเลขสมาชิก
        public void RetrieveByMembno(string memb_no) {
            string sql = @"SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.COOP_ID,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag    ,
         0 as loanrequest_status,
null as slip_date
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
         ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
         ( ( LNCONTMASTER.COOP_ID = {0} ) AND 
(LNCONTMASTER.MEMBER_NO = {1}) and
         ( ( lncontmaster.withdrawable_amt > 0 ) AND  
         ( lncontmaster.contract_status > 0 ) ) AND  
         ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT nvl(SLSLIPPAYOUT.LOANCONTRACT_NO,'-') FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
   UNION   
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag    ,
        lnreqloan.loanrequest_status,
lnreqloan.printpay_date as slip_date
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
(LNREQLOAN.MEMBER_NO = {1}) and
         ( ( lnreqloan.loanrequest_status in (11,12) ) ) 
)    
";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId,memb_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        //ใช้งานอยู่ ค้นหาข้อมูลที่อนุมัติแบบมีเลบสัญญา
        public void RetrieveListEntryApvContno(string group, string entry)
        {
            String sql = @"SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.COOP_ID,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  ,
         0 as loanrequest_status,
null as slip_date
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.MEMCOOP_ID ) and  
         ( LNLOANTYPE.COOP_ID = LNCONTMASTER.COOP_ID) and  
         ( ( LNCONTMASTER.COOP_ID = {0} ) AND 
           ( LNLOANTYPE.LOANGROUP_CODE like {1}  ) AND
           ( LNCONTMASTER.APPROVE_ID like {2}  ) AND
         ( ( lncontmaster.withdrawable_amt > 0 ) AND  
         ( lncontmaster.contract_status > 0 ) ) AND  
         ( LNCONTMASTER.LOANCONTRACT_NO not in ( SELECT SLSLIPPAYOUT.LOANCONTRACT_NO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )   
";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, group, entry);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        //ใช้งานอยู่ ค้นหาข้อมูลที่อนุมัติแบบไม่มีเลขสัญญา
        public void RetrieveListEntryApvNoContno(string group, string entry)
        {
            String sql = @"
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO as LOANCONTRACT_NO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT as withdrawable_amt,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  ,
        lnreqloan.loanrequest_status,
lnreqloan.printpay_date as slip_date
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
           ( LNLOANTYPE.LOANGROUP_CODE like {1}  ) AND
           ( LNREQLOAN.ENTRY_ID like {2}  ) AND
         ( ( lnreqloan.loanrequest_status in (11,12) ) ) AND  
         ( LNREQLOAN.LOANREQUEST_DOCNO not in ( SELECT SLSLIPPAYOUT.LOANREQUEST_DOCNO FROM SLSLIPPAYOUT WHERE SLSLIPPAYOUT.SLIP_STATUS = 8 )) )    
  
order by LOANCONTRACT_NO";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, group, entry);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        //ใช้งานอยู่ ค้นหาข้อมูลที่อนุมัติแบบไม่มีเลขสัญญาและพิมพ์จ่ายแล้ว
        public void RetrieveListEntryPrintPay(string group, string entry)
        {
            String sql = @"
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO as LOANCONTRACT_NO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT as withdrawable_amt,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  ,
        lnreqloan.loanrequest_status,
lnreqloan.printpay_date as slip_date
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
           ( LNLOANTYPE.LOANGROUP_CODE like {1}  ) AND
           ( LNREQLOAN.ENTRY_ID like {2}  ) AND
         ( ( lnreqloan.loanrequest_status = 12 ) ) )    
 
order by entry_date,loanrequest_docno";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, group, entry);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        //ใช้งานอยู่ ค้นหาข้อมูลที่อนุมัติแบบไม่มีเลขสัญญาและพิมพ์จ่ายแล้ว ตามวันที่ที่พิมพ์จ่าย
        public void RetrieveListEntryPrintPayDate(string group, string entry,DateTime by_date)
        {
            String sql = @"
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.COOP_ID,   
         LNREQLOAN.LOANREQUEST_DOCNO as LOANCONTRACT_NO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT as withdrawable_amt,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  ,
        lnreqloan.loanrequest_status,
lnreqloan.printpay_date as slip_date
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( MBMEMBMASTER.COOP_ID = LNREQLOAN.MEMCOOP_ID ) and  
         ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( ( LNREQLOAN.MEMCOOP_ID = {0} ) AND 
           ( LNLOANTYPE.LOANGROUP_CODE like {1}  ) AND
           ( LNREQLOAN.ENTRY_ID like {2}  ) AND
         ( ( lnreqloan.loanrequest_status = 12 ) )  
and printpay_date={3}
)    
 
order by entry_date,loanrequest_docno";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, group, entry,by_date);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}