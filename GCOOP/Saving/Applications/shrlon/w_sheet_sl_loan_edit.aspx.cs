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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loan_edit : PageWebSheet, WebSheet
    {
        private DwThDate tDwstatement;
        private DwThDate tDwdetail;
        protected String postMemberNo;
        protected String postcontractNo;

        public void InitJsPostBack()
        {
            postMemberNo = WebUtil.JsPostBack(this, "postMemberNo");
            postcontractNo = WebUtil.JsPostBack(this, "postcontractNo");

            tDwstatement = new DwThDate(dw_statement, this);
            tDwstatement.Add("operate_date", "operate_tdate");
            tDwstatement.Add("slip_date", "slip_tdate");
            tDwstatement.Add("calintfrom_date", "calintfrom_tdate");
            tDwstatement.Add("calintto_date", "calintto_tdate");  
            
            tDwdetail = new DwThDate(dw_detail1, this);
            tDwdetail.Add("startcont_date", "startcont_tdate");
            tDwdetail.Add("startkeep_date", "startkeep_tdate");
            tDwdetail.Add("expirecont_date", "expirecont_tdate");
            tDwdetail.Add("lastaccess_date", "lastaccess_tdate");
            tDwdetail.Add("lastcalint_date", "lastcalint_tdate");
            tDwdetail.Add("lastprocess_date", "lastprocess_tdate");
            tDwdetail.Add("contintexp_date", "contintexp_tdate");
            
            
        }

        public void WebSheetLoadBegin()
        {

            if (!IsPostBack)
            {
                dw_main.InsertRow(0);
                dw_detail1.InsertRow(0);
                dw_list.InsertRow(0);
                dw_statement.InsertRow(0);
            }
            else
            {
                dw_main.RestoreContext();
                dw_detail1.RestoreContext();
                dw_list.RestoreContext();
                dw_statement.RestoreContext();
            }

        }



        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postMemberNo")
            {
                JsPostMemberNo();
            }
            else if (eventArg == "postcontractNo")
            {
                JsPostcontractNo();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                DwUtil.UpdateDataWindow(dw_detail1, "sl_loan_edit.pbl", "lncontmaster");
                DwUtil.UpdateDataWindow(dw_statement, "sl_loan_edit.pbl", "lncontstatement");

                JsPostMemberNo();

                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลได้");
            }
        }

        public void WebSheetLoadEnd()
        {
            dw_main.SaveDataCache();
            dw_detail1.SaveDataCache();
            dw_list.SaveDataCache();
            dw_statement.SaveDataCache();
        }

        private void JsPostMemberNo()
        {
            String memberNo = WebUtil.MemberNoFormat(dw_main.GetItemString(1, "member_no"));
            dw_main.SetItemString(1, "member_no", memberNo);
            DwUtil.RetrieveDataWindow(dw_main, "sl_loan_edit.pbl", null, memberNo);
            DwUtil.RetrieveDataWindow(dw_list, "sl_loan_edit.pbl", null, state.SsCoopControl, memberNo);
            
        }

        private void JsPostcontractNo()
        {
            int list_row = int.Parse( Hd_listrow.Value );
            String cont_no = dw_list.GetItemString(list_row, "loancontract_no");
            String lncode = dw_list.GetItemString(list_row, "loantype_code");
            DwUtil.RetrieveDataWindow(dw_detail1, "sl_loan_edit.pbl", tDwdetail, cont_no);
            DwUtil.RetrieveDataWindow(dw_statement, "sl_loan_edit.pbl", tDwstatement, cont_no);
            DwUtil.RetrieveDDDW(dw_detail1, "loantype_code", "sl_loan_edit.pbl", null);
            DwUtil.RetrieveDDDW(dw_detail1, "loanobjective_code", "sl_loan_edit.pbl", lncode);
        }


    }
}
