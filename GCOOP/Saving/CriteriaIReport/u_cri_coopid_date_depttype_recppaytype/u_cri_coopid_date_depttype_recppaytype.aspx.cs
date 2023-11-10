using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Saving.CriteriaIReport.u_cri_coopid_date_depttype_recppaytype
{
    public partial class u_cri_coopid_date_depttype_recppaytype : PageWebReport, WebReport
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
                dsMain.DdCoopId();
                dsMain.DdDepttype();
                //dsMain.DdDepttypee();
                dsMain.DdRecpaytype();
                
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
                String item_type = dsMain.DATA[0].RECPPAYTYPE_CODE;
                string[] rappayminmax = ReportUtil.GetMinMaxRappaytype();
                
                String end_itemtype = item_type;
                if (item_type == "ฮฮฮ" || item_type == "")
                {
                    item_type = rappayminmax[0];
                    end_itemtype = rappayminmax[1];
                }




                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("date", iReportArgumentType.Date, start_date);
                arg.Add("as_start_itemtype", iReportArgumentType.String, item_type);
                arg.Add("as_end_itemtype", iReportArgumentType.String, end_itemtype);
                arg.Add("as_type", iReportArgumentType.String, start_depttype);
                


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