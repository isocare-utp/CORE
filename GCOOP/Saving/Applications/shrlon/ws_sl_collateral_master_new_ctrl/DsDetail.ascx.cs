using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.LNCOLLMASTERDataTable DATA { get; set; }
        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTER;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Button.Add("b_landsideno");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void DdCollmasttype()
        {
            string sql = @" 
                  SELECT LNUCFCOLLMASTTYPE.COLLMASTTYPE_CODE,   
                         LNUCFCOLLMASTTYPE.COLLMASTTYPE_DESC,
                         ( LNUCFCOLLMASTTYPE.COLLMASTTYPE_CODE||':'|| LNUCFCOLLMASTTYPE.COLLMASTTYPE_DESC) as  COLLMAST_DESC ,
                         LNUCFCOLLMASTTYPE.DOCUMENT_CODE,   
                         LNUCFCOLLMASTTYPE.COLLDOC_PREFIX  
                    FROM LNUCFCOLLMASTTYPE  
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_code", "COLLMAST_DESC", "COLLMASTTYPE_CODE");

        }
        public void DdCollrelation()
        {
            string sql = @" 
                  SELECT MBUCFGAINCONCERN.CONCERN_CODE,   
                         MBUCFGAINCONCERN.GAIN_CONCERN  ,
                         (MBUCFGAINCONCERN.CONCERN_CODE||':'|| MBUCFGAINCONCERN.GAIN_CONCERN  )as CONCERN
                    FROM MBUCFGAINCONCERN     
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collrelation_code", "CONCERN", "CONCERN_CODE");

        }


    }
}