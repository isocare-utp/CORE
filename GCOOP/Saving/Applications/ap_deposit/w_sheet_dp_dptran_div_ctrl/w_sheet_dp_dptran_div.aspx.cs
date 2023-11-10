using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;

namespace Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl
{
    public partial class w_sheet_dp_dptran_div : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostInitDiv { get; set; }
        [JsPostBack]
        public string PostDetail { get; set; }
        [JsPostBack]
        public string PostMember { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsList.InitDs(this);
            dsDetail.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// แปลง format เลขบัญชี
        /// </summary>
        /// <param name="deptaccount_no">เลขบัญชี</param>
        /// <returns></returns>
        public String DeptFormat(String deptaccount_no)
        {
            deptaccount_no = deptaccount_no.Trim();
            deptaccount_no = Convert.ToInt64(deptaccount_no).ToString("00-00-00000-0");//hard

            return deptaccount_no;
        }

        /// <summary>
        /// เรียก list ทั้งหมดที่ปันผล
        /// </summary>
        /// <param name="year">ปีที่ปันผล</param>
        public void InitList(decimal year)
        {

            String deptaccount_no = "";
            dsList.RetrieveListDIV(year);
            dsMain.SetItem(0, dsMain.DATA.MEMBER_NOColumn, "");

            dsDetail.ResetRow();//clear value

            int row = dsList.RowCount;
            for (int i = 0; i < row; i++)
            {
                deptaccount_no = dsList.DATA[i].DEPTACCOUNT_NO.Trim();
                dsList.SetItem(i, dsList.DATA.DEPTACCOUNT_NOColumn, deptaccount_no);//
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            String member_no = dsMain.DATA[0].MEMBER_NO;
            Decimal tran_year = dsMain.DATA[0].TRAN_YEAR;

            if (eventArg == PostInitDiv)
            {
                InitList(tran_year);
            }
            else if (eventArg == PostMember)
            {
                String deptaccount_no = "";

                if (member_no != null || member_no != "")
                {
                    if (member_no.Length <= 8)
                    {
                        for (int i = member_no.Length; i < 8; i++)
                        {
                            member_no = 0 + member_no;
                        }
                    }
                    dsDetail.RetrieveDetail(member_no, tran_year);
                    dsMain.SetItem(0, dsMain.DATA.MEMBER_NOColumn, member_no);
                    //ถ้า retrieve ไม่พบช้อมูลให้ alert message
                    if (dsDetail.DATA[0].DEPTACCOUNT_NO == "" || dsDetail.DATA[0].DEPTACCOUNT_NO == null)
                    {
                        Response.Write(@"<script language='javascript'>alert('ไม่พบเลขสมาชิก');</script>");
                    }
                    else
                    {
                        //
                        deptaccount_no = dsDetail.DATA[0].DEPTACCOUNT_NO.Trim();
                        dsDetail.SetItem(0, dsDetail.DATA.DEPTACCOUNT_NOColumn, deptaccount_no);//

                        if (dsDetail.DATA[0].TRAN_STATUS == 1)
                        {
                            dsDetail.SetItem(0, dsDetail.DATA.TRAN_TEXTColumn, "ผ่านรายการแล้ว");//
                            dsDetail.FindTextBox(0, "deptaccount_no").Enabled = false;
                        }
                        else if (dsDetail.DATA[0].TRAN_STATUS < 0)
                        {
                            dsDetail.SetItem(0, dsDetail.DATA.TRAN_TEXTColumn, "ยกเลิกรายการ");//
                            dsDetail.FindTextBox(0, "deptaccount_no").Enabled = false;
                        }
                        else
                        {
                            dsDetail.SetItem(0, dsDetail.DATA.TRAN_TEXTColumn, "ยังไม่ผ่านรายการ");
                            dsDetail.FindTextBox(0, "deptaccount_no").Enabled = true;
                        }
                    }
                }
                else { InitList(tran_year); }

            }
            else if (eventArg == PostDetail)
            {
                int row = dsList.GetRowFocus();
                decimal year = dsMain.DATA[0].TRAN_YEAR;
                String deptaccount_no = dsList.DATA[row].DEPTACCOUNT_NO;
                String listMember_no = dsList.DATA[row].MEMBER_NO;
                //เรียกข้อมูลเพื่อโชว์รายละเอียด
                dsDetail.RetrieveDetail(listMember_no, deptaccount_no, year);

                //ตัดช่องว่างออก
                deptaccount_no = dsDetail.DATA[0].DEPTACCOUNT_NO.Trim();
                dsDetail.SetItem(0, dsDetail.DATA.DEPTACCOUNT_NOColumn, deptaccount_no);//

                //แปลง flag ให้เป็น text และกำหนดการแก้ไขเลขบัญชี
                if (dsDetail.DATA[0].TRAN_STATUS == 1)
                {
                    dsDetail.SetItem(0, dsDetail.DATA.TRAN_TEXTColumn, "ผ่านรายการแล้ว");//
                    dsDetail.FindTextBox(0, "deptaccount_no").Enabled = false;
                }
                else if (dsDetail.DATA[0].TRAN_STATUS < 0)
                {
                    dsDetail.SetItem(0, dsDetail.DATA.TRAN_TEXTColumn, "ยกเลิกรายการ");//
                    dsDetail.FindTextBox(0, "deptaccount_no").Enabled = false;
                }
                else
                {
                    dsDetail.SetItem(0, dsDetail.DATA.TRAN_TEXTColumn, "ยังไม่ผ่านรายการ");
                    dsDetail.FindTextBox(0, "deptaccount_no").Enabled = true;
                }
            }
        }

        public void SaveWebSheet()
        {
            //
            String deptaccount_no = dsDetail.DATA[0].DEPTACCOUNT_NO;
            String member_no = dsDetail.DATA[0].MEMBER_NO;
            Decimal tran_year = dsDetail.DATA[0].TRAN_YEAR;
            Decimal deptitem_amt = dsDetail.DATA[0].DEPTITEM_AMT;

            try
            {
                String sql = @"UPDATE DPDEPTTRAN SET DEPTACCOUNT_NO='" + deptaccount_no +
                             @"'where MEMBER_NO='" + member_no + "' and TRAN_YEAR=" + tran_year +
                             @" and coop_id ='" + state.SsCoopControl + @"' and system_code = 'DIV'";
                WebUtil.QuerySdt(sql);

                InitList(tran_year);
                LtServerMessage.Text = WebUtil.CompleteMessage("แก้ไขข้อมูลสำเร็จ");
            }

            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}