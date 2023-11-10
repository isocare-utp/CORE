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

namespace Saving.Criteria
{
    public partial class u_cri_rcondition : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
	
        protected String checkFlag;
        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            checkFlag = WebUtil.JsPostBack(this, "checkFlag");
            
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();

            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "dept_type1", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "dept_type2", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "printer", "criteria.pbl", null);
            //DwUtil.RetrieveDDDW(dw_criteria, "branch_id", "criteria.pbl", null);
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
            
                dw_criteria.SetItemString(1, "start_dp_type", "000");
                string[] minmax = ReportUtil.GetMinMaxDepttype();
                dw_criteria.SetItemString(1, "dept_type1", minmax[0]);
                dw_criteria.SetItemString(1, "dept_type2", minmax[1]);
                dw_criteria.SetItemString(1, "adtm_tdate", "");
                //dw_criteria.SetItemString(1, "branch_id", state.SsCoopId);
                DwUtil.RetrieveDDDW(dw_criteria, "select_coop", "criteria.pbl", state.SsCoopControl);
                DwUtil.RetrieveDDDW(dw_criteria, "printer", "criteria.pbl");
                dw_criteria.SetItemString(1, "select_coop", state.SsCoopId);
                dw_criteria.SetItemDecimal(1,"condition",1);
                dw_criteria.SetItemDecimal(1, "start_amt", 0);
                dw_criteria.SetItemDecimal(1, "end_amt", 0);
                
              //  tdw_criteria.Eng2ThaiAllRow();
            }

           

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

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }
   public void CheckJsPostBack(string eventArg)
	        {
	            if (eventArg == "runProcess")
	            {
	                RunProcess();
	            }
                else if (eventArg == "checkFlag")
                {
                    CheckFlag();


                }
	            else if (eventArg == "popupReport")
	            {
	                PopupReport();
	            }
	        
           
        }

        public void SaveWebSheet()
        {
            //throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            //throw new NotImplementedException();
        }

        #endregion
        private void CheckFlag()
        {
            //ให้หน้า page refresh

        }
        #region Report Process
        public String outputProcess = "";
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            String start_dp_type = dw_criteria.GetItemString(1, "start_dp_type");
            String dept_type1 = dw_criteria.GetItemString(1, "dept_type1");
            String dept_type2 = dw_criteria.GetItemString(1, "dept_type2");
            String adtm_tdate = dw_criteria.GetItemString(1, "adtm_tdate");
            //String branch_id = dw_criteria.GetItemString(1, "branch_id");
            String coop_id = dw_criteria.GetItemString(1, "select_coop");
            Int32 condition = Convert.ToInt32(dw_criteria.GetItemDecimal(1, "condition").ToString());
            double start_amt = Convert.ToDouble(dw_criteria.GetItemDecimal(1, "start_amt"));
            double end_amt = Convert.ToDouble(dw_criteria.GetItemDecimal(1, "end_amt"));

            switch (condition)  //กรณีเลือกเงื่อนไข
            {
                case 1:  //ช่วงเงิน
                    break;
                case 2:  //มากกว่า(>)
                    start_amt = start_amt + 0.01;
                    end_amt = 999999999999.99;
                    break;

                case 3:  //น้อยกว่า(<)
                    end_amt = start_amt - 0.01;
                    start_amt = -999999999999.99;
                    break;
                case 4:  //เท่ากับ(=)
                    end_amt = start_amt;
                    break;
                case 5:  //มากกว่า/เท่ากับ(>=)
                    end_amt = 999999999999.99;
                    break;
                case 6:  //น้อยกว่า/เท่ากับ(<=)
                    end_amt = start_amt;
                    start_amt = -999999999999.99;

                    break;

            }

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            //lnv_helper.AddArgument(start_dp_type, ArgumentType.String);
            //lnv_helper.AddArgument(adtm_tdate, ArgumentType.String);
            //lnv_helper.AddArgument(branch_id, ArgumentType.String);
            //lnv_helper.AddArgument(condition.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(start_amt.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(end_amt.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(dept_type1, ArgumentType.String);
            lnv_helper.AddArgument(dept_type2, ArgumentType.String);
            lnv_helper.AddArgument(coop_id, ArgumentType.String);

            //******************************************************************************

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                string printerName = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printerName);
                //outputProcess = WebUtil.runProcessingReportExtend(state, app, gid, rid, criteriaXML, pdfFileName, printerName);
                //String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                //if (li_return == "true")
                //{
                //    HdOpenIFrame.Value = "True";
                //}
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
