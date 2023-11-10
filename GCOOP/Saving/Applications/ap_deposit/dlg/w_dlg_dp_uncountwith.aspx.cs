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
    public partial class w_dlg_dp_uncountwith : PageWebDialog,WebDialog
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

                String deptitem_code, deptitem_desc;
                int row = DwMain.RowCount;

                String sql = "";

                sql = @"DELETE FROM DPUCFWITHNCOUNT WHERE DEPTTYPE_CODE='" + HdDeptType.Value + "'";
                WebUtil.Query(sql);

                for (int i = 1; i <= row; i++)
                {

                    try { deptitem_code = DwMain.GetItemString(i, "deptitem_code"); }
                    catch { deptitem_code = ""; }
                    try { deptitem_desc = DwMain.GetItemString(i, "deptitem_desc"); }
                    catch { deptitem_desc = ""; }

                    sql = @"insert into DPUCFWITHNCOUNT 
	 	    (DEPTTYPE_CODE,		            DEPTITEM_CODE	, 	        DEPTITEM_DESC ,	          COOP_ID                      ) values
            ('" + HdDeptType.Value + "' , 	'" + deptitem_code + "' ,   '" + deptitem_desc + "',      '" + state.SsCoopControl + "')";
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
