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
using CoreSavingLibrary.WcfNCommon;
using DataLibrary;

namespace Saving.Criteria
{
    public partial class u_cri_coop_id_slip_payout_swd_dept : PageWebSheet, WebSheet
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

        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_nec);
            }
            else
            {
                //default values.
                dw_main.InsertRow(0);
                dw_main.SetItemDateTime(1, "start_date", state.SsWorkDate);
               
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

        private void Check_all()
        {
            decimal chk_all = dw_main.GetItemDecimal(1, "chk_all");
            if (chk_all == 1)
            {
                for (int j = 1; j <= dw_nec.RowCount; j++)
                {
                    dw_nec.SetItemDecimal(j, "check_flag", 1);
                }
            }
            else
            {
                for (int j = 1; j <= dw_nec.RowCount; j++)
                {
                    dw_nec.SetItemDecimal(j, "check_flag", 0);
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
            string tdate = dw_main.GetItemString(1, "start_tdate");
            DateTime reqdate;
            reqdate = DateTime.ParseExact(tdate, "ddMMyyyy", WebUtil.TH);
            //dw_criteria.GetItemDateTime(1, "req_date");
            DwUtil.RetrieveDataWindow(dw_nec, "criteria", tdw_criteria, state.SsCoopControl,reqdate);
           
          
         
          
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_nec.SaveDataCache();
        }

        #endregion
        #region Report Process

        private void RunProcess()
        {
           

            //String[] loan
            String slipno = "";
            int iii = 0;
            for (int i = 1; i <= dw_nec.RowCount; i++)
            {
                if (dw_nec.GetItemDecimal(i, "check_flag") == 1)
                {
                    if (iii == 0)
                    {
                        slipno += "'" + dw_nec.GetItemString(i, "compute_1") + "'";
                    }
                    else
                    {
                        slipno += ", '" + dw_nec.GetItemString(i, "compute_1") + "'";
                    }
                    iii++;
                }
            }
            slipno = "(" + slipno + ")";




            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            //DateTime start_date = dw_criteria.GetItemDateTime(1, "start_date");
            //DateTime end_date = dw_criteria.GetItemDateTime(1, "end_date");
            //String start_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "start_tdate", null);
            //String end_date = WebUtil.ConvertDateThaiToEng(dw_criteria, "end_tdate", null);
            string coop_id = "'" + state.SsCoopControl + "'";


            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();

            lnv_helper.AddArgument(slipno, ArgumentType.String);
            lnv_helper.AddArgument(coop_id, ArgumentType.String);



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
