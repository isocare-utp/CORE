using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.ws_dlg_sl_editcollateral_master_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTERDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTER;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater3, this.DATA, "dsList");

            this.Register();
        }
        public void RetrieveList(string member_no)
        {
            String sql = @"   SELECT 
                                     LNCOLLMASTER.COLLMAST_REFNO,   
                                     LNCOLLMASTER.COLLMAST_NO,   
                                     LNCOLLMASTER.COLLMAST_DESC,   
                                     LNCOLLMASTER.LANDESTIMATE_AMT,   
                                     LNCOLLMASTER.COLLMASTTYPE_CODE,   
                                     LNCOLLMASTER.HOUSEESTIMATE_AMT,   
                                     LNCOLLMASTER.ESTIMATE_PRICE,   
                                     LNCOLLMASTER.MORTGAGE_PRICE,   
                                     LNCOLLMASTER.MORTGAGE_DATE,   
                                     LNCOLLMASTER.EXPIRE_DATE,   
                                     LNCOLLMASTER.REDEEM_FLAG,   
                                     LNCOLLMASTER.REDEEM_DATE,   
                                     LNCOLLMASTER.REMARK,   
                                     LNCOLLMASTER.BLINDLAND_FLAG,   
                                     LNCOLLMASTER.LANDSIDE_NO,   
                                     LNCOLLMASTER.MEMBER_NO,   
                                     LNCOLLMASTER.COLLRELATION_CODE,   
                                     0.00 as colluse_amt  
                                FROM LNCOLLMASTER,   
                                     LNCOLLMASTMEMCO  
                               WHERE ( LNCOLLMASTER.COLLMAST_NO = LNCOLLMASTMEMCO.COLLMAST_NO ) and  
                                     ( ( LNCOLLMASTMEMCO.MEMCO_NO = {0} ) )        
";

            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}