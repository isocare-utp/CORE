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
//using CoreSavingLibrary.WcfDivavg;
using System.Web.Services.Protocols;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_cnt_constant : PageWebSheet, WebSheet
    {
        protected String postNewClear;
        protected String postRefresh;
        //=======================
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
        }

        public void SaveWebSheet()
        {
            try
            {
                DwUtil.UpdateDataWindow(Dw_main, "kp_constant.pbl", "kpconstant");
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