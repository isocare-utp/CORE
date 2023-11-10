using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl
{
    public partial class DsMoneytr : DataSourceRepeater
    {
        public DataSet1.DT_MONEYTRDataTable DATA { get; set; }

        public void InitDsMoneytr(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MONEYTR;
            this.EventItemChanged = "OnDsMoneytrItemChanged";
            this.EventClicked = "OnDsMoneytrClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsMoneytr");
            this.Register();
        }

        public void RetrieveMoneytr(String ls_member_no)
        {
            String sql = @"  
                          SELECT MBMEMBMONEYTR.MEMBER_NO,   
                                 MBMEMBMONEYTR.TRTYPE_CODE,   
                                 MBMEMBMONEYTR.MONEYTYPE_CODE,   
                                 MBMEMBMONEYTR.BANK_CODE,   
                                 MBMEMBMONEYTR.BANK_BRANCH,   
                                 MBMEMBMONEYTR.BANK_ACCID,   
                                 CMUCFBANK.ACCOUNT_FORMAT,   
                                 CMUCFMONEYTYPE.MONEYTYPE_GROUP,   
                                 CMUCFBANK.BANK_DESC,   
                                 CMUCFMONEYTYPE.MONEYTYPE_DESC  
                            FROM MBMEMBMONEYTR left join  CMUCFMONEYTYPE on MBMEMBMONEYTR.MONEYTYPE_CODE = CMUCFMONEYTYPE.MONEYTYPE_CODE
                                                                 left join CMUCFBANK on mbmembmoneytr.bank_code = cmucfbank.bank_code
                                   
                           WHERE  
                                  MBMEMBMONEYTR.COOP_ID ={0}  AND  
                                    MBMEMBMONEYTR.MEMBER_NO = {1} ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdMoneytrtype()
        {
            string sql = @"
              SELECT TRTYPE_CODE,   
                     TRTYPE_DESC,   
                     SIGN_FLAG  ,1 as sorter
                FROM MBUCFMONEYTRTYPE  
            union
            select '','',0,0 from dual order by sorter,TRTYPE_CODE ASC";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "trtype_code", "TRTYPE_CODE", "TRTYPE_CODE");
        }
    }
}