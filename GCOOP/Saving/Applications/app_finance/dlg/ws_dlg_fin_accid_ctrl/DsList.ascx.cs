using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.app_finance.dlg.ws_dlg_fin_accid_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FINUCFITEMTYPEDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FINUCFITEMTYPE;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
        public void RetrieveDetail(string ls_coopid,string ls_sqlext)
        {
            string sql = @"SELECT FINUCFITEMTYPE.SLIPITEMTYPE_CODE,   
                         FINUCFITEMTYPE.ITEM_DESC,   
                         FINUCFITEMTYPE.ACCNATURE_FLAG,   
                         FINUCFITEMTYPE.ACCMAP_CODE,   
                         FINUCFITEMTYPE.GENVC_FLAG,   
                         FINUCFITEMTYPE.ACCOUNT_ID,   
                         FINUCFITEMTYPE.COOP_ID  
                    FROM FINUCFITEMTYPE   
                where COOP_ID='" + ls_coopid + "'" + ls_sqlext + " order by SLIPITEMTYPE_CODE";
            //sql = WebUtil.SQLFormat(sql, ls_coopid);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}