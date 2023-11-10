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
    public partial class DsStatement : DataSourceRepeater
    {
        public DataSet1.DT_STATEMENTDataTable DATA { get; set; }

        public void InitDsStatement(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_STATEMENT;
            this.EventItemChanged = "OnDsStatementItemChanged";
            this.EventClicked = "OnDsStatementClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsStatement");
            this.Register();
        }
        public void RetrieveStatement(String ls_member_no, String ls_shareype)
        {
            String sql = @"  
                  SELECT SHSHARESTATEMENT.MEMBER_NO,   
                         SHSHARESTATEMENT.SHARETYPE_CODE,   
                         SHSHARESTATEMENT.SEQ_NO,   
                         SHSHARESTATEMENT.OPERATE_DATE,   
                         SHSHARESTATEMENT.REF_DOCNO,   
                         SHSHARESTATEMENT.SHRITEMTYPE_CODE,   
                         SHSHARESTATEMENT.PERIOD,   
                         SHSHARESTATEMENT.SHARE_AMOUNT,   
                         SHSHARESTATEMENT.SHARESTK_AMT,   
                         SHSHARETYPE.UNITSHARE_VALUE,   
                         SHUCFSHRITEMTYPE.SIGN_FLAG,   
                         SHSHARESTATEMENT.SHRARREAR_AMT,   
                         SHSHARESTATEMENT.SLIP_DATE,   
                         SHSHARESTATEMENT.REMARK  
                    FROM SHSHARESTATEMENT,   
                         SHSHARETYPE,   
                         SHUCFSHRITEMTYPE  
                   WHERE ( SHSHARESTATEMENT.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                         ( SHSHARESTATEMENT.SHRITEMTYPE_CODE = SHUCFSHRITEMTYPE.SHRITEMTYPE_CODE ) and  
                         ( SHSHARESTATEMENT.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                         ( ( shsharestatement.member_no = {0} ) AND  
                         ( shsharestatement.sharetype_code = {1} ) )
                    order by shsharestatement.seq_no";

            sql = WebUtil.SQLFormat(sql, ls_member_no, ls_shareype);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}