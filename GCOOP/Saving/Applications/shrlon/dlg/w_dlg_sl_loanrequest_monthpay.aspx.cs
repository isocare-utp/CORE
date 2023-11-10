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
using DataLibrary;

namespace Saving.Applications.shrlon.dlg
{
    public partial class w_dlg_sl_loanrequest_monthpay : PageWebDialog, WebDialog
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
            str_itemchange strList = new str_itemchange();
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
            this.ConnectSQLCA();
            if (IsPostBack)
            {
                this.RestoreContextDw(dw_head);
                this.RestoreContextDw(dw_detail);
            }
            else
            {
                strList = WebUtil.nstr_itemchange_session(this);
                String xmlHead = "";
                String xmlDetail = "";
                //income=" + income + "&paymonth=" + paymonth + "&member_no=" + member_no + "&
                //salary_amt=" + salary_amt + "&paymonth_coop_2=" + paymonth_coop_2 + "&principal_balance=" + principal_balance);

                Decimal income = Convert.ToDecimal(Request["income"].ToString());
                Decimal paymonth = Convert.ToDecimal(Request["paymonth"].ToString());
                Decimal salary_amt = Convert.ToDecimal(Request["salary_amt"].ToString());
                Decimal paymonth_coop_2 = Convert.ToDecimal(Request["paymonth_coop_2"].ToString());
                Decimal salbalance = 2000;

                string member_no = Request["member_no"].ToString();
                try
                {
                    shrlonService.of_createmthpaytab(state.SsWsPass, state.SsCoopId, strList.xml_main, strList.xml_clear, ref xmlHead, ref xmlDetail);
                    try
                    {
                        dw_head.Reset();
                        dw_head.ImportString(xmlHead, FileSaveAsType.Xml);
                    }
                    catch
                    {
                        dw_head.Reset(); dw_head.InsertRow(0);
                    }
                    try
                    {
                        dw_detail.Reset();
                        dw_detail.ImportString(xmlDetail, FileSaveAsType.Xml);

                        string operate_type = "";
                        decimal loanpayment_type = 1;
                        decimal period_payment = 0;

                    }
                    catch
                    {
                        dw_head.Reset(); dw_head.InsertRow(0);
                    }
                    t_salaryamt.Text = salary_amt.ToString("#,###,##0.00");
                    t_basesalbal.Text = salbalance.ToString("#,###,##0.00");
                    decimal itempayment_amt = dw_head.GetItemDecimal(1, "itempayment_amt");
                    t_paymonthcoop.Text = itempayment_amt.ToString("#,###,##0.00");
                    // t_paymonthcoop.Text = Convert.ToString(paymonth_coop_2 + paymonth);// paymonth_coop_2.ToString("#,###,##0.00");
                    //decimal salabal = salary_amt - paymonth_coop_2 - paymonth ;
                    decimal salabal = salary_amt - itempayment_amt;
                    t_salbal.Text = salabal.ToString("#,###,##0.00");
                    // int row = dw_detail.RowCount;
                    dw_head.SetItemDecimal(1, "itemincome_other", income);
                    dw_head.SetItemDecimal(1, "itempayment_oth", 0); //paymonth
                }
                catch
                {
                    dw_head.Reset(); dw_head.InsertRow(0);
                }
            }
        
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
            dw_head.SaveDataCache();
            dw_detail.SaveDataCache();

        }

        protected void OnCloseClick()
        {
            HfChkStatus.Value = "1";
        }

        protected void Text_Changed(object sender, EventArgs e)
        {
           // string percen = TextBox1.Text;
            Decimal income = Convert.ToDecimal(Request["income"].ToString());
            Decimal paymonth = Convert.ToDecimal(Request["paymonth"].ToString());

            Decimal salary_amt = Convert.ToDecimal(t_salaryamt.Text);// dw_head.GetItemDecimal(1, "salary_amt");
            Decimal itemincome_amt = dw_head.GetItemDecimal(1, "itemincome_amt");
            Decimal itemincome_other = dw_head.GetItemDecimal(1, "itemincome_other");
            int i = 0;
            Decimal period_payment = 0, intestimate_amt = 0;
            Decimal itempayment_amt = 0;
            decimal minsalary_bal = Convert.ToDecimal(t_basesalbal.Text);
            for (i = 1; i <= dw_detail.RowCount; i++)
            {
                period_payment = dw_detail.GetItemDecimal(i, "period_payment");
                intestimate_amt = dw_detail.GetItemDecimal(i, "intestimate_amt");
                itempayment_amt += period_payment + intestimate_amt;

            }


            //dw_head.GetItemDecimal(1, "itempayment_amt");
            Decimal itempayment_oth = dw_head.GetItemDecimal(1, "itempayment_oth");
            Decimal emer_loop = dw_head.GetItemDecimal(1, "emer_loop");
            // t_salaryamt.Text = salary_amt.ToString("#,###,#00.00");
            salary_amt = salary_amt - minsalary_bal ; //salary_amt * Convert.ToDecimal(percen) / 100;
            t_basesalbal.Text = salary_amt.ToString("#,###,#00.00");
            Decimal total = salary_amt + itemincome_amt + itemincome_other - itempayment_amt - itempayment_oth + emer_loop;
            t_salbal.Text = total.ToString("#,###,#00.00");
            t_paymonthcoop.Text = (itempayment_amt - itempayment_oth + emer_loop).ToString("#,###,#00.00");



        }

        private void of_gennew()
        {
            string percen = TextBox1.Text;
            Decimal income = Convert.ToDecimal(Request["income"].ToString());
            Decimal paymonth = Convert.ToDecimal(Request["paymonth"].ToString());
            Decimal salary_amt = Convert.ToDecimal(t_salaryamt.Text);  //dw_head.GetItemDecimal(1, "salary_amt");
            Decimal itemincome_amt = dw_head.GetItemDecimal(1, "itemincome_amt");
            Decimal itemincome_other = dw_head.GetItemDecimal(1, "itemincome_other");
            int i = 0;
            Decimal period_payment = 0, intestimate_amt = 0;
            Decimal itempayment_amt = 0;
            for (i = 1; i <= dw_detail.RowCount; i++)
            {
                period_payment = dw_detail.GetItemDecimal(i, "itempayment_amt");
                itempayment_amt += period_payment  ;

            }

            //  Decimal itempayment_amt = dw_head.GetItemDecimal(1, "itempayment_amt");
            Decimal itempayment_oth = dw_head.GetItemDecimal(1, "itempayment_oth");
            Decimal emer_loop = dw_head.GetItemDecimal(1, "emer_loop");
            t_salaryamt.Text = salary_amt.ToString("#,###,#00.00");
            //salary_amt = salary_amt;// *Convert.ToDecimal(percen) / 100;
            t_basesalbal.Text = "2000";// salary_amt.ToString("#,###,#00.00");
            Decimal total = salary_amt + itemincome_amt + itemincome_other - itempayment_amt - itempayment_oth ;
            t_salbal.Text = total.ToString("#,###,#00.00");
            t_paymonthcoop.Text = itempayment_amt.ToString("#,###,#00.00");
        
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string percen = TextBox1.Text;
            Decimal income = Convert.ToDecimal(Request["income"].ToString());
            Decimal paymonth = Convert.ToDecimal(Request["paymonth"].ToString());
            Decimal salary_amt = Convert.ToDecimal(t_salaryamt.Text);// dw_head.GetItemDecimal(1, "salary_amt");
            Decimal itemincome_amt = dw_head.GetItemDecimal(1, "itemincome_amt");
            Decimal itemincome_other = dw_head.GetItemDecimal(1, "itemincome_other");
            int i = 0;
            Decimal period_payment = 0, intestimate_amt = 0;
            Decimal itempayment_amt = 0;
            for (i = 1; i <= dw_detail.RowCount; i++)
            {
                period_payment = dw_detail.GetItemDecimal(i, "period_payment");
                intestimate_amt = dw_detail.GetItemDecimal(i, "intestimate_amt");
                itempayment_amt += period_payment + intestimate_amt;

            }

            //  Decimal itempayment_amt = dw_head.GetItemDecimal(1, "itempayment_amt");
            Decimal itempayment_oth = dw_head.GetItemDecimal(1, "itempayment_oth");
            Decimal emer_loop = dw_head.GetItemDecimal(1, "emer_loop");
            t_salaryamt.Text = salary_amt.ToString("#,###,#00.00");
            //salary_amt = salary_amt;// *Convert.ToDecimal(percen) / 100;
            t_basesalbal.Text = "2000";// salary_amt.ToString("#,###,#00.00");
            Decimal total = salary_amt + itemincome_amt + itemincome_other - itempayment_amt - itempayment_oth + emer_loop;
            t_salbal.Text = total.ToString("#,###,#00.00");
            t_paymonthcoop.Text = (itempayment_amt - itempayment_oth).ToString("#,###,#00.00");
        }


    }
}
