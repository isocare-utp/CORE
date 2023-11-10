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
    public partial class DsCollmast : DataSourceFormView
    {
        public DataSet1.LNCOLLMASTERDataTable DATA { get; set; }
        public void InitDsCollmast(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCOLLMASTER;
            this.EventItemChanged = "OnDsCollmastItemChanged";
            this.EventClicked = "OnDsCollmastClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsCollmast");

            this.Register();
        }
        public void RetrieveDsCollmast(String collmast_no)
        {
            String sql = @"  SELECT LNCOLLMASTER.COLLMAST_NO,   
                                     LNCOLLMASTER.MEMBER_NO,   
                                     LNCOLLMASTER.COLLMAST_REFNO,   
                                     LNCOLLMASTER.COLLMASTTYPE_CODE,   
                                     LNCOLLMASTER.COLLMAST_DESC,   
                                     LNCOLLMASTER.LANDESTIMATE_AMT,   
                                     LNCOLLMASTER.HOUSEESTIMATE_AMT,   
                                     LNCOLLMASTER.ESTIMATE_PRICE,   
                                     LNCOLLMASTER.MORTGAGE_PRICE,   
                                     LNCOLLMASTER.MORTGAGE_DATE,   
                                     LNCOLLMASTER.EXPIRE_DATE,   
                                     LNCOLLMASTER.REDEEM_FLAG,   
                                     LNCOLLMASTER.REDEEM_DATE,   
                                     LNCOLLMASTER.REMARK,   
                                     LNCOLLMASTER.LAND_NO,   
                                     LNCOLLMASTER.SURVEY_NO,   
                                     LNCOLLMASTER.BOOK_NO,   
                                     LNCOLLMASTER.PAGE_NO,   
                                     LNCOLLMASTER.GROUP_NO,   
                                     LNCOLLMASTER.COOP_ID  
                                FROM LNCOLLMASTER  
                               WHERE lncollmaster.collmast_no = {0}   ";

            sql = WebUtil.SQLFormat(sql, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdCollmasttype()
        {
            string sql = @" 
                      SELECT LNUCFCOLLMASTTYPE.COLLMASTTYPE_CODE,   
                             LNUCFCOLLMASTTYPE.COLLMASTTYPE_DESC,   
                             LNUCFCOLLMASTTYPE.DOCUMENT_CODE,   
                             LNUCFCOLLMASTTYPE.COLLDOC_PREFIX  
                        FROM LNUCFCOLLMASTTYPE    
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "collmasttype_code", "COLLMASTTYPE_DESC", "COLLMASTTYPE_CODE");

        }
    }
}