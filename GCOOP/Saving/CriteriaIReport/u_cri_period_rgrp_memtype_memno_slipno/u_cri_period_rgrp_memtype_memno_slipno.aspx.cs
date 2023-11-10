using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_period_rgrp_memtype_memno_slipno
{
    public partial class u_cri_period_rgrp_memtype_memno_slipno : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
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
                dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DdMembgroup();
                dsList.RetrieveList();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void RunReport()
        {
            int an_period = Convert.ToInt32(dsMain.DATA[0].year + dsMain.DATA[0].month.ToString("00"));
            string as_sgroup = dsMain.DATA[0].smembgroup_code;
            string as_egroup = dsMain.DATA[0].emembgroup_code;
            string as_memtype = "";
            string as_memno = dsMain.DATA[0].member_no;            
            string as_receiptno = dsMain.DATA[0].receipt_no;

            string[] minmax = ReportUtil.GetMinMaxMembgroup();
            if (as_sgroup.Length < 1)
            {
                as_sgroup = minmax[0];
            }

            if (as_egroup.Length < 1)
            {
                as_egroup = minmax[1];
            }

            for (int i = 0; i < dsList.RowCount; i++)
            {
                if (dsList.DATA[i].operate_flag == 1)
                {
                    if (as_memtype == "")
                    {
                        as_memtype = dsList.DATA[i].MEMBTYPE_CODE;
                    }
                    else
                    {
                        as_memtype += "," + dsList.DATA[i].MEMBTYPE_CODE;
                    }
                }
            }

            if (as_memtype.Length < 1)
            {
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (as_memtype == "")
                    {
                        as_memtype = dsList.DATA[i].MEMBTYPE_CODE;
                    }
                    else
                    {
                        as_memtype += "," + dsList.DATA[i].MEMBTYPE_CODE;
                    }
                }
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

            if (as_receiptno.Length < 1)
            {
                as_receiptno = "%";
            }

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("an_period", iReportArgumentType.Integer, an_period);
                arg.Add("as_sgroup", iReportArgumentType.String, as_sgroup);
                arg.Add("as_egroup", iReportArgumentType.String, as_egroup);
                arg.Add("as_memtype", iReportArgumentType.String, as_memtype);
                arg.Add("as_memno", iReportArgumentType.String, as_memno);
                arg.Add("as_receiptno", iReportArgumentType.String, as_receiptno);

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