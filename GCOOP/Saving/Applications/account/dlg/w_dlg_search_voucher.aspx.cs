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

using System.Globalization;
using Sybase.DataWindow;
using DataLibrary;
using System.Data.OracleClient;


namespace Saving.Applications.account.dlg
{
    public partial class w_dlg_search_voucher : PageWebDialog, WebDialog 
    {
        private DwThDate tDwMain;
      //  private WebState state;
   //     private DwTrans sqlca;
        private string is_sql;
        
        protected String postNewClear;



        private void JspostNewClear() {
            Dw_main.Reset();
            Dw_list.Reset();
            Dw_main.InsertRow(0);
            Dw_list.InsertRow(0);
        }


        #region WebDialog Members

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_list.SetTransaction(sqlca);
           // Dw_main.SetTransaction(sqlca);
            is_sql = Dw_list.GetSqlSelect();
            
            
            tDwMain = new DwThDate(Dw_main);
            tDwMain.Add("voucher_date", "voucher_tdate"); // วันที่ภาษาอังกฤษและวันที่ภาษาไทย

            if (!IsPostBack)
            {
                Dw_list.InsertRow(0);
                Dw_main.InsertRow(0);
                HSqlTemp.Value = is_sql;

                String VcTdate = "";
                String branchId = "";
                try { VcTdate = Request["vcDate"].Trim(); }
                    catch { }
                try {branchId = Request["branchId"].Trim();}
                    catch {}
                try
                {
                    DateTime vcDate = DateTime.ParseExact(VcTdate, "ddMMyyyy", new CultureInfo("th-TH"));
                        if (vcDate.Year > 1370)
                        {
                            // การ set วันที่ด้วย code behind
                            Dw_main.SetItemDate(1, "voucher_date", vcDate);
                            Dw_main.SetItemString(1, "voucher_tdate", vcDate.ToString("ddMMyyyy", new CultureInfo("th-TH")));

                            string ls_sqltext, ls_temp, ls_branch_id, ls_voucher_date;
                            //string ls_voucher_date;
    
                            ls_sqltext = "";

                            try
                            {
                                ls_voucher_date = Dw_main.GetItemString(1, "voucher_date");
                            }
                            catch
                            {

                            }
                            try
                            {
                                ls_branch_id = branchId;
                            }
                            catch 
                            {
                                ls_branch_id = "";
                            }

                            ls_voucher_date = Dw_main.GetItemString(1, "voucher_date");
                            if (ls_voucher_date != null)
                            {
                                ls_sqltext = "and ( VCVOUCHER.VOUCHER_DATE = to_date('"+ls_voucher_date+"','dd/mm/yyyy'))";
                            }
                            if (ls_branch_id.Length > 0)
                            {
                                ls_sqltext += " and ( VCVOUCHER.BRANCH_ID = '" + ls_branch_id + "') ";
                            }
                            

                            if (ls_sqltext == null) ls_sqltext = "";
                            ls_temp = is_sql + ls_sqltext;
                            HSqlTemp.Value = ls_temp;
                            Dw_list.SetSqlSelect(HSqlTemp.Value);//.SetSqlSelect(HSqlTemp.Value);
                            Dw_list.Retrieve();
                            if (Dw_list.RowCount < 1) {
                                LtServerMessage.Text = WebUtil.CompleteMessage("ยังไม่มีรายการ Voucher ใด  ๆ ในวันดังกล่าว");
                                Dw_list.Reset();
                                Dw_list.InsertRow(0);

                            }
                        }
                }
                catch(Exception ex)
                {
                    ex.ToString();
                }
                
            }
            else {
                Dw_list.RestoreContext();
                Dw_main.RestoreContext();
            }
        }

       
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear") {
                JspostNewClear();
            }
        }

        public void WebDialogLoadEnd()
        {
        }

        #endregion

        protected void B_search_Click(object sender, EventArgs e)
        {
         

            string ls_voucher_no, ls_sqltext, ls_temp, ls_branch_id;
            string ls_voucher_date;

            tDwMain = new DwThDate(Dw_main);
            tDwMain.Add("voucher_date", "voucher_tdate");


          
            //string ls_voucher_date;

            ls_sqltext = "";

            try
            {
                ls_voucher_date = Dw_main.GetItemString(1, "voucher_date");
            }
            catch
            {

            }
            try
            {
                ls_branch_id = state.SsCoopId;
            }
            catch
            {
                ls_branch_id = "";
            }
            try
            {
                ls_voucher_no = Dw_main.GetItemString(1, "voucher_no");
            }
            catch
            {
                ls_voucher_no = "";
            }
            //===
            ls_voucher_date = Dw_main.GetItemString(1, "voucher_date");
            if (ls_voucher_date != null)
            {
                ls_sqltext = "and ( VCVOUCHER.VOUCHER_DATE = to_date('" + ls_voucher_date + "','dd/mm/yyyy'))";
            }
            if (ls_branch_id.Length > 0)
            {
                ls_sqltext += " and ( VCVOUCHER.BRANCH_ID = '" + ls_branch_id + "') ";
            }
            if (ls_voucher_no.Length > 0)
            {
                ls_sqltext += "and ( VCVOUCHER.VOUCHER_NO like  '%" + ls_voucher_no + "%') ";
            }


            if (ls_sqltext == null) ls_sqltext = "";
            ls_temp = is_sql + ls_sqltext;
            HSqlTemp.Value = ls_temp;
            Dw_list.SetSqlSelect(HSqlTemp.Value);//.SetSqlSelect(HSqlTemp.Value);
            Dw_list.Retrieve();
            
            if (Dw_list.RowCount < 1)
            {
                LtServerMessage.Text = WebUtil.CompleteMessage("ยังไม่มีรายการ Voucher ใด  ๆ ในวันดังกล่าว");
                Dw_list.Reset();
                Dw_list.InsertRow(0);


            }
        }
    }
}
