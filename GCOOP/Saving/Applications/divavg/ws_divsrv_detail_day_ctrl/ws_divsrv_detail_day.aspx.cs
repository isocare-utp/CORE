using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.divavg.ws_divsrv_detail_day_ctrl
{
    public partial class ws_divsrv_detail_day : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public String PostMemberNo { get; set; }
        [JsPostBack]
        public String PostDivYear { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMaster.InitDsMaster(this);
            dsMethodpayment.InitMethodpayment(this);
            dsStatement.InitStatement(this);
            dsShrday.InitShrday(this);
            dsLoan.InitLoan(this);
        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                dsMaster.ResetRow();
                dsMethodpayment.ResetRow();
                dsStatement.ResetRow();
                dsShrday.ResetRow();
                dsLoan.ResetRow();

                String ls_member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                dsMain.RetrieveMain(ls_member_no);
                dsMain.DdDivYear(ls_member_no);
                dsMethodpayment.DdMethpaytype();
            }
            else if (eventArg == PostDivYear)
            {
                String ls_member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                String ls_year = dsMain.DATA[0].DIV_YEAR;
                dsMaster.RetrieveMaster(ls_member_no, ls_year);
                dsMethodpayment.RetrieveMethodpayment(ls_member_no, ls_year);
                dsStatement.RetrieveStatement(ls_member_no, ls_year);
                //string ls_sql = "select divpercent_rate * 100 as div_rate from yrcfrate where div_year = " + ls_year;
                //Sdt dt = WebUtil.QuerySdt(ls_sql);
                //if (dt.Next())
                //{
                //    dsShrmonth.getdiv_rate.Text = dt.GetDecimal("div_rate").ToString("#,##0.00");
                //}
                dsShrday.RetrieveShrday(ls_member_no, ls_year);
                dsLoan.RetrieveLoan(ls_member_no, ls_year);
                dsMethodpayment.DdMethpaytype();
                dsStatement.DdDivitemtype();
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