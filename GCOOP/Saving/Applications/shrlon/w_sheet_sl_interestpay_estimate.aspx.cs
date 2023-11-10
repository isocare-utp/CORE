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
//using CoreSavingLibrary.WcfPrint;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_interestpay_estimate : PageWebSheet, WebSheet
    {

        private DwThDate tDwMain;
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String initLnRcv = "";
        protected String initSlipCalInt = "";
        protected String getPeriodCont = "";
        protected String calculateAmt = "";
        protected String downloadPDF = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            initLnRcv = WebUtil.JsPostBack(this, "initLnRcv");
            initSlipCalInt = WebUtil.JsPostBack(this, "initSlipCalInt");
            getPeriodCont = WebUtil.JsPostBack(this, "getPeriodCont");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
            downloadPDF = WebUtil.JsPostBack(this, "downloadPDF");
            //----------------------------------------------------------
            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("operate_date", "operate_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;

                if (IsPostBack)
                {
                    this.RestoreContextDw(dw_main);
                    this.RestoreContextDw(dw_loan);
                }
                else
                {
                    dw_main.InsertRow(0);
                    dw_main.SetItemDate(1, "slip_date", DateTime.Today);
                    dw_main.SetItemDate(1, "operate_date", DateTime.Today);
                    DwUtil.RetrieveDDDW(dw_main, "sliptype_code", "sl_interestpay_estimate.pbl", null);
                    dw_main.SetItemString(1, "entry_id", state.SsUsername);
                    tDwMain.Eng2ThaiAllRow();
                }



            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "initLnRcv")
            {
                InitLnRcv();
            }
            else if (eventArg == "initSlipCalInt")
            {
                this.InitSlipCalInt();
            }
            else if (eventArg == "getPeriodCont")
            {
                //GetPeriodCont();
            }
            else if (eventArg == "calculateAmt")
            {
                this.InitSlipCalInt();
                CalculateAmt();
            }
            else if (eventArg == "downloadPDF")
            {
                this.DownloadPDF();
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            dw_loan.SaveDataCache();
            dw_main.SaveDataCache();
        }

        #endregion

        private void InitDataWindow()
        {
            dw_main.InsertRow(0);
            dw_main.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            dw_main.SetItemString(1, "sliptype_code", "PX");
            dw_main.SetItemString(1, "entry_id", state.SsUsername);

            tDwMain.Eng2ThaiAllRow();
        }

        private void InitSlipCalInt()
        {

            try
            {
                tDwMain.Eng2ThaiAllRow();
                DateTime dt = new DateTime();
                dt = dw_main.GetItemDateTime(1, "operate_date");
                String as_xmlloan = dw_loan.Describe("DataWindow.Data.XML");
                String as_sliptype = dw_main.GetItemString(1, "sliptype_code");
                Int32 result = shrlonService.of_initslippayin_calint(state.SsWsPass,ref as_xmlloan, as_sliptype, dt);
                dw_loan.Reset();
                dw_loan.ImportString(as_xmlloan, FileSaveAsType.Xml);
                CalculateAmt();
                HfisCalInt.Value = "true";
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private void InitLnRcv()
        {
            try
            {
                HfisCalInt.Value = "false";
                String ls_memno = dw_main.GetItemString(1, "member_no");
                String ls_sliptype = dw_main.GetItemString(1, "sliptype_code");
                DateTime ldtm_slipdate = dw_main.GetItemDateTime(1, "slip_date");
                DateTime ldtm_opedate = dw_main.GetItemDateTime(1, "operate_date");
                str_slippayin slippayin = new str_slippayin();
                //ls_memno, ls_sliptype, ldtm_slipdate, ldtm_opedate, state.SsUsername, state.SsCoopId
                Int32 resultXML = shrlonService.of_initslippayin(state.SsWsPass, ref slippayin);
                try
                {
                    dw_main.Reset();
                    DwUtil.ImportData(slippayin.member_no, dw_main, null, FileSaveAsType.Xml);
                    DwUtil.DeleteLastRow(dw_main);

                    tDwMain.Eng2ThaiAllRow();
                    //DataWindowChild c_moneytype_code = dw_main.GetChild("moneytype_code");
                    //c_moneytype_code.ImportString(commonService.GetDDDWXml(state.SsWsPass, "dddw_sl_ucf_moneytype"), FileSaveAsType.Xml);
                    //DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slip_operate.pbl", null);
                    //dw_main.SetItemString(1, "moneytype_code", "CSH");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("initLnRcv error: " + ex.Message);
                    InitDataWindow();
                }

                try
                {
                    dw_loan.Reset();
                    dw_loan.ImportString(slippayin.sliptype_code, FileSaveAsType.Xml);
                }
                catch { dw_loan.Reset(); }


            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString()); }
        }

        private void CalculateAmt()
        {
            Decimal totalamt = 0;
            Decimal lonTotal = 0;
            Decimal lonPrincipal = 0;

            int rowSelected = 0;
            try
            {
                rowSelected = Convert.ToInt32(HfRowSelected.Value);
            }
            catch { }

            int loanAllRow = dw_loan.RowCount;
            for (int j = 1; j <= loanAllRow; j++)
            {
                Decimal payspec = dw_loan.GetItemDecimal(j, "bfpayspec_method");
                Decimal lonAmt = dw_loan.GetItemDecimal(j, "item_payamt");
                Decimal lonPrnAmt = dw_loan.GetItemDecimal(j, "principal_payamt");
                Decimal bfloanbalamt = dw_loan.GetItemDecimal(j, "bfshrcont_balamt");
                Decimal intLoanpayamt = dw_loan.GetItemDecimal(j, "interest_payamt");
                Decimal prnPayAmt = dw_loan.GetItemDecimal(j, "principal_payamt");
                String loanFlag = dw_loan.GetItemString(j, "operate_flag");
                if (loanFlag == "1")
                {
                    if (lonAmt != 0)
                    {
                        totalamt = totalamt + lonAmt;
                        lonTotal = bfloanbalamt - prnPayAmt;
                        lonPrincipal = lonAmt - intLoanpayamt;
                        dw_loan.SetItemDecimal(j, "item_balance", lonTotal);
                        dw_loan.SetItemDecimal(j, "principal_payamt", lonPrincipal);
                    }
                }
            }
            dw_main.SetItemDecimal(1, "slip_amt", totalamt);
        }

        private void DownloadPDF()
        {
            //เอา XML จากหน้าจอที่แสดงอยู่ มาใส่ใน Report เพื่อเปลี่ยนรูปแบบหน้าตา ก่อนสั่งพิมพ์.
            dw_pdf.Reset();
            dw_pdf.InsertRow(0);
            dw_pdf.SetItemDateTime(1, "SLSLIPPAYIN_OPERATE_DATE", DwUtil.GetDateTime(dw_main, 1, "operate_date"));
            dw_pdf.SetItemString(1, "SLSLIPPAYIN_MONEYTYPE_CODE", "");
            dw_pdf.SetItemDecimal(1, "SLSLIPPAYIN_SLIP_AMT", 0);
            dw_pdf.SetItemString(1, "MBUCFPRENAME_PRENAME_DESC", DwUtil.GetString(dw_main, 1, "PRENAME_DESC", ""));
            dw_pdf.SetItemString(1, "MBMEMBMASTER_MEMB_NAME", DwUtil.GetString(dw_main, 1, "MEMB_NAME", ""));
            dw_pdf.SetItemString(1, "MBMEMBMASTER_MEMB_SURNAME", DwUtil.GetString(dw_main, 1, "MEMB_SURNAME", ""));
            dw_pdf.SetItemString(1, "SLSLIPPAYIN_MEMBER_NO", DwUtil.GetString(dw_main, 1, "MEMBER_NO", ""));
            dw_pdf.SetItemString(1, "MBMEMBMASTER_MEMBGROUP_CODE", DwUtil.GetString(dw_main, 1, "MEMBGROUP_CODE", ""));
            dw_pdf.SetItemString(1, "MBUCFMEMBGROUP_MEMBGROUP_DESC", DwUtil.GetString(dw_main, 1, "MEMBGROUP_DESC", ""));
            dw_pdf.SetItemString(1, "SLSLIPPAYINDET_SLIPITEM_DESC", "ซื้อหุ้นพิเศษ");
            dw_pdf.SetItemDecimal(1, "SLSLIPPAYINDET_PRINCIPAL_PAYAMT", 0);
            dw_pdf.SetItemDecimal(1, "SLSLIPPAYINDET_INTEREST_PAYAMT", 0);
            dw_pdf.SetItemDecimal(1, "SLSLIPPAYINDET_ITEM_PAYAMT", 0);
            int iii = 1;
            for (int i = 1; i <= dw_loan.RowCount; i++)
            {
                if (DwUtil.GetDec(dw_loan, i, "operate_flag", 0) == 1)
                {
                    dw_pdf.InsertRow(0);
                    dw_pdf.SetItemDateTime(iii + 1, "SLSLIPPAYIN_OPERATE_DATE", DateTime.Today);
                    dw_pdf.SetItemString(iii + 1, "SLSLIPPAYIN_MONEYTYPE_CODE", "");
                    dw_pdf.SetItemDecimal(iii + 1, "SLSLIPPAYIN_SLIP_AMT", DwUtil.GetDec(dw_loan, i, "item_payamt", 0));
                    dw_pdf.SetItemString(iii + 1, "MBUCFPRENAME_PRENAME_DESC", DwUtil.GetString(dw_main, 1, "PRENAME_DESC", ""));
                    dw_pdf.SetItemString(iii + 1, "MBMEMBMASTER_MEMB_NAME", DwUtil.GetString(dw_main, 1, "MEMB_NAME", ""));
                    dw_pdf.SetItemString(iii + 1, "MBMEMBMASTER_MEMB_SURNAME", DwUtil.GetString(dw_main, 1, "MEMB_SURNAME", ""));
                    dw_pdf.SetItemString(iii + 1, "SLSLIPPAYIN_MEMBER_NO", DwUtil.GetString(dw_main, 1, "MEMBER_NO", ""));
                    dw_pdf.SetItemString(iii + 1, "MBMEMBMASTER_MEMBGROUP_CODE", DwUtil.GetString(dw_main, 1, "MEMBGROUP_CODE", ""));
                    dw_pdf.SetItemString(iii + 1, "MBUCFMEMBGROUP_MEMBGROUP_DESC", DwUtil.GetString(dw_main, 1, "MEMBGROUP_DESC", ""));
                    dw_pdf.SetItemString(iii + 1, "SLSLIPPAYINDET_SLIPITEM_DESC", DwUtil.GetString(dw_loan, i, "loancontract_no", ""));
                    dw_pdf.SetItemDecimal(iii + 1, "SLSLIPPAYINDET_PRINCIPAL_PAYAMT", DwUtil.GetDec(dw_loan, i, "principal_payamt", 0));
                    dw_pdf.SetItemDecimal(iii + 1, "SLSLIPPAYINDET_INTEREST_PAYAMT", DwUtil.GetDec(dw_loan, i, "interest_payamt", 0));
                    dw_pdf.SetItemDecimal(iii + 1, "SLSLIPPAYINDET_ITEM_PAYAMT", DwUtil.GetDec(dw_loan, i, "item_payamt", 0));
                    iii++;
                }
            }
            int ii = 0;
            while (dw_pdf.RowCount < 5)
            {
                dw_pdf.InsertRow(0);
                ii++;
                if (ii > 20) break;
            }
            for (int i = 4; i < 9; i++)
            {
                String desc = "";
                if (i == 4)
                {
                    desc = "EMS";
                }
                else if (i == 5)
                {
                    desc = "Online";
                }
                else if (i == 6)
                {
                    desc = "ค่าธรรมเนียม - ค่าโอน";
                }
                else if (i == 7)
                {
                    desc = "VAT7%";
                }
                else if (i == 8)
                {
                    desc = "EMS";
                }
                dw_pdf.InsertRow(0);
                dw_pdf.SetItemDateTime(i + 1, "SLSLIPPAYIN_OPERATE_DATE", DateTime.Today);
                dw_pdf.SetItemString(i + 1, "SLSLIPPAYIN_MONEYTYPE_CODE", "");
                dw_pdf.SetItemDecimal(i + 1, "SLSLIPPAYIN_SLIP_AMT", DwUtil.GetDec(dw_loan, i, "item_payamt", 0));
                dw_pdf.SetItemString(i + 1, "MBUCFPRENAME_PRENAME_DESC", DwUtil.GetString(dw_main, 1, "PRENAME_DESC", ""));
                dw_pdf.SetItemString(i + 1, "MBMEMBMASTER_MEMB_NAME", DwUtil.GetString(dw_main, 1, "MEMB_NAME", ""));
                dw_pdf.SetItemString(i + 1, "MBMEMBMASTER_MEMB_SURNAME", DwUtil.GetString(dw_main, 1, "MEMB_SURNAME", ""));
                dw_pdf.SetItemString(i + 1, "SLSLIPPAYIN_MEMBER_NO", DwUtil.GetString(dw_main, 1, "MEMBER_NO", ""));
                dw_pdf.SetItemString(i + 1, "MBMEMBMASTER_MEMBGROUP_CODE", DwUtil.GetString(dw_main, 1, "MEMBGROUP_CODE", ""));
                dw_pdf.SetItemString(i + 1, "MBUCFMEMBGROUP_MEMBGROUP_DESC", DwUtil.GetString(dw_main, 1, "MEMBGROUP_DESC", ""));
                dw_pdf.SetItemString(i + 1, "SLSLIPPAYINDET_SLIPITEM_DESC", desc);
                dw_pdf.SetItemDecimal(i + 1, "SLSLIPPAYINDET_PRINCIPAL_PAYAMT", 0);
                dw_pdf.SetItemDecimal(i + 1, "SLSLIPPAYINDET_INTEREST_PAYAMT", 0);
                dw_pdf.SetItemDecimal(i + 1, "SLSLIPPAYINDET_ITEM_PAYAMT", 0);
            }


            String xml = dw_pdf.Describe("DataWindow.Data.XML");
            //FileName
            //PrintClient svPrint = wcf.Print;
            String pdfFile = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFile += "_interestpay_estimate.pdf";
            pdfFile = pdfFile.Trim();

            //Print
            //int li_rv = svPrint.PrintPDF(state.SsWsPass, xml, pdfFile);
            //if (li_rv < 0)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("PrintPDF failed : (" + Convert.ToString(li_rv) + ") returned.");
            //    return;
            //}

            //Popup
            //String pdfURL = svPrint.GetPDFURL(state.SsWsPass) + pdfFile;
            //String pop = "Gcoop.OpenPopup('" + pdfURL + "')";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "PaymentTable", pop, true);
        }
    }
}
