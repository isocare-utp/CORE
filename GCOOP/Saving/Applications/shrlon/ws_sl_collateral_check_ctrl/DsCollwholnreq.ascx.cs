using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_check_ctrl
{
    public partial class DsCollwholnreq : DataSourceRepeater
    {
        public DataSet1.LNREQLOANDataTable DATA { get; set; }

        public void InitDsCollwholnreq(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNREQLOAN;
            this.EventItemChanged = "OnDsCollwholnreqItemChanged";
            this.EventClicked = "OnDsCollwholnreqClicked";
            this.InitDataSource(pw, Repeater3, this.DATA, "dsCollwholnreq");
            this.Register();
        }
        public void RetrieveDsCollwholnreq(String concoll_no, String collTypeCode)
        {
            String sql = @"select lnreqloan.loanrequest_docno,   
                    lnreqloan.member_no,   
                    lnreqloan.loantype_code,   
                    lnreqloan.loanrequest_amt,   
                    lnreqloancoll.collactive_amt, 
                    lnreqloancoll.collactive_percent,   
                    lnreqloancoll.loancolltype_code,   
                    lnreqloancoll.collbase_percent,   
                    lnloantype.cntmangrtval_flag,   
                    lnloantype.mangrtpermgrp_code,   
                    mbucfprename.prename_desc,   
                    mbmembmaster.memb_name,   
                    mbmembmaster.memb_surname,   
                    lnreqloan.member_no +' - '+ mbucfprename.prename_desc + mbmembmaster.memb_name +'  '+ mbmembmaster.memb_surname as full_name  
            from lnreqloan,   
                    lnreqloancoll,   
                    lnloantype,   
                    mbmembmaster,   
                    mbucfprename  
            where ( lnreqloancoll.loanrequest_docno = lnreqloan.loanrequest_docno )  
                    and ( lnreqloan.loantype_code = lnloantype.loantype_code )  
                    and ( lnreqloan.member_no = mbmembmaster.member_no )  
                    and ( mbucfprename.prename_code = mbmembmaster.prename_code )  
                    and ( lnreqloan.coop_id = lnreqloancoll.coop_id )  
                    and ( lnreqloan.coop_id = lnloantype.coop_id )  
                    and ( lnreqloan.coop_id = mbmembmaster.coop_id )  
                    and ( ( lnreqloan.loanrequest_status in (8,11) )   
                    and ( lnreqloancoll.ref_collno = {0} )   
                    and ( lnreqloancoll.loancolltype_code = {1} ) )  ";

            sql = WebUtil.SQLFormat(sql,concoll_no, collTypeCode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public TextBox TContract
        {
            get { return this.cp_sumcp_collamt; }
        }
    }
}