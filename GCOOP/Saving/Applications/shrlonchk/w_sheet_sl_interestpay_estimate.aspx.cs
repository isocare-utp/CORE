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

namespace Saving.Applications.shrlonchk
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

        #region WebSheet Members

        public void InitJsPostBack()
        {
            initLnRcv = WebUtil.JsPostBack(this, "initLnRcv");
            initSlipCalInt = WebUtil.JsPostBack(this, "initSlipCalInt");
            getPeriodCont = WebUtil.JsPostBack(this, "getPeriodCont");
            calculateAmt = WebUtil.JsPostBack(this, "calculateAmt");
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


            //this.ConnectSQLCA();

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_loan);
            }
            if (dw_main.RowCount < 1)
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
                this.InitLnRcv();
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
            dw_main.SetItemDate(1, "slip_date", state.SsWorkDate);
            dw_main.SetItemDate(1, "operate_date", state.SsWorkDate);
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
                Int32 result = shrlonService.of_initslippayin_calint(state.SsWsPass, ref as_xmlloan, as_sliptype, dt);
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
                str_slippayin slip = new str_slippayin();
                HfisCalInt.Value = "false";
                String ls_memno = dw_main.GetItemString(1, "member_no");
                String ls_sliptype = dw_main.GetItemString(1, "sliptype_code");
                DateTime ldtm_slipdate = dw_main.GetItemDate(1, "slip_date");
                DateTime ldtm_opedate = dw_main.GetItemDate(1, "operate_date");

                slip.member_no = ls_memno;
                slip.sliptype_code = ls_sliptype;
                slip.slip_date = ldtm_slipdate;
                slip.operate_date = ldtm_opedate;

                Int32 resultXML = shrlonService.of_initslippayin(state.SsWsPass, ref slip);
                try
                {
                    dw_main.Reset();
                    DwUtil.ImportData(slip.member_no, dw_main, null, FileSaveAsType.Xml);
                    DwUtil.DeleteLastRow(dw_main);

                    tDwMain.Eng2ThaiAllRow();
                    //DataWindowChild c_moneytype_code = dw_main.GetChild("moneytype_code");
                    //c_moneytype_code.ImportString(commonService.GetDDDWXml(state.SsWsPass, "dddw_sl_ucf_moneytype"), FileSaveAsType.Xml);
                    //DwUtil.RetrieveDDDW(dw_main, "moneytype_code", "sl_slip_operate.pbl", null);
                    //dw_main.SetItemString(1, "moneytype_code", "CSH");
                }
                catch(Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("initLnRcv error: " + ex.Message);
                    InitDataWindow();
                }
                
                try
                {
                    dw_loan.Reset();
                    dw_loan.ImportString(slip.sliptype_code, FileSaveAsType.Xml);
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
    }
}
