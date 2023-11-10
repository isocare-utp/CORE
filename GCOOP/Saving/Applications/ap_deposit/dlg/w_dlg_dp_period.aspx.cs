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

namespace Saving.Applications.ap_deposit.dlg
{
    public partial class w_dlg_dp_period : PageWebDialog, WebDialog
    {
        string depType;
        protected String postUpdate;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postUpdate = WebUtil.JsPostBack(this, "postUpdate");
        }

        public void WebDialogLoadBegin()
        {
            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            depType = Request["deptType"];
            string coopid = state.SsCoopId;
            HdCloseDlg.Value = "false";
            HdDeptType.Value = depType;
            if (!IsPostBack)
            {
                DwMain.Retrieve(depType);
                if (DwMain.RowCount < 1)
                {
                    DwMain.InsertRow(1);
                    DwMain.SetItemString(1, "depttype_code", depType);
                }
            }
            else
            {
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postUpdate")
            {

                Decimal all_equalflag, missdept_allow, latedept_allow;
                int row = DwMain.RowCount;

                String sql = "";

                sql = @"DELETE FROM DPDEPTTYPEPERIOD WHERE DEPTTYPE_CODE='" + HdDeptType.Value + "'";
                WebUtil.Query(sql);

                for (int i = 1; i <= row; i++)
                {

                    try { all_equalflag = DwMain.GetItemDecimal(i, "all_equalflag"); }
                    catch { all_equalflag = 0; }
                    try { missdept_allow = DwMain.GetItemDecimal(i, "missdept_allow"); }
                    catch { missdept_allow = 0; }
                    try { latedept_allow = DwMain.GetItemDecimal(i, "latedept_allow"); }
                    catch { latedept_allow = 0; }

                    sql = @"insert into DPDEPTTYPEPERIOD 
	 	    (DEPTTYPE_CODE,		            ALL_EQUALFLAG	, 	      MISSDEPT_ALLOW ,			LATEDEPT_ALLOW , 		    COOP_ID) values
            ('" + HdDeptType.Value + "' , 	" + all_equalflag + " ,   " + missdept_allow + ", 	" + latedept_allow   + ",   '" + state.SsCoopControl + "')";
                    WebUtil.Query(sql);
                }
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.Execute();

                //DwMain.UpdateData();
                HdCloseDlg.Value = "true";
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion
    }
}
