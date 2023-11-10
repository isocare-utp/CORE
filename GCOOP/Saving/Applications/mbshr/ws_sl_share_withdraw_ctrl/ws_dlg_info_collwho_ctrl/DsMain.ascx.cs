using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_info_collwho_ctrl
{
    public partial class DsMain : DataSourceRepeater
    {
        public DataSet1.LNCONTCOLLDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTCOLL;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMain");
            this.Register();
        }

        public void RetrieveMain(String member_no)
        {
            String sql = @"
            SELECT LNCONTCOLL.LOANCONTRACT_NO,   
                 LNCONTMASTER.MEMBER_NO,   
                 LNCONTCOLL.REF_COLLNO,   
                 MBUCFPRENAME.PRENAME_DESC,   
                 MBMEMBMASTER.MEMB_NAME,   
                 MBMEMBMASTER.MEMB_SURNAME,   
                 LNCONTMASTER.PRINCIPAL_BALANCE,   
                 LNCONTCOLL.COLL_PERCENT,   
                 LNCONTMASTER.WITHDRAWABLE_AMT,   
                 LNCONTMASTER.LOANAPPROVE_AMT,   
                 LNCONTMASTER.STARTCONT_DATE,   
                 SHSHAREMASTER.SHARESTK_AMT,   
                 SHSHAREMASTER.SHAREARREAR_AMT,   
                 SHSHARETYPE.UNITSHARE_VALUE  
            FROM LNCONTCOLL,   
                 LNCONTMASTER,   
                 MBMEMBMASTER,   
                 MBUCFPRENAME,   
                 SHSHAREMASTER,   
                 SHSHARETYPE  
           WHERE ( LNCONTMASTER.LOANCONTRACT_NO = LNCONTCOLL.LOANCONTRACT_NO ) and  
                 ( MBMEMBMASTER.MEMBER_NO = LNCONTMASTER.MEMBER_NO ) and  
                 ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                 ( MBMEMBMASTER.MEMBER_NO = SHSHAREMASTER.MEMBER_NO ) and  
                 ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                 ( LNCONTCOLL.COOP_ID = LNCONTMASTER.COOP_ID ) and  
                 ( LNCONTMASTER.MEMCOOP_ID = MBMEMBMASTER.COOP_ID ) and  
                 ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                 ( ( lncontcoll.loancolltype_code in ( '01', '05' ) ) AND 
                 ( lncontcoll.coop_id = {0} ) AND  
                 ( lncontcoll.ref_collno = {1} ) AND  
                 ( lncontmaster.contract_status > 0 ) AND  
                 ( lncontcoll.coll_status = 1 ) ) 
            ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}