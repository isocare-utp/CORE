using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.keeping.ws_kp_adjmth_ccl_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.DT_DetailDataTable DATA { get; set; }
        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_Detail;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Register();
        }

        public void Retrieve(string as_adjno)
        {
            String sql = @"select 0 as operate_flag , sa.coop_id , sa.adjslip_no , sa.adjslip_date , ft_cnvtdate( sa.adjslip_date , 1 ) as adjslip_tdate ,
                sa.ref_recvperiod , sa.slip_amt ,
                sad.bfmthpay_kpitemtyp , kui.keepitemtype_desc , sad.seq_no, sad.shrlontype_code , sad.loancontract_no , 
                sad.principal_adjamt , sad.interest_adjamt , sad.intarrear_adjamt , sad.item_adjamt , kui.sort_in_receive
                from slslipadjust sa , slslipadjustdet sad , kpucfkeepitemtype kui
                where sa.coop_id = sad.coop_id
                and sa.adjslip_no = sad.adjslip_no
                and sad.coop_id = kui.coop_id
                and sad.bfmthpay_kpitemtyp = kui.keepitemtype_code
                and sa.coop_id = {0}
                and sa.adjslip_no = {1}
                order by sad.adjslip_no, kui.sort_in_receive";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_adjno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            DdTofromaccid();
        }

        public void DdTofromaccid()
        {
            string sql = @"SELECT accmaster.account_id,   
                accmaster.account_name || ' - ' || accmaster.account_id as tofrom_accid
                FROM accmaster  
                WHERE ( accmaster.coop_id = {0} ) AND  
                (  exists ( select 1 from cmucftofromaccid cuf 
                    where cuf.coop_id = accmaster.coop_id 
                    and cuf.account_id = accmaster.account_id 
                    and cuf.coop_id = {0} 
                    and cuf.applgroup_code = 'KEP' 
                    and cuf.sliptype_code = 'ADJ' ) )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "tofrom_accid", "tofrom_accid", "account_id");
        }

        public TextBox itemadj_amt
        {
            get { return this.sum_itemadj; }
        }
    }
}