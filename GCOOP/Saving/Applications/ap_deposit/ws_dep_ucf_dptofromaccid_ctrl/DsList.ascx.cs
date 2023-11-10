using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_ucf_dptofromaccid_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPUCFTOFROMACCIDDataTable DATA { get; private set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFTOFROMACCID;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();

        }
        public void DdMoneytype(string moneytype_code)
        {
            string sql = @" SELECT moneytype_code AS VALUE_CODE,   
                            moneytype_desc AS VALUE_DESC,
                            1 as sort
                           from CMUCFMONEYTYPE where  moneytype_code = {0}";
            sql = WebUtil.SQLFormat(sql, moneytype_code);
            DataTable dt = WebUtil.Query(sql );
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.DefaultView.Sort = "sort asc, value_code asc";
            this.DropDownDataBind(dt, "cash_type", "display", "value_code");
        }
        public void DS_Accountid()
        {
            string sql = @" SELECT ACCOUNT_ID,   
                        ACCOUNT_NAME as type_desc
                        FROM ACCMASTER  
                        WHERE ACCOUNT_TYPE_ID = 3  and  coop_id={0} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            dt.Columns.Add("sort", typeof(System.Int32));
            foreach (DataRow row in dt.Rows)
            {
                string ls_name = row["ACCOUNT_ID"].ToString() + " - " + row["type_desc"].ToString();
                row["display"] = ls_name;
                row["sort"] = 1;
            }
            dt.Rows.Add(new Object[] { "", "", "--กรุณาเลือก--", 0 });
            dt.DefaultView.Sort = "sort asc,ACCOUNT_ID";
            dt = dt.DefaultView.ToTable();

            this.DropDownDataBind(dt, "account_id", "display", "account_id");
        }

        public void RetrieveList(string cash_type)
        {

            string sql = @"select dpucftofromaccid.cash_type,
                                dpucftofromaccid.Account_Id,
		                        dpucftofromaccid.account_Desc
                                from dpucftofromaccid
                            where  dpucftofromaccid.coop_id = {0} and
                            dpucftofromaccid.cash_type = {1}
                            order by dpucftofromaccid.cash_type";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, cash_type);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}