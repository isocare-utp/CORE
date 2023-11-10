using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
namespace Saving.Applications.ap_deposit.ws_dp_sheet_cancel_deptitemtype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_save");           
            this.Register();
        }

        public void DDDeptitemtype()
        {
            string sql = @" select 
	                            deptitemtype_code,   
	                            deptitemtype_code+' - ' + deptitemtype_desc as display  ,
	                            1 as sorter
                            from dpucfdeptitemtype
                            where dpucfdeptitemtype.coop_id ={0}
                            union
                            select '00','--เลือกรหัสที่ต้องการยกเลิก--',0  order by sorter,deptitemtype_code";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "system_code", "display", "deptitemtype_code");

        }
    }
}