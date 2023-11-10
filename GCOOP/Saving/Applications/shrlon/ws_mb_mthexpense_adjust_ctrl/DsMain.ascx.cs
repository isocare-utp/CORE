using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_mb_mthexpense_adjust_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_memsearch");
            this.Register();
        }

        public void RetrieveMain(string member_no)
        {
            String sql = @"SELECT  MBMEMBMASTER.MEMBER_NO ,           
	                                MBUCFPRENAME.PRENAME_DESC ,           
	                                MBMEMBMASTER.MEMB_NAME ,           
	                                MBMEMBMASTER.MEMB_SURNAME ,           
	                                MBMEMBMASTER.MEMBGROUP_CODE ,           
	                                MBUCFMEMBGROUP.MEMBGROUP_DESC ,           
	                                MBMEMBMASTER.MEMBTYPE_CODE ,           
	                                MBUCFMEMBTYPE.MEMBTYPE_DESC ,           
	                                MBMEMBMASTER.MEMBER_STATUS ,           
	                                MBMEMBMASTER.SEX ,                    
	                                MBMEMBMASTER.MEMBER_TYPE   
                                FROM MBMEMBMASTER ,           
	                                MBUCFMEMBGROUP ,           
	                                MBUCFMEMBTYPE ,           
	                                MBUCFPRENAME   
                                WHERE ( MBUCFMEMBGROUP.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) 
	                                and ( MBUCFMEMBTYPE.MEMBTYPE_CODE = MBMEMBMASTER.MEMBTYPE_CODE ) 
	                                and ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) 
                                and (MBMEMBMASTER.MEMBER_NO={0})
 ";

            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
        public void DdMembtypeCode()
        {
            string sql = @" 
                SELECT membtype_code,
	                   membtype_desc, 1 as sorter
                FROM mbucfmembtype  
             
                union
                select '','', 0 from dual order by sorter, membtype_code
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "membtype_code", "membtype_desc", "membtype_code");

        }
    }
}