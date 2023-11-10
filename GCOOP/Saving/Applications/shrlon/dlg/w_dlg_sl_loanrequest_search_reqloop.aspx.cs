using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
using System.Web.Services.Protocols;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_search_reqloop : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String refresh;
        protected String setDocNo;
        protected String searchLoanrequest;
        
        public void InitJsPostBack()
        {
            searchLoanrequest = WebUtil.JsPostBack(this, "searchLoanrequest");
            setDocNo = WebUtil.JsPostBack(this, "setDocNo");
            refresh = WebUtil.JsPostBack(this, "refresh");
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            this.ConnectSQLCA();
            if (!IsPostBack)
            {
                dw_criteria.InsertRow(0);
            }
            else
            {
                this.RestoreContextDw(dw_criteria);
                this.RestoreContextDw(dw_detail);
            }

            if (!HfSearch.Value.Equals(""))
            {
                DwUtil.ImportData(HfSearch.Value, dw_detail, null);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "searchLoanrequest")
            {
                SearchLoanrequest();
            }
            if (eventArg == "setDocNo")
            {
                SetDocNo();
            }
            if (eventArg == "refresh") {
                Refresh();
            }
        }

        public void WebDialogLoadEnd()
        {
            try
            {
                DwUtil.RetrieveDDDW(dw_criteria, "member_group_no_1", "sl_loan_requestment.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "loantype_code", "sl_loan_requestment.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "expense_code", "sl_loan_requestment.pbl", null);
            }
            catch { }
            dw_criteria.SaveDataCache();
            dw_detail.SaveDataCache();
        }

        public void SearchLoanrequest()
        {
            String strMemberNo = "", strMemberName = "", strMemberSurename = "";
            String strRequestNo = "", strMemberGroupNo = "";
            String strLoanTypeCode = "", strSQL = "", strTemp = "", strSQLT = "";
            //DateTime  strReqestTDate ;
            int rowNumber = 1;
            
            strSQL = @"
   SELECT LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBUCFMEMBGROUP.MEMBGROUP_CODE,   
         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
         MBUCFPRENAME.PRENAME_DESC,   
         LNREQLOAN.LOANTYPE_CODE,   
         LNREQLOAN.LOANREQUEST_DATE,   
         LNREQLOAN.LOANREQUEST_STATUS,   
         LNREQLOAN.LOANCONTRACT_NO,   
         LNREQLOAN.BRANCH_ID,
	    LNREQLOAN.LOANRCVFIX_DATE,
	    LNREQLOAN.EXPENSE_CODE,
	    LNREQLOAN.PAYTOORDER_DESC
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFMEMBGROUP,   
         MBUCFPRENAME,
	    LNLOANTYPE
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNREQLOAN.MEMBER_NO ) and  
         ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  
         ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and
	    ( LNLOANTYPE.LOANTYPE_CODE = LNREQLOAN.LOANTYPE_CODE ) and
	    ( LNLOANTYPE.LOANGROUP_CODE = '01' ) and
	    ( LNLOANTYPE.REQLOOP_FLAG = 1 ) and 
        rownum <= 500
";

            try
            {
                strMemberNo = dw_criteria.GetItemString(rowNumber, "member_no");
                strMemberNo = string.IsNullOrEmpty(strMemberNo) ? "" : strMemberNo.Trim();
                if (strMemberNo == "000000") 
                {
                    strMemberNo = "";
                }
            }
            catch { strMemberNo = ""; }
            try
            {
                strMemberName = dw_criteria.GetItemString(rowNumber, "member_name");
                strMemberName = string.IsNullOrEmpty(strMemberName) ? "" : strMemberName.Trim();

            }
            catch { strMemberName = ""; }
            try
            {
                strMemberSurename = dw_criteria.GetItemString(rowNumber, "member_surname");
                strMemberSurename = string.IsNullOrEmpty(strMemberSurename) ? "" : strMemberSurename.Trim();
            }
            catch { strMemberSurename = ""; }
            try
            {
                strRequestNo = dw_criteria.GetItemString(rowNumber, "request_no");
                strRequestNo = string.IsNullOrEmpty(strRequestNo) ? "" : strRequestNo.Trim();
            }
            catch { strRequestNo = ""; }
            try
            {
                strMemberGroupNo = dw_criteria.GetItemString(rowNumber, "member_group_no");
                strMemberGroupNo = string.IsNullOrEmpty(strMemberGroupNo) ? "" : strMemberGroupNo.Trim();
            }
            catch { strMemberGroupNo = ""; }
            //try
            //{
            //    strReqestTDate = dw_criteria.GetItemDateTime(rowNumber, "request_tdate");
            //    strReqestTDate = DateTime.is .IsNullOrEmpty(strReqestTDate) ? "" : strReqestTDate.Trim();
            //}
            //catch { strReqestTDate = ""; }
            try
            {
                strLoanTypeCode = dw_criteria.GetItemString(rowNumber, "loantype_code");
                strLoanTypeCode = string.IsNullOrEmpty(strLoanTypeCode) ? "" : strLoanTypeCode.Trim();
            }
            catch { strLoanTypeCode = ""; }


            if (strMemberNo.Length > 0)
            {
                strSQLT = "and (  mbmembmaster.member_no = '" + strMemberNo + "') ";
            }
            if (strMemberName.Length > 0)
            {
                strSQLT = "and (  mbmembmaster.memb_name like '" + strMemberName + "%') ";
            }
            if (strMemberSurename.Length > 0)
            {
                strSQLT = "and (  mbmembmaster.memb_surname like '" + strMemberSurename + "%') ";
            }
            if (strRequestNo.Length > 0)
            {
                strSQLT = "and (  lnreqloan.loanrequest_docno = '" + strRequestNo + "') ";
            }
            if (strMemberGroupNo.Length > 0)
            {
                strSQLT = "and (  mbucfmembgroup.membgroup_code = '" + strMemberGroupNo + "') ";
            }
            //if (strReqestTDate.Length > 0)
            //{
            //    strSQLT = "and (  lnreloan.loanrequest_date = to_date( '" + strReqestTDate + "') ";
            //}
            if (strLoanTypeCode.Length > 0)
            {
                strSQLT = "and (  lnreqloan.loantype_code = '" + strLoanTypeCode + "') ";
            }
            try
            {
                strTemp = strSQL + strSQLT;
                HfSearch.Value = strTemp;
                DwUtil.ImportData(strTemp, dw_detail, null);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void SetDocNo()
        {
            int row = Convert .ToInt32 ( HfRow.Value);
            if ((HfRow.Value  != "") && (HfRow.Value  != null)) {
                HfDocNo.Value = dw_detail.GetItemString(row, "loanrequest_docno");

            }
        }
        public void Refresh() { }

    }
}
