using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.DT_DETAILDataTable DATA { get; set; }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAIL;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Register();
        }

        public void DdAssettype()
        {
            string sql = @" 
                SELECT assettype_code,   
                assettype_desc,   
                assettype_prefix  
                FROM lnucfassettype  order by assettype_code ASC";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assettype_code", "assettype_desc", "assettype_code");
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,
                mrtgmast_no,   
                member_no,   
                collmate_flag,   
                collmate_memno,   
                assettype_code,   
                mortgage_type,   
                mortgage_landnum,   
                mortgage_partname,   
                mortgage_partamt,   
                mortgage_partall,   
                mortgage_date,
                mortgage_grtname,   
                interest_rate,   
                mortgagefirst_amt,   
                mortgagesum_amt,   
                land_office,   
                mrtgmast_status,   
                entry_id  
                from lnmrtgmaster  
                where coop_id = {0}
                and mrtgmast_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}