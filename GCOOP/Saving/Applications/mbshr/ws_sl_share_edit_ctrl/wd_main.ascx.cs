using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_sl_share_edit_ctrl
{
    public partial class wd_main : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }
        public void InitMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnMainItemChanged";
            this.EventClicked = "OnMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "wd_main");
            this.Button.Add("b_search");
            //this.Button.Add("b_show");            

            this.Register();
        }
        public void RetrieveMain(string ls_member_no)
        {
            string sql = @"
                  SELECT MBMEMBMASTER.MEMBER_NO,   
                         MBUCFPRENAME.PRENAME_DESC,   
                         MBMEMBMASTER.MEMB_NAME,   
                         MBMEMBMASTER.MEMB_SURNAME,   
                         MBMEMBMASTER.MEMBGROUP_CODE,   
                         MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                         MBMEMBMASTER.MEMBTYPE_CODE,   
                         MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                         MBMEMBMASTER.ACCUM_INTEREST,   
                         MBMEMBMASTER.MEMBER_STATUS,   
                         MBMEMBMASTER.MEMBER_DATE  ,
                         SHSHAREMASTER.SHARETYPE_CODE,
                         SHSHARETYPE.SHARETYPE_DESC
                    FROM MBMEMBMASTER,   
                         MBUCFMEMBGROUP,   
                         MBUCFMEMBTYPE,   
                         MBUCFPRENAME,
                         SHSHAREMASTER  ,
                         SHSHARETYPE
                   WHERE ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  
                         ( MBUCFMEMBTYPE.MEMBTYPE_CODE = MBMEMBMASTER.MEMBTYPE_CODE ) and  
                         ( MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE ) and  
                         ( SHSHAREMASTER.MEMBER_NO=MBMEMBMASTER.MEMBER_NO)and
                         ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE )and
                         ( (mbmembmaster.coop_id={0})and
                           ( mbmembmaster.member_no ={1} ))   ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}