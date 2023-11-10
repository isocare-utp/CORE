using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.ws_divsrv_constant_ucfdivitem_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.YRUCFDIVITEMTYPEDataTable DATA { get; set; }

        public void InitList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.YRUCFDIVITEMTYPE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_delete");
            this.Register();

        }
        public void RetrieveList()
        {
            string sql = @"SELECT YRUCFDIVITEMTYPE.DIVITEMTYPE_CODE,   
                                  YRUCFDIVITEMTYPE.DIVITEMTYPE_DESC,   
                                  YRUCFDIVITEMTYPE.SIGN_FLAG,   
                                  YRUCFDIVITEMTYPE.PRINT_CODE,   
                                  YRUCFDIVITEMTYPE.REVERSE_ITEMTYPE,   
                                  YRUCFDIVITEMTYPE.COOP_ID  
                             FROM YRUCFDIVITEMTYPE";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}