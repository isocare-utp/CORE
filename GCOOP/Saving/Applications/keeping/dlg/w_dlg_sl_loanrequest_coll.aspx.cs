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
using System.Globalization;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_sl_loanrequest_coll : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String closeWebDialog;

        public void InitJsPostBack()
        {
            closeWebDialog = WebUtil.JsPostBack(this, "closeWebDialog");
        }

        public void WebDialogLoadBegin()
        {
            String collType = "";
            String xmlHead = "";
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
                this.RestoreContextDw(dw_colldet );
            }
            else
            {
                try
                {
                    strList = WebUtil.nstr_itemchange_session(this);
                    
                    String xmlColl = strList.xml_guarantee;
                    String date = Request["date"].ToString();
                    String refCollNo = Request["refCollNo"].ToString();
                    collType = Request["collType"].ToString();
                    String strData = collType + refCollNo;

                    DateTime requestDate = DateTime.ParseExact(date, "ddMMyyyy", new CultureInfo("th-TH"));
                    
                    dw_colldet.Reset();
                    dw_colldet.InsertRow(0);
                    //int result = shrlonService.ViewCollDetail(state.SsWsPass, strData, requestDate, xmlColl, ref xmlHead, ref xmlDetail);

                    //if (result == 1) 
                    //{
                    //    if ((collType == "01") || (collType == "05")) 
                    //    {
                    //        dw_colldet.Visible = false;
                    //        dw_collmem.Visible = true;
                    //        dw_collshare.Visible = false;

                    //        DwUtil.ImportData(xmlHead, dw_collmem, null, FileSaveAsType.Xml);

                    //        if ((xmlDetail != null) && (xmlDetail != "")) 
                    //        {
                    //            DwUtil.ImportData(xmlDetail, dw_detail, null, FileSaveAsType.Xml);
                    //        }
                            
                    //    }
                    //    else if ((collType == "03") || (collType == "04"))
                    //    {
                    //        dw_colldet.Visible = true;
                    //        dw_collmem.Visible = false;
                    //        dw_collshare.Visible = false;

                    //        DwUtil.ImportData(xmlHead, dw_colldet, null, FileSaveAsType.Xml);
                    //        if ((xmlDetail != null) && (xmlDetail != ""))
                    //        {
                    //            DwUtil.ImportData(xmlDetail, dw_detail, null, FileSaveAsType.Xml);
                    //        }
                    //    }
                    //    else if (collType == "02")
                    //    {
                    //        dw_colldet.Visible = false;
                    //        dw_collmem.Visible = false;
                    //        dw_collshare.Visible = true;

                    //        DwUtil.ImportData(xmlHead, dw_collshare, null, FileSaveAsType.Xml);
                    //        if ((xmlDetail != null) && (xmlDetail != ""))
                    //        {
                    //            DwUtil.ImportData(xmlDetail, dw_detail, null, FileSaveAsType.Xml);
                    //        }
                        
                    //    }
                        
                    //}
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "closeWebDialog")
            {
                OnCloseClick();
            }
            
        }
        public void WebDialogLoadEnd()
        {
            dw_colldet.SaveDataCache();
        }
        public void OnCloseClick()
        {
            HfChkStatus.Value = "1";
        }
       
    }
}
