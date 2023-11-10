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

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_chqformat_add : PageWebDialog, WebDialog
    {
        protected String postAddFormat;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postAddFormat = WebUtil.JsPostBack(this, "postAddFormat");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();

            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwUtil.RetrieveDDDW(DwMain, "bank_code", "setcheque_format.pbl", null);
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postAddFormat")
            {
                AddFormat();
            }
            else if (eventArg == "Update")
            {
                AddFormat();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion

        private void AddFormat()
        {
            DwMain.SetTransaction(sqlca);
            DwMain.UpdateData();


            String bankCode, description, printer_type;
            Decimal cheque_type;

            bankCode = DwMain.GetItemString(1, "bank_code");
            cheque_type = DwMain.GetItemDecimal(1, "cheque_type");
            description = DwMain.GetItemString(1, "description");
            printer_type = DwMain.GetItemString(1, "printer_type");

            String sql = "INSERT INTO FINUCFCHEQUEFORMAT (bank_code,cheque_type,branch_id,printer_type,description) VALUES('" + bankCode + "'," + cheque_type + ",'" + state.SsCoopId + "','" + printer_type + "','" + description + "')";

            //Sta ta = new Sta("Data Source=imm/gcoop;Persist Security Info=True;User ID=scobkcatoff;Password=scobkcatoff;Unicode=True");
            //Sdt dt = new Sdt();
            //ta.Query(sql);
            DwUtil.ImportData(sql, DwMain, null);
        }

        protected void dw_main_BeginUpdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!state.IsWritable)
            {
                e.Cancel = true;
                sqlca.Rollback();
            }
        }

        protected void dw_main_EndUpdate(object sender, Sybase.DataWindow.EndUpdateEventArgs e)
        {
            if (e.RowsUpdated > 0 && state.IsWritable)
            {
                sqlca.Commit();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            }
            else if (e.RowsInserted > 0 && state.IsWritable)
            {
                sqlca.Commit();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            }
            else if (e.RowsDeleted > 0 && state.IsWritable)
            {
                sqlca.Commit();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
            }
            else
            {
                sqlca.Rollback();
            }
        }
    }
}
