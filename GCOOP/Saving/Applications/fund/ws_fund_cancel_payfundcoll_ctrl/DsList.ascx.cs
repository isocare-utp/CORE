using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_cancel_payfundcoll_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; private set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void RetriveData(string coop_control,DateTime return_date)
        {
            string sql = @"SELECT 
            MEMBER_NO, LOANCONTRACT_NO,OPERATE_DATE,
            dbo.ft_getmemname(COOP_ID,MEMBER_NO)  AS FULLNAME,
            ITEMPAY_AMT,ENTRY_ID,REFSLIP_NO
            FROM FUNDCOLLSTATEMENT 
            WHERE COOP_ID={0}
            AND OPERATE_DATE={1}
            AND ITEMTYPE_CODE= 'CLS'
            AND FUND_STATUS = 1";
            sql = WebUtil.SQLFormat(sql, coop_control, return_date);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}