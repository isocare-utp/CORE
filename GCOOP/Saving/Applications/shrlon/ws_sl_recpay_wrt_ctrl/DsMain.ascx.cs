using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_recpay_wrt_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FINSLIPDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINSLIP;
            //this.EventItemChanged = "OnDsMainItemChanged";            
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Register();
            this.Button.Add("b_memsearch");
            //this.Button.Add("b_ret");

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
            this.DropDownDataBind(dt, "cash_type", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");
           
            
        }


        public void DdFromAccId(string moneytype_code)
        {

            string sql = @"SELECT 
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
and (CMUCFTOFROMACCID.MONEYTYPE_CODE = 'CSH')
                union select '','','','',0 from dual order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "account_id");
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

        public void RetrieveMemb(string memb_no) {
            string sql = @"select mb.member_no,mp.prename_desc||mb.memb_name||' '||mb.memb_surname as fullname ,mg.membgroup_code||'-'||mg.membgroup_desc as membgroup
from mbmembmaster mb  ,mbucfprename mp,mbucfmembgroup mg
where mb.member_no = {0}
and mb.coop_id = {1}  
and mb.prename_code = mp.prename_code
and mb.membgroup_code = mg.membgroup_code
and mb.coop_id = mg.coop_id";
            sql = WebUtil.SQLFormat(sql, memb_no, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        
        }
    }
}