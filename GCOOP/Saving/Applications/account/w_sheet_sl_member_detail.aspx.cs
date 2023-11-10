using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using System.Threading;

namespace Saving.Applications.account
{
    public partial class w_sheet_sl_member_detail : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        protected String chkTrue;
        protected String chkFalse;
        protected String postNew;
        protected String tabLoancheckbox;
        protected String sslcontno = "";
        protected String postMemberNo;
        protected String CheckCoop;
        protected String CoopSelect;
        String req = "";
        private String pbl = "sl_member_detail.pbl";

        string coop_id = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tabLoancheckbox = WebUtil.JsPostBack(this, "tabLoancheckbox");
            sslcontno = WebUtil.JsPostBack(this, "sslcontno");
            chkTrue = WebUtil.JsPostBack(this, "chkTrue");
            chkFalse = WebUtil.JsPostBack(this, "chkFalse");
            postNew = WebUtil.JsPostBack(this, "postNew");
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
            popupReport = WebUtil.JsPostBack(this, "popupReport");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");

        }

        public void WebSheetLoadBegin()
        {
            Hloancheck.Value = "true";
            //try
            //{
            //    sqlca = new DwTrans();
            //    sqlca.Connect();
            //    dw_main.SetTransaction(sqlca);
            //    dw_data.SetTransaction(sqlca);
            //    dw_bank.SetTransaction(sqlca);
            //    dw_data2.SetTransaction(sqlca);
            //    dw_coll.SetTransaction(sqlca);
            //    dw_coll2.SetTransaction(sqlca);
            //    dw_data3.SetTransaction(sqlca);
            //    dw_data4.SetTransaction(sqlca);
            //    dw_data5.SetTransaction(sqlca);
            //    dw_data_1.SetTransaction(sqlca);
            //    dw_data_2.SetTransaction(sqlca);
            //    dw_data_3.SetTransaction(sqlca);
            //    dw_data_4.SetTransaction(sqlca);
            //    dw_remarkstat.SetTransaction(sqlca);
            //}
            //catch { LtServerMessage.Text = WebUtil.ErrorMessage("[Cannot Connect Database...]"); }
            if (!IsPostBack)
            {

                dw_main.InsertRow(0);
                dw_data.InsertRow(0);
                dw_bank.InsertRow(0);
                dw_data2.InsertRow(0);
                dw_coll.InsertRow(0);
                dw_coll2.InsertRow(0);
                dw_data3.InsertRow(0);
                dw_data4.InsertRow(0);
                dw_data5.InsertRow(0);
                dw_data8.InsertRow(0);
                dw_data_1.InsertRow(0);
                dw_data_2.InsertRow(0);
                dw_data_3.InsertRow(0);
                dw_data_4.InsertRow(0);
                dw_remarkstat.InsertRow(0);
                CbCheckLoan.Checked = true;

                DwUtil.RetrieveDDDW(dw_data8, "gain_relation", "sl_member_detail.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "dddw_coop", "sl_member_detail.pbl", state.SsCoopControl);

            }
            else
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_data);
                this.RestoreContextDw(dw_bank);
                this.RestoreContextDw(dw_data2);
                this.RestoreContextDw(dw_coll);
                this.RestoreContextDw(dw_coll2);
                this.RestoreContextDw(dw_data3);
                this.RestoreContextDw(dw_data4);
                this.RestoreContextDw(dw_data5);
                this.RestoreContextDw(dw_data8);
                this.RestoreContextDw(dw_data_1);
                this.RestoreContextDw(dw_data_2);
                this.RestoreContextDw(dw_data_3);
                this.RestoreContextDw(dw_data_4);
                this.RestoreContextDw(dw_remarkstat);
                Decimal i = 0;
                try
                {
                    i = dw_main.GetItemDecimal(1, "check_coop");
                }
                catch
                { }
                if (i == 1)
                {
                    coop_id = dw_main.GetItemString(1, "dddw_coop");
                }
                else
                {
                    coop_id = state.SsCoopControl;
                }
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            HfOpenLnContDlg.Value = "false";
            if (eventArg == "sslcontno")
            {
                Session["sslncontno"] = HfLncontno.Value;
                HfOpenLnContDlg.Value = "true";
            }
            else if (eventArg == "chkTrue")
            {
                JschkTrue();
            }
            else if (eventArg == "chkFalse")
            {
                JschkFalse();
            }
            else if (eventArg == "postNew")
            {
                JspostNew();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "CheckCoop")
            {
                checkCoop();
            }

            else if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }

        }



        private void checkCoop()
        {
            decimal i = 0;
            try
            {
                i = dw_main.GetItemDecimal(1, "check_coop");
            }
            catch
            { }
            JspostNew();
            if (i == 1)
            {
                dw_main.SetItemDecimal(1, "check_coop", i);
            }

        }

        private void JspostNew()
        {
            dw_main.Reset();
            dw_data.Reset();
            dw_bank.Reset();
            dw_data2.Reset();
            dw_coll.Reset();
            dw_coll2.Reset();
            dw_data3.Reset();
            dw_data4.Reset();
            dw_data5.Reset();
            dw_data8.Reset();
            dw_data_1.Reset();
            dw_data_2.Reset();
            dw_data_3.Reset();
            dw_data_4.Reset();
            dw_remarkstat.Reset();

            dw_main.InsertRow(0);
            dw_data.InsertRow(0);
            dw_bank.InsertRow(0);
            dw_data2.InsertRow(0);
            dw_coll.InsertRow(0);
            dw_coll2.InsertRow(0);
            dw_data3.InsertRow(0);
            dw_data4.InsertRow(0);
            dw_data5.InsertRow(0);
            dw_data8.InsertRow(0);
            dw_data_1.InsertRow(0);
            dw_data_2.InsertRow(0);
            dw_data_3.InsertRow(0);
            dw_data_4.InsertRow(0);
            dw_remarkstat.InsertRow(0);
            HdIsPostBack.Value = "false";

            DwUtil.RetrieveDDDW(dw_data8, "gain_relation", "sl_member_detail.pbl", null);
            DwUtil.RetrieveDDDW(dw_main, "dddw_coop", "sl_member_detail.pbl", state.SsCoopControl);
        }

        private void JschkFalse()
        {
            try
            {
                Hloancheck.Value = "false";
                dw_data_2.SetFilter("contract_status  < 0");
                dw_data_2.Filter();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        private void JschkTrue()
        {
            try
            {
                Hloancheck.Value = "true";
                dw_data_2.SetFilter("contract_status  > 0");
                dw_data_2.Filter();

            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        public void SaveWebSheet()
        {
            //throw new NotImplementedException();
        }

        public void WebSheetLoadEnd()
        {
            //sqlca.Disconnect();
            try
            {
                bool isChecked = CbCheckLoan.Checked;
                String memberNo = dw_main.GetItemString(1, "member_no").Trim(); //WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
                if (!string.IsNullOrEmpty(memberNo))
                {
                    memberNo = WebUtil.MemberNoFormat(memberNo);
                    DwUtil.RetrieveDataWindow(dw_data_2, pbl, null, coop_id, memberNo);
                    if (dw_data_2.RowCount > 0)
                    {
                        if (isChecked)
                        {
                            dw_data_2.SetFilter("contract_status  > 0");
                            dw_data_2.Filter();
                        }
                        else
                        {
                            dw_data_2.SetFilter("contract_status  < 0");
                            dw_data_2.Filter();
                        }
                    }
                }
            }
            catch { }
            dw_main.SaveDataCache();
            dw_data.SaveDataCache();
            dw_bank.SaveDataCache();
            dw_data2.SaveDataCache();
            dw_coll.SaveDataCache();
            dw_coll2.SaveDataCache();
            dw_data3.SaveDataCache();
            dw_data4.SaveDataCache();
            dw_data5.SaveDataCache();
            dw_data8.SaveDataCache();
            dw_data_1.SaveDataCache();
            dw_data_2.SaveDataCache();
            dw_data_3.SaveDataCache();
            dw_data_4.SaveDataCache();
            dw_remarkstat.SaveDataCache();
        }

        #endregion

        private void RunProcess()
        {
            app = state.SsApplication;
            try
            {
                //gid = Request["gid"].ToString();
                gid = "shrlonchk";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "SHRLONCHK001";
            }
            catch { }



            String start_membno = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            if (start_membno == null || start_membno == "")
            {
                return;
            }
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);

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
            Session["pdf"] = pdf;
            //PopupReport();
        }

        private void JsPostMemberNo()
        {
            CbCheckLoan.Checked = true;
            String memberNo = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            DwUtil.RetrieveDataWindow(dw_main, pbl, null, coop_id, memberNo);
            if (dw_main.RowCount > 0)
            {
                //RunProcess();
                DwUtil.RetrieveDataWindow(dw_main, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_bank, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data2, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_coll, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_coll2, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data3, pbl, null, memberNo);
                DwUtil.RetrieveDataWindow(dw_data4, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data5, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data8, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_1, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_2, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_3, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_4, pbl, null, memberNo);
                DwUtil.RetrieveDataWindow(dw_remarkstat, pbl, null, coop_id, memberNo);

                DateTime ldtm_birth = new DateTime();
                DateTime ldtm_retry = new DateTime();

                try
                {
                    ldtm_birth = dw_data.GetItemDate(1, "birth_date");
                }
                catch { }
                try
                {
                    ///<หาวันที่เกษียณ>

                    ldtm_retry = wcf.NShrlon.of_calretrydate(state.SsWsPass,ldtm_birth);

                }
                catch { }
                try
                {
                    dw_data.SetItemDateTime(1, "retry_date", ldtm_retry);
                }
                catch { dw_data.SetItemDateTime(1, "retry_date", DateTime.Now); }

            }
            else
            {
                this.JspostNew();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
            }
        }

        public void PopupReport()
        {
            RunProcess();
            // Thread.Sleep(5000);
            Thread.Sleep(4500);
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }
    }
}
