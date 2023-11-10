using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNDeposit;
using System.Globalization;
using System.IO;
using System.Text;
using DataLibrary;
//using Saving.WcfFinance;


namespace Saving.Applications.ap_deposit
{
    public partial class w_sheet_dp_atm_trans_detail : PageWebSheet, WebSheet
    {
        protected String jsSearch;
        private DwThDate tDwMain;

        private String pbl = "dp_atm_memberdetail.pbl";
        public void InitJsPostBack()
        {
            jsSearch = WebUtil.JsPostBack(this, "jsSearch");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("st_date", "st_tdate");
            tDwMain.Add("end_date", "end_tdate");
        }

        public void WebSheetLoadBegin()
        {

            if (!IsPostBack)
            {
                DwMain.Reset();
                DwMain.InsertRow(0);
                DwMain.SetItemDateTime(1, "st_date", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                DwMain.SetItemDateTime(1, "end_date", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                tDwMain.Eng2ThaiAllRow();
                // DateTime operate_date = DwMain.GetItemDateTime(1, "operate_date");
                //DwUtil.RetrieveDataWindow(DwDetail, pbl, null);
            }
            else
            {
                this.RestoreContextDw(DwMain);
                this.RestoreContextDw(DwDetail);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearch":
                    Retrieve();
                    break;
            }
        }

        public void SaveWebSheet()
        {
            //Sta ta = new Sta(state.SsConnectionString);
            //try
            //{
            //    ta.Transection();
            //    ta.Close();
            //    DwDetail.SetFilter("item_status = 0 and join_flag = 1");
            //    DwDetail.Filter();
            //    for (int i = 1; i <= DwDetail.RowCount; i++)
            //    {
            //        String coop_id = String.Empty,
            //               member_no = String.Empty,
            //               account_no = String.Empty,
            //        SqlUpdate = String.Empty;
            //        Decimal item_status = 0,
            //                join_flag = 0;
            //        DateTime ccs_operate_date = DateTime.MinValue;

            //        item_status = DwDetail.GetItemDecimal(i, "item_status");
            //        join_flag = DwDetail.GetItemDecimal(i, "join_flag");

            //        if (item_status == 0 && join_flag == 1)
            //        {
            //            coop_id = DwDetail.GetItemString(i, "coop_id");
            //            member_no = DwDetail.GetItemString(i, "member_no");
            //            account_no = DwDetail.GetItemString(i, "account_no");
            //            ccs_operate_date = DwDetail.GetItemDateTime(i, "ccs_operate_date");

            //            SqlUpdate = @"UPDATE ATMTRANSACTION SET ITEM_STATUS = 1, RECONCILE_DATE = sysdate WHERE COOP_ID = '" + coop_id + "' AND MEMBER_NO = '" + member_no + "' AND CCS_OPERATE_DATE = TO_DATE('" + ccs_operate_date.ToString("ddMMyyyy HHmmss") + "','ddmmyyyy hh24miss')";
            //            ta.Query(SqlUpdate);
            //        }
            //    }
            //    ta.Commit();
            //    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
            //    ta.Close();
            //}
            //catch (Exception ex)
            //{
            //    ta.RollBack();
            //    ta.Close();
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            //}
            //try
            //{
            //    DwDetail.SetFilter("");
            //    DwDetail.Filter();
            //    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, Convert.ToDateTime(HdOperate_Date.Value));

            //}
            //catch
            //{ }
        }

        public void WebSheetLoadEnd()
        {
            tDwMain.Eng2ThaiAllRow();
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }

        public void Retrieve()
        {
            DwTrans SQLCA = new DwTrans();
            SQLCA.Connect();

            try
            {

            Decimal ck_mem = DwMain.GetItemDecimal(1, "ck_mem");
            Decimal ck_date = DwMain.GetItemDecimal(1, "ck_date");
            Decimal ck_type = DwMain.GetItemDecimal(1, "ck_type");
            Decimal ck_acc = DwMain.GetItemDecimal(1, "ck_acc");

            if (ck_mem == 0 && ck_date == 0 && ck_type == 0 && ck_acc == 0)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณา ติ๊กเลือก เงื่อนไขที่ค้นหา");
                SQLCA.Disconnect();
            }
            else
            {

                DwDetail.SetTransaction(SQLCA);
                String SQLBegin = DwDetail.GetSqlSelect();
                String SQLcon = String.Empty,
                       st_memno = String.Empty,
                       ed_memno = String.Empty,
                       type = String.Empty,
                       acc_no = String.Empty;
                DateTime st_date = DateTime.MinValue,
                         end_date = DateTime.MinValue;

                if (ck_mem == 1)
                {
                    try
                    {
                        st_memno = DwMain.GetItemString(1, "st_memno");
                        ed_memno = DwMain.GetItemString(1, "ed_memno");
                        SQLcon = SQLcon + " and to_number(ATMTRANSACTION.MEMBER_NO) between to_number('" + st_memno.Trim() + "') and  to_number('" + ed_memno.Trim() + "')  ";
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณา กรอกข้อมูล เลขที่สมาชิก");
                    }
                }
                if (ck_date == 1)
                {
                    st_date = DwMain.GetItemDateTime(1, "st_date");
                    end_date = DwMain.GetItemDateTime(1, "end_date");
                    SQLcon = SQLcon + " and trunc(ATMTRANSACTION.OPERATE_DATE) between to_date('" + st_date.ToString("ddMMyyyy") + "','ddmmyyyy') and to_date('" + end_date.ToString("ddMMyyyy") + "','ddmmyyyy')  ";
                }
                if (ck_type == 1)
                {
                    type = DwMain.GetItemString(1, "type");
                    SQLcon = SQLcon + " and ATMTRANSACTION.SYSTEM_CODE = '" + type + "' ";
                }
                if (ck_acc == 1)
                {
                    try
                    {
                        acc_no = DwMain.GetItemString(1, "acc_no");
                        SQLcon = SQLcon + " and ATMTRANSACTION.ACCOUNT_NO = '" + acc_no.Trim() + "' ";
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณา กรอกข้อมูล เลขที่บัญชี/สัญญา");
                    }
                }
                //try
                //{
                //    deptaccount_name = DWCtrl.GetItemString(1, "deptaccount_name");
                //}
                //catch { deptaccount_name = ""; }

                //try
                //{
                //    deptaccount_sname = DWCtrl.GetItemString(1, "deptaccount_sname");
                //}
                //catch { deptaccount_sname = ""; }

                //try
                //{
                //    card_person = DWCtrl.GetItemString(1, "card_person");
                //}
                //catch { card_person = ""; }

                //try
                //{
                //    deptopen_tdate = DWCtrl.GetItemString(1, "deptopen_tdate");
                //}
                //catch { deptopen_tdate = ""; }

                //try
                //{
                //    st_tdate = DWCtrl.GetItemString(1, "st_tdate");
                //    // st_DateEN = st_tdate.Substring(0, 6) + Convert.ToString(Convert.ToInt32(st_tdate.Substring(6, 4)) - 543);
                //    end_tdate = DWCtrl.GetItemString(1, "end_tdate");
                //    // end_DateEN = end_tdate.Substring(0, 6) + Convert.ToString(Convert.ToInt32(end_tdate.Substring(6, 4)) - 543);
                //}
                //catch
                //{
                //    st_tdate = "";
                //    end_tdate = "";
                //}

                //try
                //{
                //    deptacc_st = DWCtrl.GetItemString(1, "deptacc_st");
                //    deptacc_end = DWCtrl.GetItemString(1, "deptacc_end");
                //}
                //catch
                //{
                //    deptacc_st = "";
                //    deptacc_end = "";
                //}
                //try
                //{
                //    member_nos = DWCtrl.GetItemString(1, "member_nos");
                //    member_noe = DWCtrl.GetItemString(1, "member_noe");
                //}
                //catch
                //{
                //    member_nos = "";
                //    member_noe = "";
                //}


                //if (deptaccount_name != "")
                //{
                //    SQLcon = SQLcon + " and wcdeptmaster.deptaccount_name like '%" + deptaccount_name.Trim() + "%'";
                //}

                //if (deptaccount_sname != "")
                //{
                //    SQLcon = SQLcon + " and wcdeptmaster.deptaccount_sname like '%" + deptaccount_sname.Trim() + "%'";
                //}

                //if (card_person != "")
                //{
                //    SQLcon = SQLcon + " and wcdeptmaster.card_person like '%" + card_person.Trim() + "%'";
                //}

                //if (deptopen_tdate != "")
                //{
                //    deptopen_ENdate = deptopen_tdate.Substring(0, 6) + Convert.ToString(Convert.ToInt32(deptopen_tdate.Substring(6, 4)) - 543);
                //    SQLcon = SQLcon + " and wcdeptmaster.deptopen_date = to_date('" + deptopen_ENdate + "','dd/mm/yyyy') ";
                //}

                //if (st_tdate != "" && end_tdate != "")
                //{
                //    st_DateEN = st_tdate.Substring(0, 6) + Convert.ToString(Convert.ToInt32(st_tdate.Substring(6, 4)) - 543);
                //    end_DateEN = end_tdate.Substring(0, 6) + Convert.ToString(Convert.ToInt32(end_tdate.Substring(6, 4)) - 543);
                //    SQLcon = SQLcon + " and WCDEPTSLIP.DEPTSLIP_DATE between to_date('" + st_DateEN + "','dd/mm/yyyy') and to_date('" + end_DateEN + "','dd/mm/yyyy')";
                //}

                //if (deptacc_st != "" && deptacc_end != "")
                //{
                //    SQLcon = SQLcon + " and trim(WCDEPTSLIP.DEPTACCOUNT_NO) between '" + deptacc_st.Trim() + "' and '" + deptacc_end.Trim() + "'";
                //}

                //if (member_nos != "" && member_noe != "")
                //{
                //    SQLcon = SQLcon + " and trim(wcdeptmaster.member_no) between '" + member_nos.Trim() + "' and '" + member_noe.Trim() + "'";
                //}
                //String sqlSost = "";
                //String sort_sele = DWCtrl.GetItemString(1, "sort_sele");
                //if (sort_sele == "1")
                //{
                //    sqlSost = "order by WCDEPTSLIP.DEPTACCOUNT_NO";
                //}
                //else if (sort_sele == "2")
                //{
                //    sqlSost = "order by wcdeptmaster.member_no";
                //}
                //else
                //{
                //    sqlSost = "order by WCDEPTSLIPDET.DEPTSLIP_NO";
                //}

                String SQL;
                if (SQLcon == "")
                {
                    SQL = SQLBegin;
                }
                else
                {
                    SQL = SQLBegin + SQLcon;
                }
                try
                {
                    DwDetail.SetSqlSelect(SQL);
                    DwDetail.Retrieve();
                    SQLCA.Disconnect();
                }
                catch
                {
                    SQLCA.Disconnect();
                    LtServerMessage.Text = WebUtil.ErrorMessage("แก้ข้อผิดพลาด ไม่สามารถค้นหาข้อมูลได้");
                }
            }

               

                }
                catch (Exception ex)
                {
                    SQLCA.Disconnect();
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            
        }
    }
}
