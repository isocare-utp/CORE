using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_intratespc : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String saveWebDialog;
        protected String refreshDW;

        public void InitJsPostBack()
        {
            saveWebDialog = WebUtil.JsPostBack(this, "saveWebDialog");
            refreshDW = WebUtil.JsPostBack(this, "refreshDW");
        }

        public void WebDialogLoadBegin()
        {
            //str_requestopen strRequestOpen = new str_requestopen();
            str_itemchange strList = new str_itemchange();
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            this.ConnectSQLCA();
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_intspc);
            }
            if (HdFirst.Value == "")
            {

                try
                {
                    HdFirst.Value = "1";
                    strList = WebUtil.nstr_itemchange_session(this);
                    dw_intspc.Reset();
                    dw_intspc.ImportString(strList.xml_intspc, FileSaveAsType.Xml);
                }
                catch { dw_intspc.Reset(); dw_intspc.InsertRow(0); }
            }
           
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveWebDialog")
            {
                OnSaveClick();
            }
            if (eventArg == "refreshDW")
            {
                RefreshDW();
            }
        }

        public void WebDialogLoadEnd()
        {
            DataWindowChild dwIntTabCode = dw_intspc.GetChild("int_continttabcode");
            String StrXml = commonService.of_getdddwxml(state.SsWsPass, "dddw_sl_cfloanintrate");
            dwIntTabCode.Reset();
            dwIntTabCode.ImportString(StrXml, FileSaveAsType.Xml);
            dw_intspc.SaveDataCache();
        }
        protected void OnSaveClick()
        {
            //str_requestopen strRequestOpen = new str_requestopen();
            str_itemchange strList = new str_itemchange();

            if (dw_intspc.RowCount > 0)
            {
                try
                {
                    strList.xml_intspc = dw_intspc.Describe("DataWindow.Data.XML");
                    //strRequestOpen.xml_intspc  = dw_intspc.Describe("DataWindow.Data.XML");
                    Session["strItemchange"] = strList;
                    HfChkStatus.Value = "1";
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }

            }
            else if (dw_intspc.RowCount == 0) 
            {
                try
                {
                    strList.xml_intspc = dw_intspc.Describe("DataWindow.Data.XML");
                    Session["strItemchange"] = strList;
                    HfChkStatus.Value = "2";
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }
        protected void RefreshDW()
        {

        }

       
    }
}
