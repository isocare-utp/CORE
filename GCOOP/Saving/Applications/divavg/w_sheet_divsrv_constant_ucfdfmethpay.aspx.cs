using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Sybase.DataWindow;
using DataLibrary;
using CoreSavingLibrary.WcfNDivavg;
using System.Web.Services.Protocols;

namespace Saving.Applications.divavg
{
    public partial class w_sheet_divsrv_constant_ucfdfmethpay : PageWebSheet, WebSheet
    {
        public String pbl = "divsrv_constant.pbl";
        protected String postNewClear;
        protected String postRefresh;
        protected String postDeleteRow;
        protected String postAddRow;
        //=======================
        private void JspostAddRow()
        {
            try 
            {
                Dw_main.InsertRow(0);
                Dw_main.SetItemDecimal(Dw_main.RowCount, "seq_no", Dw_main.RowCount);
                Dw_main.SetItemString(Dw_main.RowCount, "coop_id", state.SsCoopId);
                
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
        private void JspostDeleteRow()
        {
            try 
            {
                int rowcurrent = int.Parse(Hd_row.Value);
                Dw_main.DeleteRow(rowcurrent);
                Dw_main.UpdateData();
              //  DwUtil.UpdateDataWindow(Dw_main, "divsrv_constant.pbl", "yrucfdfmethpay");
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.Retrieve(state.SsCoopId);
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postDeleteRow = WebUtil.JsPostBack(this, "postDeleteRow");
            postAddRow = WebUtil.JsPostBack(this, "postAddRow");

            DwUtil.RetrieveDDDW(Dw_main, "methpaytype_code", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(Dw_main, "expense_bank", pbl, null);
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
            }
            catch { LtServerMessage.Text = WebUtil.CompleteMessage("ติดต่อ Database ไม่ได้"); }

            Dw_main.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            else if (eventArg == "postRefresh")
            { 

            }
            else if (eventArg == "postDeleteRow")
            {
                JspostDeleteRow();
            }
            else if (eventArg == "postAddRow")
            {
                JspostAddRow();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
              //  DwUtil.UpdateDataWindow(Dw_main, "divsrv_constant.pbl", "yrucfdfmethpay");
                Dw_main.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
        }
        #endregion
    }
}