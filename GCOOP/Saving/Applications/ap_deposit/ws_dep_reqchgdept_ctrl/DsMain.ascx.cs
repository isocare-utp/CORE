using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_reqchgdept_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataDsMainDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataDsMain;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }
        public void RetriveData(string sql_text)
        {
            string sql = @"SELECT  NULL  AS DPREQCHG_DOC ,           DPDEPTMASTER.DEPTACCOUNT_NO ,     NULL AS DEPTMONTCHG_DATE ,      
            DPDEPTMASTER.DEPTMONTH_AMT AS DEPTMONTH_OLDAMT ,         0 AS DEPTMONTH_NEWAMT ,           0 AS APPROVE_FLAG , 
            NULL AS REMARK ,           NULL AS ENTRY_DATE ,           NULL AS ENTRY_ID ,  
            1 AS CHANGE_STATUS ,           DPDEPTMASTER.PRNCBAL ,    DPDEPTMASTER.COOP_ID ,    
            DPDEPTMASTER.MEMBER_NO,DPDEPTMASTER.DEPTACCOUNT_NAME,DPDEPTMASTER.PRNCBAL,DPDEPTMASTER.DEPTMONTH_AMT,
            MBUCFMEMBGROUP.MEMBGROUP_CODE,MBUCFMEMBGROUP.MEMBGROUP_DESC,
            MBUCFPRENAME.PRENAME_DESC,MBMEMBMASTER.MEMB_NAME,MBMEMBMASTER.MEMB_SURNAME
            FROM DPDEPTMASTER 
            LEFT JOIN MBMEMBMASTER ON MBMEMBMASTER.MEMBER_NO = DPDEPTMASTER.MEMBER_NO
            LEFT JOIN MBUCFPRENAME ON MBUCFPRENAME.PRENAME_CODE = MBMEMBMASTER.PRENAME_CODE
            LEFT JOIN MBUCFMEMBGROUP ON MBUCFMEMBGROUP.MEMBGROUP_CODE =  MBMEMBMASTER.MEMBGROUP_CODE 
            WHERE  DPDEPTMASTER.COOP_ID ={0} " + sql_text;
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("MEMBGROUP_FULLDESC", typeof(System.String));
            dt.Columns.Add("FULLNAME", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_groupdesc = row["MEMBGROUP_CODE"].ToString().Trim() + " - " + row["MEMBGROUP_DESC"].ToString().Trim();
                row["MEMBGROUP_FULLDESC"] = ls_groupdesc;
                string ls_fullname = row["PRENAME_DESC"].ToString().Trim() + row["MEMB_NAME"].ToString().Trim() + "  " + row["MEMB_SURNAME"].ToString().Trim();
                row["FULLNAME"] = ls_fullname;
            }
            this.ImportData(dt);
        }
    }
}