using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.app_finance.dlg.ws_dlg_fin_extmember_search_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();

        }
        public void RetrieveDetail(string ls_coopid)
        {
            string sql = @"SELECT FINCONTACKMASTER.CONTACK_NO,   
                     FINCONTACKMASTER.FIRST_NAME,   
                     FINCONTACKMASTER.LAST_NAME,   
                     ( case when  LEN(FINCONTACKMASTER.TAX_ID) > 0 then FINCONTACKMASTER.TAX_ID else 'ไม่ระบุ' end )TAX_ID,      
                     MBUCFPRENAME.PRENAME_DESC,   
                     FINCONTACKMASTER.COOP_ID ,
		            MBUCFPRENAME.PRENAME_DESC +FINCONTACKMASTER.FIRST_NAME+'  '+FINCONTACKMASTER.LAST_NAME as fullname  
                FROM FINCONTACKMASTER,   
                     MBUCFPRENAME  
               WHERE ( FINCONTACKMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                     ( ( FINCONTACKMASTER.COOP_ID = {0}) )  
                      order by FINCONTACKMASTER.CONTACK_NO ";
            sql = WebUtil.SQLFormat(sql, ls_coopid);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
          
    }
}