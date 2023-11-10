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
using Sybase.DataWindow;
using CoreSavingLibrary.WcfNFinance;
using System.Web.Services.Protocols;

namespace Saving.Applications.app_finance
{
    public partial class w_sheet_sendchq1 : PageWebSheet, WebSheet
    {
        private n_commonClient com;
        private n_financeClient fin;
        private DwThDate tDwDate;
        protected String postRowMove;
        protected String postRowMoveBack;
        protected String postCheckAll;
        protected String postRowDelete;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwDate = new DwThDate(DwDate, this);
            tDwDate.Add("send_date", "send_tdate");
            postCheckAll = WebUtil.JsPostBack(this, "postCheckAll");
            postRowMove = WebUtil.JsPostBack(this, "postRowMove");
            postRowDelete = WebUtil.JsPostBack(this, "postRowDelete");
            postRowMoveBack = WebUtil.JsPostBack(this, "postRowMoveBack");
        }

        public void WebSheetLoadBegin()
        {
            com = wcf.NCommon;
            fin = wcf.NFinance;

            if (!IsPostBack)
            {
                DwDate.InsertRow(0);
                DwSendChqAcc.InsertRow(0);

                DwDate.SetItemDateTime(1, "send_date", state.SsWorkDate);
                tDwDate.Eng2ThaiAllRow();

                DwUtil.RetrieveDDDW(DwSendChqAcc, "as_account", "sendchq.pbl", state.SsCoopId);

                Int32 resultXml = fin.of_init_sendchq(state.SsWsPass, state.SsCoopControl, state.SsUsername, state.SsWorkDate);
                if (resultXml.ToString() != "")
                {
                    DwSendChqList.Reset();
                    DwSendChqList.ImportString(resultXml.ToString(), FileSaveAsType.Xml);
                }
            }
            else
            {
                this.RestoreContextDw(DwDate);
                this.RestoreContextDw(DwSendChqAcc);
                this.RestoreContextDw(DwSendChqList);
                this.RestoreContextDw(DwSendChq);
                this.RestoreContextDw(DwWaitSendChq);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRowMove")
            {
                RowMoveTo();
            }
            else if (eventArg == "postRowMoveBack")
            {
                RowMoveBack();
            }
            else if (eventArg == "postRowDelete")
            {
                RowDelete();
            }
            else if (eventArg == "postCheckAll")
            {
                PostCheckAll();
            }
        }

        public void SaveWebSheet()
        {
            Int16 ai_accknow;
            String as_sendchq_xml, as_waitchq_xml, as_sendchqacc_xml;

            if ((DwSendChq.RowCount > 0) || (DwWaitSendChq.RowCount > 0))
            {
                try
                {
                    as_sendchq_xml = DwSendChq.Describe("DataWindow.Data.XML");
                    as_waitchq_xml = DwWaitSendChq.Describe("DataWindow.Data.XML");
                    as_sendchqacc_xml = DwSendChqAcc.Describe("DataWindow.Data.XML");

                    try
                    {
                        ai_accknow = Convert.ToInt16(RadioButtonAccKnow.SelectedValue);
                    }
                    catch
                    {
                        ai_accknow = 0;
                    }

                    int re = fin.of_postsavesendchq(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, as_sendchq_xml, as_waitchq_xml, as_sendchqacc_xml, ai_accknow);

                    if (re > 0)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");

                        Int32 result = fin.of_init_sendchq(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate);
                        if (result.ToString() != "")
                        {
                            DwSendChqList.Reset();
                            DwSendChqList.ImportString(result.ToString(), FileSaveAsType.Xml);
                        }
                    }
                }
                catch (SoapException ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ยังไม่ได้เลือกย้ายรายการ");
            }
        }

        public void WebSheetLoadEnd()
        {
            DwDate.SaveDataCache();
            DwSendChqAcc.SaveDataCache();
            DwSendChqList.SaveDataCache();
            DwSendChq.SaveDataCache();
            DwWaitSendChq.SaveDataCache();

            if (DwWaitSendChq.RowCount == 0)
            {
                RadioButtonAccKnow.Enabled = false;
            }
        }

        #endregion


        protected void PostCheckAll()
        {
            int row = 0, found = 0;
            Decimal Set = 0;
            row = DwSendChqList.RowCount;
            Boolean Select = CheckBoxAll.Checked;

            if (Select == true)
            {
                Set = 1;
            }
            else if (Select == false)
            {
                Set = 0;
            }

            for (int i = 1; i <= DwSendChqList.RowCount; i++)
            {
                DwSendChqList.SetItemDecimal(i, "select_flag", Set);
            }
        }

        private void RowDelete()
        {
            String as_chqno, as_bank, as_bankbranch;
            Int16 ai_seqno;
            Decimal flag;

            for (int i = 1; i <= DwSendChqList.RowCount; i++)
            {
                flag = DwSendChqList.GetItemDecimal(i, "select_flag");

                if (flag == 1)
                {
                    as_chqno = DwSendChqList.GetItemString(i, "check_no");
                    as_bank = DwSendChqList.GetItemString(i, "bank_code");
                    as_bankbranch = DwSendChqList.GetItemString(i, "bankbranch_code");
                    ai_seqno = Convert.ToInt16(DwSendChqList.GetItemDecimal(i, "seq_no"));

                    int re = fin.of_postcancelsendchq(state.SsWsPass, state.SsCoopId, as_chqno, as_bank, as_bankbranch, ai_seqno);
                    if (re == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อยแล้ว");

                        Int32 result = fin.of_init_sendchq(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate);
                        DwSendChqList.Reset();
                        DwSendChqList.ImportString(result.ToString(), FileSaveAsType.Xml);
                    }
                }
            }

            //this.Response.Redirect(state.SsUrl + "Applications/app_finance/w_sheet_sendchq.aspx");
        }

        private void RowMoveTo()
        {
            Decimal flag;
            String MoveTo;

            MoveTo = HfMoveTo.Value;

            if (MoveTo == "Send")
            {
                for (int i = 1; i <= DwSendChqList.RowCount; i++)
                {
                    try
                    {
                        flag = DwSendChqList.GetItemDecimal(i, "select_flag");
                    }
                    catch { flag = 0; }

                    if (flag == 1)
                    {
                        DwSendChqList.RowsMove(i, i, Sybase.DataWindow.DataBuffer.Primary, DwSendChq, DwSendChq.RowCount + 1, Sybase.DataWindow.DataBuffer.Primary);
                        i = 0;
                    }
                }
            }
            else if (MoveTo == "Wait")
            {
                RadioButtonAccKnow.Enabled = true;


                for (int i = 1; i <= DwSendChqList.RowCount; i++)
                {
                    try
                    {
                        flag = DwSendChqList.GetItemDecimal(i, "select_flag");
                    }
                    catch { flag = 0; }

                    if (flag == 1)
                    {
                        DwSendChqList.RowsMove(i, i, Sybase.DataWindow.DataBuffer.Primary, DwWaitSendChq, DwWaitSendChq.RowCount + 1, Sybase.DataWindow.DataBuffer.Primary);
                        i = 0;
                    }
                }
            }
        }

        private void RowMoveBack()
        {
            Decimal flag;
            String MoveFrom;

            MoveFrom = HfMoveTo.Value;

            if (MoveFrom == "Send")
            {
                for (int i = 1; i <= DwSendChq.RowCount; i++)
                {
                    try
                    {
                        flag = DwSendChq.GetItemDecimal(i, "select_flag");
                    }
                    catch { flag = 0; }

                    if (flag == 1)
                    {
                        DwSendChq.RowsMove(i, i, Sybase.DataWindow.DataBuffer.Primary, DwSendChqList, DwSendChqList.RowCount + 1, Sybase.DataWindow.DataBuffer.Primary);
                        i = 0;
                    }
                }
            }
            else if (MoveFrom == "Wait")
            {

                for (int i = 1; i <= DwWaitSendChq.RowCount; i++)
                {
                    try
                    {
                        flag = DwWaitSendChq.GetItemDecimal(i, "select_flag");
                    }
                    catch { flag = 0; }

                    if (flag == 1)
                    {
                        DwWaitSendChq.RowsMove(i, i, Sybase.DataWindow.DataBuffer.Primary, DwSendChqList, DwSendChqList.RowCount + 1, Sybase.DataWindow.DataBuffer.Primary);
                        i = 0;
                    }
                }
            }
        }
    }
}
