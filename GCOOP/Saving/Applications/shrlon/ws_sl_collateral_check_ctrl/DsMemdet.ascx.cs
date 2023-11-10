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
    public partial class DsMemdet : DataSourceFormView
    {
        public DataSet1.SHSHAREMASTERDataTable DATA { get; set; }

        public void InitDsMemdet(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHAREMASTER;
            this.EventItemChanged = "OnDsMemdetItemChanged";
            this.EventClicked = "OnDsMemdetClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMemdet");

            this.Register();
        }

        public void RetrieveMemdet(String member_no)
        {
            String sql = @"  SELECT MBMEMBMASTER.MEMBGROUP_CODE,   
                                     MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                                     MBMEMBMASTER.BIRTH_DATE,   
                                     MBMEMBMASTER.MEMBER_DATE,   
                                     MBMEMBMASTER.SALARY_AMOUNT,   
                                     MBMEMBMASTER.WORK_DATE,   
                                     MBMEMBMASTER.RETRY_DATE,   
                                     SHSHAREMASTER.SHARESTK_AMT,   
                                     SHSHAREMASTER.LAST_PERIOD,   
                                     SHSHAREMASTER.PERIODSHARE_AMT,   
                                     SHSHAREMASTER.PAYMENT_STATUS,   
                                     SHSHAREMASTER.SHAREARREAR_AMT,   
                                     SHSHARETYPE.UNITSHARE_VALUE,   
                                     0.00 as birth_age,   
                                     0.00 as retry_age,   
                                     0 as member_age,   
                                     0.00 as work_age,   
                                     MBUCFMEMBTYPE.MEMBTYPE_CODE,   
                                     MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                     MBMEMBMASTER.APPLTYPE_CODE,   
                                     MBMEMBMASTER.MEMBER_TYPE,   
                                     MBUCFPRENAME.PRENAME_DESC + MBMEMBMASTER.MEMB_NAME + ' ' + MBMEMBMASTER.MEMB_SURNAME as memfull_name,   
                                     MBMEMBMASTER.RESIGN_STATUS  
                                FROM MBMEMBMASTER,   
                                     MBUCFMEMBGROUP,   
                                     SHSHAREMASTER,   
                                     SHSHARETYPE,   
                                     MBUCFMEMBTYPE,   
                                     MBUCFPRENAME  
                               WHERE ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  
                                     ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                                     ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                                     ( MBMEMBMASTER.MEMBTYPE_CODE = MBUCFMEMBTYPE.MEMBTYPE_CODE ) and  
                                     ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID ) and  
                                     ( MBMEMBMASTER.COOP_ID = SHSHAREMASTER.COOP_ID ) and  
                                     ( MBMEMBMASTER.COOP_ID = MBUCFMEMBTYPE.COOP_ID ) and  
                                     ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                                     ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                     ( ( mbmembmaster.member_no = {0} ) AND  
                                     ( shsharemaster.sharetype_code = '01' ) ) ";

            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}