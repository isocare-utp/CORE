using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNCommon; // new common
using System.Globalization;
using Sybase.DataWindow;
//using CoreSavingLibrary.WcfNDeposit;
using CoreSavingLibrary.WcfNDeposit;// new deposit
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.ap_deposit
{
    public partial class w_dp_sheet_dptrans_div : PageWebSheet, WebSheet
    {
        protected string PostRetrive;
        protected string PostRetriveTrans;
        protected string PostSearchmemno;
        String pbl = "dp_depttrans.pbl";



        public void InitJsPostBack()
        {
            PostRetrive = WebUtil.JsPostBack(this, "PostRetrive");
            PostRetriveTrans = WebUtil.JsPostBack(this, "PostRetriveTrans");
            PostSearchmemno = WebUtil.JsPostBack(this, "PostSearchmemno");
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                DwMain.InsertRow(0);
                DwDetail.InsertRow(0);
                DwList.InsertRow(0);
                DwHead.InsertRow(0);

            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
                this.RestoreContextDw(DwList);
                this.RestoreContextDw(DwHead);
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostRetrive")
            {
                JsRetrive();
            }
            else if (eventArg == "PostRetriveTrans")
            {
                JsRetriveTrans();
            }
            else if (eventArg == "PostSearchmemno")
            {
                JstSearchmemno();
            }
        }

        public void SaveWebSheet()
        {
            String branch_operate = "001";
            String deptaccount_no = DwList.GetItemString(1, "deptaccount_no");
            String memcoop_id = DwList.GetItemString(1, "memcoop_id");
            String member_no = DwList.GetItemString(1, "member_no");
            String system_code = DwList.GetItemString(1, "system_code");
            Decimal tran_year = DwList.GetItemDecimal(1, "tran_year");
            DateTime tran_date = DwList.GetItemDateTime(1, "tran_date");
            Decimal seq_no = DwList.GetItemDecimal(1, "seq_no");
            Decimal deptitem_amt = DwList.GetItemDecimal(1, "deptitem_amt");

            try
            {
                String sql = @"UPDATE DPDEPTTRAN SET DEPTACCOUNT_NO='" + deptaccount_no +
                             @"'where MEMBER_NO='" + member_no + "' and TRAN_YEAR=" + tran_year +
                             @" and coop_id ='" + state.SsCoopId + "' and system_code ='DIV' ";
                WebUtil.QuerySdt(sql);

                LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
            DwList.SaveDataCache();
            DwHead.SaveDataCache();
        }

        #region WebSheet Members
        public void JsRetrive()
        {
            Decimal year = DwMain.GetItemDecimal(1, "year");
            DwUtil.RetrieveDataWindow(DwDetail, pbl, null, year);
        }
        public void JsRetriveTrans()
        {
            String accountno = Hd_deptaccountno.Value;
            DwUtil.RetrieveDataWindow(DwList, pbl, null, accountno);
        }

        public void JstSearchmemno()
        {

            String member_no = DwHead.GetItemString(1, "member_no");
            Decimal tran_year = DwMain.GetItemDecimal(1, "year");
            if (member_no.Length <= 8)
            {
                for (int i = member_no.Length; i < 8; i++)
                {
                    member_no = 0 + member_no;
                }

                String sql1 = @"select deptaccount_no from dpdepttran where member_no='" + member_no + "' and tran_year=" + tran_year + "  and coop_id ='" + state.SsCoopId + "' ";
                WebUtil.QuerySdt(sql1);

                Sdt dt = WebUtil.QuerySdt(sql1);
                if (dt.Next())
                {
                    String accountno = dt.GetString("deptaccount_no");
                    DwUtil.RetrieveDataWindow(DwList, pbl, null, accountno);
                    DwHead.SetItemString(1, "member_no", member_no);
                }
            }

        }
        #endregion
    }
}