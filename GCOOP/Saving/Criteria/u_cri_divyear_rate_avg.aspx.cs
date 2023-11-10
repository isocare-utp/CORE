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

namespace Saving.Criteria
{
    public partial class u_cri_divyear_rate_avg : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();

            if (IsPostBack)
            {
                //dw_criteria.RestoreContext();
                this.RestoreContextDw(dw_criteria);
                
            }
            else
            {
                //default values.
               // int year = (DateTime.Now.Year) + 543;
                dw_criteria.InsertRow(0);
                JsGetYear();
                String div_year = dw_criteria.GetItemString(1, "div_year");
                DwUtil.RetrieveDDDW(dw_criteria, "rate1", "criteria.pbl", div_year);
                DwUtil.RetrieveDDDW(dw_criteria, "rate2", "criteria.pbl", div_year);
                DwUtil.RetrieveDDDW(dw_criteria, "rate3", "criteria.pbl", div_year);
                DwUtil.RetrieveDDDW(dw_criteria, "rate4", "criteria.pbl", div_year);
                DwUtil.RetrieveDDDW(dw_criteria, "rate5", "criteria.pbl", div_year);

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
        }

        #endregion
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            //String div_year = dw_criteria.GetItemString(1, "div_year");
            //Decimal rate1 = dw_criteria.GetItemDecimal(1, "rate1");
            //Decimal rate2 = dw_criteria.GetItemDecimal(1, "rate2");
            //Decimal rate3 = dw_criteria.GetItemDecimal(1, "rate3");
            //Decimal rate4 = dw_criteria.GetItemDecimal(1, "rate4");
            //Decimal rate5 = dw_criteria.GetItemDecimal(1, "rate5");
            String div_year = "";
            Decimal rate1, rate2, rate3, rate4, rate5 = 0;
            try 
            {
                div_year = Hddiv_year.Value.Trim();
            }
            catch { div_year = Convert.ToString(DateTime.Now.Year + 543); }
            
            try 
            {
                rate1 = Convert.ToDecimal(Hdrate1.Value);
            }
            catch { rate1 = 0; }
            try
            {
                rate2 = Convert.ToDecimal(Hdrate2.Value);
            }
            catch { rate2 = 0; }
            try
            {
                rate3 = Convert.ToDecimal(Hdrate3.Value);
            }
            catch { rate3 = 0; }
            try
            {
                rate4 = Convert.ToDecimal(Hdrate4.Value);
            }
            catch { rate4 = 0; }
            try
            {
                rate5 = Convert.ToDecimal(Hdrate5.Value);
            }
            catch { rate5 = 0; }
           
            

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(div_year, ArgumentType.String);
            lnv_helper.AddArgument(rate1.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate2.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate3.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate4.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate5.ToString(), ArgumentType.Number);


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                //String criteriaXML = lnv_helper.PopArgumentsXML();
                //this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
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

        private void JsGetYear()
        {
          //  Sta ta = new Sta(sqlca.ConnectionString);
            int account_year = 0;
            try
            {
                String sql = @"select max(current_year) from yrcfconstant";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    account_year = int.Parse(dt.GetString("max(current_year)"));
                    dw_criteria.SetItemString(1, "div_year", Convert.ToString(account_year));
                    Hddiv_year.Value = Convert.ToString(account_year);
                    //hd
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                account_year = DateTime.Now.Year;
                account_year = account_year + 543;
                dw_criteria.SetItemString(1, "div_year", account_year.ToString());
            }
        }
        #endregion
    }
}