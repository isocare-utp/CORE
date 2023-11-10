using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using System.Globalization;


namespace Saving.CriteriaIReport.u_cri_acc_pl
{
    public partial class u_cri_acc_pl_excel : PageWebReport, WebReport
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
                String sqlStr = @"select min(moneysheet_code) as moneysheet_code from accsheetmoneyhead where coop_id = {0}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
                Sdt dt1 = WebUtil.QuerySdt(sqlStr);
                dt1.Next();

                dsMain.DdMoneysheetcode();
                
                dsMain.DATA[0].show_all = "1";
                dsMain.DATA[0].data_1 = "1";
                dsMain.DATA[0].data_2 = "1";
                dsMain.DATA[0].data_2 = "1";
                dsMain.DATA[0].compare_b1_b3 = "0";
                dsMain.DATA[0].show_remark = "0";
                dsMain.DATA[0].total_show = "2";
                dsMain.DATA[0].percent_status = "0";
                dsMain.DATA[0].moneysheet_code = dt1.GetString("moneysheet_code"); 
                dsMain.DATA[0].month_1_1 = "1";
                dsMain.DATA[0].month_2_1 = "1"; 
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {

            string coop_id = state.SsCoopId;
            String coop_name = state.SsCoopName;

            string str_year_1 = "";
            string str_year_2 = "";
            string str_period_1 = "";
            string str_period_2 = "";
            int flag = 0;
            String szDescTitle = "";
            //string total_show_new = "";
            try
            {
                iReportArgument arg = new iReportArgument();
            string str_sheet = dsMain.DATA[0].moneysheet_code.Trim();

            try
            {
                str_year_1 = dsMain.DATA[0].year_1;
                str_period_1 = dsMain.DATA[0].month_1_1;
                int li_year1 = Convert.ToInt16(str_year_1) - 543;
                if (Convert.ToInt16(str_period_1) < 4)
                {
                    li_year1 = li_year1 - 1;
                }
                DataTable dt = WebUtil.Query("select account_year from accaccountyear where account_year ='" + li_year1 + "'");
                if (dt.Rows.Count > 0)
                {
                    string acc_year1;
                    acc_year1 = dt.Rows[0]["account_year"].ToString();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลในปี พ.ศ. " + str_year_1 + "");
                    flag = 1;
                }
            }
            catch
            {
                str_year_1 = "";
            }
            try
            {
                str_year_2 = dsMain.DATA[0].year_2;
                str_period_2 = dsMain.DATA[0].month_2_1;
                int li_year2 = Convert.ToInt16(str_year_2) - 543;
                if (Convert.ToInt16(str_period_2) < 4)
                {
                    li_year2 = li_year2 - 1;
                }
                DataTable dt2 = WebUtil.Query("select account_year from accaccountyear where account_year ='" + li_year2 + "'");
                if (dt2.Rows.Count > 0)
                {
                    string acc_year2;
                    acc_year2 = dt2.Rows[0]["account_year"].ToString();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลในปี พ.ศ. " + str_year_2 + "");
                    flag = 1;
                }

            }
            catch
            {
                str_year_2 = "";
            }
            if (flag == 0)
            {

                short str_month_1 = Convert.ToInt16(dsMain.DATA[0].month_1_1);
                short str_month_2 = Convert.ToInt16(dsMain.DATA[0].month_2_1);
                short str_show_all = Convert.ToInt16(dsMain.DATA[0].show_all);
                short str_data_1 = Convert.ToInt16(dsMain.DATA[0].data_1);
                short str_data_2 = Convert.ToInt16(dsMain.DATA[0].data_2);
                short str_compare_b1_b3 = 0;
                short str_show_remark = Convert.ToInt16(dsMain.DATA[0].show_remark);
                short str_total_show = Convert.ToInt16(dsMain.DATA[0].total_show);
                short percent_status = Convert.ToInt16(dsMain.DATA[0].percent_status);

                short str_year_1_new = Convert.ToInt16(str_year_1);
                short str_year_2_new = Convert.ToInt16(str_year_2);

                //int result = wcf.NAccount.of_gen_trial_bs2(state.SsWsPass, adtm_date_new, adtm_date_new2, check_flag_new, state.SsCoopControl);
                int result = wcf.NAccount.of_gen_balance_sheet_excel(state.SsWsPass, str_sheet, str_year_1_new, str_year_2_new, str_month_1, str_month_2, str_show_all, str_data_1, str_data_2, str_compare_b1_b3, str_show_remark, state.SsCoopControl, str_total_show, percent_status);

                //String szDescTitle = "";
                if (state.SsCoopId == "061001")
                {

                    if (str_total_show == 1)
                    {
                        szDescTitle = "r_acc_pl_excel_one_mju";
                    }
                    else
                    {
                        szDescTitle = "r_acc_pl_excel_mju";

                    }
                }
                else
                {

                    if (str_total_show == 1)
                    {
                        szDescTitle = "r_acc_pl_excel_one";
                    }
                    else
                    {
                        szDescTitle = "r_acc_pl_excel";

                    }
                }

                //**
            } 
                
            //arg.Add("adtm_date", iReportArgumentType.Date, adtm_date_new);
            //arg.Add("adtm_edate", iReportArgumentType.Date, adtm_date_new2);
            arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);

            iReportBuider report = new iReportBuider(this, arg, szDescTitle);

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