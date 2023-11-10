using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_reqchg_sequest_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.YRREQCHGDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.YRREQCHG;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void RetrieveMain(string member_no)
        {
            string sql = @"
            SELECT MBMEMBMASTER.COOP_ID,
                MBMEMBMASTER.MEMBER_NO,
                MBUCFPRENAME.PRENAME_DESC || MBMEMBMASTER.MEMB_NAME || ' ' || MBMEMBMASTER.MEMB_SURNAME as cp_membname,
                MBUCFMEMBGROUP.MEMBGROUP_CODE || ' - ' || MBUCFMEMBGROUP.MEMBGROUP_DESC as cp_membgroup,
                1 as sorter
            FROM 
	            MBMEMBMASTER,
	            MBUCFPRENAME,	
                MBUCFMEMBGROUP
            WHERE ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE )
                AND ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID )
                AND ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE )
                AND ( MBMEMBMASTER.COOP_ID = {0} )
	            AND ( MBMEMBMASTER.MEMBER_NO = {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}