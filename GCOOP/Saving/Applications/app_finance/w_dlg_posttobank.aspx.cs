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
using CoreSavingLibrary.WcfNCommon;
using Sybase.DataWindow;
using DataLibrary;
using System.Web.Services.Protocols;
using CoreSavingLibrary.WcfNFinance;

namespace Saving.Applications.app_finance
{
    public partial class w_dlg_posttobank : PageWebSheet, WebSheet
    {
        protected DwThDate tDwHead;
        protected n_financeClient fin;

        protected String postCheckAll;
        protected String postBankProcess;
        protected String postRetreiveData;
        protected String postCancelBankProcess;
       
      

        #region WebSheet Members

        public void InitJsPostBack()
        {
            tDwHead = new DwThDate(DwHead, this);
            tDwHead.Add("work_date", "work_tdate");

            postCheckAll = WebUtil.JsPostBack(this, "postCheckAll");
            postBankProcess = WebUtil.JsPostBack(this, "postBankProcess");
            postRetreiveData = WebUtil.JsPostBack(this, "postRetreiveData");
            postCancelBankProcess = WebUtil.JsPostBack(this, "postCancelBankProcess");
        }

        public void WebSheetLoadBegin()
        {
            try
            {
                fin = wcf.NFinance;

                if (!IsPostBack)
                {
                    DwHead.InsertRow(0);

                    DwHead.SetItemDateTime(1, "work_date", state.SsWorkDate);
                    tDwHead.Eng2ThaiAllRow();
                    PostRetreiveData(); 
                }
                else
                {
                    this.RestoreContextDw(DwHead);
                    this.RestoreContextDw(DwList);
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

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "postBankProcess":
                    ProcessPostToBank();
                    break;
                case "postRetreiveData":
                    PostRetreiveData();
                    break;
                case "postCheckAll":
                    PostCheckAll();
                    break;
                case "postCancelBankProcess":
                    ProcessCancelPostToBank();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            DwHead.SaveDataCache();
            DwList.SaveDataCache();
            this.DisConnectSQLCA();
        }

        #endregion



        protected void PostCheckAll()
        {
            int row = 0, found = 0;
            Decimal Set = 0;
            row = DwList.RowCount;
            Boolean Select = CheckBoxAll.Checked;

            if (Select == true)
            {
                Set = 1;
            }
            else if (Select == false)
            {
                Set = 0;
            }

            found = DwList.FindRow("post_flag=0", 1, row);
            while (found > 0)
            {
                DwList.SetItemDecimal(found, "select_flag", Set);

                found++;

                if (found > row)
                {
                    break;
                }
                found = DwList.FindRow("post_flag=0", found, row);
            }

        }

        protected void PostRetreiveData()
        {
            DateTime WorkDate = DwHead.GetItemDateTime(1, "work_date");
            string Xmlinfo = "";
            try
            {
                Int32 result = fin.of_init_posttobank(state.SsWsPass, state.SsCoopId, WorkDate, ref Xmlinfo);
                if (Xmlinfo != "")
                {
                    DwList.Reset();
                    DwList.ImportString(Xmlinfo, FileSaveAsType.Xml);
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

        protected void ProcessPostToBank()
        {
            try
            {
                int row = 0, found = 0;
                row = DwList.RowCount;
                found = DwList.FindRow("select_flag=1", 1, row);

                String as_item_xml = DwList.Describe("DataWindow.Data.Xml");
                int re = fin.of_fin_posttobank(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, as_item_xml);

                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    this.Response.Redirect(state.SsUrl + "Applications/app_finance/w_dlg_posttobank.aspx");
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

        protected void ProcessCancelPostToBank()
        {
            try
            {
                String as_item_xml = DwList.Describe("DataWindow.Data.Xml");
                int re = fin.of_postcancelposttobank(state.SsWsPass, state.SsCoopId, state.SsUsername, state.SsWorkDate, state.SsClientIp, as_item_xml);

                if (re == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกเรียบร้อย");
                    this.Response.Redirect(state.SsUrl + "Applications/app_finance/w_dlg_posttobank.aspx");
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
    }
}
