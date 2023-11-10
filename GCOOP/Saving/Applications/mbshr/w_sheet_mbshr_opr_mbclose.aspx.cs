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
//using CoreSavingLibrary.WcfNCommon;
//using CoreSavingLibrary.WcfShrlon;

using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNShrlon;
using Sybase.DataWindow;
//using DBAccess;

namespace Saving.Applications.mbshr
{
    public partial class w_sheet_mbshr_opr_mbclose : PageWebSheet, WebSheet
    {
        public String pbl = "mb_close_mems.pbl";
        public String sql = "";
        public DataTable dt;
        public String ls_member_no = "";
        //================================
        public void InitJsPostBack()
        {

        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                InitDataWindow();
            }
            else
            {
                this.RestoreContextDw(Dw_main);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void SaveWebSheet()
        {
            if (Dw_main.RowCount > 0)
            {
                SaveDataWindow();
                Dw_main.Reset();
                InitDataWindow();
            }
        }

        public void WebSheetLoadEnd()
        {
            Dw_main.SaveDataCache();
            
        }

        private void InitDataWindow()
        {
            try 
            {
                DwUtil.RetrieveDataWindow(Dw_main, pbl, null, state.SsCoopId);
                if (Dw_main.RowCount > 0)
                {
                    for (int i = 1; i <= Dw_main.RowCount; i++)
                    {
                        String ls_memberno = Dw_main.GetItemString(i, "member_no");
                        //ตรวจสอบยอดหุ้นคงเหลือ
                        sql = @"select sharemaster_status,sharestk_amt from shsharemaster 
                                where coop_id = '" + state.SsCoopId + "' and member_no = '" + ls_memberno + "'and sharemaster_status = 1 and sharestk_amt > 0";

                        DataTable dt = WebUtil.Query(sql);
                        if (dt.Rows.Count > 0)
                        {
                            Dw_main.SetItemDecimal(i, "count_shr", Convert.ToDecimal(dt.Rows[0]["sharestk_amt"].ToString()));
                        }
                        else
                        {
                            Dw_main.SetItemDecimal(i, "count_shr", 0);
                        }

                        //ตรวจสอบยอดหนี้คงเหลือ
                        sql = @"select count(loancontract_no) from lncontmaster 
                        where coop_id = '" + state.SsCoopId + "' and member_no = '" + ls_memberno + "'and principal_balance > 0 and contract_status > 0";
                        dt = WebUtil.Query(sql);
                        if (dt.Rows.Count > 0)
                        {
                            Dw_main.SetItemDecimal(i, "count_loan", Convert.ToDecimal(dt.Rows[0]["count(loancontract_no)"].ToString()));
                        }
                        else
                        {
                            Dw_main.SetItemDecimal(i, "count_loan", 0);
                        }


                        //ตรวจสอบข้อมูลเงินฝากคงเหลือ
                        sql = @"select count(deptaccount_no) from dpdeptmaster
                                where coop_id = '" + state.SsCoopId + "' and member_no = '" + ls_memberno + "' and deptclose_status = 0 and prncbal >0";
                        dt = WebUtil.Query(sql);
                        if (dt.Rows.Count > 0)
                        {
                            Dw_main.SetItemDecimal(i, "count_dept", Convert.ToDecimal(dt.Rows[0]["count(deptaccount_no)"].ToString()));
                        }
                        else
                        {
                            Dw_main.SetItemDecimal(i, "count_dept", 0);
                        }

                        Decimal count_shr, count_dept, count_loan;
                        count_shr = Dw_main.GetItemDecimal(i, "count_shr");
                        count_dept = Dw_main.GetItemDecimal(i, "count_dept");
                        count_loan = Dw_main.GetItemDecimal(i, "count_loan");

                        if (count_shr == 0 && count_dept == 0 && count_loan == 0)
                        {
                            Dw_main.SetItemDecimal(i, "close_status", 1);
                        }
                        else
                        {
                            Dw_main.SetItemDecimal(i, "close_status", 0);
                        }
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไม่พบข้อมูลสมาชิกที่ลาออก");
                }
                    
               
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }


        private void SaveDataWindow()
        {
            try 
            {
                if (Dw_main.RowCount > 0)
                {
                    Dw_main.SetFilter("close_status = 1");
                    Dw_main.Filter();
                    for (int i = 1; i <= Dw_main.RowCount; i++)
                    {
                        Decimal close_status = Dw_main.GetItemDecimal(i, "close_status");
                        ls_member_no = Dw_main.GetItemString(i,"member_no");
                        if (close_status == 1)
                        { 
                            sql = @"update mbmembmaster set member_status = -1, pausekeep_flag = 1 
                                    where member_no = '"+ ls_member_no+ "' and coop_id = '"+state.SsCoopId +"'";
                            dt = WebUtil.Query(sql);
                        }
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิกที่จะปิดบัญชี");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

       
    }
}
