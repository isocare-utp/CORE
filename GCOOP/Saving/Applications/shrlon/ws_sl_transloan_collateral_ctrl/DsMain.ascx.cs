using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.shrlon.ws_sl_transloan_collateral_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNREQTRNLNRESPONSDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQTRNLNRESPONS;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_searchmemb");
            this.Button.Add("b_searchcont");
            this.Register();
        }

        public void Retrieve(string memb_no,string loancont_no)
        {
            String sql = @" select lnreqtrnlnrespons.coop_id,   
         lnreqtrnlnrespons.trnlnreq_docno,   
         lnreqtrnlnrespons.trnlnreq_code,   
         lnreqtrnlnrespons.trnlnreq_date,   
         lnreqtrnlnrespons.concoop_id,   
         lnreqtrnlnrespons.loancontract_no,   
         lnreqtrnlnrespons.memcoop_id,   
         lnreqtrnlnrespons.member_no,   
         lnreqtrnlnrespons.trnln_prnamt,   
         lnreqtrnlnrespons.trnln_intamt,   
         lnreqtrnlnrespons.bfperiod_payment,   
         lnreqtrnlnrespons.bflast_periodpay,   
         lnreqtrnlnrespons.bfprnbal_amt,   
         lnreqtrnlnrespons.bfintarrear_amt,   
         lnreqtrnlnrespons.bflastcalint_date,   
         lnreqtrnlnrespons.trnlnreq_status,   
         lnreqtrnlnrespons.entry_id,   
         lnreqtrnlnrespons.entry_date,   
         lnreqtrnlnrespons.entry_bycoopid,   
         lncontmaster.loantype_code,   
         lnloantype.loantype_desc,   
         mbucfprename.prename_desc,
         mbmembmaster.memb_name,
         mbmembmaster.memb_surname,
         lncontmaster.period_payamt  
    from lnreqtrnlnrespons,   
         lncontmaster,   
         mbmembmaster,   
         mbucfprename,   
         lnloantype  
   where ( lnreqtrnlnrespons.concoop_id = lncontmaster.coop_id ) and  
         ( lnreqtrnlnrespons.loancontract_no = lncontmaster.loancontract_no ) and  
         ( lnreqtrnlnrespons.memcoop_id = mbmembmaster.coop_id ) and  
         ( lnreqtrnlnrespons.member_no = mbmembmaster.member_no ) and  
         ( mbmembmaster.prename_code = mbucfprename.prename_code ) and  
         ( lncontmaster.coop_id = lnloantype.coop_id ) and  
         ( lncontmaster.loantype_code = lnloantype.loantype_code ) and
         ( lnreqtrnlnrespons.member_no = {0} ) and
         ( lnreqtrnlnrespons.loancontract_no = {1} )";
            sql = WebUtil.SQLFormat(sql, memb_no,loancont_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}