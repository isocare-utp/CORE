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
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using DataLibrary;
using System.Text;
using CoreSavingLibrary.WcfNFinance;
using System.IO;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_fn_printfinstatus : PageWebSheet, WebSheet
    {
        private string pbl = "billpayment.pbl";
        private n_financeClient fin;


        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String postSaveItem;
        protected String Getstring;
        protected String postOpenEdit;
        protected String postGetList;
        protected String postjsExptxt;
        protected String postSetSort;
        protected String postSelectMemb;
        protected String postRefresh;
        protected DwThDate tDwHead;
        protected String postFillReject;
        DataStore DStore;

        public void InitJsPostBack()
        {
            Getstring = WebUtil.JsPostBack(this, "Getstring");
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            postSaveItem = WebUtil.JsPostBack(this, "postSaveItem");
            postOpenEdit = WebUtil.JsPostBack(this, "postOpenEdit");
            postGetList = WebUtil.JsPostBack(this, "postGetList");
            postjsExptxt = WebUtil.JsPostBack(this, "postjsExptxt");
            postSetSort = WebUtil.JsPostBack(this, "postSetSort");
            postSelectMemb = WebUtil.JsPostBack(this, "postSelectMemb");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postFillReject = WebUtil.JsPostBack(this, "postFillReject");
            tDwHead = new DwThDate(dw_main, this);
            tDwHead.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            dw_main.SetTransaction(sqlca);
            //dw_maccid.SetTransaction(sqlca);
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                //dw_main.InsertRow(0);
                //dw_main.InsertRow(0);
                String re = "";

                re = fin.of_init_printfinstatus(state.SsWsPass, state.SsCoopId, state.SsWorkDate);
                if (re != "")
                {
                    dw_main.Reset();
                    dw_main.ImportString(re, FileSaveAsType.Xml);
                }
                else
                {
                    dw_main.Reset();
                    dw_main.InsertRow(0);
                }
                //DataWindowChild DcAccid = dw_main.GetChild("coopbranch_id");
                //DcAccid.Reset();
                DwUtil.RetrieveDDDW(dw_main, "coopbranch_id", "start_day.pbl", null);
                //DcAccid.SetFilter("moneytype_code = 'CBT'");
                //DcAccid.Filter();
                dw_main.SetItemString(1, "coopbranch_id", state.SsCoopId);
                dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
                tDwHead.Eng2ThaiAllRow();
            }
            else
            {
                //this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_main);
            }


            try
            {
                app = "app_finance";
                // app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = "finance";
                // gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = "fnd00000";
                //rid = Request["rid"].ToString();
            }
            catch { }


            //Report Name.
            //            try
            //            {
            //                Sta ta = new Sta(state.SsConnectionString);
            //                String sql = "";
            //                sql = @"SELECT REPORT_NAME  
            //                    FROM WEBREPORTDETAIL  
            //                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
            //                Sdt dt = ta.Query(sql);
            //                //ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
            //                ta.Close();
            //            }
            //            catch
            //            {
            //                //ReportName.Text = "[" + rid + "]";
            //            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "runProcess":
                    RunProcess();
                    break;
                case "popupReport":
                    PopupReport();
                    break;
                case "postjsExptxt":
                    ExpTextSMS();
                    break;

            }
        }

        public void SaveWebSheet()
        {

            //DwMainXML = dw_main.Describe("DataWindow.Data.XML");

            try
            {
                Int16 re = 0;// fin.OfPostSaveBillPayMent(state.SsWsPass, state.SsWorkDate, DwMainXML);

                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("เรียบร้อย");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

            try
            {
                //DwUtil.RetrieveDDDW(dw_main, "loancontract_no", pbl);
            }
            catch
            {
            }
            //dw_main.SaveDataCache();
            dw_main.SaveDataCache();
        }

        public void ExpTextSMS()
        {
            String xmlmain = dw_main.Describe("Datawindow.Data.Xml");
            String fileName = "FinStatus_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            string str= System.AppDomain.CurrentDomain.BaseDirectory; 
            String path = WebUtil.PhysicalPath + "Saving\\filecommon\\" + fileName;
            if (1 > 0)
            {
                if (xmlmain != null && xmlmain != "")
                {

                    try
                    {
                        int result = 0;
                        result = fin.of_postfinstatusexport(state.SsWsPass, state.SsCoopName, xmlmain, path);

                        if (result != 1)
                        {
                            throw new Exception(path);
                        }
                        //DStore = new DataStore();
                        //DStore.LibraryList = WebUtil.PhysicalPath + @"Saving\DataWindow\app_finance\billpayment.pbl";
                        //DStore.DataWindowObject = "d_kp_dsksrv_linetext_egat";
                        //DStore.ImportString(Result, FileSaveAsType.Xml);

                        //string TextData = DStore.Describe("Datawindow.Data");
                        //StreamWriter writer = new StreamWriter(path);
                        //writer.Write(TextData);
                        //writer.Close();

                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ คุณสามารถดาวน์โหลดไฟล์ <a href=\"" + state.SsUrl + "filecommon/" + fileName + "\" target='_blank'>" + fileName + "</a>");

                    }
                    catch (Exception ex)
                    {
                        //throw ex.Message + path ;
                        //LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                        //throw new Exception(path + " : " + ex.Message);
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูล ไม่สามารถทำรายการได้");
                }
            }
            else { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่มีข้อมูล ไม่สามารถทำรายการได้"); }
        }

        #region Report Process
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            //String start_membgroup = dw_criteria.GetItemString(1, "start_membgroup");
            //String end_membgroup = dw_criteria.GetItemString(1, "end_membgroup");
            String workdate = WebUtil.ConvertDateThaiToEng(dw_main, "operate_tdate", null); ;
            String bankbegin = dw_main.GetItemDecimal(1, "bankbegin").ToString("#,##0.00");
            String bankforward = dw_main.GetItemDecimal(1, "bankforward").ToString("#,##0.00");
            String bankforward_sav = dw_main.GetItemDecimal(1, "bankforward_sav").ToString("#,##0.00");

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(state.SsCoopId, ArgumentType.String);
            lnv_helper.AddArgument(state.SsCoopName, ArgumentType.String);
            lnv_helper.AddArgument(workdate, ArgumentType.DateTime);
            lnv_helper.AddArgument(bankbegin, ArgumentType.Number);
            lnv_helper.AddArgument(bankforward, ArgumentType.Number);
            lnv_helper.AddArgument(bankforward_sav, ArgumentType.Number);

            //****************************************************************

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