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
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.LNREQTRNLNRESPONSDETDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQTRNLNRESPONSDET;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string memb_no, string loancont_no)
        {
            String sql = @"select mbucfprename.prename_desc,
         mbmembmaster.memb_name,
         mbmembmaster.memb_surname,  
         lnreqtrnlnresponsdet.coop_id,   
         lnreqtrnlnresponsdet.trnlnreq_docno,   
         lnreqtrnlnresponsdet.seq_no,   
         lnreqtrnlnresponsdet.operate_flag,   
         lnreqtrnlnresponsdet.trn_memcoopid,   
         lnreqtrnlnresponsdet.trn_memno,   
         lnreqtrnlnresponsdet.trn_concoopid,   
         lnreqtrnlnresponsdet.trn_contractno,   
         lnreqtrnlnresponsdet.trnprnbal_amt,   
         lnreqtrnlnresponsdet.trnintarrear_amt,   
         lnreqtrnlnresponsdet.trnlastcalint_date,   
         lnreqtrnlnresponsdet.periodpayprn_amt,   
         lnreqtrnlnresponsdet.periodpayintarr_amt,   
         lnreqtrnlnresponsdet.bfprnbal_amt,   
         lnreqtrnlnresponsdet.bfintarrear_amt  
    from lnreqtrnlnresponsdet,   
         mbmembmaster,   
         mbucfprename  
   where ( lnreqtrnlnresponsdet.trn_memcoopid = mbmembmaster.coop_id ) and  
         ( lnreqtrnlnresponsdet.trn_memno = mbmembmaster.member_no ) and  
         ( mbmembmaster.prename_code = mbucfprename.prename_code )";
            sql = WebUtil.SQLFormat(sql, memb_no, loancont_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}