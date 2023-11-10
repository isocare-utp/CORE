using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.assist.ws_as_ucfassisttype_ctrl
{
    public partial class DsAssLoan : DataSourceRepeater
    {
        public DataSet1.ASSUCFASSISTTYPELOANDataTable DATA { get; set; }

        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPELOAN;
            this.EventItemChanged = "OnDsAssLoanItemChanged";
            this.EventClicked = "OnDsAssLoanClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsAssLoan");
            this.Register();
        }

        public void RetrieveAssLoan(string as_asscode)
        {
            String sql = @"select a.coop_id, a.assisttype_code, a.assisttype_loan, 1 as check_flag, b.loantype_code+' - '+b.loantype_desc as lntype_name
                            from assucfassisttypeloan as a
                            join lnloantype as b on a.assisttype_loan = b.loantype_code
                            where assisttype_code = 0
                            union
                            select coop_id, 0, loantype_code, 0 , loantype_code+' - '+loantype_desc
                            from lnloantype as a 
                            where not exists ( select 1 from assucfassisttypeloan as b where b.assisttype_code = 0 and a.loantype_code = b.assisttype_loan )
                            order by assisttype_loan";
            sql = WebUtil.SQLFormat(sql, as_asscode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}