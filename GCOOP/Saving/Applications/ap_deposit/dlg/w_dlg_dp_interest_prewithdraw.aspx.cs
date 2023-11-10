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
using System.Globalization;

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_interest_prewithdraw : PageWebDialog, WebDialog
    {
        protected String postSave;
        protected String postTDwMain;
        private String typeCode;
        private decimal preSeq;
        private DwThDate tDwMain;
        protected String InsertRow;

        private void DwMainUpdate() 
        {
            try
            {
                DwMain.UpdateData();
            }
            catch (Exception ex)
            {
                sqlca.Rollback();
            }
        }

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSave = WebUtil.JsPostBack(this, "postSave");
            postTDwMain = WebUtil.JsPostBack(this, "postTDwMain");
            InsertRow = WebUtil.JsPostBack(this, "InsertRow");
            tDwMain = new DwThDate(DwMain,this);
            tDwMain.Add("effective_date", "effective_tdate");
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                this.ConnectSQLCA();
                DwMain.SetTransaction(sqlca);
                HdCloseDlg.Value = "false";

                try
                {
                    typeCode = Request["typecode"];

                    preSeq = Convert.ToDecimal(Request["preseq"]);

                }
                catch(Exception) { }
                if (!IsPostBack)
                {
                    string coopid = state.SsCoopId;
                    DwMain.Retrieve(coopid,typeCode, preSeq);
                    tDwMain.Eng2ThaiAllRow();
                }
                else
                {
                    this.RestoreContextDw(DwMain);
                    //DwMain.RestoreContext();
                }
            }
            catch(Exception) { }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSave")
            {
                DwMainUpdate();
                HdCloseDlg.Value = "true";
            }
            else if( eventArg == "InsertRow")
            {
                int row = DwMain.RowCount;
                DwMain.InsertRow(row + 1);
                DwMain.SetItemDateTime(row + 1, "effective_date", state.SsWorkDate);
                DwMain.SetItemDecimal(row + 1, "preseq_no", preSeq);
                DwMain.SetItemString(row + 1, "depttype_code", typeCode);
                DwMain.SetItemString(row + 1, "entry_id", state.SsUsername);
                tDwMain.Eng2ThaiAllRow();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion
    }
}
