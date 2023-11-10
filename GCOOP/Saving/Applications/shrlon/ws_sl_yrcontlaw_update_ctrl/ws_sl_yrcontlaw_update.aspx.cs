using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.shrlon.ws_sl_yrcontlaw_update_ctrl
{
    public partial class ws_sl_yrcontlaw_update : PageWebSheet, WebSheet
    {

        [JsPostBack]
        public String Post { get; set; }
        [JsPostBack]
        public String PostCheck { get; set; }

        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                year.Text = Convert.ToString(Convert.ToDecimal(state.SsWorkDate.Year) + 543);
                //month.Text = Convert.ToDecimal(state.SsWorkDate.Month).ToString("00");//String.Format("{0:00}", value)

                of_checkstatus();
            }
        }        

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "Post")
            {
                try
                {
                    string ls_period = Hdperiod.Value;
                    ExecuteDataSource exe = new ExecuteDataSource(this);

                    String ls_sql = (@"update slclsmthbalance sl set sl.yrbizztype_status = 
                    ( select ln.contlaw_status from lncontmaster ln where sl.coop_id = ln.coop_id and sl.loancontract_no = ln.loancontract_no )
                    where sl.coop_id = {0}
                    and sl.clsmth_period = {1}
                    and exists ( select (1) from lncontmaster ln where sl.coop_id = ln.coop_id and sl.loancontract_no = ln.loancontract_no )");
                    ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_period);
                    Sta taupdate = new Sta(state.SsConnectionString);
                    int result = taupdate.Exe(ls_sql);

                    LtServerMessage.Text = WebUtil.CompleteMessage("ปรับปรุงสำเร็จ");
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if (eventArg == "PostCheck")
            {
                of_checkstatus();
            }
        }

        public void of_checkstatus()
        {
            string ls_period = "", ls_year = "", ls_mount = "";
            int li_mount = 0;
            String ls_sql = (@"select accend_date 
                from cmaccountyear 
                where coop_id = {0}
                and account_year = {1}");
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, year.Text);
            Sdt dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                ls_year = Convert.ToString(dt.GetDate("accend_date").Year + 543);
                ls_mount = dt.GetDate("accend_date").Month.ToString("00");
                ls_period = ls_year + ls_mount;
                li_mount = Convert.ToInt32(ls_mount);
                Hdperiod.Value = ls_period;
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบปีบัญชี " + year.Text + " กรุณาตรวจสอบ");
                this.SetOnLoadedScript("alert('ไม่พบปีบัญชี " + year.Text + " กรุณาตรวจสอบ')");
                return;
            }

            ls_sql = @"select count(1)
                from slclsmthbalance 
                where coop_id = {0}
                and clsmth_period = {1}";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_period);
            dt = WebUtil.QuerySdt(ls_sql);
            if (dt.Next())
            {
                int count = dt.GetInt32("count(1)");
                if (count == 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ระบบยังไม่ได้ปิดสิ้นเดือน " + WebUtil.ConvertMonthtoThai(li_mount) + " " + ls_year);
                    this.SetOnLoadedScript("alert('ระบบยังไม่ได้ปิดสิ้นเดือน " + WebUtil.ConvertMonthtoThai(li_mount) + " " + ls_year + "')");
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