using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FINSLIPDETDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINSLIPDET;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_sliptypecode");
            this.Register();
        }

        public void RetrieveFinslipDet(string coop_id, string slipno)
        {
            string sql = @"  SELECT FINSLIPDET.SLIP_NO,   
                             FINSLIPDET.COOP_ID,   
                             FINSLIPDET.SEQ_NO,   
                             FINSLIPDET.SLIPITEMTYPE_CODE,   
                             FINSLIPDET.SLIPITEM_DESC,   
                             FINSLIPDET.SLIPITEM_STATUS,   
                             FINSLIPDET.CANCEL_ID,   
                             FINSLIPDET.CANCEL_DATE,   
                             FINSLIPDET.POSTTOVC_FLAG,   
                             FINSLIPDET.VOUCHER_NO,   
                             FINSLIPDET.CANCELTOVC_FLAG,   
                             FINSLIPDET.CANCELVC_NO,   
                             FINSLIPDET.DISPLAYONLY_STATUS,   
                             FINSLIPDET.ITEMPAY_AMT,   
                             FINSLIPDET.ACCOUNT_ID,   
                             FINSLIPDET.ITEMPAYAMT_NET,   
                             FINSLIPDET.TAX_FLAG,   
                             FINSLIPDET.VAT_FLAG,   
                             FINSLIPDET.TAX_CODE,   
                             FINSLIPDET.TAXWAY_KEEP,   
                             FINSLIPDET.TAX_AMT,   
                             FINSLIPDET.MEMBGROUP_CODE,   
                             FINSLIPDET.VAT_AMT, 
                             FINSLIPDET.operate_flag,   
                             '        ' as slip_code_key  
                        FROM FINSLIPDET  
                       WHERE ( FINSLIPDET.SLIP_NO = {1} ) AND  
                             ( FINSLIPDET.COOP_ID = {0} )      ";
            sql = WebUtil.SQLFormat(sql, coop_id, slipno);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }

        public void DDAccid()
        {
            string sql = @"
                  SELECT FINUCFITEMTYPE.ITEM_DESC,  
                    FINUCFITEMTYPE.SLIPITEMTYPE_CODE +'  '+FINUCFITEMTYPE.ITEM_DESC as fullname,
                    FINUCFITEMTYPE.ACCNATURE_FLAG,   
                    FINUCFITEMTYPE.ACCMAP_CODE,   
                    FINUCFITEMTYPE.GENVC_FLAG,   
                    FINUCFITEMTYPE.ACCOUNT_ID,   
                    FINUCFITEMTYPE.SLIPITEMTYPE_CODE  
            FROM FINUCFITEMTYPE  
            WHERE FINUCFITEMTYPE.COOP_ID = {0}
            union select '','',0,'',0,'','' 
		    order by GENVC_FLAG,SLIPITEMTYPE_CODE";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "slipitemtype_code", "fullname", "slipitemtype_code");            
            //this.DropDownDataBind(dt, "account_id", "fullname", "slipitemtype_code");
        }
        public static string SetSlipDesc(string cooptrol,string sliptypecode)
        {
            string ls_desc="";
            string sql = @"
                  SELECT FINUCFITEMTYPE.ITEM_DESC,  
                    FINUCFITEMTYPE.SLIPITEMTYPE_CODE,                     
                    FINUCFITEMTYPE.ACCOUNT_ID
            FROM FINUCFITEMTYPE  
            WHERE FINUCFITEMTYPE.COOP_ID = {0}
            AND FINUCFITEMTYPE.SLIPITEMTYPE_CODE ={1}";
            sql = WebUtil.SQLFormat(sql, cooptrol, sliptypecode);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                ls_desc = dt.GetString("ITEM_DESC");
            }
            return ls_desc;
        }
    }
}