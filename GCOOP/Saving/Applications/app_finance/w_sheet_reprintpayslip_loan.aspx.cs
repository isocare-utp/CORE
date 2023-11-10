using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;
using System.Threading;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_reprintpayslip_loan : PageWebSheet, WebSheet
    {
        private n_financeClient fin;
        private DwThDate tDwHead;
        protected String postPaySlipRetrieve;
        protected String postSetItem;
        protected String postPrint;

        string reqdoc_no = "";
        String member_no = "";
        String ref_slipno = "";
        string fromset = "";

        int x = 1;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        #region WebSheet Members

        public void InitJsPostBack()
        {

            postPrint = WebUtil.JsPostBack(this, "postPrint");
            postPaySlipRetrieve = WebUtil.JsPostBack(this, "postPaySlipRetrieve");
            postSetItem = WebUtil.JsPostBack(this, "postSetItem");
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("adtm_date", "adtm_tdate");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;
            this.ConnectSQLCA();
            DwList.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                DwHead.InsertRow(0);
                DwHead.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                tDwHead.Eng2ThaiAllRow();
            }
            else
            {
                this.RestoreContextDw(DwHead);
                this.RestoreContextDw(DwList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postPaySlipRetrieve":
                    PaySlipRetrieve();
                    break;
                case "postSetItem":
                    SetItem();
                    break;
                case "postPrint":
                    Print();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwList.SaveDataCache();
        }

        #endregion


        private void Print()
        {

            int row;
            row = DwList.FindRow("ai_select=1", 1, DwList.RowCount);

            if (row > 0)
            {
                try
                {
                    DwList.SetFilter("ai_select=1");
                    DwList.Filter();
                    String as_list_xml = DwList.Describe("DataWindow.Data.XML");
                    member_no = DwList.GetItemString(DwList.RowCount, "member_no");
                    ref_slipno = DwList.GetItemString(DwList.RowCount, "payoutslip_no");
                    x = 2;
                    JspopupReportslipfin();

                }

                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }


            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่เลือกรายการ");
            }
        }

        private void SetItem()
        {
            String Coloum = HfColoum.Value;
            String NewValue = HfNewValue.Value;
            String memberNo = DwHead.GetItemString(1, "as_memberno");
            String as_receipt = DwHead.GetItemString(1, "as_receipt");

            if (NewValue == "//" && Coloum == "adtm_tdate")
            {
                DwHead.SetItemNull(1, "adtm_date");
                tDwHead.Eng2ThaiAllRow();
            }
            else if (Coloum == "adtm_tdate")
            {
                DwHead.SetItemDateTime(1, "adtm_date", Convert.ToDateTime(NewValue));
            }
            else if (Coloum == "as_memberno")
            {

                DwHead.SetItemString(1, "as_memberno", memberNo);
                DwList.SetFilter("SLSLIPPAYOUT.MEMBER_NO='" + memberNo + "'");
                DwList.Filter();
            }
            else if (Coloum == "as_receipt")
            {

                DwHead.SetItemString(1, "as_receipt", as_receipt);
                DwList.SetFilter("SLSLIPPAYOUT.PAYOUTSLIP_NO='" + as_receipt + "'");
                DwList.Filter();
            }

        }

        private void PaySlipRetrieve()
        {

            DateTime adtm_date = DwHead.GetItemDate(1, "adtm_date");
            DwList.Reset();
            DwList.Retrieve(state.SsCoopId, adtm_date);

        }

        private void JspopupReportslipfin()
        {
            JsRunProcessslipfin();
            // Thread.Sleep(5000);
            Thread.Sleep(2500);
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdfslipfin"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }
        private void JsRunProcessslipfin()
        {

            // --- Page Arguments
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
                //gid = Request["gid"].ToString();
                gid = "LNNORM_DAILY";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "LNNORM_DAILY20";
            }
            catch { }
            String doc_no = "";


            doc_no = ref_slipno;

            String coop_id;
            coop_id = state.SsCoopId;
            if (x == 2)
            {
                doc_no = ref_slipno;
            }

            if (doc_no == null || doc_no == "")
            {
                return;
            }


            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(doc_no, ArgumentType.String);

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
                //    HdcheckPdf.Value = "True";

                //}
                //else if (li_return != "true")
                //{
                //    HdcheckPdf.Value = "False";
                //}
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdfslipfin"] = pdf;


        }
    }
}
