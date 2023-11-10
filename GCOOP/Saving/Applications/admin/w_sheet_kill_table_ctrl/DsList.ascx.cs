using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.admin.w_sheet_kill_table_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.dm_tran_locksDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.dm_tran_locks;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("B_DEL");
            this.Register();

        }

        public void retrieve()
        {
            string sql = @"SELECT TOP 100 OBJECT_NAME(P.OBJECT_ID) AS TABLENAME
                , RESOURCE_TYPE
                , RESOURCE_DESCRIPTION
                ,request_session_id
                FROM SYS.DM_TRAN_LOCKS L
                JOIN SYS.PARTITIONS P 
                ON L.RESOURCE_ASSOCIATED_ENTITY_ID = P.HOBT_ID
                order by request_session_id";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}