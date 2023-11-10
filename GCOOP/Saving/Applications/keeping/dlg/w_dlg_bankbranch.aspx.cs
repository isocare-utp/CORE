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
using DataLibrary;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_bankbranch : PageWebDialog, WebDialog
    {
        protected String postSave;
        protected String insertRow;
        protected String getbranch;
        String bank_code;
        protected String changebankbranch;

        public void InitJsPostBack()
        {
            postSave = WebUtil.JsPostBack(this, "postSave");
            insertRow = WebUtil.JsPostBack(this, "insertRow");
            changebankbranch = WebUtil.JsPostBack(this, "changebankbranch");
            getbranch = WebUtil.JsPostBack(this, "getbranch");
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
                dw_main.SetTransaction(sqlca);
                dw_search.SetTransaction(sqlca);
                HSql.Value = dw_main.GetSqlSelect();
                HdCloseDlg.Value = "false";
                try
                {

                    bank_code = Request["bank_code"];
                    if (bank_code == "")
                    {
                        dw_main.Retrieve();
                    }
                    else
                    {

                        String sql_bank = " WHERE  CMUCFBANKBRANCH.BANK_CODE = '" + bank_code + "'";
                        dw_main.SetSqlSelect(HSql.Value + sql_bank);
                        dw_main.Retrieve();

                    }
                }
                catch { }

                if (!IsPostBack)
                {

                    dw_main.Retrieve();

                }
                else
                {
                    this.RestoreContextDw(dw_main);

                }
            }
            catch (Exception) { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSave")
            {
                Dw_mainUpdate();
                //    HdCloseDlg.Value = "true";
            }
            else if (eventArg == "insertRow")
            {

                InsertRow();

            }
            else if (eventArg == "getbranch")
            {
                Jsgetbranch();
            }
        }

        private void InsertRow()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                int row = dw_main.RowCount;
                dw_main.InsertRow(row + 1);
                dw_main.SetItemString(row + 1, "bank_code", bank_code);
                
                String Sql = @"SELECT CAST( MAX(CMUCFBANKBRANCH.BRANCH_ID)AS INT)+1 as BRANCH_ID  FROM CMUCFBANKBRANCH WHERE CMUCFBANKBRANCH.BANK_CODE ='" + bank_code + "'  ";
                Sdt dt = ta.Query(Sql);
                if (dt.Next())
                {
                    Int32 BRANCHID = Convert.ToInt32(dt.GetString("BRANCH_ID"));
                    String branch_id = BRANCHID.ToString("0000");
                    dw_main.SetItemString(row + 1, "branch_id", branch_id);

                }

            }
            catch (Exception ex) { LtServerMessage.Text = ex.ToString(); }
            ta.Close();
        }


        private void Jsgetbranch()
        {
            String b_no = "", b_name = "";


            String ls_sql = "", ls_sqlext = "", ls_temp = "";

            ls_sql = dw_main.GetSqlSelect();
            try
            {
                b_no = Hbank_no.Value;

            }
            catch { b_no = ""; }
            try
            {
                b_name = Hbank_name.Value;

            }
            catch { b_name = ""; }


            if (b_no.Length > 0)
            {
                ls_sqlext = "  AND (  CMUCFBANKBRANCH.BRANCH_ID like '" + b_no + "%') ";
            }
            if (b_name.Length > 0)
            {
                ls_sqlext += "  AND ( CMUCFBANKBRANCH.BRANCH_NAME like '" + b_name + "%') ";
            }


            ls_temp = ls_sql + ls_sqlext;
            dw_main.SetSqlSelect(ls_temp);
            dw_main.Retrieve();


        }

        public void WebDialogLoadEnd()
        {
            dw_search.InsertRow(1);
            dw_main.SaveDataCache();
        }

        private void Dw_mainUpdate()
        {
            try
            {
                dw_main.SetItemString(dw_main.RowCount, "bank_code", bank_code);
                dw_main.UpdateData();
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
            }
        }
        public void ChangeDistrict()
        {
            try
            {
                String bank_code = Hprovince_code.Value;
                String sql_bank = " WHERE  CMUCFBANKBRANCH.BANK_CODE = '" + bank_code + "'";
                dw_main.SetSqlSelect(HSql.Value + sql_bank);
                dw_main.Retrieve();

            }
            catch (Exception ex)
            {

                LtServerMessage.Text = WebUtil.ErrorMessage(ex);

            }

        }
    }
}
