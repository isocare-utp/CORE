using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;
namespace Saving.Applications.ap_deposit.ws_dep_ucf_dptofromaccid_ctrl
{
    public partial class ws_dep_ucf_dptofromaccid : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMoneytypeCode { get; set; }
        [JsPostBack]
        public String PostInsertRow { get; set; }
        [JsPostBack]
        public String PostDeleteRow { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsList.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DDMoneyCode();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

            if (eventArg == "PostInsertRow")
            {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsList.InsertLastRow();
                int row = dsList.RowCount;
                for (int i = 0; i < row; i++)
                {
                    dsList.DATA[i].CASH_TYPE = moneytype_code;
                }
                dsList.DdMoneytype(moneytype_code);
                dsList.DS_Accountid();
                dsList.DdMoneytype(moneytype_code);
            }

            else if (eventArg == "PostMoneytypeCode")
            {
                string moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                dsList.RetrieveList(moneytype_code);
                dsList.DdMoneytype(moneytype_code);
                dsList.DS_Accountid();                
            }
            else if (eventArg == "PostDeleteRow")
            {

                String chktype = "";
                int rowDel = dsList.GetRowFocus();
                string cash_type = dsList.DATA[rowDel].CASH_TYPE;
                string account_id = dsList.DATA[rowDel].ACCOUNT_ID;
                //check delete
                if (of_checkdelete(account_id,cash_type))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบได้เนื่องจากมีการใช้งานอยู่");
                }
                else
                {
                    dsList.DeleteRow(rowDel);
                }
                
                dsList.DS_Accountid();
                dsList.DdMoneytype(cash_type);
            }
        }
        public bool of_checkdelete(string account_id, string cash_type)
        {
            bool returnvalue = false;
            try
            {
                string sql = "SELECT TOFROM_ACCID,CASH_TYPE FROM DPDEPTSLIP WHERE TOFROM_ACCID={0} AND CASH_TYPE={1}";
                sql = WebUtil.SQLFormat(sql, account_id, cash_type);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    returnvalue = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return returnvalue;
        }
        public void SaveWebSheet()
        {
            String cash_type = "";
            try
            {                
                String account_Desc = "";
                String account_id = "";
                int row = dsList.RowCount;
                String sqlStr = "";
                int num = 0;
                string sql = @"DELETE from DPUCFTOFROMACCID WHERE coop_id={0}  and CASH_TYPE ={1}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, dsMain.DATA[0].MONEYTYPE_CODE);
                WebUtil.ExeSQL(sql);
                for (int i = 0; i < row; i++)
                {
                    cash_type = dsList.DATA[i].CASH_TYPE;
                    account_Desc = dsList.DATA[i].ACCOUNT_DESC;
                    account_id = dsList.DATA[i].ACCOUNT_ID;
                    num = i + 1;

                    try
                    {
                        sqlStr = @"INSERT INTO DPUCFTOFROMACCID 
                        (COOP_ID,CASH_TYPE,ACCOUNT_DESC,ACCOUNT_ID)
                        VALUES ({0},{1},{2},{3})";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, cash_type, account_Desc, account_id);
                        WebUtil.ExeSQL(sqlStr);
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                }
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ");
            }
            dsList.DS_Accountid();
            dsList.DdMoneytype(cash_type);
        }

        public void WebSheetLoadEnd()
        {
        }

    }
}