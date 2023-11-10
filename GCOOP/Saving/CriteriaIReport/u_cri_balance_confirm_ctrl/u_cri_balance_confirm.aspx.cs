using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_balance_confirm_ctrl
{
    public partial class u_cri_balance_confirm : PageWebReport, WebReport
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

            dsMain.DdMembgroupEnd();
            dsMain.DdMembgroupStart();
            dsMain.DATA[0].document_date = state.SsWorkDate;
            dsMain.DATA[0].operate_flag_1 = 1;
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            string as_period = dsMain.DATA[0].bal_yyyy + dsMain.DATA[0].bal_mm;
            string as_docdate = dsMain.DATA[0].document_date.ToString("dd/MM/yyyy");
            string as_sgroup = dsMain.DATA[0].membgroup_start;
            string as_egroup = dsMain.DATA[0].membgroup_end;
            string as_smemno = dsMain.DATA[0].memno_start;
            string as_ememno = dsMain.DATA[0].memno_end;

            if (as_sgroup.Length < 1)
            {
                string sql = "select min(membgroup_code) as getminmemgroup from mbucfmembgroup";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);

                Sdt result = WebUtil.QuerySdt(sql);
                if (result.Next())
                {
                    as_sgroup = result.GetString("getminmemgroup");
                }
            }

            if (as_egroup.Length < 1)
            {
                string sql = "select max(membgroup_code) as getmaxmemgroup from mbucfmembgroup";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);

                Sdt result = WebUtil.QuerySdt(sql);
                if (result.Next())
                {
                    as_egroup = result.GetString("getmaxmemgroup");
                }
            }

            if (as_smemno.Length < 1)
            {
                string sql = "select min(member_no) as getminmembno from mbmembmaster";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);

                Sdt result = WebUtil.QuerySdt(sql);
                if (result.Next())
                {
                    as_smemno = result.GetString("getminmembno");
                }
            }
            else { as_smemno = WebUtil.MemberNoFormat(as_smemno); }

            if (as_ememno.Length < 1)
            {
                string sql = "select max(member_no) as getmaxmembno from mbmembmaster";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);

                Sdt result = WebUtil.QuerySdt(sql);
                if (result.Next())
                {
                    as_ememno = result.GetString("getmaxmembno");
                }
            }
            else { as_ememno = WebUtil.MemberNoFormat(as_ememno); }



            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_docdate", iReportArgumentType.String, as_docdate);
                arg.Add("as_egroup", iReportArgumentType.String, as_egroup);
                arg.Add("as_ememno", iReportArgumentType.String, as_ememno);
                arg.Add("as_period", iReportArgumentType.String, as_period);
                arg.Add("as_sgroup", iReportArgumentType.String, as_sgroup);
                arg.Add("as_smemno", iReportArgumentType.String, as_smemno);


                iReportBuider report = new iReportBuider(this, arg);
                report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            dsMain.DATA[0].memno_start = as_smemno;
            dsMain.DATA[0].memno_end = as_ememno;

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}