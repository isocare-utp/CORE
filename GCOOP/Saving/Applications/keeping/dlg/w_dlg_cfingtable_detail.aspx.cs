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
using DataLibrary;

namespace Saving.Applications.keeping.dlg
{
    public partial class w_dlg_cfingtable_detail : PageWebDialog, WebDialog
    {
        protected String postAddData;
        protected String postSaveData;
        protected String postDeleteData;

        private DwThDate tDwDetail;

        #region WebDialog Members

        public void InitJsPostBack()
        {
            postAddData = WebUtil.JsPostBack(this, "postAddData");
            postSaveData = WebUtil.JsPostBack(this, "postSaveData");
            postDeleteData = WebUtil.JsPostBack(this, "postDeleteData");

            tDwDetail = new DwThDate(DwDetail, this);
            tDwDetail.Add("effective_date", "effective_tdate");
        }

        public void WebDialogLoadBegin()
        {
            LtServerMessage.Text = "";
            HdIsSaved.Value = "false";
            HdEntryId.Value = state.SsUsername;
            HdEntryDate.Value = state.SsWorkDate.ToString("yyyy-MM-dd", WebUtil.EN);

            this.ConnectSQLCA();
            DwMain.SetTransaction(sqlca);
            DwDetail.SetTransaction(sqlca);
            if (!IsPostBack)
            {
                HdCommand.Value = Request["cmds"];
                try
                {
                    String code_tmp = "";
                    try
                    {
                        code_tmp = Request["code_tmp"];
                    }
                    catch { code_tmp = ""; }
                    DwMain.Retrieve(code_tmp);
                    if (HdCommand.Value == "insert")
                    {
                        DwDetail.InsertRow(0);
                        String a1 = DwMain.GetItemString(1, "loanintrate_code");
                        DwDetail.SetItemDate(1, "effective_date", state.SsWorkDate);
                        DwDetail.SetItemString(1, "loanintrate_code", DwMain.GetItemString(1, "loanintrate_code").Trim());
                        DwDetail.SetItemString(1, "entry_id", state.SsUsername);
                        DwDetail.SetItemDecimal(1, "loan_step", 999999999.99m);
                        DwDetail.SetItemDateTime(1, "entry_date", state.SsWorkDate);
                    }
                    else
                    {
                        DwDetail.Retrieve(code_tmp);
                    }
                    tDwDetail.Eng2ThaiAllRow();
                }
                catch { }
            }
            else
            {
                try
                {
                    this.RestoreContextDw(DwMain);
                    this.RestoreContextDw(DwDetail);
                }
                catch { }
            }
            if (HdCommand.Value == "insert")
            {
                DwMain.Modify("loanintrate_code.Protect=1");
                DwMain.Modify("loanintrate_desc.Protect=1");
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postSaveData")
            {
                JsPostSaveData();
            }
            else if (eventArg == "postDeleteData")
            {
                JsPostDeleteData();
            }
            else if (eventArg == "postAddData")
            {
                JsPostAddData();
            }
        }

        public void WebDialogLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        #endregion

        private void JsPostAddData()
        {
            DwDetail.InsertRow(1);
            String a1 = DwMain.GetItemString(1, "loanintrate_code");

            DwDetail.SetItemDate(1, "effective_date", state.SsWorkDate);
            DwDetail.SetItemString(1, "loanintrate_code", DwMain.GetItemString(1, "loanintrate_code").Trim());
            DwDetail.SetItemString(1, "entry_id", state.SsUsername);
            DwDetail.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            tDwDetail.Eng2ThaiAllRow();
        }

        private void JsPostDeleteData()
        {
            if (HdCommand.Value != "insert")
            {
                int row = 0;//
                try
                {
                    row = int.Parse(HdDeleteRow.Value);
                }
                catch
                {
                    row = -1;
                }
                if (row > 0)
                {
                    DwDetail.DeleteRow(row);
                }
            }
        }

        private void JsPostSaveData()
        {
            DwMain.UpdateData();
            try
            {
                if (HdCommand.Value == "insert")
                {
                    String a_loanintratecode = "";
                    Decimal a_loanstep = 0;
                    Decimal a_intrate = 0;
                    DateTime a_efdate = state.SsWorkDate;
                    String a_entryid = state.SsUsername;
                    DateTime a_entrydate = state.SsWorkDate;
                    String a_efdate2 = "", a_entrydate2 = "";
                    try
                    {
                        a_loanintratecode = DwDetail.GetItemString(1, "loanintrate_code");
                        a_loanstep = DwDetail.GetItemDecimal(1, "loan_step");
                        a_intrate = DwDetail.GetItemDecimal(1, "interest_rate");
                        a_efdate = DwDetail.GetItemDateTime(1, "effective_date");
                        a_efdate2 = a_efdate.ToString("dd/MM/yyyy", WebUtil.EN);
                        a_entrydate2 = a_entrydate.ToString("dd/MM/yyyy", WebUtil.EN);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ไม่สามารถกำหนดค่าเพื่อเพิ่มแถวได้ " + ex.Message);
                    }
                    Sta ta = new Sta(new DwTrans().ConnectionString);
                    try
                    {
                        //insert
                        String q_insert = "insert into  LNCFLOANINTRATEDET (LNCFLOANINTRATEDET.LOANINTRATE_CODE , LNCFLOANINTRATEDET.EFFECTIVE_DATE ,LNCFLOANINTRATEDET.LOAN_STEP ,LNCFLOANINTRATEDET.INTEREST_RATE , LNCFLOANINTRATEDET.ENTRY_ID ,LNCFLOANINTRATEDET.ENTRY_DATE ) values ('" + a_loanintratecode + "' ,to_date(  '" + a_efdate2 + "', 'dd/mm/yyyy' )," + a_loanstep + "," + a_intrate + ",'" + a_entryid + "', to_date(  '" + a_entrydate2 + "', 'dd/mm/yyyy' ))";
                        ta.Exe(q_insert);
                        ta.Close();
                    }
                    catch (Exception ei)
                    {
                        ta.Close();
                        throw new Exception("ไม่สามารถเพิ่มแถวได้ " + ei.Message);
                    }
                    HdIsSaved.Value = "true";
                    DwDetail.SetItemString(1, "edit_by", state.SsUsername + " " + state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.TH));
                }
                else
                {
                    DwDetail.UpdateData();
                    HdIsSaved.Value = "true";
                    DwDetail.Visible = false;
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");
            }
            catch (Exception e0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e0);
            }
        }
    }
}