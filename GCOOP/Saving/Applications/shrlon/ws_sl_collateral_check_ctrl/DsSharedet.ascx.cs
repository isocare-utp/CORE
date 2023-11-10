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
    public partial class DsSharedet : DataSourceFormView
    {
        public DataSet1.SHSHAREMASTERDataTable DATA { get; set; }

        public void InitDsSharedet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHAREMASTER;
            this.EventItemChanged = "OnDsSharedetItemChanged";
            this.EventClicked = "OnDsSharedetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsSharedet");

            this.Register();
        }
        public void RetrieveDsSharedet(String member_no)
        {
            String sql = @"    SELECT SHSHARETYPE.UNITSHARE_VALUE,   
                                     SHSHAREMASTER.SHAREBEGIN_AMT,   
                                     SHSHAREMASTER.SHARESTK_AMT,   
                                     SHSHAREMASTER.LAST_PERIOD,   
                                     SHSHAREMASTER.PERIODSHARE_AMT,   
                                     SHSHAREMASTER.SHAREMASTER_STATUS,   
                                     SHSHAREMASTER.PAYMENT_STATUS,   
                                     SHSHAREMASTER.SHAREARREAR_AMT,   
                                     SHSHAREMASTER.SHAREARREARBF_AMT,   
                                     MBUCFPRENAME.PRENAME_DESC || MBMEMBMASTER.MEMB_NAME || ' ' || MBMEMBMASTER.MEMB_SURNAME as memfull_name  
                                FROM SHSHAREMASTER,   
                                     SHSHARETYPE,   
                                     MBMEMBMASTER,   
                                     MBUCFPRENAME  
                               WHERE ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                                     ( SHSHARETYPE.COOP_ID = MBMEMBMASTER.COOP_ID ) and  
                                     ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and  
                                     ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                     ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                                     ( ( shsharemaster.member_no = {0} ) AND  
                                     ( shsharemaster.sharetype_code = '01' ) )   ";

            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}