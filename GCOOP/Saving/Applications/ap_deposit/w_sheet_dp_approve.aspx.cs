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
    public partial class w_sheet_dp_approve : PageWebSheet, WebSheet
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
                Dw_Main.Retrieve(state.SsWorkDate, Convert.ToDecimal(ApvStatus.SelectedValue));
            }
            else
            {
                Dw_Main.Retrieve(state.SsWorkDate, Convert.ToDecimal(ApvStatus.SelectedValue));
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
                if (Convert.ToDecimal(ApvStatus.SelectedValue) == 1)
                {
                    Dw_Main.SetFilter("apv_status = -1");
                    Dw_Main.Filter();
                }
                for (int i = 1; i <= Dw_Main.RowCount; i++)
                {
                    if (Dw_Main.GetItemDecimal(i, "apv_status") == 1)
                    {
                        String as_apv_docno = Dw_Main.GetItemString(i, "apv_docno");
                        String deptgroup_type = Dw_Main.GetItemString(i, "deptgroup_type");
                        String as_apvuser_id = state.SsUsername;
                        if( deptgroup_type == "ERR"){
                            int result = ndept.of_apv_cancel(state.SsWsPass, as_apv_docno, state.SsWorkDate, as_apvuser_id);
                           
                        }
                        else if (deptgroup_type == "ADJ")
                        {

                            int result = ndept.of_apv_adj(state.SsWsPass, as_apv_docno, state.SsWorkDate, as_apvuser_id);
                            
                        }
                        else
                        {
                            int result = ndept.of_apv_permiss(state.SsWsPass, as_apv_docno, state.SsWorkDate, as_apvuser_id);
                        }
                    }
                    else if ((Dw_Main.GetItemDecimal(i, "apv_status") == -1))
                    {
                        string sql = @"update 	dpdeptapprove
	                                set	 		apv_status 		= -1,
				                                approve_date	= {0}
	                                where 	coop_id			= {1}
	                                and		apv_docno 		= {2}";
                        string as_apv_docno = Dw_Main.GetItemString(i, "apv_docno");
                        sql = WebUtil.SQLFormat(sql,state.SsWorkDate,state.SsCoopId,as_apv_docno);
                        WebUtil.QuerySdt(sql);
                    
                    }
                }
                Dw_Main.Reset();
                Dw_Main.Retrieve(state.SsWorkDate, Convert.ToDecimal(ApvStatus.SelectedValue));
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
        }
    }
}