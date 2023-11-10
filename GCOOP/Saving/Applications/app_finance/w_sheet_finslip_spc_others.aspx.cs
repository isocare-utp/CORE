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
using DataLibrary;
using System.Threading;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_slip_spc_others : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        protected String postSetHeadAndList;
        protected String postInitDet;
        protected String postPrintSlip;
        private DwThDate tDwFromOther;
        private String pbl = "finslip_spc.pbl";
        //*******ประกาศตัวเกี่ยวกับ  Reprot********//
        string reqdoc_no = "";
        String member_no = "";
        String ref_slipno = "";
        string fromset = "";
        string adtm_tdate = "";
        int x = 1;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwFromOther = new DwThDate(DwFromOther, this);
            tDwFromOther.Add("adtm_date", "adtm_tdate");
            postSetHeadAndList = WebUtil.JsPostBack(this, "postSetHeadAndList");
            postInitDet = WebUtil.JsPostBack(this, "postInitDet");
            postPrintSlip = WebUtil.JsPostBack(this, "postPrintSlip");
        }

        public void WebSheetLoadBegin()
        {
            fin = wcf.NFinance;
            com = wcf.NCommon;

            if (!IsPostBack)
            {
                DwFromOther.InsertRow(0);
                DwSlipNet.InsertRow(0);

                try
                {
                    // ประเภทเงินทำรายการ
                    DwUtil.RetrieveDDDW(DwSlipNet, "cash_type", "finslip_spc.pbl", null);

                    // คู่บัญชี
                    DwUtil.RetrieveDDDW(DwSlipDet, "account_id", "finslip_spc.pbl", null);

                    DwUtil.RetrieveDDDW(DwSlipNet, "tofrom_accid", "finslip_spc.pbl", null);
                    DwUtil.RetrieveDDDW(DwFromOther, "member_group_no", "finslip_spc.pbl", null);
                    DwFromOther.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                    DwFromOther.SetItemString(1, "entry_id", state.SsUsername);
                    DwFromOther.SetItemString(1, "coop_id", state.SsCoopId);

                    //DwFromOther.SetItemString(1, "coopbranch_id", state.SsCoopId);
                    //DataWindowChild DcBranchId = DwFromOther.GetChild("coopbranch_id");
                    //DcBranchId.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);

                    tDwFromOther.Eng2ThaiAllRow();
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else
            {
                this.RestoreContextDw(DwFromOther);
                this.RestoreContextDw(DwList);
                this.RestoreContextDw(DwSlipNet);
                this.RestoreContextDw(DwSlipDet);
                this.RestoreContextDw(DwCancelList);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postSetHeadAndList":
                    SetHeadAndList();
                    break;
                case "postInitDet":
                    InitDet();
                    break;
                case "postPrintSlip":
                    PostPrintSlip();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            int foundRow;
            String result, fromSystem = "";
            String ls_main_xml, ls_itemdet_xml, ls_cancel_Xml;
            member_no = DwFromOther.GetItemString(1, "member_no");
            adtm_tdate = DwFromOther.GetItemString(1, "adtm_tdate");
            string as_concoopid = state.SsCoopControl;
            Decimal item_amtnet;
            try
            {
                item_amtnet = DwSlipNet.GetItemDecimal(1, "item_amtnet");

            }
            catch { item_amtnet = 0; }
            //if (item_amtnet == 0)
            //{
            //    LtServerMessage.Text = WebUtil.WarningMessage("ยอดจ่ายเงินเป็น 0");
            //}
            //else
            //{
            try
            {
                try
                {
                    foundRow = DwList.FindRow("select_flag=1", 1, DwList.RowCount);
                    ref_slipno = DwList.GetItemString(foundRow, "ref_slipno");
                    fromSystem = DwList.GetItemString(foundRow, "from_system");
                }
                catch { }

                if (fromSystem == "SHL")
                {
                    Hprintslip.Value = "true";
                }
                else
                {
                    Hprintslip.Value = "false";
                }

                ls_main_xml = DwList.Describe("DataWindow.Data.XML");
                ls_itemdet_xml = DwSlipDet.Describe("DataWindow.Data.XML");
                ls_cancel_Xml = "";
                if (DwCancelList.RowCount > 0)
                {
                    ls_cancel_Xml = DwCancelList.Describe("DataWindow.Data.XML");
                }

                result = fin.of_postotherto_fin(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsApplication, ls_main_xml, ls_itemdet_xml, ls_cancel_Xml);
                Hslipno.Value = result;
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");

                Newpage();
                // เอ แก้ไข เรียก Newpage(); แทน
                //DwFromOther.Reset();
                //DwFromOther.InsertRow(0);
                //DwList.Reset();
                //DwSlipDet.Reset();
                //DwSlipDet.InsertRow(0);
                //DwSlipNet.Reset();
                //DwSlipNet.InsertRow(0);
                //DwCancelList.Reset();
                //DwFromOther.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                //tDwFromOther.Eng2ThaiAllRow();
                
                JspopupReportslipfin();

            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            //}
        }

        public void WebSheetLoadEnd()
        {
            DwFromOther.SaveDataCache();
            DwList.SaveDataCache();
            DwSlipNet.SaveDataCache();
            DwSlipDet.SaveDataCache();
            DwCancelList.SaveDataCache();
        }

        #endregion


        private void InitDet()
        {
            int row;
            Decimal Check;
            String SlipNo, moneytype, accid,slipdet_Xml = "";
            Int32 result;
            row = Convert.ToInt32(HdRow.Value);
            Check = Convert.ToDecimal(HdCheck.Value);

            for (int i = 1; i <= DwList.RowCount; i++)
            {
                DwList.SetItemDecimal(i, "select_flag", 0);
            }
            DwList.SetItemDecimal(row, "select_flag", Check);

            if (Check == 1)
            {
                try
                {
                    SlipNo = DwList.GetItemString(row, "slip_no");
                    result = fin.of_retrievefinslipdet(state.SsWsPass, state.SsCoopId, SlipNo,ref slipdet_Xml);
                    DwSlipDet.Reset();
                    DwSlipDet.ImportString(slipdet_Xml, FileSaveAsType.Xml);

                    DwUtil.RetrieveDDDW(DwSlipDet, "account_id", pbl, state.SsCoopId);
                    //DataWindowChild dcAccid = DwSlipDet.GetChild("account_id");
                    //dcAccid.ImportString(fin.OfGetChildAccid(state.SsWsPass), FileSaveAsType.Xml);

                    DwSlipNet.SetItemString(1, "cash_type", DwList.GetItemString(row, "cash_type"));

                    try
                    {
                        moneytype = DwList.GetItemString(row, "cash_type");
                    }
                    catch { moneytype = "CSH"; }

                    accid = fin.of_defaultaccid(state.SsWsPass, moneytype);
                    DwList.SetItemString(row, "tofrom_accid", accid);

                    DwSlipNet.SetItemString(1, "tofrom_accid", DwList.GetItemString(row, "tofrom_accid"));
                    DwSlipNet.SetItemDecimal(1, "item_amtnet", DwList.GetItemDecimal(row, "item_amtnet"));
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else
            {
                DwSlipDet.Reset();
                DwSlipDet.InsertRow(0);
                DwSlipNet.Reset();
                DwSlipNet.InsertRow(0);
            }
        }

        private void SetHeadAndList()
        {
            Int32 result;
            String as_memb_xml = "", slipmain_xml = "", slipcancel_xml = "";

            try
            {
                DwSlipDet.Reset();
                DwSlipDet.InsertRow(0);
                DwSlipNet.Reset();
                DwSlipNet.InsertRow(0);
                String member_no = WebUtil.MemberNoFormat(DwFromOther.GetItemString(1, "member_no").Trim());// ("00000000" + DwFromOther.GetItemString(1, "member_no").Trim()).PadRight(8);
                DwFromOther.SetItemString(1, "member_no", member_no);

                as_memb_xml = DwFromOther.Describe("DataWindow.Data.XML");
                result = fin.of_init_postotherto_fin(state.SsWsPass, ref as_memb_xml, ref slipmain_xml,ref slipcancel_xml, state.SsApplication);

                DwFromOther.Reset();
                DwFromOther.ImportString(as_memb_xml, FileSaveAsType.Xml);

                DwList.Reset();
                DwCancelList.Reset();
                if (slipmain_xml != "")
                {
                    DwList.Reset();
                    DwList.ImportString(slipmain_xml ,FileSaveAsType.Xml);
                }
                if (slipcancel_xml != "")
                {
                    DwCancelList.Reset();
                    DwCancelList.ImportString(slipcancel_xml, FileSaveAsType.Xml);
                }
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void PostPrintSlip()
        {
            String slipno;
            slipno = Hslipno.Value;
            slipno = slipno.Trim();
            //fin.OfPostPrintSlip(state.SsWsPass, state.SsCoopId, slipno, state.SsPrinterSet);
        }

        private void Newpage()
        {

            DwFromOther.Reset();
            DwFromOther.InsertRow(0);
            DwList.Reset();
            DwSlipDet.Reset();
            DwSlipDet.InsertRow(0);
            DwSlipNet.Reset();
            DwSlipNet.InsertRow(0);
            DwCancelList.Reset();
            //DwFromOther.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
            //tDwFromOther.Eng2ThaiAllRow();
            try
            {
                // ประเภทเงินทำรายการ
                DwUtil.RetrieveDDDW(DwSlipNet, "cash_type", "finslip_spc.pbl", null);

                // คู่บัญชี
                DwUtil.RetrieveDDDW(DwSlipDet, "account_id", "finslip_spc.pbl", null);

                DwUtil.RetrieveDDDW(DwSlipNet, "tofrom_accid", "finslip_spc.pbl", null);
                DwUtil.RetrieveDDDW(DwFromOther, "member_group_no", "finslip_spc.pbl", null);
                DwFromOther.SetItemDateTime(1, "adtm_date", state.SsWorkDate);
                DwFromOther.SetItemString(1, "entry_id", state.SsUsername);
                DwFromOther.SetItemString(1, "coop_id", state.SsCoopId);

                //DwFromOther.SetItemString(1, "coopbranch_id", state.SsCoopId);
                //DataWindowChild DcBranchId = DwFromOther.GetChild("coopbranch_id");
                //DcBranchId.ImportString(fin.GetChildBranch(state.SsWsPass), FileSaveAsType.Xml);

                tDwFromOther.Eng2ThaiAllRow();
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

        private void JspopupReportslipfin()
        {
            JsRunProcessslipfin();
            // Thread.Sleep(5000);
            Thread.Sleep(700);
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


            String entry_year = WebUtil.Right(adtm_tdate, 4);
            int yyyy = Convert.ToInt32(entry_year) - 543;
            String entry_day = WebUtil.Left(adtm_tdate, 4);

            String dd = WebUtil.Left(entry_day, 2);
            String mm = WebUtil.Right(entry_day, 2);
            String entry_tt = dd + "/" + mm + "/" + yyyy.ToString();

            // string sql = @"select loanrequest_docno from slslippayout where  member_no ='" + member_no + @"' and slip_date=to_date('" + entry_tt + "','dd/mm/yyyy') ";
            //Sdt dt = WebUtil.QuerySdt(sql);
            //if (dt.Next())
            //{
            //    int rowCount = dt.GetRowCount();

            //    doc_no = dt.GetString("loanrequest_docno");
            //}
            doc_no = ref_slipno;

            String coop_id;
            coop_id = state.SsCoopId;
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
            //JspopupReportslipfin();

        }
    }
}
