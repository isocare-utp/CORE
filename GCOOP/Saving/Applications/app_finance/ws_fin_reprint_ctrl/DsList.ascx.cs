using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.app_finance.ws_fin_reprint_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DTFINSLIPDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTFINSLIP;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string member_no, string entry_id, decimal pay_recv_status, string slip_no_s, string slip_no_e, DateTime slip_date_s, DateTime slip_date_e)
        {
            string sqlext = "";
            if (member_no != "")
            {
                sqlext += " and member_no = '" + String.Format("{0:00000000}", Convert.ToDecimal(member_no)) + "'";
            }
            if (entry_id != "--กรุณาเลือก--")
            {
                sqlext += " and entry_id = '" + entry_id + "'";
            }
            sqlext += " and pay_recv_status = " + pay_recv_status + "";
            if (slip_no_s != "" && slip_no_e != "")
            {
                sqlext += " and (slslippayin.payinslip_no between '" + slip_no_s + "' and '" + slip_no_e + "')";
            }
            if (slip_date_s.Year > 1900 && slip_date_e.Year > 1900)
            {
                sqlext += " and operate_date between {1} and {2} ";

            }
            string sql = @" select 0 as checkselect, slip_no, operate_date, entry_id, from_system, member_no, item_amtnet, pay_towhom, 
                            payment_desc from finslip where (from_system = 'FIN' or (from_system='DEP' and itempaytype_code='FEE'))and      
                            coop_id = {0} and payment_status = 1 " + sqlext + "order by " + "slip_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, slip_date_s, slip_date_e);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}