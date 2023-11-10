using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_constant_ucfmethpay_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.YRUCFMETHPAYDataTable DATA { get; set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.YRUCFMETHPAY;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"   SELECT YRUCFMETHPAY.METHPAYTYPE_CODE,   
                                     YRUCFMETHPAY.METHPAYTYPE_DESC,   
                                     YRUCFMETHPAY.SIGN_FLAG,   
                                     YRUCFMETHPAY.METHPAYTYPE_SORT,   
                                     YRUCFMETHPAY.METHPAYSTM_ITEMTYPE,   
                                     YRUCFMETHPAY.SHOWLIST_FLAG,   
                                     YRUCFMETHPAY.JOIN_MONEYTYPE_CODE,   
                                     YRUCFMETHPAY.COOP_ID,   
                                     YRUCFMETHPAY.PAYNXTDIV_FLAG  
                                FROM YRUCFMETHPAY    
                            order by METHPAYTYPE_SORT
";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}   