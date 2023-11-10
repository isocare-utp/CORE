using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsPauseloan : DataSourceRepeater
    {
        public DataSet1.DT_PAUSELOANDataTable DATA { get; set; }

        public void InitDsPauseloan(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_PAUSELOAN;
            this.EventItemChanged = "OnDsPauseloanItemChanged";
            this.EventClicked = "OnDsPauseloanClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsPauseloan");
            this.Register();
        }

        public void RetrievePauseloan(String ls_member_no)
        {
            String sql = @" SELECT
	                        lnloantype.loantype_code,   
	                        lnloantype.loantype_desc,   
	                        lnmembpauseloan.pauseloan_cause  
                       FROM lnmembpauseloan,     
                            lnloantype  
                       WHERE 
                            ( lnmembpauseloan.loantype_code = lnloantype.loantype_code ) and 
                            (( lnloantype.coop_id = {0} ) AND  
                            ( lnmembpauseloan.member_no = {1} ) )  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}