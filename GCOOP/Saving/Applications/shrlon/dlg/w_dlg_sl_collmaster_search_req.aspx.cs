using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNBusscom;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;
namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_collmaster_search_req : PageWebDialog, WebDialog
    {

        protected String collmastersSearch;


        public void InitJsPostBack()
        {
            collmastersSearch = WebUtil.JsPostBack(this, "collmastersSearch");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            dw_master.SetTransaction(sqlca);
            dw_list.SetTransaction(sqlca);
            if (dw_master.RowCount < 1)
            {
                dw_master.InsertRow(0);
                //dw_list.InsertRow(0);
            }
            if (IsPostBack)
            {
                dw_master.RestoreContext();
                dw_list.RestoreContext();
            } if (!IsPostBack) {
                try
                {
                    String memno = Request["member"].Trim();
                    string loantype_code = Request["loantype_code"];
                    dw_master.SetItemString(1, "member_no", memno);
                    HdLoantype_code.Value = loantype_code;
                    CollmastersSearch(memno);
                }
                catch { }
            }
            if (!hidden_search.Value.Equals(""))
            {
                dw_list.SetSqlSelect(hidden_search.Value);
                dw_list.Retrieve();
            }

        }
        private decimal of_getpercentcollmast(string as_coopid, string as_loantype, string as_colltype, string as_collmasttype)
        {
            decimal percent_collmast = 0;
            try
            {
                string sql_collperc = "select coll_percent from lnloantypecolluse where coop_id = '" + as_coopid + "'  and loantype_code = '" + as_loantype + "' and  loancolltype_code  =  '" + as_colltype + "' and  collmasttype_code  = '" + as_collmasttype + "'";

                Sdt dt = WebUtil.QuerySdt(sql_collperc);
                if (dt.Next())
                {
                    percent_collmast = dt.GetDecimal("coll_percent");

                }
            }
            catch
            {
                percent_collmast = 1;

            }
            return percent_collmast;
        }
        private void JsGenLoanColl()
        {

            try
            {
                decimal colluse_perc = 0;
                decimal coll_amt = 0;
                decimal mortgage_price = 0;
                string collmasttype;
                string loantype_code = HdLoantype_code.Value;
                for (int i = 1; i <= dw_list.RowCount; i++)
                {

                    mortgage_price = dw_list.GetItemDecimal(i, "mortgage_price");
                    collmasttype = dw_list.GetItemString(i, "collmasttype_code");
                    colluse_perc = of_getpercentcollmast(state.SsCoopControl, loantype_code, "04", collmasttype);
                    //if (colluse_perc == 0 || colluse_perc == null) { colluse_perc = Convert.ToDecimal(0.9); }
                    coll_amt = mortgage_price * (colluse_perc/100);
                    dw_list.SetItemDecimal(i, "coll_percent", colluse_perc);
                    dw_list.SetItemDecimal(i, "collmast_price", coll_amt);
                    //collmast_price
                
                }

            }
            catch { 
            
            }
        
        
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "collmastersSearch")
            {
              //  CollmastersSearch();
            }
        }
        public void CollmastersSearch(string memno)
        {
           

            string strSQL = @" SELECT DISTINCT LNCOLLMASTER.COLLMAST_NO,   
         LNCOLLMASTER.COLLMAST_REFNO,   
         LNCOLLMASTER.COLLMASTTYPE_CODE,   
         LNCOLLMASTER.COLLMAST_DESC,   
         LNCOLLMASTER.MORTGAGE_PRICE,   
         LNCOLLMASTER.REDEEM_FLAG,   
         LNCOLLMASTMEMCO.MEMCO_NO  
    FROM LNCOLLMASTER,   
         LNCOLLMASTMEMCO  
   WHERE ( LNCOLLMASTER.COLLMAST_NO = LNCOLLMASTMEMCO.COLLMAST_NO (+) ) and  
         ( ( LNCOLLMASTER.MEMBER_NO = '" + memno + @"' ) )  ";
            try
            {
                //dw_list.Reset();
              //  DwUtil.ImportData(strSQL, dw_list, null);
                //dw_list.Retrieve();
             //   dw_list.SetSqlSelect(strSQL);

              //  DwUtil.RetrieveDataWindow(dw_list, "sl_loan_requestment.pbl_cen", null, null);

                dw_list.Retrieve(memno);
                JsGenLoanColl();

            }
            catch (Exception ex)
            {
               // LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        public void WebDialogLoadEnd()
        {
            if(dw_master.RowCount>1){
            dw_master.DeleteRow(dw_master.RowCount);}
        }

    }
}
