using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfdepttypemidgroup_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPDEPTMIDGROUPDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPDEPTMIDGROUP;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");//ถ้าหน้าจอไม่มีปุ่มให้ลบหรือคอมเมนท์บรรทัดนี้
            this.Register();
        }

        /// <summary>
        /// ดึงข้อมูล
        /// </summary>
        public void Retrieve()
        {
            string sql = @"SELECT 
                            COOP_ID,
                            DEPTTYPE_GROUP,
                            DEPTTYPE_DESC,
                            DEPTCODE_FORMAT,
                            DW_FIRST_PAGE,
                            DW_MOVEMENT  
                        FROM DPDEPTMIDGROUP  
                        where COOP_ID = '" + state.SsCoopControl + "' ORDER BY DEPTTYPE_GROUP ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}