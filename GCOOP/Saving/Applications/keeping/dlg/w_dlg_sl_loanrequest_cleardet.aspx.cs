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
    public partial class w_dlg_sl_loanrequest_cleardet : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String closeWebDialog;
        protected String refresh;

        public void InitJsPostBack()
        {
            closeWebDialog = WebUtil.JsPostBack(this, "closeWebDialog");
            refresh = WebUtil.JsPostBack(this, "refresh");
        }

        public void WebDialogLoadBegin()
        {
           
            String xmlDetail = "";
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
                this.RestoreContextDw(dw_cleardet);
            }
            else
            {
                //try
                //{
                //    strList = WebUtil.nstr_itemchange_session(this);
                //    String xmlClear = strList.xml_clear;
                //    String contractNo = Request["contractNo"].ToString();
                    
                //    try
                //    {
                //        xmlDetail = Session["xmlloandetail"].ToString();
                //    }
                //    catch
                //    {
                //        xmlDetail = dw_cleardet.Describe("DataWindow.Data.XML");
                //    }
                //    if ((xmlDetail != null)&&(xmlDetail != ""))
                //    {
                //        int result = shrlonService.ViewLoanClearDetail(state.SsWsPass, contractNo, xmlClear, ref xmlDetail);

                //        if (result == 1)
                //        {
                //            dw_cleardet.Reset();
                //            dw_cleardet.ImportString(xmlDetail, FileSaveAsType.Xml);
                //        }
                //        else
                //        {
                //            dw_cleardet.Reset();
                //            dw_cleardet.InsertRow(0);
                //        }
                //    }
                //    else {
                //        dw_cleardet.Reset();
                //        dw_cleardet.ImportString(xmlDetail, FileSaveAsType.Xml);
                //    }
                //}
                //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
           }  
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "closeWebDialog")
            {
                OnCloseClick();
            }
            if (eventArg == "refresh")
            {
                Refresh();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_cleardet.SaveDataCache();
        }
        public void OnCloseClick()
        {
            String xmlLoanDetail = "";
            try
            {
                xmlLoanDetail = dw_cleardet.Describe("DataWindow.Data.XML");
            }
            catch { xmlLoanDetail = null; }
            Session["xmlloandetail"] = xmlLoanDetail;
            HfChkStatus.Value = "1";
        }
        public void Refresh()
        { 
            
        }
       
       
    }
}
