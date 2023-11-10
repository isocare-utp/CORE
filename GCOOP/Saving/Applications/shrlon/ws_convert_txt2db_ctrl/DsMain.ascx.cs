using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_convert_txt2db_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SLSLIPPAYINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYIN;
            //this.EventItemChanged = "OnDsMainItemChanged";            
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
            this.Button.Add("b_memsearch");

            //this.Button.Add("b_contsearch");

        }


        public void DdSliptype()
        {
            string sql = @" 
              	     SELECT   trim(SLUCFSLIPTYPE.SLIPTYPE_CODE) AS SLIPTYPE_CODE,
                    SLUCFSLIPTYPE.SLIPTYPE_DESC,
                    SLUCFSLIPTYPE.SLIPTYPE_SORT,
                    SLUCFSLIPTYPE.SLIPTYPE_CODE || ' - ' || SLUCFSLIPTYPE.SLIPTYPE_DESC as SLTYPE_DISPLAY,
                    1 as sorter 
                FROM SLUCFSLIPTYPE  
                WHERE slucfsliptype.slipmanual_flag = 1  
                union
                select '','',0,'', 0 from dual order by sorter, SLIPTYPE_SORT 
            ";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "SLIPTYPE_CODE", "SLTYPE_DISPLAY", "SLIPTYPE_CODE");

        }

        public void DdMoneyType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,   
                          MONEYTYPE_CODE || ' - '|| MONEYTYPE_DESC as MONEYTYPE_DISPLAY,1 as sorter
                   FROM CMUCFMONEYTYPE 
                   union
                   select '','', 0 from dual order by sorter, MONEYTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MONEYTYPE_CODE", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");

        }
        public void DdFromAccId(string sliptype_code, string moneytype_code)
        {

            string sql = @"
                         
                SELECT 
                        CMUCFTOFROMACCID.MONEYTYPE_CODE, 
			             CMUCFTOFROMACCID.ACCOUNT_ID,    
			             CMUCFTOFROMACCID.ACCOUNT_ID ||'-'||ACCMASTER.ACCOUNT_NAME AS fromacc_display,
                  		 ACCMASTER.ACCOUNT_NAME  ,  
                         1 as sorter
                 FROM ACCMASTER,   
        		      CMUCFTOFROMACCID  
                 WHERE 
		              ( ACCMASTER.COOP_ID = CMUCFTOFROMACCID.COOP_ID )  
                 and  (CMUCFTOFROMACCID.ACCOUNT_ID =ACCMASTER.ACCOUNT_ID)
                 and (CMUCFTOFROMACCID.SLIPTYPE_CODE = '" + sliptype_code + "') and (CMUCFTOFROMACCID.MONEYTYPE_CODE = '" + moneytype_code + @"')
                union select '','','','',0 from dual order by sorter
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "account_id");
        }
        public void DdTofromAccBlank()
        {
            string sql = "select '' as fromacc_display,'' as account_id from dual";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "account_id");

        }

        public void RetrieveMain(string payinslip_no)
        {
            String sql = @"SELECT SLSLIPPAYIN.MEMBGROUP_CODE,   
                         SLSLIPPAYIN.COOP_ID,  
                         SLSLIPPAYIN.DOCUMENT_NO,  
                         SLSLIPPAYIN.PAYINSLIP_NO,   
                         SLSLIPPAYIN.MEMBER_NO,   
                         SLSLIPPAYIN.SLIP_DATE,   
                         SLSLIPPAYIN.OPERATE_DATE,   
                         SLSLIPPAYIN.SLIPTYPE_CODE,   
                         MBMEMBMASTER.MEMB_NAME,   
                         MBMEMBMASTER.MEMB_SURNAME,   
                         MBUCFPRENAME.PRENAME_DESC,   
                         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                         SLSLIPPAYIN.SLIP_AMT,   
                         SLSLIPPAYIN.MONEYTYPE_CODE,   
                         SLSLIPPAYIN.ENTRY_ID,   
                         SLSLIPPAYIN.ACCID_FLAG,   
                         SLSLIPPAYIN.TOFROM_ACCID,   
                         SLSLIPPAYIN.SLIP_STATUS  
                    FROM SLSLIPPAYIN,   
                         MBMEMBMASTER,   
                         MBUCFMEMBGROUP,   
                         SLUCFSLIPTYPE,   
                         CMUCFMONEYTYPE,   
                         MBUCFPRENAME  
                   WHERE ( SLSLIPPAYIN.COOP_ID = MBMEMBMASTER.COOP_ID ) and  
                         ( SLSLIPPAYIN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                         ( SLSLIPPAYIN.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
                         ( SLSLIPPAYIN.SLIPTYPE_CODE = SLUCFSLIPTYPE.SLIPTYPE_CODE ) and  
                         ( SLSLIPPAYIN.MONEYTYPE_CODE = CMUCFMONEYTYPE.MONEYTYPE_CODE ) and  
                         ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and  
                         ( (SLSLIPPAYIN.COOP_ID={0} )and 
                           (SLSLIPPAYIN.PAYINSLIP_NO={1}) )    
   
 ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, payinslip_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);



        }

    }
}