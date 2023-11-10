using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rdate_memno_loancont
{
    public partial class u_cri_coopid_rdate_memno_loancont : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

        [JsPostBack]
        public string PostMemberNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            if (!IsPostBack)
            {
                dsMain.DdCoopId();
                dsMain.DATA[0].coop_id = state.SsCoopControl;
                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string membno = dsMain.DATA[0].memno;
                membno = WebUtil.MemberNoFormat(membno);
                dsMain.DATA[0].memno = membno;
            }
        }

        public void RunReport()
        {
            try
            {
                String coop_id = dsMain.DATA[0].coop_id;
                DateTime start_date = dsMain.DATA[0].start_date;
                DateTime end_date = dsMain.DATA[0].end_date;
                String memno = dsMain.DATA[0].memno;
                String loancont = dsMain.DATA[0].loancont;

                iReportArgument arg = new iReportArgument();

                arg.Add("as_coopid", iReportArgumentType.String, coop_id);
                arg.Add("adt_contdate_start", iReportArgumentType.Date, start_date);
                arg.Add("adt_contdate_stop", iReportArgumentType.Date, end_date);
                arg.Add("as_memno", iReportArgumentType.String, memno);
                arg.Add("as_loancont", iReportArgumentType.String, loancont);

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