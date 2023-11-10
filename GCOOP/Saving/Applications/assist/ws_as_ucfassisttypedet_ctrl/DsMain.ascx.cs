using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_ucfassisttypedet_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ASSUCFASSISTTYPEDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPE;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void AssistType()
        {
            string sql = @"select
                        assisttype_code, assisttype_code + ' - ' + assisttype_desc as display, 1 as sorter
                        from assucfassisttype
                        union
                        select '00','กรุณาเลือกสวัสดิการ',0  
                        order by sorter, assisttype_code     ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "display", "assisttype_code");
        }
        public void GetAssYear()
        {
            string sql = @"select ass_year + 543 ass_show from assucfyear order by ass_year desc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "process_year", "ass_show", "ass_show");
        }
    }
}