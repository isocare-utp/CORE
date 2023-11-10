using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Data;

namespace Saving.Applications.admin
{
    public partial class w_sheet_am_replication_detail : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostTry { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsDetail.InitDs(this);
            dsSpare.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsDetail.Retrieve();
                //dsSpare.Retrieve();
                dsMain.DATA[0].sid = "192.168.99.104:1522/iorcl4";
                dsMain.DATA[0].username = "iscocen";
                dsMain.DATA[0].password = "Icoop2556";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostTry)
            {
                try
                {
                    Sta ta = new Sta("Data Source=" + dsMain.DATA[0].sid + ";Persist Security Info=True;User ID=" + dsMain.DATA[0].username + ";Password=" + dsMain.DATA[0].password + ";Unicode=True");
                    try
                    {
                        String sql = @"
                        SELECT
                          (SELECT MAX(DEPTSLIP_NO) FROM DPDEPTSLIP) AS DEPT_SLIP,
                          (SELECT COUNT(*) FROM DPDEPTSLIP) AS DEPT_COUNT,
  
                          (SELECT MAX(payinslip_no) FROM slslippayin) AS SL_SLIP,
                          (SELECT COUNT(*) FROM slslippayin) AS SL_COUNT,
  
                          (SELECT MAX(SLIP_NO) FROM FINSLIP) AS FIN_SLIP,
                          (SELECT COUNT(*) FROM FINSLIP) AS FIN_COUNT
                        FROM DUAL";
                        DataTable dt = ta.QueryDataTable(sql);
                        dsSpare.ImportData(dt);
                        dsDetail.Retrieve();
                        ta.Close();
                    }
                    catch (Exception ex)
                    {
                        ta.Close();
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}