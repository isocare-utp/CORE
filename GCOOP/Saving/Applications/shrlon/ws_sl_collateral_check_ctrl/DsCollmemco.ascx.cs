using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_check_ctrl
{
    public partial class DsCollmemco : DataSourceRepeater
    {
        public DataSet1.LNCOLLMASTMEMCODataTable DATA { get; set; }

        public void InitDsCollmemco(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTMEMCO;
            this.EventItemChanged = "OnDsCollmemcoItemChanged";
            this.EventClicked = "OnDsCollmemcoClicked";
            this.InitDataSource(pw, Repeater3, this.DATA, "dsCollmemco");
            this.Register();
        }
        public void RetrieveDsCollmemco(String collmast_no)
        {
            String sql = @" SELECT LNCOLLMASTMEMCO.COLLMAST_NO,   
                                     LNCOLLMASTMEMCO.MEMCO_NO,   
                                     LNCOLLMASTMEMCO.COLLMASTMAIN_FLAG,   
                                     MBUCFPRENAME.PRENAME_SHORT,   
                                     MBMEMBMASTER.MEMB_NAME,   
                                     MBMEMBMASTER.MEMB_SURNAME  
                                FROM LNCOLLMASTMEMCO,   
                                     MBMEMBMASTER,   
                                     MBUCFPRENAME  
                               WHERE ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                                     ( LNCOLLMASTMEMCO.MEMCO_NO = MBMEMBMASTER.MEMBER_NO ) and  
                                     ( ( lncollmastmemco.collmast_no = {0} ) )   ";

            sql = WebUtil.SQLFormat(sql, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}