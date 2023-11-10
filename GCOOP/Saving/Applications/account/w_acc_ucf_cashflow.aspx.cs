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
using DataLibrary; //เพิ่มเติม
using System.Data.OracleClient; //เพิ่มเติม
using Sybase.DataWindow; //เพิ่มเติม
using System.Globalization; //เพิ่มเติม

namespace Saving.Applications.account
{
    public partial class w_acc_ucf_cashflow : PageWebSheet, WebSheet
    {
        protected String jsPostGetAccid;
        string pbl = "cm_constant_config.pbl";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            jsPostGetAccid = WebUtil.JsPostBack(this, "jsPostGetAccid");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwUtil.RetrieveDataWindow(Dwmain, pbl, null, null);
            }
            else
            {
                this.RestoreContextDw(Dwmain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsPostGetAccid":
                    GetAccid();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                DwUtil.UpdateDataWindow(Dwmain, pbl, "ACCUCFCASHFLOW");
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้");
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(Dwmain, "account_activity", pbl, null);
            }
            catch { }
            Dwmain.SaveDataCache();
        }

        #endregion

        #region Function
        private void GetAccid()
        {
            string acc_list = Hdacclist.Value;
            int row = Convert.ToInt32(Hdrow.Value);
            Dwmain.SetItemString(row, "accid_list", acc_list);
        }
        #endregion
    }
}