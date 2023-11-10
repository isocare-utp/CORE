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
    public partial class u_cri_divyear_rate_roundtype_div : PageWebSheet,WebSheet
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
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");
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
                string[] minmax = ReportUtil.GetMinMaxMembno();
                dw_criteria.SetItemString(1, "start_memberno", minmax[0]);
                dw_criteria.SetItemString(1, "end_memberno", minmax[1]);
                JsGetYear();
                JspostSetAccDate();
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
            String satang_type = "";
            //String truncate_pos_amt = "";
            String round_type = "";
            Decimal truncate_pos_amt = 0;
            Decimal round_pos_amt = 0;
            String report_type = dw_criteria.GetItemString(1, "report_type");
            try
            {
                String sql = @"select satang_type , truncate_pos_amt , round_type , round_pos_amt
	                            from (
		                            select satang_type , truncate_pos_amt , round_type , round_pos_amt , 2 as sort
		                            from cmroundmoney
		                            where coop_id = '010001'
		                            and applgroup_code = 'DIV'
		                            and function_code = 'ALL'
		                            and use_flag = 1
		                            union
		                            select satang_type , truncate_pos_amt , round_type , round_pos_amt , 1 as sort
		                            from cmroundmoney
		                            where coop_id = '010001'
		                            and applgroup_code = 'DIV'
		                            and function_code = 'rounddiv'
		                            and use_flag = 1
	                            ) cmrd
	                            where rownum = 1
	                            order by sort";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    satang_type = dt.GetString("satang_type");
                    truncate_pos_amt = dt.GetDecimal("truncate_pos_amt");
                    round_type = dt.GetString("round_type");
                    round_pos_amt = dt.GetDecimal("round_pos_amt");

                    // มี 7 หลัก
                    if (report_type == "MEM")
                    {
                        rid = "DIV_ESTIMATE06";
                    }
                    else
                    {
                        rid = "DIV_ESTIMATE07";
                    }

                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }


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

            String start_memberno = dw_criteria.GetItemString(1, "start_memberno").Trim();
            String end_memberno = dw_criteria.GetItemString(1, "end_memberno").Trim();

            //String start_tdate = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            //String end_tdate = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            
            DateTime start_date = dw_criteria.GetItemDate(1, "start_date");
            DateTime end_date = dw_criteria.GetItemDate(1, "end_date");


           // ToString("yyyyMMddHHmmss", WebUtil.EN);

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(div_year, ArgumentType.String);
            lnv_helper.AddArgument(start_memberno, ArgumentType.String);
            lnv_helper.AddArgument(end_memberno, ArgumentType.String);
            lnv_helper.AddArgument(start_date.ToString("yyyy/MM/dd",WebUtil.EN), ArgumentType.DateTime);
            lnv_helper.AddArgument(end_date.ToString("yyyy/MM/dd", WebUtil.EN), ArgumentType.DateTime);
            lnv_helper.AddArgument(rate1.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate2.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate3.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate4.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(rate5.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(satang_type, ArgumentType.String);
            lnv_helper.AddArgument(truncate_pos_amt.ToString(), ArgumentType.Number);
            lnv_helper.AddArgument(round_type, ArgumentType.String);
            lnv_helper.AddArgument(round_pos_amt.ToString(), ArgumentType.Number);


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

        //function การหาช่วงวันที่จากข้อมุลปีปันผล
        private void JspostSetAccDate()
        {
            String ls_divyear, ls_sacctdate, ls_eacctdate;
            int li_divyear;
            ls_divyear = dw_criteria.GetItemString(1, "div_year");
            //ls_divyear = ls_divyear.Substring(0, 4);
            li_divyear = int.Parse(ls_divyear) - 543;

            try
            {
                String sql = @"select beginning_of_accou,ending_of_account from accaccountyear where account_year = '" + li_divyear.ToString() + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ls_sacctdate = dt.GetString("beginning_of_accou");
                    ls_eacctdate = dt.GetString("ending_of_account");
                    dw_criteria.SetItemDateTime(1, "start_date", Convert.ToDateTime(ls_sacctdate));
                    dw_criteria.SetItemDateTime(1, "end_date", Convert.ToDateTime(ls_eacctdate));
                    tdw_criteria.Eng2ThaiAllRow();
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลปีบัญชี : " + ls_divyear);
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        #endregion
    }
}