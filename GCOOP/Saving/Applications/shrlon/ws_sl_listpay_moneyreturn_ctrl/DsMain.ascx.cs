using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_listpay_moneyreturn_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MONEYRETURNDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MONEYRETURN;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainItemClicked";
            this.Button.Add("b_retrieve");
            this.Register();
            
        }

        public void DdMoneyType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,   
                          MONEYTYPE_CODE + ' - '+ MONEYTYPE_DESC as MONEYTYPE_DISPLAY,1 as sorter
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
			             CMUCFTOFROMACCID.ACCOUNT_ID + '-' + ACCMASTER.ACCOUNT_NAME AS fromacc_display,
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

        public void DdBank()
        {
            string sql = @" select bank_code,bank_code + ' - '+ bank_desc as bank_desc,1 as sorter from cmucfbank
union
select '','',0 from dual order by sorter,bank_code
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "bank_code", "bank_desc", "bank_code");
        }
        public void DdCashType()
        {
            string sql = @" 
                   SELECT MONEYTYPE_CODE,   
                          MONEYTYPE_CODE + ' - '+  MONEYTYPE_DESC as MONEYTYPE_DISPLAY,1 as sorter
                   FROM CMUCFMONEYTYPE 
                   union
                   select '','', 0 from dual order by sorter, MONEYTYPE_CODE
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "cash_type", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");
        }
    }
}