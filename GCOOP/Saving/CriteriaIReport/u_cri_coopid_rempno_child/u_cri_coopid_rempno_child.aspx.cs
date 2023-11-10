using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rempno_child
{
    public partial class u_cri_coopid_rempno_child : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

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
                dsMain.DdEmpno();

                dsMain.DATA[0].start_date = state.SsWorkDate;
                dsMain.DATA[0].end_date = state.SsWorkDate;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            string as_coopid, as_sempno, as_eempno;
            rid = Request.QueryString["rid"].ToString();
            if (Request.QueryString["rid"] != null)
            {
                as_coopid = dsMain.DATA[0].coop_id;
                as_sempno = dsMain.DATA[0].semp_no;
                as_eempno = dsMain.DATA[0].eemp_no;
                string assist_state_e = dsMain.DATA[0].assist_state;

                string[] minmax = ReportUtil.GetMinMaxMembgroup();
                if (as_sempno.Length < 1)
                {
                    as_sempno = "000";
                }

                if (as_eempno.Length < 1)
                {
                    as_eempno = "ฮฮฮ";
                }

               

                    try
                    {

                        iReportArgument arg = new iReportArgument();
                        arg.Add("coop_id", iReportArgumentType.String, as_coopid);
                        arg.Add("emp_no_s", iReportArgumentType.String, as_sempno);
                        arg.Add("emp_no_e", iReportArgumentType.String, as_eempno);
                        arg.Add("assist_state_e", iReportArgumentType.String, assist_state_e);
                        iReportBuider report = new iReportBuider(this, arg);
                        report.Retrieve();
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }
                
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}