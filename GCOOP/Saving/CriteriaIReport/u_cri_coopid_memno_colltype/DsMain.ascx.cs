using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_memno_colltype
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
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"select coop_id, coop_name from cmcoopmaster ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
        }

        public void DdLoancolltype()
        {
            string sql = @" 
                    SELECT LNUCFLOANCOLLTYPE.LOANCOLLTYPE_CODE,   
                           LNUCFLOANCOLLTYPE.LOANCOLLTYPE_DESC  
                      FROM LNUCFLOANCOLLTYPE  
                     WHERE LNUCFLOANCOLLTYPE.LOANCOLLTYPE_CODE in ('01','02','03','04')   
                ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "colltype_code", "LOANCOLLTYPE_DESC", "LOANCOLLTYPE_CODE");

        }
    }
}