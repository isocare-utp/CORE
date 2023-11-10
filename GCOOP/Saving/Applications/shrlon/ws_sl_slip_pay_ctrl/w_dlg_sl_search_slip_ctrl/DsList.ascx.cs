using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_search_slip_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SLSLIPPAYINDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.SLSLIPPAYIN;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string member_no, string payinslip_no, string memb_name, string memb_surname, string membgroup_code, DateTime slip_date_s, DateTime slip_date_e)
        {
            string sqlext = "";
            if (member_no != "")
            {
                sqlext += " and slslippayin.member_no = '" + member_no + "'";
            }
            if (payinslip_no != "")
            {
                sqlext += " and slslippayin.payinslip_no = '" + payinslip_no + "'";
            }
            if (memb_name != "")
            {
                sqlext += " and slslippayin.memb_name = '" + memb_name + "'";
            }
            if (memb_surname != "")
            {
                sqlext += " and slslippayin.memb_surname = '" + memb_surname + "'";
            }
            if (membgroup_code != "")
            {
                sqlext += " and slslippayin.membgroup_code = '" + membgroup_code + "'";
            }
            //if (slip_date_s.Year > 1900 && slip_date_e.Year > 1900)
            //{
            //    sqlext += " and slslippayin.slip_date between {1} and {2} ";

            //}
            string sql = @"  select MBUCFPRENAME.PRENAME_DESC,MBMEMBMASTER.MEMB_NAME,MBMEMBMASTER.MEMB_SURNAME,   
                            MBMEMBMASTER.MEMBGROUP_CODE, slslippayin.* 
                            from slslippayin,MBMEMBMASTER,MBUCFPRENAME
                            WHERE MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE and  
                            slslippayin.MEMBER_NO = MBMEMBMASTER.MEMBER_NO and  
                            MBMEMBMASTER.COOP_ID = slslippayin.MEMCOOP_ID  and 
                            slslippayin.sliptype_code in ('PX','HPX','LRT','YDI') and        
                            slslippayin.COOP_ID = {0} " + sqlext +
                            " order by slslippayin.payinslip_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, slip_date_s);
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}