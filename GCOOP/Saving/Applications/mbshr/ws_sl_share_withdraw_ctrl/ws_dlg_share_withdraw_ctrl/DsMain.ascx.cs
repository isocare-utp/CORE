﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_share_withdraw_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.SLSLIPPAYOUTDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYOUT;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            // this.Button.Add("b_search");
            //this.Button.Add("b_contsearch");
            this.Register();
        }

        public void DdMoneyType()
        {
            string sql = "";
            if (this.DATA[0].BFSHARE_STATUS == 8)
            {
                sql = @" 
            SELECT MONEYTYPE_CODE,   
                MONEYTYPE_DESC,   
                SORT_ORDER  ,
                MONEYTYPE_CODE +' - '+ MONEYTYPE_DESC as MONEYTYPE_DISPLAY,
                1 as sorter
            FROM CMUCFMONEYTYPE where MONEYTYPE_STATUS = 'DAY'
            union 
            select '','', 0,'', 0 from dual order by sorter, MONEYTYPE_CODE";
            }
            else
            {
                 sql = @" 
            SELECT MONEYTYPE_CODE,   
                MONEYTYPE_DESC,   
                SORT_ORDER  ,
                MONEYTYPE_CODE + ' - '+ MONEYTYPE_DESC as MONEYTYPE_DISPLAY,
                1 as sorter
            FROM CMUCFMONEYTYPE
            where moneytype_code <> 'TSA' and MONEYTYPE_STATUS = 'DAY'
            union 
            select '','', 0,'', 0 from dual order by sorter, MONEYTYPE_COD ";
          
            }
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MONEYTYPE_CODE", "MONEYTYPE_DISPLAY", "MONEYTYPE_CODE");

        }
        public void DdBankDesc()
        {
            string sql = @"
            select  bank_code,bank_code + ' '+ bank_desc  as bank_desc, 1 as sorter from cmucfbank 
            union 
            select '','', 0 from dual order by sorter, bank_desc
            ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_bank", "bank_desc", "bank_code");
        }
        public void DdBranch(string bank_code)
        {
            string sql = @"
            select bank_code,branch_id, branch_name, 1 as sorter from cmucfbankbranch where bank_code = {0} 
            union
            select '', '','', 0 from dual order by sorter, branch_name
            ";
            sql = WebUtil.SQLFormat(sql, bank_code);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "expense_branch", "branch_name", "branch_id");
        }

        public void DdFromAccId(string sliptype_code, string moneytype_code)
        {
            //            string sql = @"
            //            select moneytype_code,moneytype_code||' - '||account_id||' - '||account_desc as fromacc_display,1 as sorter from cmucftofromaccid
            //            union
            //            select '','', 0 from dual order by sorter, moneytype_code
            //            ";
            string sql = @"
                   SELECT 
                        CMUCFTOFROMACCID.MONEYTYPE_CODE, 
			             CMUCFTOFROMACCID.ACCOUNT_ID,    
			             CMUCFTOFROMACCID.ACCOUNT_ID +'-'+ ACCMASTER.ACCOUNT_NAME AS fromacc_display,
                  		 ACCMASTER.ACCOUNT_NAME  ,  
                         1 as sorter
                 FROM ACCMASTER,   
        		      CMUCFTOFROMACCID  
                 WHERE 
		              ( ACCMASTER.COOP_ID = CMUCFTOFROMACCID.COOP_ID )  
                 and  (CMUCFTOFROMACCID.ACCOUNT_ID =ACCMASTER.ACCOUNT_ID)
                 and (CMUCFTOFROMACCID.SLIPTYPE_CODE = '" + sliptype_code + "') and (CMUCFTOFROMACCID.MONEYTYPE_CODE = '" + moneytype_code + @"')
                union select '','','','',0 from dual order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "fromacc_display", "ACCOUNT_ID");
        }
    }
}