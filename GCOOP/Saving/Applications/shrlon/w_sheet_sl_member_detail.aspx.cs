using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using System.Threading;
using DataLibrary;
using System.IO;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_member_detail : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReportloan;
        protected String popupReportshr;
        protected String chkTrue;
        protected String chkFalse;
        protected String postNew;
        protected String tabLoancheckbox;
        protected String sslcontno = "";
        protected String postMemberNo;
        protected String CheckCoop;
        protected String CoopSelect;
        protected String postSalaryId;
        String req = "";
        private String pbl = "sl_member_detail.pbl";
        private n_shrlonClient shrlonService; 
        string coop_id = "";
        public String outputProcess = "";

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
            popupReportshr = WebUtil.JsPostBack(this, "popupReportshr");
            popupReportloan = WebUtil.JsPostBack(this, "popupReportloan");
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            CheckCoop = WebUtil.JsPostBack(this, "CheckCoop");
            postSalaryId = WebUtil.JsPostBack(this, "postSalaryId");
        }

        public void WebSheetLoadBegin()
        {
            Hloancheck.Value = "true";
            try
            {
                shrlonService = wcf.NShrlon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            this.ConnectSQLCA();
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
            //}
            //catch { LtServerMessage.Text = WebUtil.ErrorMessage("[Cannot Connect Database...]"); }
            if (!IsPostBack)
            {

                dw_main.InsertRow(0);
              //  dw_trnhistory.InsertRow(0);
                dw_data.InsertRow(0);
                dw_bank.InsertRow(0);
                dw_bank2.InsertRow(0);
                //dw_bank3.InsertRow(0);
                //dw_bank4.InsertRow(0);
                dw_data2.InsertRow(0);
                dw_coll.InsertRow(0);
                dw_coll2.InsertRow(0);
                dw_data3.InsertRow(0);
                dw_data4.InsertRow(0);
                dw_data5.InsertRow(0);
                dw_data8.InsertRow(0);
                dw_data9.InsertRow(0);
                dw_data10.InsertRow(0);
                dw_data_1.InsertRow(0);
                dw_data_2.InsertRow(0);
                dw_data_3.InsertRow(0);
                dw_data_4.InsertRow(0);
                CbCheckLoan.Checked = true;

                DwUtil.RetrieveDDDW(dw_data8, "gain_relation", "sl_member_detail.pbl", null);
                DwUtil.RetrieveDDDW(dw_main, "dddw_coop", "sl_member_detail.pbl", state.SsCoopControl);

            }
            else
            {
                this.RestoreContextDw(dw_main);
            //    this.RestoreContextDw(dw_trnhistory);
                this.RestoreContextDw(dw_data);
                this.RestoreContextDw(dw_bank);
                this.RestoreContextDw(dw_bank2);
                //this.RestoreContextDw(dw_bank3);
                //this.RestoreContextDw(dw_bank4);
                this.RestoreContextDw(dw_data2);
                this.RestoreContextDw(dw_coll);
                this.RestoreContextDw(dw_coll2);
                this.RestoreContextDw(dw_data3);
                this.RestoreContextDw(dw_data4);
                this.RestoreContextDw(dw_data5);
                this.RestoreContextDw(dw_data8);
                this.RestoreContextDw(dw_data9);
                this.RestoreContextDw(dw_data10);
                this.RestoreContextDw(dw_data_1);
                this.RestoreContextDw(dw_data_2);
                this.RestoreContextDw(dw_data_3);
                this.RestoreContextDw(dw_data_4);
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
            else if (eventArg == "popupReportloan")
            {
                PopupReportloan();
            }
            else if (eventArg == "popupReportshr")
            {
                PopupReportshr();
            }
            else if (eventArg == "CheckCoop")
            {
                checkCoop();
            }

            else if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }
            else if (eventArg == "postSalaryId")
            {
                JsPostSalaryId();
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
         //   dw_trnhistory.Reset();
            dw_data.Reset();
            dw_bank.Reset();
            dw_bank2.Reset();
            //dw_bank3.Reset();
            //dw_bank4.Reset();
            dw_data2.Reset();
            dw_coll.Reset();
            dw_coll2.Reset();
            dw_data3.Reset();
            dw_data4.Reset();
            dw_data5.Reset();
            dw_data8.Reset();
            dw_data9.Reset();
            dw_data10.Reset();
            dw_data_1.Reset();
            dw_data_2.Reset();
            dw_data_3.Reset();
            dw_data_4.Reset();

            dw_main.InsertRow(0);
        //    dw_trnhistory.InsertRow(0);
            dw_data.InsertRow(0);
            dw_bank.InsertRow(0);
            dw_bank2.InsertRow(0);
            //dw_bank3.InsertRow(0);
            //dw_bank4.InsertRow(0);
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
            ShowImage_Member();

            dw_main.SaveDataCache();
         //   dw_trnhistory.SaveDataCache();
            dw_data.SaveDataCache();
            dw_bank.SaveDataCache();
            dw_bank2.SaveDataCache();
            //dw_bank3.SaveDataCache();
            //dw_bank4.SaveDataCache();
            dw_data2.SaveDataCache();
            dw_coll.SaveDataCache();
            dw_coll2.SaveDataCache();
            dw_data3.SaveDataCache();
            dw_data4.SaveDataCache();
            dw_data5.SaveDataCache();
            dw_data8.SaveDataCache();
            dw_data9.SaveDataCache();
            dw_data10.SaveDataCache();
            dw_data_1.SaveDataCache();
            dw_data_2.SaveDataCache();
            dw_data_3.SaveDataCache();
            dw_data_4.SaveDataCache();
        }

        #endregion

        private void RunProcessshr()
        {
            String memberNo = dw_main.GetItemString(1, "member_no").Trim();
            shrlonService.of_genintestimate(state.SsWsPass, memberNo, state.SsWorkDate);

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
                rid = "SHRLONCHK202";
            }
            catch { }


            string coop_id = state.SsCoopControl;
            String start_membno = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            if (start_membno == null || start_membno == "")
            {
                return;
            }
            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            lnv_helper.AddArgument(start_membno, ArgumentType.String);
            //  lnv_helper.AddArgument(start_membno, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF

            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {

                String criteriaXML = lnv_helper.PopArgumentsXML();
                //String printer = dw_main.GetItemString(1, "printer");
                String printer    = "PDF";
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer );

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

        private void RunProcessloan()
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
                rid = "SHRLONCHK303";
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
            lnv_helper.AddArgument(HfLncontno.Value, ArgumentType.String);

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
            // PopupReport();
        }
        private void JsPostMemberNo()
        {
            string recv_period = "";
            CbCheckLoan.Checked = true;
            String memberNo = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            //ying
            string sql = "select max(recv_period) as recv_period from kptempreceive where member_no='" + memberNo + "'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                recv_period = dt.GetString("recv_period");
            }

            DwUtil.RetrieveDataWindow(dw_main, pbl, null, coop_id, memberNo);

            if (dw_main.RowCount > 0)
            {
                //RunProcess();
                DwUtil.RetrieveDataWindow(dw_main, pbl, null, coop_id, memberNo);
         //       DwUtil.RetrieveDataWindow(dw_trnhistory, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_bank, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_bank2, pbl, null, coop_id, memberNo);
                //DwUtil.RetrieveDataWindow(dw_bank3, pbl, null, coop_id, memberNo);
                //DwUtil.RetrieveDataWindow(dw_bank4, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data2, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_coll, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_coll2, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data3, pbl, null, memberNo, recv_period);
                DwUtil.RetrieveDataWindow(dw_data4, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data5, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data8, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data9, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data10, pbl, null, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_1, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_2, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_3, pbl, null, coop_id, memberNo);
                DwUtil.RetrieveDataWindow(dw_data_4, pbl, null, memberNo);


                DateTime ldtm_birth = new DateTime();
                DateTime ldtm_retry = new DateTime();
                string deptaccount_no;
                try
                {
                    // li_cramationstatus = dt.GetInt32("cremation_status");
                    //เลขฌาปนกิจสงเคราะห์
                    string sqldapt = @"select deptaccount_no from wcdeptmaster where member_no ='" + memberNo + @"' and wftype_code = '01' "; //เพิ่มเงื่อนไข คือ and wftype_code = '01' โดยดึงมาเฉพาะประเภทของสมาชิกเท่านั้น
                    Sdt dtdapt = WebUtil.QuerySdt(sqldapt);
                    if (dtdapt.Next())
                    {
                        deptaccount_no = dtdapt.GetString("deptaccount_no");
                        dw_data.SetItemString(1, "deptaccount_no", deptaccount_no);
                    }
                }
                catch { deptaccount_no = ""; }
            }
            else
            {
                this.JspostNew();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
            }
        }
        private void JsPostSalaryId()
        {
            CbCheckLoan.Checked = true;
            String memberNo = "";
            String salary_id = dw_main.GetItemString(1, "salary_id").Trim();
            //ดึงเลขสมาชิกจากเลขพนักงาน
            string sqlMemb = @"select member_no from mbmembmaster where salary_id like '" + salary_id + @"%' and member_status = 1";
            Sdt dtMemb = WebUtil.QuerySdt(sqlMemb);
            if (dtMemb.Next())
            {
                memberNo = dtMemb.GetString("member_no");
                dw_main.SetItemString(1, "member_no", memberNo);
                JsPostMemberNo();
            }
            else
            {
                this.JspostNew();
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถดึงข้อมูลเลขสมาชิก " + memberNo);
            }
        }
        public void PopupReportshr()
        {
            RunProcessshr();
            // Thread.Sleep(5000);
            //Thread.Sleep(700);
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            //String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }
        public void PopupReportloan()
        {
            RunProcessloan();
            // Thread.Sleep(5000);
            Thread.Sleep(700);
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }

        public void ShowImage_Member()
        {
            try
            {
                String memberNo = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
                string path_profile = "", path_signature = "" ;
                string sv_path_profile = "",sv_path_signature="";
                if (memberNo != "" && memberNo != "00000000" && memberNo != null)
                {
                    Img_member_profile.Visible = true;
                    sv_path_profile = Server.MapPath("~/ImageMember/profile/profile_" + memberNo + ".jpg");
                    sv_path_signature = Server.MapPath("~/ImageMember/signature/signature_" + memberNo + ".jpg");
                    //path_profile = state.SsUrl + "ImageMember/profile/profile_" + memberNo + ".jpg";
                    //path_signature= state.SsUrl + "ImageMember/signature/signature_" + memberNo + ".jpg";
                    if (File.Exists(sv_path_profile))
                    {
                        Img_member_profile.ImageUrl = "../../ImageMember/profile/profile_" + memberNo + ".jpg";
                    }
                    else
                    {
                        Img_member_profile.ImageUrl = "../../ImageMember/profile/profile_nopic.jpg";
                    }

                    Img_member_signature.Visible = true;
                    if (File.Exists(sv_path_signature))
                    {
                        Img_member_signature.ImageUrl = "../../ImageMember/signature/signature_" + memberNo + ".jpg";
                    }
                    else
                    {
                        Img_member_signature.ImageUrl = "../../ImageMember/signature/signature_nopic.jpg";
                    }
                }
                else
                {
                    Img_member_profile.Visible = false;
                    Img_member_signature.Visible = false;
                }



            }
            catch
            {
                Img_member_profile.Visible = false;
                Img_member_signature.Visible = false;
            }
        }
    }
}
