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

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_show_accid : PageWebDialog, WebDialog
    {
        protected String refresh;
        protected String setDocNo;

        public void InitJsPostBack()
        {
            setDocNo = WebUtil.JsPostBack(this, "setDocNo");
            refresh = WebUtil.JsPostBack(this, "refresh");
        }

        public void WebDialogLoadBegin()
        {

            if (IsPostBack)
            {
                dw_detail.RestoreContext();
            }
            else
            {
                String memberNo = Request["member"].ToString();
                LoadAccID(memberNo);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "setDocNo")
            {
                SetDocNo();
            }
            if (eventArg == "refresh")
            {
                Refresh();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_detail.SaveDataCache();

        }
        public void loadMoneytr(string member_no)
        {
            DwUtil.RetrieveDataWindow(dw_detail, "sl_loan_requestment_cen.pbl", null, member_no);
        
        }
        public void LoadAccID(String memberNo)
        {
            String pbl = "sl_loan_requestment_cen.pbl";
            string[] arg = new string[2]  {state.SsCoopControl,memberNo};
             try
            {
                dw_detail.Reset();
                //DwUtil.ImportData(strSQL, dw_detail, null);
                DwUtil.RetrieveDataWindow(dw_detail, pbl, null, arg);

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        public void Refresh() { }

        public void SetDocNo()
        {
            int row = Convert.ToInt32(HfRow.Value);
            if ((HfRow.Value != "") && (HfRow.Value != null))
            {
                HfAccID.Value = dw_detail.GetItemString(row, "deptaccount_no");

            }
        }

    }
}
