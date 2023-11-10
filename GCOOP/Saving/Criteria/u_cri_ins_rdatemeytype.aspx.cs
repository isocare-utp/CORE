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
    public partial class u_cri_ins_rdatemeytype : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("adt_start", "as_start");
            tdw_criteria.Add("adt_end", "as_end");
        }
        private String getThDate(DateTime adtm_input)
        {
            string ls_ouput;
            //int li_year;

            String yrs = (adtm_input.Year + 543).ToString("0000");
            String mth = (adtm_input.Month.ToString("00"));
            String dty = (adtm_input.Day.ToString("00"));

            //li_year = adtm_input.Year;
            //li_year += 543;
            //adtm_input = Convert.ToDateTime(adtm_input.Month.ToString() + '/' + adtm_input.Day.ToString() + '/' + li_year.ToString());
            //ls_ouput = adtm_input.ToString("ddMMyyyy");
            ls_ouput = dty + mth + yrs;
            return ls_ouput;
        }

        public void WebSheetLoadBegin()
        {
            

           
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria);
            }
            else
            {
                String strDate = getThDate(DateTime.Now);
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "adt_start", DateTime.ParseExact(strDate, "ddMMyyyy", WebUtil.TH));  //state.SsWorkDate.AddYears(-1)
                dw_criteria.SetItemDateTime(1, "adt_end", DateTime.ParseExact(strDate, "ddMMyyyy", WebUtil.TH));
                tdw_criteria.Eng2ThaiAllRow();

                //DwUtil.RetrieveDataWindow(dw_criteria, "criteria.pbl", tdw_criteria, null);
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
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            try { DwUtil.RetrieveDDDW(dw_criteria, "as_pay", "criteria.pbl", null); }
            catch { }
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.


            String asyrs = WebUtil.ConvertDateThaiToEng(dw_criteria, "as_start", null);
            String asmth = WebUtil.ConvertDateThaiToEng(dw_criteria, "as_end", null);
            String cmpy = dw_criteria.GetItemString(1, "as_pay");

           
            
            
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.

            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(asyrs, ArgumentType.DateTime);
            lnv_helper.AddArgument(asmth, ArgumentType.DateTime);
            lnv_helper.AddArgument(cmpy, ArgumentType.String);
            
            //----------------------------------------------------


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

                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);

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
    }
}
