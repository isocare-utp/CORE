using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl
{
    public partial class DsStatus : DataSourceFormView
    {
        public DataSet1.DT_STATUSDataTable DATA { get; set; }

        public void InitDsStatus(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_STATUS;
            this.EventItemChanged = "OnDsStatusItemChanged";
            this.EventClicked = "OnDsStatusClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsStatus");
            this.Register();
        }

        public void RetrieveStatus(String ls_member_no)
        {
            String sql = @"  SELECT  
                                    MBMEMBMASTER.MEMBER_NO ,           
                                    MBMEMBMASTER.PAUSEKEEP_DATE ,           
                                    MBMEMBMASTER.KLONGTOON_FLAG ,           
                                    MBMEMBMASTER.ALLOWLOAN_FLAG ,           
                                    MBMEMBMASTER.PAUSEKEEP_FLAG ,           
                                    MBMEMBMASTER.TRANSRIGHT_FLAG ,           
                                    MBMEMBMASTER.DIVIDEND_FLAG ,           
                                    MBMEMBMASTER.AVERAGE_FLAG ,           
                                    MBMEMBMASTER.DROPLOANALL_FLAG ,           
                                    MBMEMBMASTER.DROPGURANTEE_FLAG ,           
                                    MBMEMBMASTER.MARIAGE_STATUS ,                 
                                    MBMEMBMASTER.SEQUEST_DIVAVG ,           
                                    MBMEMBMASTER.REMARK ,           
                                    MBMEMBMASTER.APPLTYPE_CODE ,  
                                    MBUCFAPPLTYPE.APPLTYPE_DESC,   
                                    MBMEMBMASTER.HAVE_GAIN ,           
                                    MBMEMBMASTER.DIVAVGSHOW_FLAG ,           
                                    MBMEMBMASTER.INSURANCE_FLAG    
                                FROM 
                                    MBMEMBMASTER   ,
                                    MBUCFAPPLTYPE  
                               WHERE 
                                    (MBMEMBMASTER.APPLTYPE_CODE=MBUCFAPPLTYPE.APPLTYPE_CODE)and
                                    ( MBMEMBMASTER.COOP_ID = {0} )  and          
                                    ( MBMEMBMASTER.MEMBER_NO = {1} )";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
//        public void DdAppltype()
//        {
//            string sql = @"
//              SELECT APPLTYPE_CODE,   
//                     APPLTYPE_DESC,   
//                     FIRST_FEE ,1 as sorter 
//                FROM MBUCFAPPLTYPE  
//                union
//                select '','',0,0 from dual order by sorter,APPLTYPE_CODE ASC";
//            DataTable dt = WebUtil.Query(sql);
            
//            this.DropDownDataBind(dt, "appltype_code", "APPLTYPE_DESC", "APPLTYPE_CODE");

//          //this.SetItem(0, "appltype_code",);
           
//        }
    }
}