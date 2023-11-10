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
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_cfinttable : PageWebSheet, WebSheet
    {

        private DwThDate tDwDetail;
        protected String IntRateType;
        protected String postEstimateDate;
        protected String NewRateCodeNo;
        protected String getDelete;

        private void JsIntRateType()
        {
            int rowNumber = Convert.ToInt16(HfRowNumber.Value);
            string rateCode = dw_list.GetItemString(rowNumber, "loanintrate_code");
            dw_main.Retrieve(rateCode);
            dw_detail.Retrieve(rateCode);
            tDwDetail.Eng2ThaiAllRow();

        }

        private void JsNewRateCodeNo()
        {
            String code = "";
            try
            {
                code = HiddenFieldCode.Value;
            }
            catch
            {
                code = "";
            }
            if (code != "")
            {
                dw_list.Reset();
                dw_list.Retrieve();
                dw_main.Reset();
                dw_main.Retrieve(code);
            }
        }


        #region WebSheet Members

        public void InitJsPostBack()
        {
            IntRateType = WebUtil.JsPostBack(this, "IntRateType");
            postEstimateDate = WebUtil.JsPostBack(this, "postEstimateDate");
            NewRateCodeNo = WebUtil.JsPostBack(this, "NewRateCodeNo");
            getDelete = WebUtil.JsPostBack(this, "getDelete");

            tDwDetail = new DwThDate(dw_detail, this);
            tDwDetail.Add("effective_date", "effective_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();

            dw_main.SetTransaction(sqlca);
            dw_list.SetTransaction(sqlca);
            dw_detail.SetTransaction(sqlca);

            if (!IsPostBack)
            {
                //dw_main.InsertRow(0);
                dw_list.Retrieve(state.SsCoopControl);
                String code_tmp = dw_list.GetItemString(1, "loanintrate_code");
                dw_main.Reset();
                dw_main.Retrieve(state.SsCoopControl, code_tmp);
                dw_detail.Reset();
                dw_detail.Retrieve(state.SsCoopControl, code_tmp);

            }
            else
            {
                try
                {
                    dw_list.RestoreContext();
                    dw_detail.RestoreContext();
                }
                catch { }
            }



        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "IntRateType")
            {
                JsIntRateType();
            }
            else if (eventArg == "NewRateCodeNo")
            {
                JsNewRateCodeNo();
            }
            else if (eventArg == "getDelete")
            {
                GetDelete();
            }

        }

        public void SaveWebSheet()
        {
            try
            {
              
                dw_list.UpdateData();
                dw_detail.UpdateData();
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกรายการสำเร็จ");

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกรายการไม่ผ่าน");
            }
        }

        public void WebSheetLoadEnd()
        {
            dw_list.SaveDataCache();
            if (dw_main.RowCount < 1)
            {
                dw_main.InsertRow(1);
            }

            //DW1.Modify("t_desc.Font.Height = '200 ~t If(GetRow() = 1, 500, 200)'")
            //int rr = Convert.ToInt32(HfRowNumber.Value);
            //try
            //{
            //    dw_list.Modify("loanintrate_code.Edit.FocusRectangle=true");
            //}
            //catch (Exception e){ }
        }

        #endregion

        protected void btnInsertRow_Click(object sender, EventArgs e)
        {
            dw_detail.InsertRow(1);
            string a1 = dw_main.GetItemString(1, "loanintrate_code");
            dw_detail.SetItemString(1, "coop_id", state.SsCoopControl);
            dw_detail.SetItemString(1, "loanintrate_code", dw_main.GetItemString(1, "loanintrate_code"));
            dw_detail.SetItemString(1, "entry_id", state.SsUsername);
            dw_detail.SetItemDateTime(1, "entry_date", state.SsWorkDate);

        }

        private void GetDelete()
        {
            String code = HiddenFieldCode.Value;
            Sta ta = new Sta(sqlca.ConnectionString);
            try
            {
                //delete from lncfloanintratedet where loanintrate_code = 'INT77';
                //commit;
                //delete from lncfloanintrate where loanintrate_code = 'INT77';
                //commit;

                String sql = @"delete from lncfloanintratedet where loanintrate_code = '" + code + "' and coop_id = '" + state.SsCoopControl + "'";
                String sql2 = @"delete from lncfloanintrate where loanintrate_code = '" + code + "' and coop_id = '" + state.SsCoopControl + "'";
                try
                {
                    ta.Exe(sql);
                    ta.Exe(sql2);
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("Can't Delete Record.");
                }

                LtServerMessage.Text = WebUtil.CompleteMessage("ลบรายการสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ลบรายการไม่สำเร็จ");
            }
            ta.Close();

            //Retrieve ...
            HiddenFieldCode.Value = "";
            dw_list.Reset();
            dw_list.Retrieve();
            dw_main.Reset();
            dw_detail.Reset();
            JsNewRateCodeNo();
        }
    }
}
