using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl
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
            this.Button.Add("b_searchdeptno");
            this.Register();
        }
        public void RetrieveData(string coop_control, string dept_no)
        {
            string sql = @"SELECT DPDEPTMASTER.DEPTTYPE_CODE, DPDEPTMASTER.DEPTACCOUNT_NAME,DPDEPTMASTER.DEPTTYPE_CODE||' - '||DPDEPTTYPE.DEPTTYPE_DESC AS DEPTTYPE_DESC, 
                  DPDEPTMASTER.MEMBER_NO,DPDEPTMASTER.DEPTACCOUNT_NO,DPDEPTMASTER.DEPTACCOUNT_ENAME,DPDEPTMASTER.DEPT_OBJECTIVE
                FROM     DPDEPTMASTER, DPDEPTTYPE, CMCOOPMASTER
                WHERE  DPDEPTMASTER.DEPTTYPE_CODE = DPDEPTTYPE.DEPTTYPE_CODE AND DPDEPTMASTER.COOP_ID = CMCOOPMASTER.COOP_ID AND 
                DPDEPTTYPE.COOP_ID = CMCOOPMASTER.COOP_CONTROL and          
                ( ( DPDEPTMASTER.DEPTACCOUNT_NO = {1} ) and         
                ( DPDEPTMASTER.COOP_ID = {0} ) )  ";
            sql = WebUtil.SQLFormat(sql, coop_control, dept_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}