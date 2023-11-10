using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.Applications.assist.ws_as_ucfassisttype_ctrl
{
    public partial class ws_as_ucfassisttype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewAssCode { get; set; }
        [JsPostBack]
        public string PostSendAssCode { get; set; }
        [JsPostBack]
        public string PostDelAssist { get; set; }
        [JsPostBack]
        public string PostNewRowPaytype { get; set; }
        [JsPostBack]
        public string PostDelPaytype { get; set; }

        public string sqlStr;

        public void InitJsPostBack()
        {
            dsCriteria.InitDs(this);
            dsDetail.InitDs(this);
            dsPaytype.InitDs(this);
            //dsMbtype.InitDs(this);
            dsAssCut.InitDs(this);
            dsAssLoan.InitDs(this);
            dsAssPause.InitDs(this);
            dsAssins.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)//show data first  
            {
                dsCriteria.AssistList();
                dsCriteria.DATA[0].ASSISTTYPE_CODE = "00";
            }

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewAssCode)
            {
                string ls_asscode = dsCriteria.DATA[0].ASSISTTYPE_CODE;

                dsCriteria.AssistList();

                this.of_retrieveasstype(ls_asscode);
            }
            else if (eventArg == PostNewRowPaytype)
            {
                dsPaytype.InsertAtRow(0);
                dsPaytype.SetItem(0, dsPaytype.DATA.COOP_IDColumn, state.SsCoopControl);//set value to primary key
                dsPaytype.DATA[0].ASSISTTYPE_CODE = dsDetail.DATA[0].ASSISTTYPE_CODE;

                int li_chkasscode = 0;
                string ls_asscode;
                for (int li_row = 1; li_row < dsPaytype.RowCount; li_row++)
                {
                    if (li_chkasscode < Convert.ToInt32(dsPaytype.DATA[li_row].ASSISTPAY_CODE))
                    {
                        li_chkasscode = Convert.ToInt32(dsPaytype.DATA[li_row].ASSISTPAY_CODE);
                    }
                }
                li_chkasscode = li_chkasscode + 1;
                ls_asscode = li_chkasscode.ToString();
                if (ls_asscode.Length == 1) { ls_asscode = '0' + ls_asscode; }
                dsPaytype.DATA[0].ASSISTPAY_CODE = ls_asscode;
            }
            else if (eventArg == PostDelAssist)
            {
                string ls_chktype = "";
                string ls_type = dsCriteria.DATA[0].ASSISTTYPE_CODE;


                //chk ประเภทคำขอ
                string sql = @"select assisttype_code from assreqmaster where coop_id={0} and assisttype_code={1} and rownum=1";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_type);
                Sdt dt = WebUtil.QuerySdt(sql);

                //chk ทะเบียนสวัสดิการ
                string sql_pay = @"select assisttype_code from asscontmaster where coop_id={0} and assisttype_code={1} and rownum=1";
                sql_pay = WebUtil.SQLFormat(sql_pay, state.SsCoopControl, ls_type);
                Sdt dt_pay = WebUtil.QuerySdt(sql_pay);

                if (dt.Next())
                {
                    ls_chktype = dt.GetString("assisttype_code");
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('สวัสดิการประเภทนี้มีการขอสวัสดิการแล้วไม่สามารถลบได้');", true);
                    return;
                }

                if (dt_pay.Next())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('สวัสดิการประเภทนี้มีการจ่ายสวัสดิการแล้วไม่สามารถลบได้');", true);
                    return;
                }

                try
                {
                    ls_chktype = @"delete from ASSUCFASSISTTYPEINS where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);

                    ls_chktype = @"delete from ASSUCFASSISTTYPELOAN where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);

                    ls_chktype = @"delete from ASSUCFASSISTTYPEPAUSE where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);

                    ls_chktype = @"delete from ASSUCFASSISTTYPEPAY where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);

                    ls_chktype = @"delete from ASSUCFASSISTTYPECUT where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);

                    ls_chktype = @"delete from ASSUCFASSISTTYPEDET where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);

                    ls_chktype = @"delete from ASSUCFASSISTTYPE where coop_id = {0} and assisttype_code={1} ";
                    ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_type);
                    WebUtil.ExeSQL(ls_chktype);
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ");
                    return;
                }

                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");

                dsCriteria.ResetRow();
                dsDetail.ResetRow();
                dsPaytype.ResetRow();
                dsAssCut.ResetRow();
                dsAssLoan.ResetRow();
                dsAssPause.ResetRow();
                dsAssins.ResetRow();
                dsCriteria.AssistList();
            }
            else if (eventArg == PostDelPaytype)
            {
                string ls_chktype = "";
                int row = dsPaytype.GetRowFocus();
                string ls_type = dsPaytype.DATA[row].ASSISTPAY_CODE;
                string ls_asscode = dsCriteria.DATA[0].ASSISTTYPE_CODE;

                int li_assyear = 0;

                string sqlyear = @"select max(ass_year) ass_year from assucfyear where coop_id = {0}";
                sqlyear = WebUtil.SQLFormat(sqlyear, state.SsCoopControl);
                Sdt dt_year = WebUtil.QuerySdt(sqlyear);
                if (dt_year.Next())
                {
                    li_assyear = dt_year.GetInt32("ass_year");
                }

                //chk เงื่อนไขการจ่าย
                string sql = @"select assistpay_code from ASSUCFASSISTTYPEDET where coop_id = {0} and assisttype_code = {1} and assistpay_code = {2} and assist_year = {3}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopControl, ls_asscode, ls_type, li_assyear);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('เงื่อนไขการจ่ายนี้ถูกใช้งานอยู่ กรุณาตรวจสอบ');", true); return;
                }
                else
                {
                    try
                    {
                        ls_chktype = @"delete from assucfassisttypepay where coop_id = {0} and assisttype_code = {1} and assistpay_code = {2}";
                        ls_chktype = WebUtil.SQLFormat(ls_chktype, state.SsCoopControl, ls_asscode, ls_type);
                        WebUtil.ExeSQL(ls_chktype);
                        dsCriteria.AssistList();
                        dsPaytype.Retrieve(ls_asscode);
                        LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลสำเร็จ");
                    }
                    catch
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลไม่สำเสร็จ");
                    }
                }
            }
            else if (eventArg == PostSendAssCode)
            {
                string ls_asscode = dsCriteria.DATA[0].ASSISTTYPE_CODE;

                this.of_retrieveasstype(ls_asscode);

            }
        }


        public void SaveWebSheet()
        {
            string ls_asscode = dsDetail.DATA[0].ASSISTTYPE_CODE;
   
            string ls_sql;
            if (dsCriteria.DATA[0].ASSISTTYPE_CODE == "00")
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกสวัสดิการ");
            }
            else
            {
                try
                {
                    ExecuteDataSource exe = new ExecuteDataSource(this);
                    exe.AddFormView(dsDetail, ExecuteType.Update);
                    exe.AddRepeater(dsPaytype);

                    // ส่วนของประเภทสวัสดิการที่มาหักออก
                    ls_sql = "delete assucfassisttypecut where assisttype_code ='" + ls_asscode + "'";
                    exe.SQL.Add(ls_sql);
                    for (int i = 0; i < dsAssCut.RowCount; i++)
                    {
                        if (dsAssCut.DATA[i].check_flag == 1)
                        {
                            ls_sql = @"insert into assucfassisttypecut(coop_id, assisttype_code, assisttype_cut) values ({0},{1},{2})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_asscode, dsAssCut.DATA[i].ASSISTTYPE_CUT);
                            exe.SQL.Add(ls_sql);
                        }
                    }

                    // ส่วนของประเภทเงินกู้ที่นำมาหักสวัสดิการ
                    ls_sql = "delete assucfassisttypeloan where assisttype_code ='" + ls_asscode + "'";
                    exe.SQL.Add(ls_sql);
                    for (int i = 0; i < dsAssLoan.RowCount; i++)
                    {
                        if (dsAssLoan.DATA[i].check_flag == 1)
                        {
                            ls_sql = @"insert into assucfassisttypeloan(coop_id, assisttype_code, assisttype_loan) values ({0},{1},{2})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_asscode, dsAssLoan.DATA[i].ASSISTTYPE_LOAN);
                            exe.SQL.Add(ls_sql);
                        }
                    }

                    // ส่วนของประเภทสวัสดิการที่ถูกงด
                    ls_sql = "delete assucfassisttypepause where assisttype_code ='" + ls_asscode + "'";
                    exe.SQL.Add(ls_sql);
                    for (int i = 0; i < dsAssPause.RowCount; i++)
                    {
                        if (dsAssPause.DATA[i].check_flag == 1)
                        {
                            ls_sql = @"insert into assucfassisttypepause(coop_id, assisttype_code, assisttype_pause) values ({0},{1},{2})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_asscode, dsAssPause.DATA[i].ASSISTTYPE_PAUSE);
                            exe.SQL.Add(ls_sql);
                        }
                    }


                    // ส่วนของตรวจสอบประกัน
                    ls_sql = "delete assucfassisttypeins where assisttype_code ='" + ls_asscode + "'";
                    exe.SQL.Add(ls_sql);
                    for (int i = 0; i < dsAssins.RowCount; i++)
                    {
                        if (dsAssins.DATA[i].check_flag == 1)
                        {
                            ls_sql = @"insert into assucfassisttypeins(coop_id, assisttype_code, assisttype_ins) values ({0},{1},{2})";
                            ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_asscode, dsAssins.DATA[i].INSURETYPE_CODE);
                            //ls_sql = WebUtil.SQLFormat(ls_sql, state.SsCoopControl, ls_asscode, "01");
                            exe.SQL.Add(ls_sql);
                        }
                    }

                    exe.Execute();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");


                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
                }
            }

        }

        public void WebSheetLoadEnd()
        {
        }

        private void of_retrieveasstype(string as_asscode)
        {
            if (dsCriteria.DATA[0].ASSISTTYPE_CODE != "00")
            {
                dsDetail.Retrieve(as_asscode);
                dsPaytype.Retrieve(as_asscode);
                dsAssCut.RetrieveAssCut(as_asscode);
                dsAssLoan.RetrieveAssLoan(as_asscode);
                dsAssPause.RetrieveAssPause(as_asscode);
                dsAssins.RetrieveAssins(as_asscode);
                dsDetail.DdAssistGrp();
            }
            else
            {
                dsDetail.ResetRow();
                dsPaytype.ResetRow();
                dsAssCut.ResetRow();
                dsAssLoan.ResetRow();
                dsAssPause.ResetRow();
                dsAssins.ResetRow();
            }
        }
    }
}