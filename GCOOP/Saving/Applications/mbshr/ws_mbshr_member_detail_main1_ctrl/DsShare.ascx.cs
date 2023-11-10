using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl
{
    public partial class DsShare : DataSourceRepeater
    {
        public DataSet1.DT_SHAREDataTable DATA { get; set; }

        public void InitDsShare(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_SHARE;
            this.EventItemChanged = "OnDsShareItemChanged";
            this.EventClicked = "OnDsShareClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsShare");
            this.Button.Add("bshr_detail");
            this.Register();
        }

        public void RetrieveShare(String ls_member_no)
        {
            String sql = @"   SELECT SHSHAREMASTER.SHARETYPE_CODE,   
                                     SHSHARETYPE.SHARETYPE_DESC,   
                                     SHSHARETYPE.UNITSHARE_VALUE,   
                                     SHSHAREMASTER.SHAREBEGIN_AMT,   
                                     SHSHAREMASTER.SHARESTK_AMT,   
                                     SHSHAREMASTER.LAST_PERIOD,   
                                     SHSHAREMASTER.PERIODSHARE_AMT,   
                                     SHSHAREMASTER.SHAREMASTER_STATUS,   
                                     SHSHAREMASTER.PAYMENT_STATUS,   
                                     SHSHAREMASTER.MEMBER_NO,   
                                     SHSHAREMASTER.SHAREARREAR_AMT,   
                                     SHSHAREMASTER.PERIODBASE_AMT,   
                                     SHSHAREMASTER.PAYMENTSTOP_DATE  
                                FROM SHSHAREMASTER,   
                                     SHSHARETYPE  
                               WHERE ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                                     ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                     (( SHSHAREMASTER.COOP_ID = {0} )  AND  
                                      ( shsharemaster.member_no = {1} ) ) ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}