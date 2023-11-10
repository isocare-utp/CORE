using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.admin.w_sheet_am_coopmaster_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        //1.property DATA สำหรับให้ sheet นำไปใช้งาน
        public DataSet1.CMCOOPMASTERDataTable DATA { get; set; }

        //2.ฟังก์ชันสำหรับ init
        public void InitDsList(PageWeb pw)
        {
            DataSet1 ds = new DataSet1();
            this.DATA = ds.CMCOOPMASTER;

            //บังคับ
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";

            this.Register();
        }

        //3.ฟังก์ชันสำหรับ Retrieve
        public void Retrieve()
        {
            string sql = "select * from cmcoopmaster";
            DataTable dt = WebUtil.Query(sql);
            ImportData(dt);
        }
    }
}