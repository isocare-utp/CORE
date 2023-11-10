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
    public partial class w_dlg_dp_taxrateamt : PageWebDialog, WebDialog
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
                Decimal begin_amt, end_amt, taxrate;
                int row = DwMain.RowCount;

                String sql = "";

                sql = @"DELETE FROM DPUCFTAXRATE WHERE DEPTTYPE_CODE='" + HdDeptType.Value + "'";
                WebUtil.Query(sql);

                for (int i = 1; i <= row; i++)
                {

                    try { begin_amt = DwMain.GetItemDecimal(i, "begin_amt"); }
                    catch { begin_amt = 0; }
                    try { end_amt = DwMain.GetItemDecimal(i, "end_amt"); }
                    catch { end_amt = 0; }
                    try { taxrate = DwMain.GetItemDecimal(i, "taxrate"); }
                    catch { taxrate = 0; }

                    sql = @"insert into DPUCFTAXRATE 
	 	    (DEPTTYPE_CODE,		    BEGIN_AMT	, 	        END_AMT ,			TAXRATE , 		 COOP_ID ,                              SEQ_NO) values
            ('" + HdDeptType.Value + "' , 	" + begin_amt + " ,   " + end_amt + ", 	" + taxrate + ",   '" + state.SsCoopControl + "',   " + i + ")";
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
