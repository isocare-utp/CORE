using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.SHSHAREMASTERDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.SHSHAREMASTER;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            // this.Button.Add("b_memsearch");
            this.Button.Add("b_collwho");
            this.Register();
        }
        public void RetrieveList(string coop_id)
        {
            String sql = @"  SELECT MBMEMBMASTER.MEMBER_NO,   
                             MBUCFPRENAME.PRENAME_DESC,   
                             MBMEMBMASTER.MEMB_NAME,   
                             MBMEMBMASTER.MEMB_SURNAME,   
                             MBMEMBMASTER.MEMBGROUP_CODE,   
                             MBMEMBMASTER.RESIGN_DATE,   
                             SHSHAREMASTER.SHARETYPE_CODE,   
                             SHSHAREMASTER.SHARESTK_AMT,   
                             SHSHAREMASTER.SHAREMASTER_STATUS,   
                             SHSHARETYPE.UNITSHARE_VALUE,   
                             0 as operate_flag  
                        FROM MBMEMBMASTER,   
                             SHSHAREMASTER,   
                             MBUCFPRENAME,   
                             SHSHARETYPE 
                       WHERE ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
                             ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                             ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                             ( MBMEMBMASTER.COOP_ID = SHSHAREMASTER.COOP_ID ) and  
                             ( SHSHAREMASTER.SHARESTK_AMT > 0 ) and                                                    
							 ( SHSHAREMASTER.SHAREMASTER_STATUS in (5,8) )  
                        ";

            sql = WebUtil.SQLFormat(sql, coop_id);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void RetrieveMemberNo(string member_no)
        {
            String sql = @"  SELECT MBMEMBMASTER.MEMBER_NO,   
                             MBUCFPRENAME.PRENAME_DESC,   
                             MBMEMBMASTER.MEMB_NAME,   
                             MBMEMBMASTER.MEMB_SURNAME,   
                             MBMEMBMASTER.MEMBGROUP_CODE,   
                             MBMEMBMASTER.RESIGN_DATE,   
                             SHSHAREMASTER.SHARETYPE_CODE,   
                             SHSHAREMASTER.SHARESTK_AMT,   
                             SHSHAREMASTER.SHAREMASTER_STATUS,   
                             SHSHARETYPE.UNITSHARE_VALUE,   
                             0 as operate_flag  
                        FROM MBMEMBMASTER,   
                             SHSHAREMASTER,   
                             MBUCFPRENAME,   
                             SHSHARETYPE  
                       WHERE ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO )  
                             and ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE )   
                             and ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE )    
                             and ( MBMEMBMASTER.COOP_ID = SHSHAREMASTER.COOP_ID )
                             and ( SHSHAREMASTER.SHARESTK_AMT > 0 )    
                             and ( ( MBMEMBMASTER.CURRENT_COOPID = {0} )   
                             and ( shsharemaster.member_no = {1} ) )
            ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopId, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}