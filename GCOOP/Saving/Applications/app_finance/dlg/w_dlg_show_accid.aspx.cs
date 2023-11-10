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

namespace Saving.Applications.app_finance.dlg
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

        public void LoadAccID(String memberNo)
        {
            String strSQL = "";
            strSQL = @"SELECT DPDEPTMASTER.DEPTACCOUNT_NO,   
         DPDEPTMASTER.MEMBER_NO,   
         DPDEPTMASTER.DEPTACCOUNT_NAME,   
         DPDEPTMASTER.DEPTTYPE_CODE,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         DPDEPTMASTER.DEPT_OBJECTIVE,   
         DPDEPTMASTER.DEPTCLOSE_STATUS,   
         DPDEPTTYPE.DEPTTYPE_GROUP,   
         DPDEPTMASTER.PRNCBAL  
    FROM DPDEPTMASTER,   
         MBMEMBMASTER,   
         DPDEPTTYPE  
   WHERE ( DPDEPTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( DPDEPTMASTER.DEPTTYPE_CODE = DPDEPTTYPE.DEPTTYPE_CODE ) and  
         ( DPDEPTMASTER.COOP_ID = DPDEPTTYPE.COOP_ID )  and
         ( DPDEPTMASTER.MEMBER_NO = '" + memberNo + "') ORDER BY DPDEPTMASTER.DEPTACCOUNT_NO ASC ";
            try
            {
                dw_detail.Reset();
                DwUtil.ImportData(strSQL, dw_detail, null);

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
