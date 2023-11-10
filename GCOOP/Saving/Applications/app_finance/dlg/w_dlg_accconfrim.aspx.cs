using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_accconfrim : System.Web.UI.Page
    {
        WebState state;
        DwTrans sqlca;
        String pbl = "finslip_spc.pbl";
        protected void Page_Load(object sender, EventArgs e)
        {
            state = new WebState(Session, Request);
            sqlca = new DwTrans();
            sqlca.Connect();
            dw_data.SetTransaction(sqlca);



            if (!IsPostBack)
            {
                Dw_cri.InsertRow(0);
                Dw_cri.SetItemString(1, "member_no", "");
                Dw_cri.SetItemString(1, "member_name", "");
                DwUtil.RetrieveDataWindow(dw_data, pbl, null, state.SsWorkDate, state.SsCoopId);
            }

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String MemNoTxt = Dw_cri.GetItemString(1, "member_no");
                String MemNameTxt = Dw_cri.GetItemString(1, "member_name");


                if ((MemNoTxt != "") && (MemNameTxt == ""))
                {
                    dw_data.SetFilter("member_no like '%" + MemNoTxt + "%'");
                    dw_data.Filter();
                }
                else if ((MemNameTxt != "") && (MemNoTxt == ""))
                {
                    dw_data.SetFilter("member_name like '%" + MemNameTxt + "%'");
                    dw_data.Filter();
                }
                else
                {
                    DwUtil.RetrieveDataWindow(dw_data, pbl, null, state.SsWorkDate, state.SsCoopId);
                }
            }
            catch
            {
                DwUtil.RetrieveDataWindow(dw_data, pbl, null, state.SsWorkDate, state.SsCoopId);
                Dw_cri.SetItemString(1, "member_no", "");
                Dw_cri.SetItemString(1, "member_name", "");
            }

        }
    }
}