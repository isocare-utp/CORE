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
            this.Button.Add("b_print");
            this.Button.Add("b_pbreport");
            this.Register();
        }

        public void RetrieveMain(String ls_member_no)
        {
            String sql = @"  
                          SELECT MBMEMBMASTER.MEMBER_NO,   
                                 MBUCFPRENAME.PRENAME_DESC,   
                                 MBMEMBMASTER.MEMB_NAME,   
                                 MBMEMBMASTER.MEMB_SURNAME,   
                                 MBMEMBMASTER.MEMB_ENAME,   
                                 MBMEMBMASTER.MEMB_ESURNAME,   
                                (case when ltrim(rtrim(MBUCFMEMBGROUP.MEMBGROUP_CONTROL)) = '91' then 'บริษัท ไทยน้ำทิพย์ จำกัด'
               when ltrim(rtrim(MBUCFMEMBGROUP.MEMBGROUP_CONTROL)) = '92' then 'บริษัท ไทยน้ำทิพย์ แมนูแฟคเจอริ่ง จำกัด'
               when ltrim(rtrim(MBUCFMEMBGROUP.MEMBGROUP_CONTROL)) = '93' then 'บริษัท ไทยน้ำทิพย์ คอมเมอร์เชียล จำกัด'
               when ltrim(rtrim(MBUCFMEMBGROUP.MEMBGROUP_CONTROL)) = '94' then 'บริษัท โลจิสติกส์ เอเชีย จำกัด'
               when ltrim(rtrim(MBUCFMEMBGROUP.MEMBGROUP_CONTROL)) = '95' then 'บริษัท เบฟโปร เอเชีย จำกัด' else '' end) as MEMBGROUP_CONTROL,
                                 MBMEMBMASTER.MEMBGROUP_CODE,   
                                 MBUCFMEMBGROUP.MEMBGROUP_DESC,   
                                 MBMEMBMASTER.MEMBTYPE_CODE,   
                                 MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                 MBMEMBMASTER.MEMBER_STATUS,   
                                 MBMEMBMASTER.SEX,   
                                 MBMEMBMASTER.RESIGN_STATUS,   
                                 MBMEMBMASTER.SALARY_ID,  
                                 MBMEMBMASTER.MEMBER_TYPE,   
                                 MBMEMBMASTER.REMARK  
                            FROM MBMEMBMASTER LEFT JOIN MBUCFMEMBTYPE ON mbmembmaster.membtype_code = mbucfmembtype.membtype_code,     
                                 MBUCFMEMBGROUP,                      
                                 MBUCFPRENAME  
                           WHERE ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and  
                                 ( MBMEMBMASTER.MEMBGROUP_CODE = MBUCFMEMBGROUP.MEMBGROUP_CODE ) and  
                                 ( MBMEMBMASTER.COOP_ID = MBUCFMEMBGROUP.COOP_ID ) and  
                                 (( MBMEMBMASTER.COOP_ID = {0} ) AND  
                                  ( mbmembmaster.member_no = {1} )  )  ";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}