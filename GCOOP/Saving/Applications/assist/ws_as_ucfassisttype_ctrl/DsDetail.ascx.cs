using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
namespace Saving.Applications.assist.ws_as_ucfassisttype_ctrl
{
    public partial class DsDetail : DataSourceFormView
    {
        public DataSet1.ASSUCFASSISTTYPEDataTable DATA { get; set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSUCFASSISTTYPE;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetail");
            this.Button.Add("b_cal");
            this.Register();
        }

        public void Retrieve(string ls_asscode)
        {
            string sql = "SELECT * FROM ASSUCFASSISTTYPE where COOP_ID = '" + state.SsCoopControl + "' and assisttype_code = '" + ls_asscode + "' ";
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

        public void DdAssistGrp()
        {
            string sql = @"select assisttype_group,assisttype_group +'-'+ assisttype_groupdesc as display 
                            , assisttype_group as sorter
                            from assucfassisttypegroup order by assisttype_group , sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "ASSISTTYPE_GROUP", "display", "assisttype_group");
        }
    }
}