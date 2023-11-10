using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_detail_ctrl
{
    public partial class DsStatement : DataSourceRepeater
    {
        public DataSet1.DT_STATEMENTDataTable DATA { get; set; }

        public void InitStatement(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_STATEMENT;
            this.EventItemChanged = "OnDsStatementItemChanged";
            this.EventClicked = "OnDsStatementClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsStatement");
            this.Register();
        }

        public void RetrieveStatement(String ls_member_no,String ls_year)
        {
            string sql = @"select yrdivstatement.coop_id,   
	            yrdivstatement.member_no,   
	            yrdivstatement.seq_no,   
	            yrdivstatement.div_year,   
	            yrdivstatement.divitemtype_code,  
	            yrucfdivitemtype.divitemtype_desc,
	            yrdivstatement.operate_date,   
	            yrdivstatement.slip_date,   
	            yrdivstatement.div_payment,   
	            yrdivstatement.avg_payment,   
	            yrdivstatement.etc_payment,   
	            yrdivstatement.item_payment,   
	            yrdivstatement.item_balamt,   
	            yrdivstatement.item_status,   
	            yrdivstatement.ref_slipcoop,   
	            yrdivstatement.ref_slipno,   
	            yrdivstatement.entry_id,   
	            yrdivstatement.entry_date  
            from yrdivstatement,
	            yrucfdivitemtype  
            where ( yrdivstatement.divitemtype_code = yrucfdivitemtype.divitemtype_code )
	            and ( yrdivstatement.coop_id = {0} )
	            and ( yrdivstatement.member_no = {1} )
	            and ( yrdivstatement.div_year = {2} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no, ls_year);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdDivitemtype()
        {
            string sql = @"
            select divitemtype_code,   
                divitemtype_desc ,1  as sorter
            from yrucfdivitemtype
            union select '','',0 from dual order by sorter, divitemtype_code asc";

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "divitemtype_code", "divitemtype_desc", "divitemtype_code");
        }
    }
}