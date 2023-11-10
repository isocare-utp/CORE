using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_fund_statement_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DT_MAINDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_MAIN;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }

        public void Retrieve(string memberNo)
        {
            string sql = @"SELECT MBMEMBMASTER.MEMBER_NO,  
                        dbo.ft_getmemname(MBMEMBMASTER.COOP_ID,MBMEMBMASTER.MEMBER_NO)  AS FULLNAME,
                        MEMBGROUP_CONTROL,
                        MBMEMBMASTER.MEMBGROUP_CODE,   
                        MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                        MBMEMBMASTER.MEMBTYPE_CODE,   
                        MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                        MBMEMBMASTER.MEMBER_STATUS,   
                        MBMEMBMASTER.RESIGN_STATUS,   
                        MBMEMBMASTER.MEMBER_TYPE,   
                        MBMEMBMASTER.REMARK  ,
                        FUNDCOLLMASTER.FUNDBALANCE,
                        FUNDCOLLMASTER.FUND_STATUS,
                        (CASE WHEN FUNDCOLLMASTER.FUND_STATUS=1 THEN 'ปกติ' 
            WHEN FUNDCOLLMASTER.FUND_STATUS=0 THEN 'สิ้นสุด'  else 'ยกเลิก' end) FUNDSTATUS_DISPLAY
                FROM MBMEMBMASTER INNER JOIN FUNDCOLLMASTER ON MBMEMBMASTER.MEMBER_NO = FUNDCOLLMASTER.MEMBER_NO
                LEFT JOIN MBUCFMEMBTYPE ON mbmembmaster.membtype_code = mbucfmembtype.membtype_code,     
                        MBUCFMEMBGROUP
                WHERE   ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
                        ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID ) and   
                        (( MBMEMBMASTER.COOP_ID = {0} ) AND  
                        ( mbmembmaster.member_no = {1} )  )";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memberNo);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}