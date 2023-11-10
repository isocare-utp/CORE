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
    public partial class w_dlg_regloan_year : PageWebDialog, WebDialog
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
            
            dw_criteria.SaveDataCache();
            dw_detail.SaveDataCache();
        }

        public void SearchLoanrequest()
        {
            String strMemberNo = "", strMemberName = "", strMemberSurename = "", strReqestTDate = "";
            String strRequestNo = "", strMemberGroupNo = "";
            String strLoanTypeCode = "", strSQL = "", strTemp = "", strSQLT = "";
            //DateTime  strReqestTDate ;
            int rowNumber = 1;
            //ปาล์มแก้ไข คิวรี่สตริง
            strSQL = @"
  SELECT LNREQLOANYEAR.LOANREQUEST_DOCNO,   
         LNREQLOANYEAR.MEMBER_NO,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBUCFMEMBGROUP.MEMBGROUP_CODE,   
         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
         MBUCFPRENAME.PRENAME_DESC,   
         LNREQLOANYEAR.LOANTYPE_CODE,   
         LNREQLOANYEAR.LOANREQ_DATE,   
         LNREQLOANYEAR.LOANREQUEST_STATUS,   
         LNREQLOANYEAR.LOANCONTRACT_NO  
    FROM LNREQLOANYEAR,   
         MBMEMBMASTER,   
         MBUCFMEMBGROUP,   
         MBUCFPRENAME  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNREQLOANYEAR.MEMBER_NO ) and  
         ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  
         ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE )   ";

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
                strSQLT = "and (  LNREQLOANYEAR.LOANREQUEST_DOCNO = '" + strRequestNo + "') ";
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

            }
        }
        public void Refresh() { }

    }
}
