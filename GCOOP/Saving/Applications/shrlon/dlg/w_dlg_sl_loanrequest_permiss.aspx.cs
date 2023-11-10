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
using System.Globalization;
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_permiss : PageWebDialog, WebDialog
    {
        private n_shrlonClient shrlonService;
        private n_commonClient commonService;

        protected String closeWebDialog;

        public void InitJsPostBack()
        {
            closeWebDialog = WebUtil.JsPostBack(this, "closeWebDialog");
        }

        public void WebDialogLoadBegin()
        {
           
           
                String coop_id = state.SsCoopControl;// Request["coop_id"].ToString();
                String ref_contmastno = Request["ref_contmastno"].ToString();
                string memno = Request["member_no"].ToString();
                string loantype_code = Request["loantype_code"].ToString();
                decimal loangrpcredit_amt = Convert.ToDecimal(Request["loangrpcredit_amt"].ToString());
                decimal loangrpuse_amt = Convert.ToDecimal(Request["loangrpuse_amt"].ToString());
                decimal loancredit_amt = Convert.ToDecimal(Request["loancredit_amt"].ToString());
                decimal loanmaxreq_amt = Convert.ToDecimal(Request["loanmaxreq_amt"].ToString());
                decimal loanpermiss_amt = Convert.ToDecimal(Request["loanpermiss_amt"].ToString());
                decimal maxperiod_payamt = Convert.ToDecimal(Request["maxperiod_payamt"].ToString());
                decimal maxperiod_payment = Convert.ToDecimal(Request["maxperiod_payment"].ToString());

                dw_permiss.InsertRow(0);

                DateTime ldtm_expirecont = state.SsWorkDate,   ldtm_start = state.SsWorkDate;
               
                string ls_Sql = @"  select	contcredit_no, loancreditbal_amt, expirecont_date, startcont_date
                                from	lncontcredit          where	(  member_no	 =  '" + memno + @"'  ) and
		                        ( contcredit_no 	=  '" + ref_contmastno + "')"  ;


                Sdt dtcreditc = WebUtil.QuerySdt(ls_Sql);
                            
                if (dtcreditc.Next())
                {

                    ldtm_expirecont = dtcreditc.GetDate("expirecont_date");
                    ldtm_start = dtcreditc.GetDate("startcont_date");
                  //  ref_contmastno = dtcreditc.GetString("contcredit_no");
                   
                }

                dw_permiss.SetItemString(1, "ref_contmastno", ref_contmastno);
                dw_permiss.SetItemDecimal(1,"loangrpcredit_amt",loangrpcredit_amt );
                dw_permiss.SetItemDecimal(1,"loangrpuse_amt",loangrpuse_amt );
                dw_permiss.SetItemDecimal(1,"loancredit_amt",loancredit_amt );
                dw_permiss.SetItemDecimal(1,"loanmaxreq_amt", loanmaxreq_amt );
                dw_permiss.SetItemDecimal(1,"loanpermiss_amt", loanpermiss_amt);
                dw_permiss.SetItemDecimal(1,"maxperiod_payamt",maxperiod_payamt );
                dw_permiss.SetItemDecimal(1, "maxperiod_payment", maxperiod_payment);

                dw_permiss.SetItemDateTime(1, "start_date", ldtm_start);
                dw_permiss.SetItemDateTime(1, "expire_date", ldtm_expirecont);

           
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "closeWebDialog")
            {
                OnCloseClick();
            }

        }
        
        public void WebDialogLoadEnd()
        {


        }
        public void OnCloseClick()
        {
           //HfChkStatus.Value = "1";
        }

    }
}
