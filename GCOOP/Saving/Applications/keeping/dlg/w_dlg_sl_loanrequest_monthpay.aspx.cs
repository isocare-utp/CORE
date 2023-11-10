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
    public partial class w_dlg_sl_loanrequest_monthpay : PageWebDialog, WebDialog
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
            this.ConnectSQLCA();
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_head);
                this.RestoreContextDw(dw_detail);
            }
            else {
                strList = WebUtil.nstr_itemchange_session(this);
                String xmlHead ="";
                String xmlDetail = "";
                Decimal income = Convert.ToDecimal(Request["income"].ToString());
                Decimal paymonth = Convert.ToDecimal(Request["paymonth"].ToString()); 
                //try
                //{
                //    shrlonService.CreateMthPayTab(state.SsWsPass, strList.xml_main, strList.xml_clear, ref xmlHead, ref xmlDetail);
                //    try
                //    {
                //        dw_head.Reset();
                //        dw_head.ImportString(xmlHead, FileSaveAsType.Xml);
                //    }
                //    catch
                //    {
                //        dw_head.Reset(); dw_head.InsertRow(0);
                //    }
                //    try
                //    {
                //        dw_detail.Reset();
                //        dw_detail.ImportString(xmlDetail, FileSaveAsType.Xml);
                //    }
                //    catch {
                //        dw_head.Reset(); dw_head.InsertRow(0);
                //    }
                //    dw_head.SetItemDecimal(1, "itenincome_other", income);
                //    dw_head.SetItemDecimal(1, "itempayment_oth", paymonth);
                //}
                //catch (Exception ex) 
                //{
                //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                //}
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
            dw_head.SaveDataCache();
            dw_detail.SaveDataCache();
            
        }

        protected void OnCloseClick()
        {
            HfChkStatus.Value = "1";
        }

        
    }
}
