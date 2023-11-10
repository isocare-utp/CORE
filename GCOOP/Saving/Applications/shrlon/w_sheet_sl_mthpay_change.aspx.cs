using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNBusscom;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_mthpay_change : PageWebSheet, WebSheet
    {
        private n_shrlonClient shrlonService;
        private n_busscomClient BusscomService;
        private n_commonClient commonService;
        private DwThDate tDwMain;
        String pbl = "sl_mthpay_change.pbl";
        protected String JsPostMemberNo;
        protected String JsPostOperateTypeShare;
        protected String JsPostOperateTypeLoan;

        public void InitJsPostBack()
        {
            JsPostMemberNo = WebUtil.JsPostBack(this, "JsPostMemberNo");
            JsPostOperateTypeShare = WebUtil.JsPostBack(this, "JsPostOperateTypeShare");
            JsPostOperateTypeLoan = WebUtil.JsPostBack(this, "JsPostOperateTypeLoan");
            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("monthchgreq_date", "monthchgreq_tdate");
        }

        public void WebSheetLoadBegin()
        {
            try
            {

                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
                BusscomService = wcf.NBusscom;

            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
                dw_share.InsertRow(0);
                dw_loan.InsertRow(0);
                dw_main.SetItemDateTime(1, "monthchgreq_date",state.SsWorkDate);
                tDwMain.Eng2ThaiAllRow();
                
            }
            else
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_share);
                this.RestoreContextDw(dw_loan);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "JsPostMemberNo")
            {
                PostMemberNo();
            }
            else if (eventArg == "JsPostOperateTypeShare")
            {
                PostOperateTypeShare();
            }
            else if (eventArg == "JsPostOperateTypeLoan")
            {
                PostOperateTypeLoan();
            }
            
        }

        public void SaveWebSheet()
        {
            
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_share.SaveDataCache();
            dw_loan.SaveDataCache();    
        }
        public void PostMemberNo()
        {
           // shrlonService = wcf.NShrlon;
           //ShrlonClient shrlonService;
           //shrlonService = wcf.NShrlon;
            String member_no=dw_main.GetItemString(1,"member_no");
            member_no = WebUtil.MemberNoFormat(member_no);
            String xmlhead = dw_main.Describe("DataWindow.Data.XML");
            String xmlshare = dw_share.Describe("DataWindow.Data.XML");
            String xmlLoan = dw_loan.Describe("DataWindow.Data.XML");

         //   shrlonService.initreqchgmthpay(state.SsWsPass, state.SsCoopId, member_no, state.SsCoopId, state.SsWorkDate,ref xmlhead,ref xmlshare,ref xmlLoan);
            
            dw_main.Reset();
            dw_share.Reset();
            dw_loan.Reset();
            dw_main.ImportString(xmlhead, Sybase.DataWindow.FileSaveAsType.Xml);
            dw_share.ImportString(xmlshare, Sybase.DataWindow.FileSaveAsType.Xml);
            dw_loan.ImportString(xmlLoan, Sybase.DataWindow.FileSaveAsType.Xml);
        }
        public void PostOperateTypeShare()
        { 
            String OprType = dw_share.GetItemString(1,"operate_type");
            if (OprType == "PAY")
            {
                dw_share.SetItemDecimal(1, "new_status", dw_share.GetItemDecimal(1, "old_status"));
                dw_share.SetItemDecimal(1, "newperiod_payment", dw_share.GetItemDecimal(1, "oldperiod_payment"));
                dw_share.Modify("new_status.protect=1");
                dw_share.Modify("newperiod_payment.protect=0");
            }
            else if (OprType == "STS")
            {
                dw_share.SetItemDecimal(1, "new_status", dw_share.GetItemDecimal(1, "old_status"));
                dw_share.SetItemDecimal(1, "newperiod_payment", dw_share.GetItemDecimal(1, "oldperiod_payment"));
                dw_share.Modify("newperiod_payment.protect=1");
                dw_share.Modify("new_status.protect=0");
            }
            else if (OprType == "NOT")
            {
                dw_share.SetItemDecimal(1, "new_status", dw_share.GetItemDecimal(1, "old_status"));
                dw_share.SetItemDecimal(1, "newperiod_payment", dw_share.GetItemDecimal(1, "oldperiod_payment"));
                dw_share.Modify("new_status.protect=1");
                dw_share.Modify("newperiod_payment.protect=1");
            }
        
        }

        public void PostOperateTypeLoan()
        {
            Int32 row = Convert.ToInt32(HdRowOfLoan.Value.ToString().Trim());
            String OprType = dw_share.GetItemString(row, "operate_type");
            if (OprType == "PAY")
            {
                dw_loan.SetItemDecimal(row, "new_status", dw_share.GetItemDecimal(row, "old_status"));
                dw_loan.SetItemDecimal(row, "newpayment_type", dw_share.GetItemDecimal(row, "oldpayment_type"));
                dw_loan.SetItemDecimal(row, "newperiod_payment", dw_share.GetItemDecimal(row, "oldperiod_payment"));
                dw_loan.Modify("new_status.protect=1");
                dw_loan.Modify("newpayment_type.protect=1");
                dw_loan.Modify("newperiod_payment.protect=0");
            }
            else if (OprType == "STS")
            {
                dw_loan.SetItemDecimal(row, "new_status", dw_share.GetItemDecimal(row, "old_status"));
                dw_loan.SetItemDecimal(row, "newpayment_type", dw_share.GetItemDecimal(row, "oldpayment_type"));
                dw_loan.SetItemDecimal(row, "newperiod_payment", dw_share.GetItemDecimal(row, "oldperiod_payment"));
                dw_loan.Modify("new_status.protect=0");
                dw_loan.Modify("newpayment_type.protect=1");
                dw_loan.Modify("newperiod_payment.protect=1");
            }
            else if (OprType == "TYP")
            {
                dw_loan.SetItemDecimal(row, "new_status", dw_share.GetItemDecimal(row, "old_status"));
                dw_loan.SetItemDecimal(row, "newpayment_type", dw_share.GetItemDecimal(row, "oldpayment_type"));
                dw_loan.SetItemDecimal(row, "newperiod_payment", dw_share.GetItemDecimal(row, "oldperiod_payment"));
                dw_loan.Modify("new_status.protect=1");
                dw_loan.Modify("newpayment_type.protect=0");
                dw_loan.Modify("newperiod_payment.protect=1");
            }
            else if (OprType == "NOT")
            {
                dw_loan.SetItemDecimal(row, "new_status", dw_share.GetItemDecimal(row, "old_status"));
                dw_loan.SetItemDecimal(row, "newpayment_type", dw_share.GetItemDecimal(row, "oldpayment_type"));
                dw_loan.SetItemDecimal(row, "newperiod_payment", dw_share.GetItemDecimal(row, "oldperiod_payment"));
                dw_loan.Modify("new_status.protect=1");
                dw_loan.Modify("newpayment_type.protect=1");
                dw_loan.Modify("newperiod_payment.protect=1");
            }

        }

    }
}