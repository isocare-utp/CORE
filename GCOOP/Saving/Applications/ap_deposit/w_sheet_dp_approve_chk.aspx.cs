using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CoreSavingLibrary.WcfNDeposit;

using CoreSavingLibrary.WcfNDeposit;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_approve_chk : PageWebSheet, WebSheet
    {

        //private DepositClient depservice;
        private n_depositClient ndept;

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            Dw_Main.SetTransaction(sqlca);
            //depservice = wcf.Deposit;
            ndept = wcf.NDeposit;

            if (!IsPostBack)
            {
                Dw_Main.InsertRow(0);
                Dw_Main.Retrieve(state.SsWorkDate);
            }
            else
            {
                this.RestoreContextDw(Dw_Main);

            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            try
            {
                for (int i = 1; i <= Dw_Main.RowCount; i++)
                {
                    if (Dw_Main.GetItemDecimal(i, "app_status") == 1)
                    {
                        String as_apv_docno = Dw_Main.GetItemString(i, "apv_docno");
                        String as_apvuser_id = state.SsUsername;
                        //int result = depservice.of_apv_permiss(state.SsWsPass, as_apv_docno, state.SsWorkDate, as_apvuser_id);

                        int result = ndept.of_apv_permiss(state.SsWsPass, as_apv_docno, state.SsWorkDate, as_apvuser_id); //new
                    }
                }
                Dw_Main.Reset();
                LtServerMessage.Text = WebUtil.CompleteMessage("อนุมัติสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_Main.SaveDataCache();
            Dw_Main.Retrieve();
        }
    }
}