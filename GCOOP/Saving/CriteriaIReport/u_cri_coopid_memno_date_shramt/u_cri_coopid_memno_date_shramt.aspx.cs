using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_memno_date_shramt
{
    public partial class u_cri_coopid_memno_date_shramt : PageWebReport, WebReport
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
                dsMain.DATA[0].operate_date = state.SsWorkDate;
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
            string as_coopid = dsMain.DATA[0].coop_id;
            string as_memno = dsMain.DATA[0].memno;
            DateTime op_date = dsMain.DATA[0].operate_date;
            decimal share_amt = dsMain.DATA[0].share_amt;
            string str_shareamt = share_amt.ToString("#,##0.00");
            string shr_tbaht = "";
            try
            {
                string sql = "select ftreadtbaht({0}) as total_tbaht from dual";
                sql = WebUtil.SQLFormat(sql, share_amt);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    shr_tbaht = dt.GetString("total_tbaht");
                }
            }
            catch { }
            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, as_coopid);
                arg.Add("as_membno", iReportArgumentType.String, as_memno);
                arg.Add("adtm_opdate", iReportArgumentType.Date, op_date);
                arg.Add("as_sharetbaht", iReportArgumentType.String, shr_tbaht);
                arg.Add("as_shareamt", iReportArgumentType.String, str_shareamt);
                
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