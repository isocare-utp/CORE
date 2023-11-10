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
    public partial class u_cri_reprintpayment : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String retrievedata;
        protected String check_date;
        protected String check_money;
        protected String check_id;
        protected String check_all;
        private DwThDate tdw_criteria;
        private string pbl = "criteria.pbl";
        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            check_date = WebUtil.JsPostBack(this, "check_date");
            check_money = WebUtil.JsPostBack(this, "check_money");
            check_id = WebUtil.JsPostBack(this, "check_id");
            check_all = WebUtil.JsPostBack(this, "check_all");
            retrievedata = WebUtil.JsPostBack(this, "retrievedata");
            tdw_criteria = new DwThDate(dw_main, this);
            tdw_criteria.Add("start_date", "start_tdate");
            tdw_criteria.Add("end_date", "end_tdate");

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();
            //DwUtil.RetrieveDDDW(dw_main, "sliptype_code", pbl, null);
            DwUtil.RetrieveDDDW(dw_main, "moneytype_code", pbl, null);
            DwUtil.RetrieveDDDW(dw_main, "user_name", pbl, null);

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_nec);
            }
            else
            {
                //default values.
                dw_main.InsertRow(0);
                //dw_main.SetItemString(1, "money_type", "");
                //dw_main.SetItemString(1, "entry_id", "");
                 dw_main.SetItemSqlString(1, "start_tdate", DateTime.Now.ToString("ddMMyyyy",WebUtil.TH));
                 dw_main.SetItemSqlString(1, "end_tdate", DateTime.Now.ToString("ddMMyyyy", WebUtil.TH));
        
                //dw_criteria.SetItemDateTime(1, "start_date", state.SsWorkDate);

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
                string ReportName = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
              //  ReportName.Text = "[" + rid + "]";
            }

            //Link back to the report menu.
           // LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
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
            else if (eventArg == "retrievedata")
            {
                Retrievedata();
            }
            else if (eventArg == "check_date")
            {
                Check_date();
            }
            else if (eventArg == "check_money")
            {
                Check_money();
            }
            else if (eventArg == "check_id")
            {
                Check_id();
            }
            else if (eventArg == "check_all")
            {
                Check_all();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_nec.SaveDataCache();
        }

        private void Check_all()
        {
            decimal chk_all = dw_main.GetItemDecimal(1, "chk_all");
            if (chk_all == 1)
            {
                for (int j = 1; j <= dw_nec.RowCount; j++)
                {
                    dw_nec.SetItemDecimal(j, "check_flagreqdoc", 1);
                }
            }
            else
            {
                for (int j = 1; j <= dw_nec.RowCount; j++)
                {
                    dw_nec.SetItemDecimal(j, "check_flagreqdoc", 0);
                }
            }
        }

        private void Check_id()
        {
            dw_main.SetItemString(1, "entry_id", "");
        }

        private void Check_money()
        {
            dw_main.SetItemString(1, "money_type", "");

        }

        private void Check_date()
        {


            dw_main.SetItemString(1, "start_tdate", "");
        }

        private void Retrievedata()
        {
            decimal[] flag;
            flag = new decimal[6];
            string[] col;
            col = new string[6];
            if (dw_main.GetItemDecimal(1, "memno_flag") == 1)
            {
                flag[0] = 1;
                string start_memno = dw_main.GetItemString(1, "start_memno");
                string end_memno = dw_main.GetItemString(1, "end_memno");
               // DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
                col[0]="member_no between '"+ start_memno + "' and '" +end_memno+"'";
              //  dw_nec.SetFilter("member_no between '" + start_memno + "' and '" + end_memno + "'");
            }
            else
            {
                flag[0] = 0;
            }
            if (dw_main.GetItemDecimal(1, "sliptype_flag") == 1)
            {
                flag[1] = 1;
                string sliptype = dw_main.GetItemString(1, "sliptype_code");
                col[1] = " sliptype_code ='" + sliptype + "'";
               
            }
            else
            {
                flag[1] = 0;
            }
            if (dw_main.GetItemDecimal(1, "money_flag")==1)
            {
                flag[2] = 1;
                string moneytype = dw_main.GetItemString(1, "moneytype_code");
                col[2] = " moneytype_code ='" + moneytype + "'";
            }
            else
            {
                flag[2] = 0;
            }
            if (dw_main.GetItemDecimal(1, "date_flag") == 1)
            {
                flag[3] = 1;
                string date_start2 = WebUtil.ConvertDateThaiToEng(dw_main, "start_tdate", dw_main.GetItemString(1, "start_tdate"));
                string date_end2 = WebUtil.ConvertDateThaiToEng(dw_main, "end_tdate", dw_main.GetItemString(1, "end_tdate"));
                col[3] = " slip_date between date('" + date_start2 + "') and date('" + date_end2 + "')";
            }
            else
            {
                flag[3] = 0;
            }
            if (dw_main.GetItemDecimal(1, "docno_flag") == 1)
            {
                flag[4] = 1;
                string docno_start = dw_main.GetItemString(1, "start_docno");
                string docno_end = dw_main.GetItemString(1, "end_tdate");
                col[4] = "document_no between '" + docno_start + "' and '" + docno_end + "'";
               
            }
            else
            {
                flag[4] = 0;
            }
            if (dw_main.GetItemDecimal(1, "user_flag") == 1)
            {
                flag[5] = 1;
                string entry_id = dw_main.GetItemString(1, "entry_id");
                col[5] = " entry_id ='" + entry_id + "'";
            }
            else
            {
                flag[5] = 0;
            }
            int chk = 0;
            string txtfillter="";
            for(int i = 0; i <= 5; i++)
            {
                if (flag[i] == 1)
                {
                    if (chk != 0)
                    {
                        txtfillter += " and "+col[i];
                        //chk = chk + 1;
                    }
                    else
                    {
                        txtfillter += col[i];
                        chk = chk + 1;
                    }
                }
                else
                { 
                    
                }
            }
            //txtfillter = txtfillter.ToString();
            DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
            dw_nec.SetFilter(txtfillter);
            dw_nec.Filter();

            //Decimal fmemno = dw_main.GetItemDecimal(1, "memno_flag");
            ////Decimal check_fmemno = dw_main.GetItemDecimal(1, "memno_flag");
            //Decimal fsliptype = dw_main.GetItemDecimal(1, "sliptype_flag");
            //Decimal fmoney = dw_main.GetItemDecimal(1, "money_flag");
            //Decimal fdate = dw_main.GetItemDecimal(1, "date_flag");
            //Decimal fdocno = dw_main.GetItemDecimal(1, "docno_flag");
            //Decimal fuser = dw_main.GetItemDecimal(1, "user_flag");

            // if (fmemno == 0 && fsliptype == 0 && fmoney == 0 && fdate == 0 && fdocno == 0 && fuser == 0)
            //{
                
            //}
            // else if (fmemno == 1 && fsliptype == 0 && fmoney == 0 && fdate == 0 && fdocno == 0 && fuser == 0)
            //{
            //    string start_memno = dw_main.GetItemString(1, "start_memno");
            //    string end_memno = dw_main.GetItemString(1, "end_memno");
            //    DwUtil.RetrieveDataWindow(dw_nec,"criteria", null, null);
            //    dw_nec.SetFilter("member_no between '"+start_memno+"' and '"+end_memno+"'");
            //    dw_nec.Filter();
            //}
            // else if (fmemno == 0 && fsliptype == 0 && fmoney == 0 && fdate == 0 && fdocno == 1 && fuser == 0)
            //{
            //    string docno_start = dw_main.GetItemString(1, "start_docno");
            //    string docno_end = dw_main.GetItemString(1, "end_tdate");
            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
            //    dw_nec.SetFilter("document_no between '" + docno_start + "' and '" + docno_end + "'");
            //    dw_nec.Filter();

            //}
            // else if (fmemno == 0 && fsliptype == 0 && fmoney == 0 && fdate == 1 && fdocno == 0 && fuser == 0)
            //{
               
            //    string date_start2 = WebUtil.ConvertDateThaiToEng(dw_main, "start_tdate", dw_main.GetItemString(1, "start_tdate"));
            //    string date_end2 = WebUtil.ConvertDateThaiToEng(dw_main, "end_tdate", dw_main.GetItemString(1, "end_tdate"));
            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
            //    dw_nec.SetFilter(" slip_date between date('" + date_start2 + "') and date('" + date_end2 + "')");
            //    dw_nec.Filter();

            //}
            // else if (fmemno == 0 && fsliptype == 1 && fmoney == 0 && fdate == 0 && fdocno == 0 && fuser == 0)
            // {

            //     string sliptype = dw_main.GetItemString(1, "sliptype_code");
            //     DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
            //     dw_nec.SetFilter(" sliptype_code ='" + sliptype + "'");
            //     dw_nec.Filter();

            // }
            // else if (fmemno == 0 && fsliptype == 0 && fmoney == 1 && fdate == 0 && fdocno == 0 && fuser == 0)
            // {

            //     string moneytype = dw_main.GetItemString(1, "moneytype_code");
            //     DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
            //     dw_nec.SetFilter(" moneytype_code ='" + moneytype + "'");
            //     dw_nec.Filter();
            // }
            // else if (fmemno == 0 && fsliptype == 0 && fmoney == 0 && fdate == 0 && fdocno == 0 && fuser == 1)
            // {

            //     string entry_id = dw_main.GetItemString(1, "entry_id");
            //     DwUtil.RetrieveDataWindow(dw_nec, "criteria", null, null);
            //     dw_nec.SetFilter(" entry_id ='" + entry_id + "'");
            //     dw_nec.Filter();

            // }

            //else if (check_fdate == 0 && check_fmoney == 1 && check_fuser == 0)
            //{
            //    DateTime[] reqdate = ReportUtil.GetMinMaxReqdateLnreqloan();
            //    string money_type = dw_main.GetItemString(1, "money_type");
            //    string[] entry_id = ReportUtil.GetMinMaxMembentryid();

            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", tdw_criteria, reqdate[0], reqdate[1], money_type, money_type, entry_id[0], entry_id[1]);

            //}
            //else if (check_fdate == 0 && check_fmoney == 0 && check_fuser == 1)
            //{
            //    DateTime[] reqdate = ReportUtil.GetMinMaxReqdateLnreqloan();
            //    string[] money_type = ReportUtil.GetMinMaxMoneytype();
            //    string entry_id = dw_main.GetItemString(1, "entry_id");

            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", tdw_criteria, reqdate[0], reqdate[1], money_type[0], money_type[1], entry_id, entry_id);

            //}
            //else if (check_fdate == 1 && check_fmoney == 1 && check_fuser == 0)
            //{
            //    string tdate = dw_main.GetItemString(1, "start_tdate");
            //    DateTime reqdate;
            //    reqdate = DateTime.ParseExact(tdate, "ddMMyyyy", WebUtil.TH);

            //    string money_type = dw_main.GetItemString(1, "money_type");
            //    string[] entry_id = ReportUtil.GetMinMaxMembentryid();

            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", tdw_criteria, reqdate, reqdate, money_type, money_type, entry_id[0], entry_id[1]);



            //}
            //else if (check_fdate == 1 && check_fmoney == 0 && check_fuser == 1)
            //{
            //    string tdate = dw_main.GetItemString(1, "start_tdate");
            //    DateTime reqdate;
            //    reqdate = DateTime.ParseExact(tdate, "ddMMyyyy", WebUtil.TH);

            //    string[] money_type = ReportUtil.GetMinMaxMoneytype();
            //    string entry_id = dw_main.GetItemString(1, "entry_id");

            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", tdw_criteria, reqdate, reqdate, money_type[0], money_type[1], entry_id, entry_id);

            //}
            //else if (check_fdate == 0 && check_fmoney == 1 && check_fuser == 1)
            //{
            //    DateTime[] reqdate = ReportUtil.GetMinMaxReqdateLnreqloan();
            //    string money_type = dw_main.GetItemString(1, "money_type");
            //    string entry_id = dw_main.GetItemString(1, "entry_id");

            //    DwUtil.RetrieveDataWindow(dw_nec, "criteria", tdw_criteria, reqdate[0], reqdate[1], money_type, money_type, entry_id, entry_id);

            //}


        }

        #endregion
        #region Report Process

        private void RunProcess()
        {


            //String[] loan
            String loans = "";
            int iii = 0;
            for (int i = 1; i <= dw_nec.RowCount; i++)
            {
                if (dw_nec.GetItemDecimal(i, "print_flag") == 1)
                {
                    if (iii == 0)
                    {
                        loans += "'" + dw_nec.GetItemString(i, "document_no") + "'";
                    }
                    else
                    {
                        loans += ", '" + dw_nec.GetItemString(i, "document_no") + "'";
                    }
                    iii++;
                }
            }
            loans = "(" + loans + ")";




            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            //DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            //DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            //String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);



            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(loans, ArgumentType.String);



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
                //String li_return = lws_report.RunWithID(state.SsWsPass, app, gid, rid, state.SsUsername, criteriaXML, pdfFileName);
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