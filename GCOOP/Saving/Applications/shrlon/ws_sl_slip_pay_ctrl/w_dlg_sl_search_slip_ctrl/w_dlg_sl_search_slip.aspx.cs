using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_search_slip_ctrl
{
    public partial class w_dlg_sl_search_slip : PageWebDialog, WebDialog
    {
        [JsPostBack]
        public string PostMembgroup { get; set; }
        [JsPostBack]
        public string PostSearch { get; set; }
        [JsPostBack]
        public string PostRetive { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {

                dsMain.memgroup();
                dsMain.DATA[0].SLIP_DATE_S = DateTime.Today;//.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                dsMain.DATA[0].SLIP_DATE_E = DateTime.Today;//.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMembgroup)
            {

                string memgroup_code = dsMain.DATA[0].MEMBGROUP_DESC;
                dsMain.DATA[0].MEMBGROUP_CODE = memgroup_code;
            }
            else if (eventArg == PostSearch)
            {
                string member_no = "";
                string payinslip_no = "";
                string memb_name = "";
                string memb_surname = "";
                string membgroup_code = "";

                DateTime slip_date_s = dsMain.DATA[0].SLIP_DATE_S;
                DateTime slip_date_e = dsMain.DATA[0].SLIP_DATE_E;

                member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.DATA[0].MEMBER_NO = member_no;
                payinslip_no = dsMain.DATA[0].PAYINSLIP_NO;
                memb_name = dsMain.DATA[0].MEMB_NAME;
                memb_surname = dsMain.DATA[0].MEMB_SURNAME;
                membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE;


                dsList.Retrieve(member_no, payinslip_no, memb_name, memb_surname, membgroup_code, slip_date_s, slip_date_e);

            }
            else if (eventArg == PostRetive)
            {
                Search_Click();
            }
        }

        public void WebDialogLoadEnd()
        {

        }

        protected void Search_Click()
        {
            try
            {
                String payinslip_no = "";
                String coop_id = "";


                string sqlpayinslip_no = "";
                int r = dsList.GetRowFocus();

                coop_id = state.SsCoopControl;
                payinslip_no = dsList.DATA[r].PAYINSLIP_NO;

                //if (coop_id.Length > 0)
                //{
                //    sqlpayinslip_no = " and (  slslippayin.coop_id like '%" + coop_id + "%') ";

                //}
                if (payinslip_no.Length > 0)
                {
                    sqlpayinslip_no = " and (  slslippayin.payinslip_no like '%" + payinslip_no + "%') ";
                   
                }


                this.SetOnLoadedScript("parent.GetItemLoan(\"" + sqlpayinslip_no + "\";");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}