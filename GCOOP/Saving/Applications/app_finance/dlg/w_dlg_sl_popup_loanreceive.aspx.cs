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
using CoreSavingLibrary.WcfNShrlon;
using System.Text;
using Sybase.DataWindow;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_sl_popup_loanreceive : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String openDialogDetail;
        protected String initListLnRcv;
        protected String searchMemberNo;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            openDialogDetail = WebUtil.JsPostBack(this, "openDialogDetail");
            initListLnRcv = WebUtil.JsPostBack(this, "initListLnRcv");
            searchMemberNo = WebUtil.JsPostBack(this, "searchMemberNo");
            HfPageCommand.Value = "";


        }

        public void WebDialogLoadBegin()
        {
            //this.ConnectSQLCA();


            try
            {
                shrlonService = wcf.NShrlon;
                commonService = wcf.NCommon;
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }
            if (IsPostBack)
            {
                this.RestoreContextDw(Dwlist);
                //this.RestoreContextDw(DwGoto);
            }

            if (Dwlist.RowCount < 1)
            {
                InitListLnRcv();
                //DwGoto.InsertRow(0);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "openDialogDetail")
            {
                this.OpenDialogDetail();
                HfPageCommand.Value = "opendialog";
            }
            if (eventArg == "initListLnRcv")
            {
                this.InitListLnRcv();
            }
            if (eventArg == "searchMemberNo")
            {
                SearchMemberNo();
            }
        }

      
        public void WebDialogLoadEnd()
        {
            Dwlist.SaveDataCache();

        }

        #endregion
        private void InitListLnRcv()
        {
            //String strXML = shrlonService.InitListLnRcv(state.SsWsPass);
            //Dwlist.Reset();
            //Dwlist.ImportString(strXML, FileSaveAsType.Xml);
            //DataWindowFullState fs = Dwlist.GetFullState();
            //Byte[] ss = fs.ToByteArray();

        }

        private void OpenDialogDetail()
        {
            ArrayList dwlist = new ArrayList();
            int allRow = Dwlist.RowCount;
            for (int i = 0; i < allRow; i++)
            {
                int rowIndex = i + 1;
                decimal flag = Dwlist.GetItemDecimal(rowIndex, "operate_flag");
                if (flag == 1)
                {
                    String[] arrValue = new String[4];
                    arrValue[0] = Dwlist.GetItemString(rowIndex, "loancontract_no");
                    arrValue[1] = Dwlist.GetItemString(rowIndex, "lnrcvfrom_code");
                    arrValue[2] = Dwlist.GetItemString(rowIndex, "member_no");
                    arrValue[3] = "";
                    dwlist.Add(arrValue);
                }

            }
            Session["SSList"] = dwlist;
        }
        private void SearchMemberNo()
        {
            String memberNo = HfMemberNo.Value;

            String strSQL1, strSQL2, strSQL = "";
            strSQL1 = @"  
           SELECT 'CON' as lnrcvfrom_code,   
         LNCONTMASTER.LOANCONTRACT_NO,   
         LNCONTMASTER.MEMBER_NO,   
         LNCONTMASTER.WITHDRAWABLE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNCONTMASTER,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( ( lncontmaster.withdrawable_amt > 0 ) AND  
         ( lncontmaster.contract_status > 0 ) )   and
		LNCONTMASTER.MEMBER_NO = '" + memberNo + "'";
            strSQL2 = @"  
    UNION   
  SELECT 'REQ' as lnrcvfrom_code,   
         LNREQLOAN.LOANREQUEST_DOCNO,   
         LNREQLOAN.MEMBER_NO,   
         LNREQLOAN.LOANAPPROVE_AMT,   
         LNLOANTYPE.PREFIX,   
         MBUCFPRENAME.PRENAME_DESC,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         0 as operate_flag  
    FROM LNREQLOAN,   
         MBMEMBMASTER,   
         MBUCFPRENAME,   
         LNLOANTYPE  
   WHERE ( LNREQLOAN.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
         ( LNREQLOAN.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
         ( ( lnreqloan.loanrequest_status = 11 ) )    and
		LNREQLOAN.MEMBER_NO = '" + memberNo + "'  ";
            strSQL = strSQL1 + "  " + strSQL2;
            try { DwUtil.ImportData(strSQL, Dwlist, null); }
            catch { Dwlist.Reset(); }

        }

    }
}
