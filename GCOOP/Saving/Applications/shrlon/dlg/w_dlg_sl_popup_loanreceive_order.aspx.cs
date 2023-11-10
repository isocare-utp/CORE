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


namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_popup_loanreceive_order : PageWebDialog, WebDialog
    {
        private n_shrlonClient slService;
        private n_commonClient commonSrv;
        private DwThDate tDwMain;
        //*******ประกาศตัวเกี่ยวกับ  Reprot********//
        string reqdoc_no = "";
        String member_no = "";
        string fromset = "";
        int x = 1;

        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;

        protected String jsrunProcessLoan;
        protected String jsRunProcessInvoice;
        protected String jsrunProcessAgreeLoan;
        protected String jsrunProcessAgreeColl;
        protected String jsrunProcessCollReport;
        protected String jsrunProcessDeptReport;

        protected String jspopupAgreeLoanReport;
        protected String jspopupAgreeCollReport;
        protected String jspopupCollReport;
        protected String jspopupReportInvoice;
        protected String jspopupLoanReport;
        protected String jspopupDeptReport;


        protected String saveSlipLnRcv;
        protected String initDataWindow;
        protected String initLnRcvReCalInt;
        protected String calculateitempayamt;
        protected String jsRefresh;
        protected ArrayList dwList;
        protected String[] arrValue = new String[3];
        String loanContract_No;
        String as_initfrom = "", as_reqcontno = "", as_xmllnrcv = "";
        protected String GetNewLoan;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            saveSlipLnRcv = WebUtil.JsPostBack(this, "saveSlipLnRcv");
            initDataWindow = WebUtil.JsPostBack(this, "initDataWindow");
            initLnRcvReCalInt = WebUtil.JsPostBack(this, "initLnRcvReCalInt");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            GetNewLoan = WebUtil.JsPostBack(this, "GetNewLoan");
            calculateitempayamt = WebUtil.JsPostBack(this, "calculateitempayamt");



            jsrunProcessLoan = WebUtil.JsPostBack(this, "jsrunProcessLoan");
            jsRunProcessInvoice = WebUtil.JsPostBack(this, "jsRunProcessInvoice");
            jsrunProcessAgreeColl = WebUtil.JsPostBack(this, "jsrunProcessAgreeColl");
            jsrunProcessCollReport = WebUtil.JsPostBack(this, "jsrunProcessCollReport");
            jsrunProcessAgreeLoan = WebUtil.JsPostBack(this, "jsrunProcessAgreeLoan");
            jsrunProcessDeptReport = WebUtil.JsPostBack(this, "jsrunProcessDeptReport");
            jspopupAgreeLoanReport = WebUtil.JsPostBack(this, "jspopupAgreeLoanReport");
            jspopupAgreeCollReport = WebUtil.JsPostBack(this, "jspopupAgreeCollReport");
            jspopupCollReport = WebUtil.JsPostBack(this, "jspopupCollReport");
            jspopupReportInvoice = WebUtil.JsPostBack(this, "jspopupReportInvoice");
            jspopupLoanReport = WebUtil.JsPostBack(this, "jspopupLoanReport");
            jspopupDeptReport = WebUtil.JsPostBack(this, "jspopupDeptReport");

            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("operate_date", "operate_tdate");




        }

        public void WebDialogLoadBegin()
        {
            slService = wcf.NShrlon;
            commonSrv = wcf.NCommon;

            if (!IsPostBack)
            {
                this.InitDataWindow();
                DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "sl_loan_receive.pbl", null);
                //DataWindowChild moneyType = DwMain.GetChild("moneytype_code");
                //String xml = commonSrv.GetDDDWXml(state.SsWsPass, "dddw_sl_ucfmoneytypeday");
                //moneyType.ImportString(xml, FileSaveAsType.Xml);


            }
            else
            {
                this.RestoreContextDw(DwMain);
                try
                {
                    String dtString = DwMain.GetItemString(1, "operate_tdate");
                    dtString = dtString.Replace("/", "");
                    DwMain.SetItemDateTime(1, "operate_date", DateTime.ParseExact(dtString, "ddMMyyyy", WebUtil.TH));
                }
                catch { }
                DwOperateLoan.RestoreContext();
                DwOperateEtc.RestoreContext();
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "saveSlipLnRcv")
            {
                this.SaveSlipLnRcv();
            }
            if (eventArg == "initLnRcvReCalInt")
            {
                this.InitLnRcvReCalInt();
            }
            if (eventArg == "initDataWindow")
            {
                this.InitDataWindow();
            }
            if (eventArg == "fieldProperty")
            {

            }
            if (eventArg == "jsRefresh")
            {
                Refresh();
            }
            if (eventArg == "GetNewLoan")
            {
                JsGetNewLoan();
            }
            if (eventArg == "calculateitempayamt")
            {
                Calculateitempayamt();
            }
            else if (eventArg == "jsrunProcessLoan") { JsrunProcessLoan(); }
            else if (eventArg == "jsRunProcessInvoice")
            {
                // JsRunProcessInvoice();
            }
            else if (eventArg == "jsrunProcessAgreeColl") { JsrunProcessAgreeColl(); }
            else if (eventArg == "jsrunProcessCollReport") { JsrunProcessCollReport(); }
            else if (eventArg == "jsrunProcessAgreeLoan") { JsrunProcessAgreeLoan(); }

            else if (eventArg == "jspopupAgreeLoanReport") { JspopupAgreeLoanReport(); }
            else if (eventArg == "jspopupAgreeCollReport") { JspopupAgreeCollReport(); }
            else if (eventArg == "jspopupCollReport") { JspopupCollReport(); }
            else if (eventArg == "jspopupReportInvoice")
            {
                //JspopupReportInvoice();
            }
            else if (eventArg == "jspopupLoanReport") { JspopupLoanReport(); }
            else if (eventArg == "jspopupDeptReport") { JspopupDeptReport(); }
        }


        private void JspopupLoanReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            //String pop = "Gcoop.OpenPopup('" + Session["Loan"].ToString() + "')";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                ////string re = slService.of_printloan(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }


        }

        private void JspopupLoanemerReport()
        {
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                //string re = wcf.NShrlon.of_printloan(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
        }

        private void JspopupCollReport()
        {

            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                //string re = slService.of_printloancoll(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
        }

        private void JspopupAgreeCollReport()
        {

            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                ////string re = slService.of_printloancollagree(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
        }

        private void JspopupAgreeLoanReport()
        {

            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                //string re = slService.of_printloanagree(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }

        }

        private void JspopupDeptReport()
        {

            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                ////string re = slService.of_printloandept(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
        }
        private void JspopupCollReport(bool isJsPrint)
        {
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            try
            {
                //                String sql = @"
                //                    SELECT 
                //	                    MBUCFPRENAME.PRENAME_DESC,   
                //	                    MBMEMBMASTER.MEMB_NAME,   
                //	                    MBMEMBMASTER.MEMB_SURNAME,   
                //	                    LNREQLOAN.LOANRCVFIX_DATE,   
                //	                    LNLOANTYPE.LOANTYPE_DESC,   
                //	                    MBUCFMEMBGROUP_A.MEMBGROUP_CODE,   
                //	                    MBMEMBMASTER.MEMBER_NO,   
                //	                    MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                //	                    MBMEMBMASTER.POSITION_DESC,   
                //	                    MBMEMBMASTER.LEVEL_CODE,   
                //	                    MBUCFMEMBGROUP_A.MEMBGROUP_DESC,   
                //	                    MBUCFMEMBGROUP_B.MEMBGROUP_DESC,   
                //	                    LNREQLOAN.LOANREQUEST_AMT,   
                //	                    LNCFLOANINTRATEDET.INTEREST_RATE,   
                //	                    LNREQLOAN.PERIOD_PAYAMT,   
                //	                    LNUCFLOANOBJECTIVE.LOANOBJECTIVE_DESC,   
                //	                    LNLOANTYPE.LOANTYPE_CODE,   
                //	                    LNREQLOAN.PERIOD_PAYMENT ,
                //	                    (select b.prename_desc||'  '||a.memb_name||'   '||a.memb_surname from mbmembmaster a,mbucfprename b where a.prename_code =b.prename_code and a.coop_id =lnreqloan.memcoop_id and a.member_no =lnreqloan.member_no)as loanreq_name,
                //	                    (select c.membtype_desc from mbmembmaster a,mbucfprename b,mbucfmembtype c where a.prename_code =b.prename_code and a.coop_id =lnreqloan.memcoop_id and a.member_no =lnreqloan.member_no and a.coop_id = c.coop_id and a.membtype_code =c.membtype_code)as loanreq_membtype,
                //	                    (select a.position_desc from mbmembmaster a,mbucfprename b where a.prename_code =b.prename_code and a.coop_id =lnreqloan.memcoop_id and a.member_no =lnreqloan.member_no)as loanreq_position,
                //	                    (select d.membgroup_desc from mbmembmaster a,mbucfprename b,mbucfmembgroup c,mbucfmembgroup d where a.prename_code =b.prename_code and a.coop_id =lnreqloan.memcoop_id and a.member_no =lnreqloan.member_no and a.coop_id =c.coop_id and a.membgroup_code =c.membgroup_code and c.coop_id =d.coop_id and c.membgroup_control =d.membgroup_code )as loanreq_membgroup_a,
                //	                    (select c.membgroup_desc from mbmembmaster a,mbucfprename b,mbucfmembgroup c,mbucfmembgroup d where a.prename_code =b.prename_code and a.coop_id =lnreqloan.memcoop_id and a.member_no =lnreqloan.member_no and a.coop_id =c.coop_id and a.membgroup_code =c.membgroup_code and c.coop_id =d.coop_id and c.membgroup_control =d.membgroup_code )as loanreq_membgroup_b,
                //	                    (select a.level_code from mbmembmaster a,mbucfprename b where a.prename_code =b.prename_code and a.coop_id =lnreqloan.memcoop_id and a.member_no =lnreqloan.member_no)as loanreq_level,
                //	                    lnreqloan.member_no as loanreq_memberno,
                //	                    lncontmaster.loancontract_no,
                //	                    lnreqloancoll.seq_no,'  ' as age ,'        'as mem_no
                //                    FROM 
                //	                    LNREQLOAN,   
                //	                    MBMEMBMASTER,   
                //	                    MBUCFPRENAME,   
                //	                    LNCONTMASTER,   
                //	                    LNLOANTYPE,   
                //	                    MBUCFMEMBGROUP MBUCFMEMBGROUP_A,   
                //	                    MBUCFMEMBGROUP MBUCFMEMBGROUP_B,   
                //	                    MBUCFMEMBTYPE,   
                //	                    LNCFLOANINTRATE,   
                //	                    LNCFLOANINTRATEDET,   
                //	                    LNUCFLOANOBJECTIVE,
                //	                    lnreqloancoll
                //                    WHERE 
                //	                    ( lnreqloan.coop_id = lncontmaster.coop_id (+)) and  
                //	                    ( lnreqloan.loanrequest_docno = lncontmaster.loanrequest_docno (+)) and  
                //	                    ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                //	                    ( LNREQLOANCOLL.COOP_ID = MBMEMBMASTER.COOP_ID ) and  
                //	                    ( LNREQLOANCOLL.REF_COLLNO = MBMEMBMASTER.MEMBER_NO ) and  
                //	                    ( LNREQLOAN.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                //	                    ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                //	                    ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP_A.COOP_ID ) and  
                //	                    ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP_A.MEMBGROUP_CODE ) and  
                //	                    ( MBUCFMEMBGROUP_A.COOP_ID = MBUCFMEMBGROUP_B.COOP_ID ) and  
                //	                    ( MBUCFMEMBGROUP_A.MEMBGROUP_CONTROL = MBUCFMEMBGROUP_B.MEMBGROUP_CODE ) and  
                //	                    ( MBMEMBMASTER.COOP_ID = MBUCFMEMBTYPE.COOP_ID ) and  
                //	                    ( MBMEMBMASTER.MEMBTYPE_CODE = MBUCFMEMBTYPE.MEMBTYPE_CODE ) and  
                //	                    ( LNCFLOANINTRATEDET.COOP_ID = LNCFLOANINTRATE.COOP_ID ) and  
                //	                    ( LNCFLOANINTRATEDET.LOANINTRATE_CODE = LNCFLOANINTRATE.LOANINTRATE_CODE ) and  
                //	                    ( LNREQLOAN.INT_CONTINTTABCODE = LNCFLOANINTRATE.LOANINTRATE_CODE ) and  
                //	                    ( LNREQLOAN.COOP_ID = LNCFLOANINTRATE.COOP_ID ) and  
                //	                    ( LNREQLOAN.COOP_ID = LNUCFLOANOBJECTIVE.COOP_ID ) and  
                //	                    ( LNREQLOAN.LOANOBJECTIVE_CODE = LNUCFLOANOBJECTIVE.LOANOBJECTIVE_CODE ) and  
                //	                    ( ( LNREQLOAN.COOP_ID = '" + state.SsCoopId + @"' ) AND  
                //	                    ( LNREQLOAN.LOANREQUEST_DOCNO = '" + reqdoc_no + @"' ) )    and
                //	                    lnreqloan.coop_id = lnreqloancoll.coop_id and
                //	                    lnreqloan.loanrequest_docno = lnreqloancoll.loanrequest_docno and
                //	                    lnreqloancoll.loancolltype_code ='01'
                //                ";
                //                DataTable data = WebUtil.Query(sql);
                //                Printing.Print(this, "Slip/shrlon/loanrequest_coll.aspx", data, 1);
                int printMode = xmlconfig.ShrlonPrintMode;
                Printing.ShrlonPrintCollReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }

        private void JspopupLoanReport(bool isJsPrint, int printMode)
        {
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            try
            {
                Printing.ShrlonPrintLoanReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }

        private void JspopupAgreeCollReport(bool isJsPrint, int printMode)
        {
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            try
            {
                Printing.ShrlonPrintAgreeCollReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }

        private void JspopupAgreeLoanReport(bool isJsPrint, int printMode)
        {
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            try
            {
                Printing.ShrlonPrintAgreeLoanReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }

        private void JspopupDeptReport(bool isJsPrint, int printMode)
        {
            try
            {
                reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
                member_no = DwMain.GetItemString(1, "member_no");
                Printing.ShrlonPrintDeptReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }

        private void JspopupLoanemerReport(bool isJsPrint, int printMode)
        {
            reqdoc_no = DwMain.GetItemString(1, "loanrequest_docno");
            member_no = DwMain.GetItemString(1, "member_no");
            try
            {
                Printing.ShrlonPrintEmerReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }


        private void JsrunProcessAgreeLoan()
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
                rid = "LNNORM_DAILY18";
            }
            catch { }

            String doc_no = DwMain.GetItemString(1, "loanrequest_docno");

            String coop_id;
            try { coop_id = DwMain.GetItemString(1, "coop_id"); }
            catch { coop_id = state.SsCoopId; }
            if (x == 2)
            {
                doc_no = reqdoc_no;
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
            Session["AgreeLoan"] = pdf;
            // PopupReport();

        }

        private void JsrunProcessCollReport()
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
                rid = "LNNORM_DAILY17";
            }
            catch { }

            String doc_no = DwMain.GetItemString(1, "loanrequest_docno");

            String coop_id;
            try { coop_id = DwMain.GetItemString(1, "coop_id"); }
            catch { coop_id = state.SsCoopId; }
            if (x == 2)
            {
                doc_no = reqdoc_no;
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
            Session["Coll"] = pdf;
            // PopupReport();

        }

        private void JsrunProcessAgreeColl()
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
                rid = "LNNORM_DAILY16";
            }
            catch { }

            String doc_no = DwMain.GetItemString(1, "loanrequest_docno");

            String coop_id;
            try { coop_id = DwMain.GetItemString(1, "coop_id"); }
            catch { coop_id = state.SsCoopId; }
            if (x == 2)
            {
                doc_no = reqdoc_no;
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
            Session["AgreeColl"] = pdf;
            // PopupReport();

        }

        private void JsrunProcessLoan()
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
                rid = "LNNORM_DAILY15";
            }
            catch { }

            String doc_no = DwMain.GetItemString(1, "loanrequest_docno");

            String coop_id;
            try { coop_id = DwMain.GetItemString(1, "coop_id"); }
            catch { coop_id = state.SsCoopId; }
            if (x == 2)
            {
                doc_no = reqdoc_no;
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
            Session["Loan"] = pdf;
            // PopupReport();

        }

        private void jJsrunProcessDeptReport()
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
                rid = "LNNORM_DAILY19";
            }
            catch { }

            String doc_no = DwMain.GetItemString(1, "loanrequest_docno");

            String coop_id;
            try { coop_id = DwMain.GetItemString(1, "coop_id"); }
            catch { coop_id = state.SsCoopId; }
            if (x == 2)
            {
                doc_no = reqdoc_no;
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
            Session["dept"] = pdf;
            // PopupReport();

        }

        private void JspopupReportslipfin()
        {
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

            String doc_no = DwMain.GetItemString(1, "loanrequest_docno");

            String coop_id;
            try { coop_id = DwMain.GetItemString(1, "coop_id"); }
            catch { coop_id = state.SsCoopId; }
            if (x == 2)
            {
                doc_no = reqdoc_no;
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
            //PopupReport();


        }






        private void Calculateitempayamt()
        {
            int loanAllRow = DwOperateLoan.RowCount;

            Decimal totalamt = 0;

            Decimal dwmain_bfshrcontbalamt = DwMain.GetItemDecimal(1, "bfshrcont_balamt");

            //ยอดโอนชำระ
            Decimal payoutclramt = 0;



            for (int i = 1; i <= loanAllRow; i++)
            {
                Decimal interest_period = DwOperateLoan.GetItemDecimal(i, "interest_period");
                Decimal bfintarr_amt = DwOperateLoan.GetItemDecimal(i, "bfintarr_amt");
                Decimal sum_interest_bfintarr = interest_period + bfintarr_amt;
                Decimal interest_payamt = DwOperateLoan.GetItemDecimal(i, "interest_payamt");
                Decimal itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");

                //ต้นเงิน bfshrcont_balamt
                Decimal dwloan_bfshrcontbalamt = DwOperateLoan.GetItemDecimal(i, "bfshrcont_balamt");
                Decimal operateflag = DwOperateLoan.GetItemDecimal(i, "operate_flag");
                if (itempayamt > dwloan_bfshrcontbalamt)
                {
                    Response.Write("<script>alert('ยอดชำระมากกว่า ต้นเงิน');</script>");

                }
                else
                {
                    if (itempayamt != 0)
                    {
                        if (operateflag == 1)
                        {
                            if (itempayamt < sum_interest_bfintarr)
                            {
                                DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr - itempayamt);

                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", 0);
                            }
                            else if (itempayamt > sum_interest_bfintarr)
                            {
                                DwOperateLoan.SetItemDecimal(i, "interest_payamt", sum_interest_bfintarr);

                                DwOperateLoan.SetItemDecimal(i, "principal_payamt", itempayamt - sum_interest_bfintarr);
                            }

                        }
                    }


                    //ใช้คำนวณ ยอด ต้นคงเหลือ
                    Decimal principalpayamt = DwOperateLoan.GetItemDecimal(i, "principal_payamt");
                    DwOperateLoan.SetItemDecimal(i, "item_balance", dwloan_bfshrcontbalamt - principalpayamt);

                    //หายอดรวมที่ต้องชำระ

                    itempayamt = DwOperateLoan.GetItemDecimal(i, "item_payamt");
                    totalamt += itempayamt;
                    payoutclramt += principalpayamt + interest_payamt;
                }
                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดโอนชำระ payoutnet_amt
                DwMain.SetItemDecimal(1, "payoutclr_amt", payoutclramt);
                Decimal payout_amt = DwMain.GetItemDecimal(1, "payout_amt");
                //Set ค่า TotalAmt ไปที่ DwMain ช่อง ยอดหุ้นชำระ payoutnet_amt
                DwMain.SetItemDecimal(1, "payoutnet_amt", payout_amt - payoutclramt);
            }
        }
        private void JsGetNewLoan()
        {
            String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            String loan_no = slService.of_gennewcontractno(state.SsWsPass, state.SsCoopId, shrlontype_code);
            DwMain.SetItemString(1, "loancontract_no", loan_no);
        }
        public void WebDialogLoadEnd()
        {
            //Retrive DDDW
            try
            {
                //Clear ค่าใน DDDW
                DwMain.SetItemString(1, "tofrom_accid_1", "");
                //แสดงค่าใน tofrom_accid_1
                DwUtil.RetrieveDDDW(DwMain, "tofrom_accid_1", "sl_loan_receive.pbl", null);
                DataWindowChild dc = DwMain.GetChild("tofrom_accid_1");
                String moneyType = DwUtil.GetString(DwMain, 1, "moneytype_code_1", "");
                //ตรวจสอบค่าว่าง
                if (!string.IsNullOrEmpty(moneyType))
                {
                    // กำหนด filter
                    dc.SetFilter("moneytype_code = '" + moneyType + "'");
                    dc.Filter();
                }
                //แสดงข้อมูลเฉพาะ ของ โอนธนาคาร และเช็คธนาคาร
                if ((moneyType == "CBT") || (moneyType == "CHQ"))
                {

                    DwMain.Modify("expense_bank_t.visible =1");
                    DwMain.Modify("expense_bank.visible =1");
                    DwMain.Modify("expense_branch_t.visible =1");
                    DwMain.Modify("expense_branch.visible =1");
                    DwMain.Modify("expense_accid_t.visible =1");
                    DwMain.Modify("expense_accid.visible =1");
                }
                else
                {
                    //ไม่แสดงข้อมูล
                    DwMain.Modify("expense_bank_t.visible =0");
                    DwMain.Modify("expense_bank.visible =0");
                    DwMain.Modify("expense_branch_t.visible =0");
                    DwMain.Modify("expense_branch.visible =0");
                    DwMain.Modify("expense_accid_t.visible =0");
                    DwMain.Modify("expense_accid.visible =0");

                    // เฉพาะเงินสดกำหนดค่าให้ได้เลย
                    if (moneyType == "CSH")
                    {
                        DwMain.SetItemString(1, "tofrom_accid_1", "11100001");
                    }
                }
                DwUtil.RetrieveDDDW(DwMain, "shrlontype_code_1", "sl_loan_receive.pbl", null);
            }
            catch { }
            DwMain.SaveDataCache();

        }

        #endregion

        private void InitDataWindow()
        {

            String memberNo = "";
            String memcoopid;
            try { memcoopid = DwMain.GetItemString(1, "memcoop_id"); }
            catch { memcoopid = state.SsCoopControl; }
            String as_coopid = state.SsCoopId;



            str_slippayout sSlipPayOut = new str_slippayout();
            sSlipPayOut.member_no = Request["member_no"].ToString();
            int result = slService.of_getmemblnrcv(state.SsWsPass, memcoopid, sSlipPayOut.member_no, ref as_initfrom, ref as_coopid, ref as_reqcontno, ref as_xmllnrcv);
            if (result == 1)
            {
                dw_list.Visible = false;

                sSlipPayOut.coop_id = state.SsCoopId;
                sSlipPayOut.contcoop_id = state.SsCoopControl;
                sSlipPayOut.entry_id = state.SsUsername;
                sSlipPayOut.operate_date = state.SsWorkDate;
                sSlipPayOut.loancontract_no = as_reqcontno;
                sSlipPayOut.member_no = memberNo;
                sSlipPayOut.slip_date = state.SsWorkDate;
                sSlipPayOut.initfrom_type = as_initfrom;
                HfFormtype.Value = as_initfrom;
                try
                {
                    slService.of_initlnrcv(state.SsWsPass, ref sSlipPayOut);
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                DwMain.Reset();
                DwMain.ImportString(sSlipPayOut.xml_sliphead, Sybase.DataWindow.FileSaveAsType.Xml);



                DwUtil.RetrieveDDDW(DwMain, "expense_bank", "sl_loansrv_slip_all_cen.pbl", null);
                DwUtil.RetrieveDDDW(DwMain, "expense_branch", "sl_loansrv_slip_all_cen.pbl", null);

                DwUtil.DeleteLastRow(DwMain);
                tDwMain.Eng2ThaiAllRow();

                try
                {
                    DwOperateLoan.Reset();
                    DwOperateLoan.ImportString(sSlipPayOut.xml_slipcutlon, Sybase.DataWindow.FileSaveAsType.Xml);

                }
                catch (Exception ex) { String ext = ex.ToString(); }
                try
                {
                    DwOperateEtc.Reset();
                    DwOperateEtc.ImportString(sSlipPayOut.xml_slipcutetc, Sybase.DataWindow.FileSaveAsType.Xml);
                    DwUtil.RetrieveDDDW(DwOperateEtc, "slipitemtype_code", "sl_loansrv_slip_all_cen.pbl", null);
                }
                catch (Exception ex) { String ext = ex.ToString(); }
                InitLnRcvReCalInt();
            }
            else if (result == 2)
            {
                dw_list.Visible = true;
                dw_list.Reset();
                TextBox2.Text = as_xmllnrcv;
                try
                {
                    dw_list.ImportString(as_xmllnrcv, Sybase.DataWindow.FileSaveAsType.Xml);
                }
                catch { DwUtil.ImportData(as_xmllnrcv, dw_list, null, Sybase.DataWindow.FileSaveAsType.Xml); }
            }

            if (HfFormtype.Value == "REQ")
            {
                Button1.Visible = true;
                TextBox1.Visible = true;
                String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
                String loan_no = slService.of_getnextcontractno(state.SsWsPass, state.SsCoopControl, shrlontype_code);
                TextBox1.Text = loan_no;
            }
            else
            {
                Button1.Visible = false;
                TextBox1.Visible = false;
            }

            String loantype_code = DwMain.GetItemString(1, "shrlontype_code");
            //การแสดงลิงปริ้นสัญญา
            if (loantype_code == "10" || loantype_code == "11" || loantype_code == "12")
            {

                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = false;
                LinkButton4.Visible = false;
                LinkButton5.Visible = false;
                LinkButton6.Visible = true;
            }
            else if (loantype_code == "20" || loantype_code == "21" || loantype_code == "22")
            {
                LinkButton5.Visible = true;
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = true;
                LinkButton4.Visible = false;
                LinkButton6.Visible = false;
            }
            else if (loantype_code == "23" || loantype_code == "26")
            {
                LinkButton1.Visible = true;
                LinkButton2.Visible = true;
                LinkButton3.Visible = true;
                LinkButton4.Visible = true;
                LinkButton5.Visible = false;
                LinkButton6.Visible = false;

            }

        }

        private void InitLnRcvReCalInt()
        {
            str_slippayout strPayOut = new str_slippayout();
            strPayOut.coop_id = state.SsCoopId;
            strPayOut.entry_id = state.SsUsername;
            strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
            try { strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no"); }
            catch { strPayOut.loancontract_no = loanContract_No; }

            strPayOut.member_no = DwMain.GetItemString(1, "member_no"); ;
            strPayOut.slip_date = state.SsWorkDate;
            strPayOut.initfrom_type = HfFormtype.Value;
            String dwMainXML = "";
            String dwLoanXML = "";
            String dwEtcXML = "";

            dwMainXML = DwMain.Describe("DataWindow.Data.XML");
            dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML");
            try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
            catch { dwEtcXML = ""; }

            strPayOut.xml_sliphead = dwMainXML;
            strPayOut.xml_slipcutlon = dwLoanXML;
            strPayOut.xml_slipcutetc = dwEtcXML;


            slService.of_initlnrcv_recalint(state.SsWsPass, ref strPayOut);

            try
            {
                DwMain.Reset();
                DwMain.ImportString(strPayOut.xml_sliphead, FileSaveAsType.Xml);
                if (DwMain.RowCount > 1)
                {
                    DwMain.DeleteRow(DwMain.RowCount);
                }
            }
            catch { DwMain.Reset(); }
            try
            {
                DwOperateLoan.Reset();
                DwOperateLoan.ImportString(strPayOut.xml_slipcutlon, FileSaveAsType.Xml);
            }
            catch { DwOperateLoan.Reset(); }
            try
            {
                DwOperateEtc.Reset();
                DwOperateEtc.ImportString(strPayOut.xml_slipcutetc, FileSaveAsType.Xml);
            }
            catch { DwOperateEtc.Reset(); }
        }

        private void SaveSlipLnRcv()
        {
            DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
            DateTime today = state.SsWorkDate;
            Decimal payoutclr_amt = 0;
            if (today > operate_date)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("วันที่จ่ายเงินกู้น้อยกว่าวันปัจจุบัน");
            }
            else
            {
                String memno = DwMain.GetItemString(1, "member_no");
                try
                {
                    payoutclr_amt = DwMain.GetItemDecimal(1, "payoutclr_amt");
                }
                catch { payoutclr_amt = 0; }
                if (payoutclr_amt > 0) { CheckBox1.Checked = true; } else { CheckBox1.Checked = false; }
                str_slippayout strPayOut = new str_slippayout();
                strPayOut.coop_id = state.SsCoopId;
                strPayOut.contcoop_id = state.SsCoopControl;
                strPayOut.entry_id = state.SsUsername;
                strPayOut.operate_date = DwMain.GetItemDateTime(1, "operate_date");
                try
                {
                    strPayOut.loancontract_no = DwMain.GetItemString(1, "loancontract_no");
                }
                catch { strPayOut.loancontract_no = ""; }
                strPayOut.member_no = memno;
                strPayOut.slip_date = state.SsWorkDate;
                strPayOut.initfrom_type = HfFormtype.Value;

                String dwMainXML = "";
                String dwLoanXML = "";
                String dwEtcXML = "";

                dwMainXML = DwMain.Describe("DataWindow.Data.XML");
                try { dwLoanXML = DwOperateLoan.Describe("DataWindow.Data.XML"); }
                catch { dwLoanXML = ""; }
                try { dwEtcXML = DwOperateEtc.Describe("DataWindow.Data.XML"); }
                catch { dwEtcXML = ""; }

                strPayOut.xml_sliphead = dwMainXML;
                strPayOut.xml_slipcutlon = dwLoanXML;
                strPayOut.xml_slipcutetc = dwEtcXML;

                try
                {
                    int result = slService.of_saveord_lnrcv(state.SsWsPass, ref strPayOut);
                    reqdoc_no = strPayOut.payoutorder_no;
                    string as_xml = "";
                    if (result == 1)
                    {

                        try
                        {
                            fromset = state.SsPrinterSet;

                        }
                        catch (Exception ex)
                        {
                            fromset = "216";

                        }

                        if (CheckBox1.Checked == true)
                        {
                            if (xmlconfig.LnReceivePrintMode == 0)
                            {
                                //string re = wcf.NShrlon.of_printreceipt(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
                                //if (re == "1") { LtServerMessage.Text = WebUtil.CompleteMessage("พิมพ์ใบเสร็จ  " + member_no + "  เรียบร้อย "); }
                            }
                            else
                            {
                                JsPrintReceive(state.SsCoopId, reqdoc_no, xmlconfig.LnReceivePrintMode);
                            }
                        }

                    }
                    HdIsPostBack.Value = "false";
                    //Response.Write("<script>alert('บันทึกสำเร็จ');</script>");
                    //if (nextIndex != allIndex)
                    //{
                    //    this.InitDataWindow();
                    //}
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    // Response.Write("<script>alert('ไม่สามารถบันทึกเลขทะเบียน " + memno + " ได้');</script>"); 
                }


            }

        }
        private void Refresh()
        {

        }
        private void JsPrintReceive(string coopId, string reqdoc_no, int printMode)
        {
            Printing.ShrlonPrintSlipPayout(this, coopId, reqdoc_no, printMode);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (xmlconfig.ShrlonPrintMode == 0)
            {
                JspopupCollReport();
            }
            else
            {
                JspopupCollReport(true);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (xmlconfig.ShrlonPrintMode == 0)
            {
                JspopupAgreeCollReport();
            }
            else
            {
                JspopupAgreeCollReport(true, xmlconfig.ShrlonPrintMode);
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (xmlconfig.ShrlonPrintMode == 0)
            {
                JspopupLoanReport();
            }
            else
            {
                JspopupLoanReport(true, xmlconfig.ShrlonPrintMode);
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (xmlconfig.ShrlonPrintMode == 0)
            {
                JspopupAgreeLoanReport();
            }
            else
            {
                JspopupAgreeLoanReport(true, xmlconfig.ShrlonPrintMode);
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if (xmlconfig.ShrlonPrintMode == 0)
            {
                JspopupDeptReport();
            }
            else
            {
                JspopupDeptReport(true, xmlconfig.ShrlonPrintMode);
            }
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            if (xmlconfig.ShrlonPrintMode == 0)
            {
                JspopupLoanemerReport();
            }
            else
            {
                JspopupLoanemerReport(true, xmlconfig.ShrlonPrintMode);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //    String shrlontype_code = DwMain.GetItemString(1, "shrlontype_code");
            //    String loan_no = slService.GetNextContractNo(state.SsWsPass, shrlontype_code);
            //    TextBox1.Text = loan_no;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
