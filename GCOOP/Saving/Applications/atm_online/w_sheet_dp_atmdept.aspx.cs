using System;
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
//using CommonLibrary;
//using CommonLibrary.WsCommon;
//using CommonLibrary.WsDeposit;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNDeposit;
using System.Web.Services.Protocols;
using DataLibrary;
//using DBAccess;

namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_atmdept : PageWebSheet, WebSheet
    {
        Sta ta;
        Sdt dt;
        protected String postAccountNo;
        private n_depositClient depServ;
        protected String testControls;
        private DwThDate thDwMain;
        private String pblFileName = "dp_atmdept.pbl";
        protected String postTrnDiv;
        protected String jsCardChange;
        private void JsPostAccountNo()
        {
            try
            {
                String dwAccNo = HdDeptaccount_no.Value.Trim();
                String accNo = "";
                accNo = depServ.of_analizeaccno(state.SsWsPass, dwAccNo);
                object[] argsDwMain = new object[2] { accNo, state.SsCoopId };
                try
                {
                    DwUtil.RetrieveDataWindow(DwMain, pblFileName, thDwMain, argsDwMain);
                }
                catch { }
                if (DwMain.RowCount < 1)
                {
                    throw new Exception("ไม่พบเลขบัญชีดังกล่าว");
                }
                String accFormat = ViewAccountNoFormat(state.SsWsPass, DwMain.GetItemString(1, "deptaccount_no"));
                DwMain.SetItemString(1, "deptaccount_no", accFormat);

                String deptclose_status = DwMain.GetItemString(1, "deptclose_status");
                if (deptclose_status != "0")
                {
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    throw new Exception("เลขที่บัญชี \"" + dwAccNo + "\" ถูกยกเลิกหรือปิดบัญชีไปแล้ว");
                }
                String depttype_group = DwMain.GetItemString(1, "depttype_group");
                if (depttype_group != "10")
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("เลขที่บัญชี \"" + dwAccNo + "\" ไม่ใช้ประเภท[10]เงินฝากออมทรัพย์");
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
        public String ViewAccountNoFormat(String wsPass, String deptAccountNo)
        {
            dt = ta.Query("select deptcode_mask from DPDEPTCONSTANT");
            if (!dt.Next())
            {
                ta.Close();
                throw new Exception("ไม่สามารถติดต่อกับ Database ได้");
            }
            else
            {
                ta.Close();
            }
            String format = dt.GetString(0).ToUpper();//"X-XX-XXXXXXX";
            char[] fc = format.ToCharArray();
            char[] ac = deptAccountNo.ToCharArray();
            String accNo = "";
            int j = 0;
            for (int i = 0; i < fc.Length; i++)
            {
                if (fc[i] != 'X')
                {
                    accNo += fc[i].ToString();
                }
                else
                {
                    try
                    {
                        accNo += ac[j++];
                    }
                    catch { accNo += ""; }
                }
            }
            return accNo;
        }
        private void JsPostTrnDiv()
        {
            try
            {
                // เรียกฟังก์ชั่น โอนปันผลเข้าเงินฝาก
                // By Phai เฉพาะกิจ
                //depServ.PostDeptTran_DIV(state.SsWsPass, 2555, state.SsWorkDate, state.SsUsername, state.SsBranchId);
                LtServerMessage.Text = WebUtil.WarningMessage("หน้าจอนี้ไม่รองรับ โอนปันผลเข้าเงินฝาก");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        private void JsCardChange()
        {
            try
            {
                String atmcard_id = HdAtmcard_ID.Value;
                atmcard_id = atmcard_id.Trim().Replace("-", "").Replace(" ", "");
                DwMain.SetItemString(1, "atmcard_id", atmcard_id);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        #region WebSheet Members

        public void InitJsPostBack()
        {
            postAccountNo = WebUtil.JsPostBack(this, "postAccountNo");
            postTrnDiv = WebUtil.JsPostBack(this, "postTrnDiv");
            jsCardChange = WebUtil.JsPostBack(this, "jsCardChange");
            //----------------------------------------------------------------
            thDwMain = new DwThDate(DwMain, this);
            thDwMain.Add("deptopen_date", "deptopen_tdate");
            thDwMain.Add("lastcalint_date", "lastcalint_tdate");
            //----------------------------------------------------------------
        }

        public void WebSheetLoadBegin()
        {
            //depServ = WsCalling.Deposit;
            if (IsPostBack)
            {
                depServ = wcf.NDeposit;
                this.RestoreContextDw(DwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postAccountNo")
            {
                JsPostAccountNo();
            }
            else if (eventArg == "postTrnDiv")
            {
                this.JsPostTrnDiv();
            }
            else if (eventArg == "jsCardChange")
            {
                JsCardChange();
            }
        }

        public void SaveWebSheet()
        {
            try
            {
                /////// Check atmcard_id ลงบัญชีธนาคารหรือยัง

                String atmcard_id = String.Empty;
                try
                {
                    atmcard_id = DwMain.GetItemString(1, "atmcard_id").Trim();
                }
                catch
                {
                    throw new Exception("กรุณาระบุเลขที่บัญชี ATM");
                }

                if (atmcard_id.Length < 10)
                {
                    throw new Exception("กรุณาระบุเลขที่บัญชี ATM ขั้นต่ำ 10 หลัก");
                }
                String SqlCheckSameCard = "Select count(*) as cnt from dpdeptmaster where deptclose_status = 0 and atmcard_id = '" + atmcard_id + "' and depttype_code = '10'";
                this.ConnectSQLCA();
                Sta ta = new Sta(sqlca.ConnectionString);
                Sdt dt = ta.Query(SqlCheckSameCard);
                if (dt.Next())
                {
                    Decimal cnt = dt.GetDecimal("cnt");
                    if (cnt > 0)
                    {
                        throw new Exception("เลขที่บัญชี ATM \"" + atmcard_id + "\" ซ้ำ");
                    }
                }

                //////////////////////////////////////////////////////////////////////
                String coop_id = "";// DwMain.GetItemString(1, "coop_id");
                coop_id = state.SsCoopId;
                String deptaccount_no = HdDeptaccount_no.Value.Replace("-","").Trim();
                String depttype_code = DwMain.GetItemString(1, "depttype_code");
                String SqlString = String.Empty;
                SqlString = "UPDATE DPDEPTMASTER SET ATMCARD_ID = '" + atmcard_id + "' WHERE DEPTACCOUNT_NO = '" + deptaccount_no + "' AND coop_id = '" + coop_id + "' AND DEPTTYPE_CODE = '" + depttype_code + "'";
                ta.Exe(SqlString);
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }

        public void WebSheetLoadEnd()
        {
            try
            {
                if (DwMain.RowCount <= 0)
                {
                    DwMain.InsertRow(0);
                }
            }
            catch { }
            DwMain.SaveDataCache();
        }

        #endregion
    }
}