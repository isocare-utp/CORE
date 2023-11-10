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

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_grpmangrtpermiss : PageWebSheet, WebSheet
    {
        private String codeid;


        //POSTBACK
        protected String newRecord;
        protected String getDetail;
        protected String deleteRecord;

        #region WebSheet Members

        public void InitJsPostBack()
        {
            newRecord = WebUtil.JsPostBack(this, "newRecord");
            getDetail = WebUtil.JsPostBack(this, "getDetail");
            deleteRecord = WebUtil.JsPostBack(this, "deleteRecord");
        }

        public void WebSheetLoadBegin()
        {
            codeid =  HiddenFieldID.Value.Trim();
            this.ConnectSQLCA();
            DwList.SetTransaction(sqlca);
            DwDetail1.SetTransaction(sqlca);
            DwDetail2.SetTransaction(sqlca);
            DwList.Retrieve();
            DwDetail1.Reset();
            DwDetail1.InsertRow(0);
            DwDetail2.Reset();
            DwDetail2.InsertRow(0);
            if (codeid != "")
            {
                GetDetail();
            }
            if (!IsPostBack)
            {
                String code_tmp = DwList.GetItemString(1, "mangrtpermgrp_code").Trim();
                HiddenFieldID.Value = code_tmp;
                GetDetail();
            }
            else
            {
                try
                {
                    //DwMain.SetItemString(1, "entry_id", state.SsUsername);
                    DwList.RestoreContext();
                    DwDetail1.RestoreContext();
                    DwDetail2.RestoreContext();

                }
                catch { }
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "getDetail")
            {
                GetDetail();
            }
            else if (eventArg == "newRecord")
            {
                //comment because of new record , it's user already
                NewRecord();
            }
            else if (eventArg == "deleteRecord")
            {
                DeleteRecord();
            }
        }

        private void NewRecord()
        {
            DwList.InsertRow(0);
            DwDetail1.Reset();
            DwDetail1.InsertRow(0);
            DwDetail2.Reset();
            DwDetail2.InsertRow(0);
        }

        public void SaveWebSheet()
        {
            //Check values before save record
            //check new record or edit row

            try
            {
                DwDetail1.UpdateData();
                codeid = DwDetail1.GetItemString(1, "mangrtpermgrp_code");
                int rrr = DwDetail2.RowCount;
                for (int i = 0; i < DwDetail2.RowCount; i++)
                {
                    DwDetail2.SetItemString(i + 1, "mangrtpermgrp_code", codeid);
                    DwDetail2.SetItemDecimal(i + 1, "seq_no", i + 1);
                }
                try
                {
                    DwDetail2.UpdateData();
                }
                catch
                {
                    DwDetail2.UpdateData();
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกข้อมูลวงเงินการค้ำประกันได้");
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                HiddenFieldID.Value = codeid;
                GetDetail();
            }
            catch (Exception e)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(e);
            }


        }

        public void WebSheetLoadEnd()
        {
            throw new NotImplementedException();
        }

        #endregion


        private void GetDetail()
        {
            //รายละเอียด DwDetail
            try
            {
                codeid = HiddenFieldID.Value.Trim();
                DwList.Reset();
                DwList.Retrieve();
                DwDetail1.Reset();
                DwDetail1.Retrieve();
                DwDetail1.SetFilter("mangrtpermgrp_code = '" + codeid + "'");
                DwDetail1.Filter();
                DwDetail2.Reset();
                DwDetail2.Retrieve(codeid);
            }
            catch
            {
                LtServerMessage.Text = WebUtil.WarningMessage("ลองใหม่อีกครั้ง");
                codeid = null;
                if (DwDetail1.RowCount < 0)
                {
                    DwDetail1.InsertRow(0);
                } if (DwDetail2.RowCount < 0)
                {
                    DwDetail2.InsertRow(0);
                }

            }
        }

        private void DeleteRecord()
        {
            try
            {
                int coderow = Convert.ToInt32(HiddenFieldIdRow.Value.Trim());
                DwList.DeleteRow(coderow);
                DwDetail1.DeleteRow(1);
                for (int i = 0; i < DwDetail2.RowCount; i++)
                {
                    DwDetail2.DeleteRow(i + 1);
                }
                DwList.UpdateData();
                DwDetail2.UpdateData();
                try
                {
                    DwDetail1.UpdateData();
                }
                catch { }
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลได้สำเร็จ");
                HiddenFieldID.Value = "";
                GetDetail();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถลบข้อมูลได้");
                GetDetail();

            }

        }

    }
}
