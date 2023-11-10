using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_period_rgroup_memno
{
    public partial class u_cri_coopid_period_rgroup_memno : PageWebReport, WebReport
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
            //--- Page Arguments
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
                dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DATA[0].month = DateTime.Now.Month;
                dsMain.DdMembgroup();                
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string membno = dsMain.DATA[0].member_no;
                membno = WebUtil.MemberNoFormat(membno);
                dsMain.DATA[0].member_no = membno;
            }
        }

        public void RunReport()
        {
            string as_coopid = dsMain.DATA[0].coop_id;
            string as_period = Convert.ToString(dsMain.DATA[0].year + dsMain.DATA[0].month.ToString("00"));
            string as_sgroup = dsMain.DATA[0].smembgroup_code;
            string as_egroup = dsMain.DATA[0].emembgroup_code;            
            string as_memno = dsMain.DATA[0].member_no;            

            string[] minmax = ReportUtil.GetMinMaxMembgroup();
            if (as_sgroup.Length < 1)
            {
                as_sgroup = minmax[0];
            }

            if (as_egroup.Length < 1)
            {
                as_egroup = minmax[1];
            }

            if (as_memno.Length < 1)
            {
                as_memno = "%";
            }
            else
            {
                as_memno = WebUtil.MemberNoFormat(as_memno);
                dsMain.DATA[0].member_no = as_memno;
            }

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, as_coopid);
                arg.Add("as_period", iReportArgumentType.String, as_period);
                arg.Add("as_sgroup", iReportArgumentType.String, as_sgroup);
                arg.Add("as_egroup", iReportArgumentType.String, as_egroup);                
                arg.Add("as_membno", iReportArgumentType.String, as_memno);
                arg.Add("adtm_operate", iReportArgumentType.Date, state.SsWorkDate);

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