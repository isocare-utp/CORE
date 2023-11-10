using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_update_membtype_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Register();
        }
        public void RetrieveMain(String member_no)
        {
            String sql = @"  select mbmembmaster.coop_id,  
                             mbmembmaster.member_no,   
                             mbmembmaster.prename_code,   
                             mbmembmaster.memb_name,   
                             mbmembmaster.memb_surname,   
                             mbmembmaster.membgroup_code,   
                             mbmembmaster.salary_amount,   
                             mbmembmaster.membtype_code,        
                             mbucfprename.prename_desc,   
                             mbucfmembgroup.membgroup_desc
                        from mbmembmaster,    
                             mbucfmembgroup,   
                             mbucfprename
                       where (mbmembmaster.member_no = {1}) and
                             ( mbmembmaster.coop_id = {0} ) and  
                             ( mbmembmaster.coop_id = mbucfmembgroup.coop_id ) and   
                             ( mbmembmaster.prename_code = mbucfprename.prename_code ) and  
                             ( mbmembmaster.membgroup_code = mbucfmembgroup.membgroup_code ) ";
            sql = WebUtil.SQLFormat(sql,state.SsCoopControl,member_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdMemType(string membtypecode)
        {
            string sql = @"
             SELECT MEMBTYPE_CODE,MEMBTYPE_DESC
                    FROM MBUCFMEMBTYPE";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "MEMBTYPE_CODE", "MEMBTYPE_DESC", "MEMBTYPE_CODE");
        }

        public void DdMemTypeNew()
        {
            string sql = @"
             SELECT MEMBTYPE_CODE, MEMBTYPE_DESC, 1 as sorter
                    FROM MBUCFMEMBTYPE
             union 
             select '', '', 0 as sorter from dual order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "new_membtype_code", "MEMBTYPE_DESC", "MEMBTYPE_CODE");
        }
    }
}