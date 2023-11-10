using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_setpaprinting_format : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        protected String reFresh;
        protected String postSave;
        protected String postExample;
        protected String postRetrieveMain;
        protected String postRetrieveList;


        #region WebSheet Members

        public void InitJsPostBack()
        {
            reFresh = WebUtil.JsPostBack(this, "reFresh");
            postSave = WebUtil.JsPostBack(this, "postSave");
            postExample = WebUtil.JsPostBack(this, "postExample");
            postRetrieveMain = WebUtil.JsPostBack(this, "postRetrieveMain");
            postRetrieveList = WebUtil.JsPostBack(this, "postRetrieveList");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;

            this.ConnectSQLCA();
            DwPap.SetTransaction(sqlca);
            DwList.SetTransaction(sqlca);
            //DwMain.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                //รายการ
                DwPap.Retrieve();
            }
            else
            {
                //this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwPap);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postSave":
                    Save();
                    break;
                case "Update":
                    Save();
                    break;
                case "postRetrieveMain":
                    RetrieveMain();
                    break;
                case "postRetrieveList":
                    RetrieveList();
                    break;
                case "postExample":
                    SetApplyPosition();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            DwList.UpdateData();
        }

        public void WebSheetLoadEnd()
        {
            this.DisConnectSQLCA();
            DwPap.SaveDataCache();
            DwList.SaveDataCache();
            //DwMain.SaveDataCache();
        }

        #endregion


        private void Save()
        {
            try
            {
                DwList.UpdateData(true);
                DwPap.UpdateData(true);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private void RetrieveMain()
        {
            string row = HdRow.Value, valmm;
            int i = int.Parse(row);

            valmm = DwPap.GetItemString(i, "printing_name");

            DwList.Retrieve(valmm);
        }

        private void RetrieveList()
        {
            //string row = HdRowList.Value, valm, valmm;
            //int i = int.Parse(row);

            //valm = DwList.GetItemString(i, "column_name");
            //valmm = DwList.GetItemString(i, "printing_name");

            //DwMain.Retrieve(valm);
        }

        protected void SetPositionBegin()
        {

        }

        protected void SetApplyPosition()
        {
            
        }
    }
}
