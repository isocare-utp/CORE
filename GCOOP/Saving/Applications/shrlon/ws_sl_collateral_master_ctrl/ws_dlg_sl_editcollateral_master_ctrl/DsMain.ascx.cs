using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_ctrl.ws_dlg_sl_editcollateral_master_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void RetrieveMain(String member_no)
        {
            String sql = @"SELECT MBMEMBMASTER.MEMBER_NO,   
                                 MBUCFPRENAME.PRENAME_DESC,   
                                 MBMEMBMASTER.MEMB_NAME,   
                                 MBMEMBMASTER.MEMB_SURNAME,   
                                 MBMEMBMASTER.MEMBGROUP_CODE,   
                                 MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                                 MBMEMBMASTER.ACCUM_INTEREST,   
                                 MBMEMBMASTER.RESIGN_STATUS,   
                                 MBMEMBMASTER.REMARK,   
                                 SHSHAREMASTER.SHAREBEGIN_AMT,
                                 SHSHARETYPE.UNITSHARE_VALUE ,   
                                 SHSHAREMASTER.SHARESTK_AMT ,
                                 shsharemaster.sharebegin_amt * shsharetype.unitshare_value as sharebegin_value,   
                                 shsharemaster.sharestk_amt * shsharetype.unitshare_value as sharestk_value 
                            FROM MBMEMBMASTER,   
                                 MBUCFPRENAME,   
                                 MBUCFMEMBGROUP,   
                                 SHSHAREMASTER,   
                                 SHSHARETYPE  
                           WHERE ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and  
                                 ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  
                                 ( SHSHARETYPE.SHARETYPE_CODE = SHSHAREMASTER.SHARETYPE_CODE ) and  
                                 ( MBMEMBMASTER.MEMBER_NO = SHSHAREMASTER.MEMBER_NO )and  
                                 ( ( mbmembmaster.member_no = {0} ) )  ";

            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}