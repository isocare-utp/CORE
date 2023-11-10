using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_fundcoll_payment_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.FUNDCOLLMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve(string memberNo)
        {
            string sql = @"select  ft_getmemname(fundcollmaster.coop_id, fundcollmaster.member_no) as fullname,
                            trim(mbmembmaster.membgroup_code) as membgroup_code,
                            fundcollmaster.*
                            from fundcollmaster, mbmembmaster
                            where fundcollmaster.coop_id = mbmembmaster.coop_id
                            and fundcollmaster.member_no = mbmembmaster.member_no                            
                            and fundcollmaster.coop_id = {0}
                            and fundcollmaster.member_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdMoneytype()
        {
            string sql = @" select moneytype_code, moneytype_desc, sort_order from cmucfmoneytype
                    where moneytype_status = 'DAY' order by sort_order";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "moneytype_code", "moneytype_desc", "moneytype_code");
        }

        public void DdTofromaccid(string moneytypeCode)
        {
            string sql = @"select * from cmucfmoneytype where moneytype_code = {0}";
            sql = WebUtil.SQLFormat(sql, moneytypeCode);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "defaultpay_accid", "defaultpay_accid");
        }
    }
}