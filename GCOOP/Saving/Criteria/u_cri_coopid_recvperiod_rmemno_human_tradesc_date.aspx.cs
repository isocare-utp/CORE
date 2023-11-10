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
using System.IO;

namespace Saving.Criteria
{
    public partial class u_cri_coopid_recvperiod_rmemno_human_tradesc_date : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String URL;
        protected String runProcess;
        protected String popupReport;
        protected String JsPostSetTran;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            JsPostSetTran = WebUtil.JsPostBack(this, "JsPostSetTran");
            tdw_criteria = new DwThDate(dw_criteria, this);
            tdw_criteria.Add("operate_date", "operate_tdate");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "coop_id", "criteria.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_criteria, "human_id", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "trans_position", "criteria.pbl", null);

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_criteria, tdw_criteria);
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemString(1, "coop_id", state.SsCoopId);
                dw_criteria.SetItemString(1, "as_year", (DateTime.Now.Year + 543).ToString("0000"));
                dw_criteria.SetItemString(1, "as_month", (DateTime.Now.Month).ToString("00"));
                string[] minmax = ReportUtil.GetMinMaxMembno();
                dw_criteria.SetItemString(1, "start_membno", minmax[0]);
                dw_criteria.SetItemString(1, "end_membno", minmax[1]);
                dw_criteria.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tdw_criteria.Eng2ThaiAllRow();
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
                string rpttype = rdo_rtptype.SelectedValue;

                if (rpttype == "PDF")
                {
                    RunProcess();
                }
                else
                {
                    RunExcel();
                }


            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "JsPostSetTran")
            {
                decimal operate_flag = dw_criteria.GetItemDecimal(1, "operate_flag");

                if (operate_flag == 0)
                {
                    dw_criteria.SetItemString(1, "trans_position", "");
                }
            }
        }

        private void RunExcel()
        {
            URL = "";
            try
            {
                iReportArgument args = new iReportArgument();

                string coop_id = dw_criteria.GetItemString(1, "coop_id");
                string as_year = dw_criteria.GetItemString(1, "as_year");
                string as_month = dw_criteria.GetItemString(1, "as_month");
                string recv_period = as_year + as_month;
                string start_membno = dw_criteria.GetItemString(1, "start_membno");
                string end_membno = dw_criteria.GetItemString(1, "end_membno");
                string as_tmonth = "";

                switch (as_month)
                {
                    case "01": as_tmonth = "มกราคม";
                        break;
                    case "02": as_tmonth = "กุมภาพันธ์";
                        break;
                    case "03": as_tmonth = "มีนาคม";
                        break;
                    case "04": as_tmonth = "เมษายน";
                        break;
                    case "05": as_tmonth = "พฤษภาคม";
                        break;
                    case "06": as_tmonth = "มิถุนายน";
                        break;
                    case "07": as_tmonth = "กรกฎาคม";
                        break;
                    case "08": as_tmonth = "สิงหาคม";
                        break;
                    case "09": as_tmonth = "กันยายน";
                        break;
                    case "10": as_tmonth = "ตุลาคม";
                        break;
                    case "11": as_tmonth = "พฤศจิกายน";
                        break;
                    case "12": as_tmonth = "ธันวาคม";
                        break;
                }



                args.Add("coop_id", iReportArgumentType.String, coop_id);
                args.Add("recv_period", iReportArgumentType.String, recv_period);
                args.Add("end_membno", iReportArgumentType.String, end_membno);
                args.Add("start_membno", iReportArgumentType.String, start_membno);
                args.Add("as_month", iReportArgumentType.String, as_tmonth);
                iReportBuider report1 = new iReportBuider(this, "เงินโอนคืนสมาชิกเนื่องจากชำระหนี้ซ้ำประจำเดือน");
                report1.AddCriteria("r_010_kp_kpms_notify_money_return", "ดาวน์โหลด Excel", ReportType.xls_data, args);
                // report1.//.AutoOpenPDF uuk= true;
                String progressId = report1.Retrieve();


                String sqlQuery = "select CRITERIA_XML from cmreportprocessing where process_id = '" + progressId + "'";
                Sdt Query;

                Query = WebUtil.QuerySdt(sqlQuery);
                if (Query.Next())
                {
                    String CRITERIA_XML = Query.GetString("CRITERIA_XML");
                    URL = XmlReadVar(CRITERIA_XML, "output_url");
                    LtServerMessage.Text = WebUtil.CompleteMessage("<a href='" + URL + "' target='_black'>" + "ดาวน์โหลด EXCEL" + "</a>");
                    //LtServerMessage.Text = WebUtil.CompleteMessage("<a href='" + URL + "' target='_black'>" + "ดาวน์โหลด EXCEL" + "</a><script>window.open('" + URL + "');</script>");
                   // LtServerMessage.Text = WebUtil.CompleteMessage("<a href='" + URL + "' target='_black'>" + "ดาวน์โหลด EXCEL" + "</a>");
                }
                if (URL == "" || URL == null) { LtServerMessage.Text = WebUtil.ErrorMessage("สร้างรายงานไม่สำเร็จ"); }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        private static string XmlReadVar(string responseData, string szVar)
        {
            int i1stLoc = responseData.IndexOf("<" + szVar + ">");
            if (i1stLoc < 0)
                return string.Empty;
            int ilstLoc = responseData.IndexOf("</" + szVar + ">");
            if (ilstLoc < 0)
                return string.Empty;
            int len = szVar.Length;
            return responseData.Substring(i1stLoc + len + 2, ilstLoc - i1stLoc - len - 2);
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }
        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String human_name = "";
            String human_position = "";

            String coop_id = dw_criteria.GetItemString(1, "coop_id");
            String as_year = dw_criteria.GetItemString(1, "as_year");
            String as_month = dw_criteria.GetItemString(1, "as_month");
            String recv_period = as_year + as_month;
            String start_membno = dw_criteria.GetItemString(1, "start_membno");
            String end_membno = dw_criteria.GetItemString(1, "end_membno");
            String human_id = dw_criteria.GetItemString(1, "human_id");
            String trans_position = "";
            try
            {
                trans_position = dw_criteria.GetItemString(1, "trans_position");
            }
            catch (Exception ex)
            {
                trans_position = "";
            }
            String operate_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "operate_tdate", null);

            String sql = @"select prename,human_name,human_position from cmcoophuman where human_id = '" + human_id + "'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                human_name = dt.GetString("prename") + dt.GetString("human_name");
                human_position = dt.GetString("human_position");
                if (dw_criteria.GetItemDecimal(1, "operate_flag") == 1)
                {
                    human_position = human_position + " ทำการแทน";
                }
            }
            else
            {
                sqlca.Rollback();
            }

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(recv_period, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(end_membno, ArgumentType.String);
            lnv_helper.AddArgument(human_name, ArgumentType.String);
            lnv_helper.AddArgument(human_position, ArgumentType.String);
            lnv_helper.AddArgument(trans_position, ArgumentType.String);
            lnv_helper.AddArgument(operate_date, ArgumentType.DateTime);

            //-------------------------------------------------------

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
        #endregion

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        #endregion
    }
}
