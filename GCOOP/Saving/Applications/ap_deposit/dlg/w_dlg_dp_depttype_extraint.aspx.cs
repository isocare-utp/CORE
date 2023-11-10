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
    public partial class w_dlg_dp_depttype_extraint : PageWebDialog, WebDialog
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
            HdCloseDlg.Value = "false";

            if (!IsPostBack)
            {
                depType = Request["deptType"];
                string coopid = state.SsCoopId;
                DwMain.Retrieve(depType);
                HdDeptType.Value = depType;
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
                Decimal dept_smonth, dept_emonth, int_extrarate;
                int row = DwMain.RowCount;

                String sql = "";

                sql = @"DELETE FROM DPDEPTINTEXTRATE WHERE DEPTTYPE_CODE='" + HdDeptType.Value + "'";
                WebUtil.Query(sql);

                for (int i = 1; i <= row; i++)
                {
                    try { dept_smonth = DwMain.GetItemDecimal(i, "dept_smonth"); }
                    catch { dept_smonth = 0; }
                    try { dept_emonth = DwMain.GetItemDecimal(i, "dept_emonth"); }
                    catch { dept_emonth = 0; }
                    try { int_extrarate = DwMain.GetItemDecimal(i, "int_extrarate"); }
                    catch { int_extrarate = 0; }

                    sql = @"insert into DPDEPTINTEXTRATE 
	 	    (DEPTTYPE_CODE,		    DEPT_SMONTH	, 	        DEPT_EMONTH ,			INT_EXTRARATE , 		 COOP_ID) values
            ('" + HdDeptType.Value + "' , 	" + dept_smonth + " ,   " + dept_emonth + ", 	" + int_extrarate + ",   '" + state.SsCoopControl + "')";
                    WebUtil.Query(sql);
                }
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.Execute();
                try
                {
                    //DwMain.UpdateData();
                    HdCloseDlg.Value = "true";
                }
                catch (Exception)
                {
                }
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
        }

        #endregion
    }
}
