using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.mbshr_const.w_sheet_mb_mbucfremarkstatcode_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.MBUCFREMARKSTATCODEDataTable DATA { get; set; }  //**เปลี่ยนเป็นตารางของเรา อย่าเอาที่ก็อปมานะครับ **
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.MBUCFREMARKSTATCODE; //**เปลี่ยนเป็นตารางของเรา อย่าเอาที่ก็อปมานะครับ **
            this.Button.Add("b_del");
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public void retrieve()
        {
            string sql = "select * from MBUCFREMARKSTATCODE order by REMARKSTATTYPE_CODE"; //**เปลี่ยนเป็นตารางของเรา อย่าเอาที่ก็อปมานะครับ **
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}