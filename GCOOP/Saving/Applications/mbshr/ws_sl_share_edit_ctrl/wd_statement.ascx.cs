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
    public partial class wd_statement : DataSourceRepeater
    {
        public DataSet1.DT_STATEMENTDataTable DATA { get; set; }
        public void InitStatement(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_STATEMENT;
            this.EventItemChanged = "OnStatementItemChanged";
            this.EventClicked = "OnStatementClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "wd_statement");
            // this.Button.Add("b_memsearch");
            // this.Button.Add("b_collwho");
            this.Button.Add("b_delete");
            this.Register();
        }
        public void RetrieveStatement(string ls_member_no, string ls_sharetype_code)
        {
            string sql = @"
                          SELECT 
                                SHSHARESTATEMENT.* ,           
                                SHSHARETYPE.SHARE_VALUE     
                                FROM SHSHARESTATEMENT ,           SHSHARETYPE     
                        WHERE ( SHSHARESTATEMENT.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and         
                            ( ( shsharestatement.member_no = {0} ) And          
                              ( shsharestatement.sharetype_code = {1} ) )    ";
            sql = WebUtil.SQLFormat(sql, ls_member_no, ls_sharetype_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
          
        }
        public void DdShritemtype()
        {
            string sql = @"
              SELECT SHRITEMTYPE_CODE,   
                     SHRITEMTYPE_DESC,   
                     SIGN_FLAG  ,1 as sorter
                FROM SHUCFSHRITEMTYPE
                union 
            select '','',0,0 from dual order by sorter,SHRITEMTYPE_CODE ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "shritemtype_code", "SHRITEMTYPE_CODE", "SHRITEMTYPE_CODE");
        }
    }
}