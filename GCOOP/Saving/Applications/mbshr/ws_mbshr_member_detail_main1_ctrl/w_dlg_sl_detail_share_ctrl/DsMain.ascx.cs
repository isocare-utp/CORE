using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_share_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetrieveMain(String ls_member_no, String ls_shareype)
        {
            String sql = @"  
                  SELECT SHSHAREMASTER.MEMBER_NO,   
                         SHSHARETYPE.UNITSHARE_VALUE,   
                         SHSHAREMASTER.SHARETYPE_CODE,   
                         SHSHAREMASTER.SHAREBEGIN_AMT,   
                         SHSHAREMASTER.SHARESTK_AMT,   
                         SHSHAREMASTER.LAST_PERIOD,   
                         SHSHAREMASTER.PERIODSHARE_AMT,   
                         SHSHAREMASTER.SHAREMASTER_STATUS,   
                         SHSHAREMASTER.MISSPAY_AMT,   
                         SHSHAREMASTER.LAST_STM_NO,   
                         SHSHAREMASTER.COMPOUND_STATUS,   
                         SHSHAREMASTER.COMPOUND_PERIOD,   
                         SHSHAREMASTER.COMPOUNDDUE_DATE,   
                         SHSHAREMASTER.PAYMENT_STATUS,   
                         SHSHAREMASTER.PERIODBASE_AMT,   
                         SHSHAREMASTER.SEQUEST_STATUS,   
                         MBMEMBMASTER.MEMB_NAME,   
                         MBMEMBMASTER.MEMB_SURNAME,   
                         MBUCFPRENAME.PRENAME_DESC,   
                         SHSHARETYPE.SHARETYPE_DESC,   
                         SHSHAREMASTER.COMPOUND_PAYMENT,   
                         SHSHAREMASTER.COMPOUND_PAYSTATUS,   
                         SHSHAREMASTER.PAYMENTSTOP_DATE,   
                         SHSHAREMASTER.SHAREARREARBF_AMT,   
                         SHSHAREMASTER.SHAREARREAR_AMT  
                    FROM SHSHAREMASTER,   
                         SHSHARETYPE,   
                         MBMEMBMASTER,   
                         MBUCFPRENAME  
                   WHERE ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                         ( SHSHAREMASTER.COOP_ID = MBMEMBMASTER.COOP_ID ) and  
                         ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                         ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                         ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                         ( ( SHSHAREMASTER.MEMBER_NO = {0} ) AND  
                         ( SHSHAREMASTER.SHARETYPE_CODE = {1} ) )    ";

            sql = WebUtil.SQLFormat(sql, ls_member_no, ls_shareype);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}