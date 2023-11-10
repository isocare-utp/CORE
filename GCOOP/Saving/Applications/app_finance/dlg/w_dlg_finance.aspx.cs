using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

using System.Text;
using DataLibrary;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_finance : PageWebDialog, WebDialog
    {
        

        protected String postSumMoney;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            postSumMoney = WebUtil.JsPostBack(this, "postSumMoney");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {//this.ConnectSQLCA();
                //dw_main.SetTransaction(sqlca);
                dw_main.InsertRow(0);
                //dw_main.SetItemString(1, "sumb1000", "0");
            }
            else
            {
                this.RestoreContextDw(dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            //String HColumName;
           // ColumnName = HColumName.Value;

            if (eventArg == "postSumMoney")
            {
                sumMoney();
            }
        }

        public void WebDialogLoadEnd()
        {
            dw_main.SaveDataCache();
        }
        protected void sumMoney()
        {
            String money = HColumName.Value;
            decimal sum, amount;
            sum = 0;
            amount = 0;           
                if (money == "b1000")
                {
                    int b1000 = 0;
                    b1000 = Convert.ToInt16(dw_main.GetItemDecimal(1, "b1000"));
                    sum = b1000 * 1000;
                    dw_main.SetItemDecimal(1, "sumb1000", sum);
                }
                else if (money == "b500")
                {
                    int b500 = 0;
                    b500 = Convert.ToInt16(dw_main.GetItemDecimal(1, "b500"));
                    sum = b500 * 500;
                    dw_main.SetItemDecimal(1, "sumb500", sum);
                }
                else if (money == "b100")
                {
                    int b100 = 0;
                    b100 = Convert.ToInt16(dw_main.GetItemDecimal(1, "b100"));
                    sum = b100 * 100;
                    dw_main.SetItemDecimal(1, "sumb100", sum);
                }
                else if (money == "b50")
                {
                    int b50 = 0;
                    b50 = Convert.ToInt16(dw_main.GetItemDecimal(1, "b50"));
                    sum = b50 * 50;
                    dw_main.SetItemDecimal(1, "sumb50", sum);
                }
                else if (money == "b20")
                {
                    int b20 = 0;
                    b20 = Convert.ToInt16(dw_main.GetItemDecimal(1, "b20"));
                    sum = b20 * 20;
                    dw_main.SetItemDecimal(1, "sumb20", sum);
                }
                else if (money == "c10")
                {
                    int c10 = 0;
                    c10 = Convert.ToInt16(dw_main.GetItemDecimal(1, "c10"));
                    sum = c10 * 10;
                    dw_main.SetItemDecimal(1, "sumc10", sum);
                }
                else if (money == "c5")
                {
                    int c5 = 0;
                    c5 = Convert.ToInt16(dw_main.GetItemDecimal(1, "c5"));
                    sum = c5 * 5;
                    dw_main.SetItemDecimal(1, "sumc5", sum);
                }
                else if (money == "c2")
                {
                    int c2 = 0;
                    c2 = Convert.ToInt16(dw_main.GetItemDecimal(1, "c2"));
                    sum = c2 * 2;
                    dw_main.SetItemDecimal(1, "sumc2", sum);
                }
                else if (money == "c1")
                {
                    int c1 = 0;
                    c1 = Convert.ToInt16(dw_main.GetItemDecimal(1, "c1"));
                    sum = c1 * 1;
                    dw_main.SetItemDecimal(1, "sumc1", sum);
                }
                else if (money == "c50")
                {
                    double c50; double sum1;
                    c50 = Convert.ToInt32(dw_main.GetItemDecimal(1, "c50"));
                    sum1 = c50 * 0.5;
                    dw_main.SetItemDouble(1, "sumc50", sum1);
                }
                else if (money == "c25")
                {
                    double c25; double sum1;
                    c25 = Convert.ToInt32(dw_main.GetItemDecimal(1, "c25"));
                    sum1 = c25 * 0.25;
                    dw_main.SetItemDouble(1, "sumc25", sum1);
                }
                decimal b1 = dw_main.GetItemDecimal(1, "sumb1000");
                decimal b2 = dw_main.GetItemDecimal(1, "sumb500");
                decimal b3 = dw_main.GetItemDecimal(1, "sumb100");
                decimal b4 = dw_main.GetItemDecimal(1, "sumb50");
                decimal b5 = dw_main.GetItemDecimal(1, "sumb20");
                decimal b6 = dw_main.GetItemDecimal(1, "sumc10");
                decimal b7 = dw_main.GetItemDecimal(1, "sumc5");
                decimal b8 = dw_main.GetItemDecimal(1, "sumc2");
                decimal b9 = dw_main.GetItemDecimal(1, "sumc1");
                decimal b10 = dw_main.GetItemDecimal(1, "sumc50");
                decimal b11 = dw_main.GetItemDecimal(1, "sumc25");
                amount = b1+b2+b3+b4+b5+b6+b7+b8+b9+b10+b11;
                dw_main.SetItemDecimal(1, "amount",amount);
                
        }
       
        #endregion
        
    }
}