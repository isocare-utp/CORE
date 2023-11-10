using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_reqchg_sequest_ccl_ctrl
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

        public void RetrieveMain(string reqchg_docno)
        {
            string sql = @"select yrreqchg.coop_id,   
	            yrreqchg.reqchg_docno,   
	            yrreqchg.div_year,   
	            yrreqchg.member_no,   
	            yrreqchg.reqchg_date,   
	            yrreqchg.reqchg_status,   
	            yrreqchg.entry_id,   
	            yrreqchg.entry_bycoopid,   
	            yrreqchg.entry_date,   
	            yrreqchg.cancel_id,   
	            yrreqchg.cancel_bycoopid,   
	            yrreqchg.cancel_date,   
	            yrreqchg.remark,
	            mbucfprename.prename_desc + mbmembmaster.memb_name + ' ' + mbmembmaster.memb_surname as cp_membname,
	            mbucfmembgroup.membgroup_code + ' - ' + mbucfmembgroup.membgroup_desc as cp_membgroup
            from yrreqchg,
	            mbmembmaster,
	            mbucfprename,	
	            mbucfmembgroup
            where ( yrreqchg.coop_id = mbmembmaster.coop_id )
	            and ( yrreqchg.member_no = mbmembmaster.member_no )
	            and ( mbmembmaster.prename_code = mbucfprename.prename_code )
	            and ( mbmembmaster.coop_id = mbucfmembgroup.coop_id )
	            and ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code )
	            and ( yrreqchg.coop_id = {0} )
	            and ( yrreqchg.reqchg_docno = {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, reqchg_docno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}