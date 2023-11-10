using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_checkpermiss_reprint_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            //this.Button.Add("b_detail");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void Retrieve(string memno)
        {
            string sql = @"SELECT  
                                lr.loanrequest_docno,
		                        lr.loantype_code,
		                        lr.member_no +' '+ mn.PRENAME_DESC+' '+mb.MEMB_NAME+' '+mb.MEMB_SURNAME as fullname,
		                        lr.loanrequest_amt,
		                        lr.loancontract_no as loancont_no,
                                lr.entry_id
                           FROM LNREQLOAN lr ,  mbmembmaster mb ,mbucfprename mn
                           WHERE 
		                        lr.member_no = mb.member_no and
		                        mb.prename_code = mn.prename_code and
		                        (lr.COOP_ID = {0}) and 
                                lr.member_no = {1}
                           ORDER By lr.loanrequest_docno";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}