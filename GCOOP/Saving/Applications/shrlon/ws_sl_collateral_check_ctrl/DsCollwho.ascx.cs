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
    public partial class DsCollwho : DataSourceRepeater
    {
        public DataSet1.LNCONTCOLLDataTable DATA { get; set; }
        public void InitDsCollwho(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTCOLL;
            this.EventItemChanged = "OnDsCollwhoItemChanged";
            this.EventClicked = "OnDsCollwhoClicked";
            this.InitDataSource(pw, Repeater3, this.DATA, "dsCollwho");
            this.Register();
        }
        public void RetrieveDsCollwho(String collTypeCode, String concoll_no)
        {
            String sql = @"    SELECT LNCONTCOLL.REF_COLLNO,   
                                     LNCONTMASTER.LOANCONTRACT_NO,   
                                     LNCONTMASTER.MEMBER_NO,   
                                     LNCONTMASTER.LOANTYPE_CODE,   
                                     LNCONTMASTER.LOANAPPROVE_AMT,   
                                     LNCONTMASTER.WITHDRAWABLE_AMT,   
                                     LNCONTMASTER.PRINCIPAL_BALANCE,   
                                     MBUCFPRENAME.PRENAME_DESC,   
                                     MBMEMBMASTER.MEMB_NAME,   
                                     MBMEMBMASTER.MEMB_SURNAME,   
                                     LNLOANTYPE.PREFIX,   
                                     LNCONTCOLL.COLLACTIVE_PERCENT,   
                                     LNCONTCOLL.collbase_percent,   
                                     LNLOANTYPE.LOANTYPE_DESC  ,
                                     LNCONTCOLL.COLL_PERCENT,   
                                     LNCONTCOLL.base_percent,
                                     LNCONTCOLL.COLLACTIVE_AMT   ,
                                     (LNCONTMASTER.PRINCIPAL_BALANCE *    LNCONTCOLL.COLLACTIVE_PERCENT) / 100  as cp_colluse
                                FROM LNCONTCOLL,   
                                     LNCONTMASTER,   
                                     MBMEMBMASTER,   
                                     MBUCFPRENAME,   
                                     LNLOANTYPE  
                               WHERE ( LNCONTMASTER.LOANCONTRACT_NO = LNCONTCOLL.LOANCONTRACT_NO ) and  
                                     ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
                                     ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                                     ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                                     ( MBMEMBMASTER.COOP_ID = LNCONTMASTER.COOP_ID ) and  
                                     ( LNCONTMASTER.COOP_ID = LNCONTCOLL.COOP_ID ) and  
                                     ( LNCONTMASTER.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                     ( ( lncontcoll.loancolltype_code = {0} ) AND  
                                     ( lncontcoll.ref_collno = {1} ) AND  
                                     ( lncontmaster.contract_status > 0 ) AND  
                                     ( lncontcoll.coll_status = 1 ) )     ";

            sql = WebUtil.SQLFormat(sql, collTypeCode, concoll_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public TextBox TContract
        {
            get { return this.cp_sumcp_colluse; }
        }
    }
}