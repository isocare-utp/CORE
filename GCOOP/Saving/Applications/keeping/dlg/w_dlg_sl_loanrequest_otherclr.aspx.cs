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

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_loanrequest_otherclr : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected  String saveWebDialog;
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
                this.RestoreContextDw(dw_otherclr);
            }
            if (HdFirst.Value == "")
            {
              
                try
                {
                    //strRequestOpen = Session["requestopen"] as str_requestopen;
                    strList = WebUtil.nstr_itemchange_session(this);

                    HdFirst.Value = "1";
                    dw_otherclr.Reset();
                    //dw_otherclr.ImportString(strRequestOpen.xml_otherclr, FileSaveAsType.Xml);
                    dw_otherclr.ImportString(strList .xml_otherclr , FileSaveAsType.Xml);
                }
                catch { dw_otherclr.Reset(); dw_otherclr.InsertRow(0); }
            }
            else {
                this.RestoreContextDw(dw_otherclr);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveWebDialog")
            {
                OnSaveClick();
            }
            if (eventArg == "refreshDW") {
                RefreshDW();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_otherclr.SaveDataCache();

            DataWindowChild dwSlipItemType = dw_otherclr.GetChild("clrothertype_code");
            String StrXMLSIT = commonService.of_getdddwxml(state.SsWsPass, "dddw_sl_ucfslipitemtype");
            dwSlipItemType.ImportString(StrXMLSIT, FileSaveAsType.Xml);
        }

        protected void OnSaveClick()
        { 
            str_itemchange strList = new str_itemchange();
            if (dw_otherclr.RowCount > 0) {
                try
                {
                    strList.xml_otherclr = dw_otherclr.Describe("DataWindow.Data.XML");
                    Session["strItemchange"] = strList;
                    HfChkStatus.Value = "1";
                }
                catch (Exception ex){
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (dw_otherclr.RowCount == 0) 
            {
                try
                {
                    strList.xml_otherclr = null ;
                    Session["strItemchange"] = strList;
                    HfChkStatus.Value = "2";
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }
        protected void RefreshDW() { 
        
        }

       
    }
}
