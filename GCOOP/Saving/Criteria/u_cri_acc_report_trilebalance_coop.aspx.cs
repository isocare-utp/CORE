using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataLibrary;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNAccount;
using System.Globalization;

namespace Saving.Criteria
{
    public partial class u_cri_acc_report_trilebalance_coop : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        public String outputProcess = "";
        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                if (dw_criteria.RowCount < 1)
                {
                    dw_criteria.Reset();
                    dw_criteria.InsertRow(0);
                }
                tdw_criteria.Eng2ThaiAllRow();
            }

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

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            if (dw_criteria.RowCount > 1)
            {
                dw_criteria.DeleteRow(dw_criteria.RowCount);
            }
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            String coop_name = state.SsCoopName;
            String coop_id = state.SsCoopControl;
            String year = "";
            String period = "";
            int year2 = 0;
            int period2 = 0;

            //////////////////////

            DateTime sdate = dw_criteria.GetItemDateTime(1, "start_date");
            String period_sdate = sdate.ToString("yyyy-MM-dd");
            DateTime edate = dw_criteria.GetItemDateTime(1, "end_date");
            String period_edate = edate.ToString("yyyy-MM-dd");

            try
            {
                Sta ta2 = new Sta(state.SsConnectionString);
                String sql2 = "";
                sql2 = @"select  ap.account_year , ap.period  
                    from accperiod ap  
                    where ap.period_end_date in ( select min(ap2.period_end_date) from accperiod ap2 where ap2.period_end_date >= '" + period_sdate + "' AND  ap2.coop_id = '" + state.SsCoopControl + @"' )  ";
                Sdt dt2 = ta2.Query(sql2);
                year = dt2.Rows[0][0].ToString().Trim();
                period = dt2.Rows[0][1].ToString().Trim();
                ta2.Close();

                 year2 = Convert.ToInt32(year);
                 period2 = Convert.ToInt32(period);
            }
            catch { 
            
            }

            

            //////////////////////


            //ตรวจสอบการประมวลจำนวนสมาชิก
            String check_flag = dw_criteria.GetItemString(1, "check_flag"); //01 ไม่ประมวล , 02 ประมวล
            if( check_flag == "02" ) {

                int result = wcf.NAccount.of_process_member_balance(state.SsWsPass, (short)year2, (short)period2, state.SsCoopControl);
              
            }

            //ท่อนประมวลข้อมูลลง Database
            try
            {
                String ServiceXML = dw_criteria.Describe("DataWindow.Data.XML");
                String ls_temp = "";

                int re = wcf.NAccount.of_gen_trial_bs_coop(state.SsWsPass, ServiceXML, state.SsCoopControl, ref ls_temp);
            }
            catch
            { 
            
            }
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(year, ArgumentType.Number);
            lnv_helper.AddArgument(period, ArgumentType.Number);
            lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(start_date, ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date, ArgumentType.DateTime);

            //----------------------------------------------------


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                String criteriaXML = lnv_helper.PopArgumentsXML();
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
        }
        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }
        #endregion
    }
}
