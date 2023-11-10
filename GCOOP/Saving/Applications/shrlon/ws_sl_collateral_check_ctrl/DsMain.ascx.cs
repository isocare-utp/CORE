using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_check_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.LNCONTCOLLDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.LNCONTCOLL;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Button.Add("b_search");
            this.Button.Add("b_print");
            this.Register();
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