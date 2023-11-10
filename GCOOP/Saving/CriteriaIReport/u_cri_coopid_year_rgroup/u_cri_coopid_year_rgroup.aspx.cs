﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using CoreSavingLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_year_rgroup
{
    public partial class u_cri_coopid_year_rgroup : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;
        //[JsPostBack]
        //public string PostMemGroup_S { get; set; }
        //[JsPostBack]
        //public string PostMemGroup_E { get; set; }
        //[JsPostBack]
        //public string PostMemGroup_S_Code { get; set; }
        //[JsPostBack]
        //public string PostMemGroup_E_Code { get; set; }

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
                dsMain.DdMembgroup();
            }            
        }

        public void CheckJsPostBack(string eventArg)
        {
            //if (eventArg == PostMemGroup_S)
            //{
            //    //dsMain.RetrieveMembNo();

            //}
            //if (eventArg == PostMemGroup_E)
            //{
            //    //dsMain.RetrieveMembNo();

            //}
            //if (eventArg == PostMemGroup_S_Code)
            //{
            //    //dsMain.RetrieveMembNo();

            //}
            //if (eventArg == PostMemGroup_E_Code)
            //{
            //    //dsMain.RetrieveMembNo();

            //}
        }

        public void RunReport()
        {
            string as_sgroup = dsMain.DATA[0].membgroup_start;
            string as_egroup = dsMain.DATA[0].membgroup_end;
            string as_year = dsMain.DATA[0].year;

            if (as_sgroup.Length < 1)
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"select min(membgroup_code) as getminmemgroup from mbucfmembgroup";
                Sdt dt = ta.Query(sql);
                as_sgroup = dt.Rows[0]["getminmemgroup"].ToString();
                ta.Close();
                //Sta ta = new Sta(state.SsConnectionString);
                //string sql = "select min(membgroup_code) as getminmemgroup from mbucfmembgroup";
                //Sdt result = ta.Q(sql);
                //if (result.Next())
                //{
                //    as_sgroup = result.GetString("getminmemgroup");
                //}
                //ta.Close();
            }

            if (as_egroup.Length < 1)
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"select max(membgroup_code) as getmaxmemgroup from mbucfmembgroup";
                Sdt dt = ta.Query(sql);
                as_egroup = dt.Rows[0]["getmaxmemgroup"].ToString();
                ta.Close();

                //string sql = "select max(membgroup_code) as getmaxmemgroup from mbucfmembgroup";
                //Sdt result = WebUtil.QuerySdt(sql);
                //if (result.Next())
                //{
                //    as_egroup = result.GetString("getmaxmemgroup");
                //}
            }            

            try
            {
                iReportArgument arg = new iReportArgument();

                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_membgrps", iReportArgumentType.String, as_sgroup);
                arg.Add("as_membgrpe", iReportArgumentType.String, as_egroup);
                arg.Add("as_divyear", iReportArgumentType.String, as_year);

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