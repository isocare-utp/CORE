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
    public partial class w_sheet_divsrv_constant : PageWebSheet,WebSheet
    {
        public String pbl = "divsrv_constant.pbl";
        protected String postNewClear;
        protected String postRefresh;
        //=======================
        private void JspostNewClear()
        {
            Dw_main.Reset();
            Dw_main.Retrieve(state.SsCoopId);

            Dw_year.Reset();
            Dw_year.Retrieve(state.SsCoopId);
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            DwUtil.RetrieveDDDW(Dw_main, "dfmethtype_code", pbl, null);
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
            }
            catch { LtServerMessage.Text = WebUtil.CompleteMessage("ติดต่อ Database ไม่ได้"); }

            Dw_main.SetTransaction(sqlca);
            Dw_year.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
                this.RestoreContextDw(Dw_year);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postNewClear")
            {
                JspostNewClear();
            }
            
        }

        public void SaveWebSheet()
        {
            try
            {
               // DwUtil.UpdateDataWindow(Dw_main, "divsrv_constant.pbl", "yrcfconstant");
                Dw_main.UpdateData();
                Dw_year.UpdateData();
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
            Dw_year.SaveDataCache();
        }
        #endregion
    }
}

