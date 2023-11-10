using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.keeping.ws_kp_slip_mthkeep_ctrl
{
    public partial class ws_kp_slip_mthkeep : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostPrint { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].year = WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DdMembgroup();
                dsList.RetrieveList();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostPrint)
            {
                string as_period = dsMain.DATA[0].year + dsMain.DATA[0].month.ToString("00");
                string as_sgroup = dsMain.DATA[0].smembgroup_code;
                string as_egroup = dsMain.DATA[0].emembgroup_code;
                string as_memtype = "";
                string as_memno = dsMain.DATA[0].member_no;
                string as_receiptno = dsMain.DATA[0].receipt_no;

                string[] minmax = ReportUtil.GetMinMaxMembgroup();
                if (as_sgroup.Length < 1)
                {
                    as_sgroup = minmax[0];
                }

                if (as_egroup.Length < 1)
                {
                    as_egroup = minmax[1];
                }

                for (int i = 0; i < dsList.RowCount; i++)
                {
                    if (dsList.DATA[i].operate_flag == 1)
                    {
                        if (as_memtype == "")
                        {
                            as_memtype = dsList.DATA[i].MEMBTYPE_CODE;
                        }
                        else
                        {
                            as_memtype += "," + dsList.DATA[i].MEMBTYPE_CODE;
                        }
                    }
                }

                if (as_memtype.Length < 1)
                {
                    for (int i = 0; i < dsList.RowCount; i++)
                    {
                        if (as_memtype == "")
                        {
                            as_memtype = dsList.DATA[i].MEMBTYPE_CODE;
                        }
                        else
                        {
                            as_memtype += "," + dsList.DATA[i].MEMBTYPE_CODE;
                        }
                    }
                }

                if (as_memno.Length < 1)
                {
                    as_memno = "%";
                }
                else
                {
                    as_memno = WebUtil.MemberNoFormat(as_memno);
                    dsMain.DATA[0].member_no = as_memno;
                }

                if (as_receiptno.Length < 1)
                {
                    as_receiptno = "%";
                }

                string recv_period = "";
                string sql = "select max(recv_period) as recv_period from kptempreceive where coop_id = '" + state.SsCoopControl + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    recv_period = dt.GetString("recv_period");
                }
                if (Convert.ToInt32(as_period) < Convert.ToInt32(recv_period))
                {
                    Printing.PrintKpmastreceive(this, state.SsCoopControl, as_period, as_sgroup, as_egroup, as_memtype, as_memno, as_receiptno);
                }
                else
                {
                    Printing.PrintKptempreceive(this, state.SsCoopControl, as_period, as_sgroup, as_egroup, as_memtype, as_memno, as_receiptno);
                }
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
    }
}