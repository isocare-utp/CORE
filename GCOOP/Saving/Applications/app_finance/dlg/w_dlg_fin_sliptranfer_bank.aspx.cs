using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CoreSavingLibrary.WcfNCommon;
using System.Globalization;

namespace Saving.Applications.app_finance.dlg
{
    public partial class w_dlg_fin_sliptranfer_bank : PageWebDialog, WebDialog
    {
        private String sqlStr;
        private DwThDate tDwMain;
        #region WebDialog Members

        public void InitJsPostBack()
        {
            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("slip_date", "slip_tdate");
        }

        public void WebDialogLoadBegin()
        {
            try
            {

                this.ConnectSQLCA();
                sqlStr = DwList.GetSqlSelect();
                DwMain.SetTransaction(sqlca);
                DwList.SetTransaction(sqlca);
                if (!IsPostBack)
                {
                    DwMain.InsertRow(0);
                    DwMain.SetItemDateTime(1, "slip_date", state.SsWorkDate);
                    tDwMain.Eng2ThaiAllRow();

                    sqlStr = @"   SELECT FINSLIPTRANSFERBANK.SLIPTRANDOC_NO,
                                     FINSLIPTRANSFERBANK.MEMBER_NO,
                                     FINSLIPTRANSFERBANK.ACCOUNT_NAME,   
                                     FINSLIPTRANSFERBANK.TRANFER_AMT,   
                                     FINSLIPTRANSFERBANK.ACCOUNT_NO
                            FROM    FINSLIPTRANSFERBANK,   
                                    MBMEMBMASTER  
                              WHERE ( FINSLIPTRANSFERBANK.MEMBER_NO = MBMEMBMASTER.MEMBER_NO (+)) and  
                                    ( FINSLIPTRANSFERBANK.TRANFRE_STATUS = 1 ) AND     
                                     MBMEMBMASTER.COOP_ID = '" + state.SsCoopControl.Trim() + "' AND ( FINSLIPTRANSFERBANK.OPERATE_DATE = to_date('" +
                                    state.SsWorkDate.ToString("MM/dd/yyyy", WebUtil.EN) + "', 'MM/dd/yyyy') ) ";

                    DwList.SetSqlSelect(sqlStr);
                    DwList.Retrieve();
                }
                else
                {
                    DwMain.RestoreContext();
                    DwList.RestoreContext();
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void CheckJsPostBack(string eventArg)
        {
        }

        public void WebDialogLoadEnd()
        {
            DwMain.Reset();
            DwMain.InsertRow(0);
            DwMain.SetItemDateTime(1, "slip_date", state.SsWorkDate);
            tDwMain.Eng2ThaiAllRow();
            this.DisConnectSQLCA();
        }

        #endregion

        protected void B_Search_Click(object sender, EventArgs e)
        {

            try
            {

                String memno, memname, memsurname, slip_tdate, temp, sqlext;
                int rc = 0;
                try
                {
                    memno = (DwMain.GetItemString(1, "member_no")).Trim();
                }
                catch { memno = ""; }
                try
                {
                    memname = (DwMain.GetItemString(1, "member_name")).Trim();
                }
                catch { memname = ""; }
                try
                {
                    memsurname = (DwMain.GetItemString(1, "member_surname")).Trim();
                }
                catch { memsurname = ""; }
                try
                {
                    slip_tdate = (DwMain.GetItemString(1, "slip_tdate")).Trim();
                }
                catch { slip_tdate = ""; }
                sqlext = "";

                if (memno.Length > 0)
                {

                    sqlext = " and ( mbmembmaster.member_no like '%" + memno + "%') ";

                }


                if (memname.Length > 0)
                {
                    sqlext += " and ( mbmembmaster.memb_name like '" + memname + "%') ";
                }

                if (memsurname.Length > 0)
                {
                    sqlext += " and ( mbmembmaster.memb_surname like '" + memsurname + "%') ";
                }


                if (slip_tdate.Length > 0)
                {
                    sqlext += " and ( FINSLIPTRANSFERBANK.OPERATE_DATE = to_date('" + state.SsWorkDate.ToString("MM/dd/yyyy", WebUtil.EN) + "', 'MM/dd/yyyy') ) ";
                }



                sqlStr = @"   SELECT FINSLIPTRANSFERBANK.SLIPTRANDOC_NO,
                                     FINSLIPTRANSFERBANK.MEMBER_NO,
                                     FINSLIPTRANSFERBANK.ACCOUNT_NAME,   
                                     FINSLIPTRANSFERBANK.TRANFER_AMT,   
                                     FINSLIPTRANSFERBANK.ACCOUNT_NO
                            FROM    FINSLIPTRANSFERBANK,   
                                    MBMEMBMASTER  
                              WHERE ( FINSLIPTRANSFERBANK.MEMBER_NO = MBMEMBMASTER.MEMBER_NO (+)) and  
                                    ( FINSLIPTRANSFERBANK.TRANFRE_STATUS = 1 ) AND     
                                    ( FINSLIPTRANSFERBANK.COOP_ID = '" + state.SsCoopId.Trim() + "') ";

                temp = sqlStr + sqlext;
                DwList.SetSqlSelect(temp);
                rc = DwList.Retrieve();
                if (rc < 1)
                {
                    LtServerMessage.Text = "ไม่พบข้อมูลที่ค้นหา";
                }
                else { LtServerMessage.Text = ""; }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }
    }
}
