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
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNPrincipalBalance;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_principal_balance : PageWebSheet, WebSheet
    {

        protected String postRun;
        protected String postReport;

        //protected PrincipalBalanceClient wsPrin;
        protected DwThDate dwThDate;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            //JSPostBack
            postRun = WebUtil.JsPostBack(this, "postRun");
            postReport = WebUtil.JsPostBack(this, "postReport");
            //ThaiDateFields
            dwThDate = new DwThDate(dw_criteria,this);
            dwThDate.Add("operate_date", "operate_tdate");
            dwThDate.Add("today_date", "today_tdate");
        }

        public void WebSheetLoadBegin()
        {
            HdOpenIFrame.Value = "False";
            if (IsPostBack)
            {
                RestoreContextDw(dw_criteria);
            }
            else
            {
                dw_criteria.InsertRow(0);
                dw_criteria.SetItemDateTime(1, "operate_date", DateTime.Today);
                dw_criteria.SetItemDateTime(1, "today_date", DateTime.Today);
                dwThDate.Eng2ThaiAllRow();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            DateTime operateDate = dw_criteria.GetItemDateTime(1, "operate_date");
            if (eventArg == "postRun")
            {
                try
                {
                    //wsPrin = wcf.NPrincipalBalance;
                    //int itemCount = wsPrin.ItemCount(state.SsWsPass, operateDate);
                    //if (itemCount > 0)
                    //{
                    //    //ให้ประมวผลใหม่หรือไม่.
                    //    int alwayRun = Convert.ToInt32(dw_criteria.GetItemDouble(1, "already_option"));
                    //    if (alwayRun == 0)
                    //    {
                    //        //ไม่ต้องรันตัวประมวลผล.
                    //        String oDate = WebUtil.ConvertDateThaiToEng(dw_criteria,"operate_tdate",null);
                    //        LtServerMessage.Text = WebUtil.CompleteMessage("วันที่ "+oDate+" มีข้อมูลไว้แล้ว "+Convert.ToString(itemCount)+" รายการ");
                    //        return;
                    //    }
                    //}
                    ////รันตัวประมวลผล
                    //wsPrin.Run(state.SsWsPass, operateDate);
                    HdOpenIFrame.Value = "True";
                }
                catch (Exception e)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(e);
                }
            }
            else if (eventArg == "postReport")
            {
                //OpenReport(operateDate);
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            dw_criteria.SaveDataCache();
        }

        #endregion

        private void OpenReport(DateTime operateDateTime)
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String operateDate = WebUtil.ConvertDateThaiToEng(dw_criteria, "operate_date", null);

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(operateDate, ArgumentType.DateTime);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_principalbalance_operatedate.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN); //ต้องมีเพื่อดูเวลาหมดอายุไฟล์.
            pdfFileName += "_principalbalance_";
            pdfFileName += operateDateTime.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            String pdfURL = "";
            String app = "shrlon"; //application (for report)
            String gid = "LN100_DAILY";        //report group id
            String rid = "";        //report id
            try
            {
                ////CoreSavingLibrary.WcfReport.ReportClient lws_report = wcf.Report;
                ////String criteriaXML = lnv_helper.PopArgumentsXML();
                //int li_return = lws_report.ReportPDF(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                /*LtServerMessage.Text = "<b>ReportPDF</b> return(" + Convert.ToString(li_return) + @") <br>
                <b>criteria:</b><code> " + criteriaXML + @"</code><br>
                <b>app:</b> " + app + @"<br>
                <b>gid:</b>" + gid + @"<br>
                <b>rid:</b>" + rid + @"<br>
                <b>pdf:</b>" + pdfFileName;*/
                //if (1 != li_return)
                //{
                //    //throw new Exception("สร้างรายงานไม่สำเร็จ");
                //}
                //pdfURL = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }

            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdfURL + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "PrincipalReport", pop, true);
        }

    }
}
