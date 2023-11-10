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
    public partial class DsTranfer : DataSourceRepeater
    {
        public DataSet1.DT_TRANFERDataTable DATA { get; set; }

        public void InitDsTranfer(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_TRANFER;
            this.EventItemChanged = "OnDsTranferItemChanged";
            this.EventClicked = "OnDsTranferClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsTranfer");
            this.Register();
        }

        public void RetrieveTranfer(String ls_member_no)
        {
            String sql = @"  
                  SELECT MBMEMBTRANSFER.COOP_ID,   
                         MBMEMBTRANSFER.MEMBER_NO,   
                         MBMEMBTRANSFER.TRAN_DATE,   
                         MBMEMBTRANSFER.TRAN_PERIOD,   
                         MBMEMBTRANSFER.TRAN_SHARE,   
                         MBMEMBTRANSFER.TRAN_LOAN,
                         MBMEMBTRANSFER.TRAN_DETAIL   
                    FROM MBMEMBTRANSFER  
                   WHERE
                         ( MBMEMBTRANSFER.COOP_ID = {0} )  AND   
                         ( MBMEMBTRANSFER.MEMBER_NO = {1} )  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}