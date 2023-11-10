using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr.ws_mbshr_upload_mem_pic_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.MBMEMBMASTERDataTable DATA { get; private set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBMEMBMASTER;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventItemChanged = "OnDsMainItemChanged";
           
            this.Register();
            
        }

        public void Retrieve(string member_no) {
            string sql = @"select m.coop_id,m.member_no,p.prename_desc,m.memb_name,m.memb_surname 
from mbmembmaster m,mbucfprename p 
where  member_no={0}
and m.coop_id ={1}
and m.prename_code =p.prename_code";
            sql = WebUtil.SQLFormat(sql, member_no, state.SsCoopControl);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        
        }

        public void DdAccid(string member_no)
        {
            string sql = @"
             SELECT DEPTACCOUNT_NO ,DEPTTYPE_CODE  ,1 as sorter
                    FROM DPDEPTMASTER
                    WHERE DEPTCLOSE_STATUS = 0 and member_no={0}
                    union
                    select '--------กรุณาเลือกบัญชี--------','',0 as sorter order by sorter,DEPTACCOUNT_NO";
            sql = WebUtil.SQLFormat(sql, member_no);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "DEPTACCOUNT_NO", "DEPTACCOUNT_NO", "DEPTACCOUNT_NO");
        }


        
    }
}