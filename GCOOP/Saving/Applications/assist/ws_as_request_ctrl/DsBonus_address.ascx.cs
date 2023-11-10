using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsBonus_address : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsBonus_address(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsBonus_addressItemChanged";
            this.EventClicked = "OnDsBonus_addressClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsBonus_address");
            this.Button.Add("b_linkaddress");
            this.Register();
        }

        public void RetrieveAddr(String as_reqno)
        {
            string sql = @" select distinct mt.assist_docno,
                            mg.invt_id as bonus_type,
                            mg.unit_code as bonus_unit,
                            mg.methpay_rcv as bonus_methpay,
                            mt.dis_addr,
                            mt.req_date as check_date
                            from assreqmaster mt
                            join ASSREQMASTERGIFT mg on  mt.assist_docno=mg.assist_docno
                            where mt.coop_id={0} and mt.assist_docno = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, as_reqno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

    }
}