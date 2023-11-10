using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl
{
    public partial class DsDetailCondo : DataSourceFormView
    {
        public DataSet1.DT_DETAILCONDODataTable DATA { get; set; }

        public void InitDsDetailCondo(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_DETAILCONDO;
            this.EventItemChanged = "OnDsDetailCondoItemChanged";
            this.EventClicked = "OnDsDetailCondoClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsDetailCondo");
            this.TableName = "lnmrtgmaster";
            //this.Button.Add("b_landsideno");            
            this.Register();
        }

        public void Retrieve(string as_mrtgno)
        {
            string ls_sql = @"select coop_id,
                mrtgmast_no,   
                pos_tambol,   
                pos_amphur,   
                pos_province,   
                land_docno,   
                condo_regisno,   
                condo_name,   
                condo_towerno,   
                condo_floor,   
                condo_roomno,   
                condo_roomsize  
                from lnmrtgmaster    
                where coop_id = {0}
                and mrtgmast_no = {1}  ";
            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, as_mrtgno);
            DataTable dt = WebUtil.Query(ls_sql);
            this.ImportData(dt);
        }
    }
}