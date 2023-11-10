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
    public partial class w_dlg_sl_loanrequest_loanrightchoose :PageWebDialog , WebDialog 
    {

        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        //protected String createSession;
        protected String refreshDW;

        public void InitJsPostBack()
        {
            refreshDW = WebUtil.JsPostBack(this, "refreshDW");
            //createSession = WebUtil.JsPostBack(this, "createSession");

        }

        public void WebDialogLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
            if (IsPostBack)
            {
                dw_collright.RestoreContext();
            }
            
            if (dw_collright.RowCount < 1)
            {
                try
                {
                    str_itemchange strList = new str_itemchange();
                    strList = WebUtil.nstr_itemchange_session(this);
                    dw_collright.Reset();
                    dw_collright.ImportString(strList.xml_collright, FileSaveAsType.Xml);
                }
                catch { }
            }
            //dw_coll.Visible = false;
            //if (dw_collright.RowCount > 1)
            //{
            //    dw_collright.DeleteRow(dw_collright.RowCount);
            //}

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "refreshDW")
            {
                RefreshDW();
            }
        }

        public void WebDialogLoadEnd()
        {
            
        }


        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            str_itemchange strList = new str_itemchange();
            strList = WebUtil.nstr_itemchange_session(this);

            strList.xml_collright = dw_collright.Describe("DataWindow.Data.XML");
            HdChkConfirm.Value = "0";
            
            for (int i = 1; i <= dw_collright.RowCount; i++)
            {
                String operateFlag = dw_collright.GetItemString(i, "operate_flag");
                if (operateFlag == "1")
                {
                    HdChkConfirm.Value = "1";
                }
            } 
        }
        protected void RefreshDW()
        { 
            
        }
    }
}
