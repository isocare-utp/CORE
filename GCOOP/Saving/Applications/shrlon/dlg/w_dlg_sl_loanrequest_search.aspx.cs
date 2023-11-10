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
    public partial class w_dlg_sl_loanrequest_search : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;
        protected String refresh;
        protected String setDocNo;
        protected String searchLoanrequest;
        private DwThDate tDwMain;

        public void InitJsPostBack()
        {
            searchLoanrequest = WebUtil.JsPostBack(this, "searchLoanrequest");
            setDocNo = WebUtil.JsPostBack(this, "setDocNo");
            refresh = WebUtil.JsPostBack(this, "refresh");
            tDwMain = new DwThDate(dw_criteria, this);
            tDwMain.Add("request_date", "request_tdate");
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
            //this.ConnectSQLCA();
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
            if (eventArg == "refresh")
            {
                Refresh();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwUtil.RetrieveDDDW(dw_criteria, "coop_id_1", "sl_loan_requestment_cen.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "loantype_code_1", "sl_loan_requestment_cen.pbl", null);
            dw_criteria.SaveDataCache();
            dw_detail.SaveDataCache();
        }

        public void SearchLoanrequest()
        {
            String strMemberNo = "", strMemberName = "", strMemberSurename = "", strReqestTDate = "";
            String strRequestNo = "", strCoop_id = "";
            String strLoanTypeCode = "", strSQL = "", strTemp = "", strSQLT = "";
            //DateTime  strReqestTDate ;
            int rowNumber = 1;
            //ปาล์มแก้ไข คิวรี่สตริง
            strSQL = @"
       SELECT top 500
LNREQLOAN.LOANREQUEST_DOCNO,   
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
         LNREQLOAN.LOANRCVFIX_DATE,   
         LNREQLOAN.EXPENSE_CODE,   
         LNREQLOAN.PAYTOORDER_DESC,   
         LNREQLOAN.COOP_ID  
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFMEMBGROUP,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNREQLOAN.MEMBER_NO ) and  
         ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  
         ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and  
         ( LNLOANTYPE.LOANTYPE_CODE = LNREQLOAN.LOANTYPE_CODE ) and  
         ( LNREQLOAN.MEMCOOP_ID = LNLOANTYPE.COOP_ID ) and  
         ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID ) and  
         ( LNREQLOAN.MEMCOOP_ID = MBMEMBMASTER.COOP_ID ) 
       
    ";

            try
            {
                strMemberNo = dw_criteria.GetItemString(rowNumber, "member_no");
                strMemberNo = string.IsNullOrEmpty(strMemberNo) ? "" : strMemberNo.Trim();
                if (strMemberNo == "00000000")
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
                strCoop_id = dw_criteria.GetItemString(rowNumber, "coop_id");
                strCoop_id = string.IsNullOrEmpty(strCoop_id) ? "" : strCoop_id.Trim();
            }
            catch { strCoop_id = ""; }
            try
            {
                strReqestTDate = dw_criteria.GetItemString(rowNumber, "request_tdate");
                int year = Convert.ToInt32(WebUtil.Right(strReqestTDate, 4)) - 543;
                String date = WebUtil.Left(strReqestTDate, 4);
                strReqestTDate = date + year.ToString();
                //  strReqestTDate = DateTime.is .IsNullOrEmpty(strReqestTDate) ? "" : strReqestTDate.Trim();
            }
            catch { strReqestTDate = ""; }
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
            if (strCoop_id.Length > 0)
            {
                strSQLT = "and (  LNREQLOAN.MEMCOOP_ID = '" + strCoop_id + "') ";
            }
            if (strReqestTDate.Length > 0)
            {
                strSQLT = "and (  LNREQLOAN.LOANREQUEST_DATE  = to_date( '" + strReqestTDate + "','ddMMyyyy')) ";
            }
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
            int row = Convert.ToInt32(HfRow.Value);
            if ((HfRow.Value != "") && (HfRow.Value != null))
            {
                HfDocNo.Value = dw_detail.GetItemString(row, "loanrequest_docno");
                HdcoopId.Value = dw_detail.GetItemString(row, "coop_id");
            }
        }

        public void Refresh() { }
    }
}
