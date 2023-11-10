using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_assistpaygroup_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.Register();
        }

        public void RetrieveList(string sqlsearch)
        {
            String sql = @"select 
	                        mpre.prename_desc +mb.memb_name + '  ' + mb.memb_surname as full_name,
	                        mb.membgroup_code,
	                        ass.asscontract_no,   
	                        ass.member_no,
	                        ass.assisttype_code,
                            asm.approve_date,
	                        asm.assistnet_amt as approve_amt,
	                        ass.asscont_status,
                            ast.assisttype_code +':'+ast.assisttype_desc+' ('+asp.assistpay_desc+')' as assisttype_desc,
                            case when ass.moneytype_code = 'TRN' then ass.deptaccount_no else ass.expense_accid end as expense_accid
                        from asscontmaster ass
                        inner join mbmembmaster mb on ass.member_no = mb.member_no
                        inner join mbucfprename mpre on mb.prename_code = mpre.prename_code
                        inner join assucfassisttype ast on ass.assisttype_code = ast.assisttype_code
                        inner join assucfassisttypepay asp on ass.assisttype_code = asp.assisttype_code and ass.assistpay_code = asp.assistpay_code
                        inner join assreqmaster asm on ass.assisttype_code = asm.assisttype_code and ass.member_no = asm.member_no
									and asm.req_status = 1  
                        where ass.withdrawable_amt > 0 and asscont_status > 0 and
                        ass.coop_id = {0}
                        and asm.assist_docno not in ( select asp.ref_reqdocno from assslippayout asp where asp.asscontract_no = ass.asscontract_no and asp.slip_status = 1 )
                        " + sqlsearch + @"
                        order by ass.assisttype_code, ass.member_no, ass.asscontract_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}