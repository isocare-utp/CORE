using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Saving.CriteriaIReport.u_cri_date_rdepttype_recppaytype_ruserid
{
    public partial class u_cri_date_rdepttype_recppaytype_ruserid : PageWebReport, WebReport
    {
        [JsPostBack]
        public string JsPostBank { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {                
                dsMain.DATA[0].ENTRY_DATE = state.SsWorkDate;
                dsMain.DdDepttype();
                dsMain.DdDepttypee();
                dsMain.DdRecpaytype();
                dsMain.DdAmUser();
                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            
        }

        public void RunReport()
        {

            try
            {

                DateTime start_date = dsMain.DATA[0].ENTRY_DATE;
                String start_depttype = dsMain.DATA[0].depttype_scode;
                String end_depttype = dsMain.DATA[0].depttype_ecode;
                String item_type = dsMain.DATA[0].RECPPAYTYPE_CODE;
                String user_id = dsMain.DATA[0].USER_NAME;
                string[] rappayminmax = ReportUtil.GetMinMaxRappaytype();
                string[] userminmax = ReportUtil.GetMinMaxUsertype();
                String end_userid = user_id;

                if (user_id == "")
                {
                    user_id = userminmax[0];
                    end_userid = userminmax[1];
                }

                String end_itemtype = item_type;
                if (item_type == "ฮฮฮ" || item_type == "")
                {
                    item_type = rappayminmax[0];
                    end_itemtype = rappayminmax[1];
                }




                iReportArgument arg = new iReportArgument();
                arg.Add("adtm_entry_date", iReportArgumentType.Date, start_date);
                arg.Add("as_end_itemtype", iReportArgumentType.String, end_itemtype);
                arg.Add("as_end_type", iReportArgumentType.String, end_depttype);
                arg.Add("as_end_user", iReportArgumentType.String, end_userid);
                arg.Add("as_start_itemtype", iReportArgumentType.String, item_type);
                arg.Add("as_start_type", iReportArgumentType.String, start_depttype);
                arg.Add("as_start_user", iReportArgumentType.String, user_id);


                iReportBuider report = new iReportBuider(this, arg);
                report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        public void WebSheetLoadEnd()
        {

        }       
    }
}