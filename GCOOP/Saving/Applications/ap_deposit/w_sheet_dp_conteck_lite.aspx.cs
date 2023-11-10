using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_conteck_lite : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string postAccountNo { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].COOP_ID = state.SsCoopControl;
            }
            else
            {
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == postAccountNo)
            {
                try
                {
                    string accountNo = wcf.NDeposit.of_analizeaccno(state.SsWsPass, dsMain.DATA[0].DEPTACCOUNT_NO);
                    dsMain.Retrieve(state.SsCoopControl, accountNo);
                    dsDetail.Retrieve(state.SsCoopControl, accountNo);
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}