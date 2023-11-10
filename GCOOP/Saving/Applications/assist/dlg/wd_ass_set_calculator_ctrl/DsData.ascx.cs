using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.assist.dlg.wd_ass_set_calculator_ctrl
{
    public partial class DsData : DataSourceRepeater
    {
        public DataSet1.ASSCALCULATORASSISTDataTable DATA { get; set; }

        public void InitDsData(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSCALCULATORASSIST;
            this.EventItemChanged = "OnDsDataItemChanged";
            this.EventClicked = "OnDsDataClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsData");
            this.Button.Add("b_del");
            this.Register();
        }

        public void RetrieveData(string as_asscode)
        {
            String sql = @"select a.coop_id, a.assisttype_code, a.calculator_type, 1 as check_flag, b.calculator_desc as calculator_name , a.sort_order, a.operation_type
                            from asscalculatorassist a
                                join assucfcalculator b on a.calculator_type = b.calculator_type
                            where a.assisttype_code = {0}                         
                            order by a.sort_order";
            sql = WebUtil.SQLFormat(sql, as_asscode);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }


    }
}