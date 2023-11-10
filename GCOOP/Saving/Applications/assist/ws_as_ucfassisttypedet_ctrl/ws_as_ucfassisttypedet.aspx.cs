using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.assist.ws_as_ucfassisttypedet_ctrl
{
    public partial class ws_as_ucfassisttypedet : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostSeleteData { get; set; }
        [JsPostBack]
        public string PostMaxseqno { get; set; }
        [JsPostBack]
        public string Postmoneytype { get; set; }
        [JsPostBack]
        public String jsPostYear { get; set; }

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsCopy.InitDsCopy(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                Sdt dt1;
                dsMain.GetAssYear();
                string sqlStr = @"select max(ass_year) + 543 as  ass_year from assucfyear where coop_id = {0}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
                dt1 = WebUtil.QuerySdt(sqlStr);
                dt1.Next();
                dsMain.DATA[0].process_year = Convert.ToString( dt1.GetInt32("ass_year") );
                dsMain.AssistType();//show data first
                dsMain.DATA[0].ASSISTTYPE_CODE = "00";
                dsList.MemberCattype();

            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                string ls_minpaytype = "";
                dsList.InsertLastRow();
                dsList.AssistPayType(dsMain.DATA[0].ASSISTTYPE_CODE , ref ls_minpaytype);
                dsList.MemberCattype();
                int li_maxrow = dsList.RowCount - 1;
                dsList.DATA[li_maxrow].MEMBCAT_CODE = "AL";
                dsList.DATA[li_maxrow].MEMBTYPE_CODE = "AL";
                dsList.DATA[li_maxrow].ASSISTPAY_CODE = ls_minpaytype;

                for (int li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    if (dsList.DATA[li_row].MEMBCAT_CODE == "AL")
                    {
                        dsList.FindDropDownList(li_row, "membtype_code").Enabled = false;
                        dsList.MembertypeRow("03", li_row);
                    }
                    else
                    {
                        dsList.FindDropDownList(li_row, "membtype_code").Enabled = true;
                        dsList.MembertypeRow(dsList.DATA[li_row].MEMBCAT_CODE, li_row);
                    }
                }
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsList.GetRowFocus();
                string ls_minpaytype = "";
                dsList.DeleteRow(row);
                dsList.AssistPayType(dsMain.DATA[0].ASSISTTYPE_CODE , ref ls_minpaytype );
                dsList.MemberCattype();

                for (int li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    if (dsList.DATA[li_row].MEMBCAT_CODE == "AL")
                    {
                        dsList.FindDropDownList(li_row, "membtype_code").Enabled = false;
                        dsList.MembertypeRow("03", li_row);

                    }
                    else
                    {
                        dsList.FindDropDownList(li_row, "membtype_code").Enabled = true;
                        dsList.MembertypeRow(dsList.DATA[li_row].MEMBCAT_CODE, li_row);
                    }
                }
            }
            else if (eventArg == PostSeleteData)
            {
                string ls_minpaytype = "";
                string ls_asscode = dsMain.DATA[0].ASSISTTYPE_CODE;
                int li_year =  int.Parse( dsMain.DATA[0].process_year) - 543;
                GetCalculate(ls_asscode);
                dsList.RetrieveData(ls_asscode, li_year);
                dsList.AssistPayType(dsMain.DATA[0].ASSISTTYPE_CODE, ref ls_minpaytype);
                dsList.MemberCattype();

 
                for (int li_row = 0; li_row < dsList.RowCount; li_row++)
                {
                    if (dsList.DATA[li_row].MEMBCAT_CODE == "AL")
                    {
                        dsList.FindDropDownList(li_row, "membtype_code").Enabled = false;
                        dsList.MembertypeRow("03", li_row);



                    }
                    else
                    {
                        dsList.FindDropDownList(li_row, "membtype_code").Enabled = true;
                        dsList.MembertypeRow(dsList.DATA[li_row].MEMBCAT_CODE, li_row);
                    }
                }
            }
            else if (eventArg == Postmoneytype)
            {
 
                int li_row = int.Parse( Hdrow.Value);
                if (dsList.DATA[li_row].MEMBCAT_CODE == "AL")
                {
                    dsList.FindDropDownList(li_row, "membtype_code").Enabled = false;
                    dsList.MembertypeRow("03", li_row);

                }
                else
                {
                    dsList.FindDropDownList(li_row, "membtype_code").Enabled = true;
                    dsList.MembertypeRow(dsList.DATA[li_row].MEMBCAT_CODE, li_row);
                }

            }
            else if (eventArg == jsPostYear)
            {
                CopyAssistPay(Hd_year.Value.ToString());
            }
        }

        private void GetCalculate(string ls_asscode)
        {
            string sqlStr = @"select 
                            	case calculate_flag when 1 then 'เกรดเฉลี่ย' when 2 then 'อายุ(เดือน)' when 3 then 'อายุการเป็นสมาชิก(เดือน)' when 4 then 'เงินเดือน' when 5 then 'ค่าเสียหาย' when 6 then 'วันที่รักษาพยาบาล' when 7 then 'กำหนดเอง' else '' end calculate_flag,
                                default_paytype
                        from ASSUCFASSISTTYPE where coop_id = {0} and assisttype_code = {1}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopId, ls_asscode);
            Sdt dt1 = WebUtil.QuerySdt(sqlStr);
            dt1.Next();
            dsMain.DATA[0].calculate_flag = dt1.GetString("calculate_flag");
            dsMain.DATA[0].default_paytype = dt1.GetString("default_paytype");
        }

        private void CheckMbCategoryType(string ls_asscode , ref int li_mbtypechk) //ตรวจสอบการเช็คสถานะสมาชิก
        {
            li_mbtypechk = 0;
            string sqlStr = @"select membtype_flag from assucfassisttype where coop_id = {0} and assisttype_code = {1}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, ls_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_mbtypechk = dtd.GetInt32("membtype_flag");
            }

        }

        private void CopyAssistPay(String year)
        {
            string ls_asstype, ls_mbtype = "", ls_paycode, ls_mbcat = "";
            string ls_inssql = "";
            decimal ldc_min, ldc_max, ldc_pay , ldc_seq;

            int copy_year = int.Parse(year);
            Int32 Year2 = Convert.ToInt32(dsMain.DATA[0].process_year) - 543;
            String sqlselectasstype = @"select * from assucfassisttypedet  where assist_year ='" + Year2 + "' and coop_Id = '" + state.SsCoopId + "'";
            Sdt asstype = WebUtil.QuerySdt(sqlselectasstype);

            while (asstype.Next())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('มีข้อมูลเงื่อนไขการจ่ายสวัสดิการปี " + (Year2 + 543).ToString() + " แล้ว กรุณาตรวจสอบ!!');", true);
                return;

            }

            dsCopy.RetrieveCopyData(copy_year);

                ExecuteDataSource exe = new ExecuteDataSource(this);
  
                for (int li_row = 0; li_row < dsCopy.RowCount; li_row++)
                {
                    ls_paycode = dsCopy.DATA[li_row].ASSISTPAY_CODE;
                    ls_mbcat = dsCopy.DATA[li_row].MEMBCAT_CODE;
                    ldc_min = dsCopy.DATA[li_row].MIN_CHECK;
                    ldc_max = dsCopy.DATA[li_row].MAX_CHECK;
                    ldc_pay = dsCopy.DATA[li_row].MAX_PAYAMT;
                    ls_asstype = dsCopy.DATA[li_row].ASSISTTYPE_CODE;
                    ls_mbtype = dsCopy.DATA[li_row].MEMBTYPE_CODE;
                    ldc_seq = dsCopy.DATA[li_row].SEQ_NO;

                    ls_inssql = @" insert into assucfassisttypedet
                                        ( coop_id, assisttype_code, assist_year, seq_no, assistpay_code,membcat_code, membtype_code, min_check, max_check, max_payamt )
                                   values
                                        ( {0}, {1},{2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} ) ";
                    ls_inssql = WebUtil.SQLFormat(ls_inssql, state.SsCoopControl, ls_asstype, Year2, ldc_seq, ls_paycode, ls_mbcat, ls_mbtype, ldc_min, ldc_max, ldc_pay);

                    exe.SQL.Add(ls_inssql);
                }

                exe.Execute();

                LtServerMessage.Text = WebUtil.CompleteMessage("คัดลอกข้อมูลเงื่อนไขการจ่ายสวัสดิการ สำเร็จ");
        }

        public void SaveWebSheet()
        {
            if (dsMain.DATA[0].ASSISTTYPE_CODE == "00")
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกสวัสดิการ");
            }
            else
            {
                try
                {
                    string ls_asstype, ls_mbtype = "", ls_paycode, ls_mbcat = "";
                    string ls_inssql = "";
                    int li_row, li_assyear;

                    decimal ldc_min, ldc_max, ldc_pay;


                    ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE.ToString();
                    li_assyear = int.Parse(dsMain.DATA[0].process_year) - 543;
                    for (li_row = 0; li_row < dsList.RowCount; li_row++)
                    {
                        ls_mbtype = dsList.DATA[li_row].MEMBTYPE_CODE.ToString().Trim();
                        if (ls_mbtype == "")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณาเลือกประเภทสมาชิก!!');", true);
                            return;
                        }

                        if (dsList.DATA[li_row].ASSISTPAY_CODE.ToString() == "")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('กรุณาเลือกประเภทการจ่ายสวัสดิการ!!');", true);
                            return;
                        }

                        if (dsList.DATA[li_row].MAX_PAYAMT < 1 && dsMain.DATA[0].default_paytype != "GIF")
                        {
                            this.SetOnLoadedScript("alert('กรุณากรอกยอดเงินที่จ่าย!!'); ");
                            return;
                        }
                    }

                    ExecuteDataSource exe = new ExecuteDataSource(this);
                    exe.SQL.Add("delete from assucfassisttypedet where assisttype_code = '" + ls_asstype + "' and assist_year = '" + li_assyear + "' and coop_id = '" + state.SsCoopId + "'");

                    for (li_row = 0; li_row < dsList.RowCount; li_row++)
                    {
                        ls_paycode = dsList.DATA[li_row].ASSISTPAY_CODE;
                        ls_mbcat = dsList.DATA[li_row].MEMBCAT_CODE;
                        ldc_min = dsList.DATA[li_row].MIN_CHECK;
                        ldc_max = dsList.DATA[li_row].MAX_CHECK;
                        ldc_pay = dsList.DATA[li_row].MAX_PAYAMT;

                        if (ls_mbcat == "AL")
                        {
                            ls_mbtype = "AL";
                        }
                        else
                        {
                            ls_mbtype = dsList.DATA[li_row].MEMBTYPE_CODE;
                        }

                        ls_inssql = @" insert into assucfassisttypedet
                                        ( coop_id, assisttype_code, assist_year, seq_no, assistpay_code,membcat_code, membtype_code, min_check, max_check, max_payamt )
                                   values
                                        ( {0}, {1},{2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} ) ";
                        ls_inssql = WebUtil.SQLFormat(ls_inssql, state.SsCoopControl, ls_asstype, li_assyear, li_row + 1, ls_paycode, ls_mbcat, ls_mbtype, ldc_min, ldc_max, ldc_pay);

                        exe.SQL.Add(ls_inssql);
                    }

                    exe.Execute();

                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");

                    string ls_minpaytype = "";

                    dsList.RetrieveData(dsMain.DATA[0].ASSISTTYPE_CODE, li_assyear);
                    dsList.AssistPayType(dsMain.DATA[0].ASSISTTYPE_CODE, ref ls_minpaytype);
                    dsList.MemberCattype();
                    for (int row = 0; row < dsList.RowCount; row++)
                    {
                        if (dsList.DATA[row].MEMBCAT_CODE == "AL")
                        {
                            dsList.FindDropDownList(row, "membtype_code").Enabled = false;
                            dsList.MembertypeRow("03", row);

                        }
                        else
                        {
                            dsList.FindDropDownList(row, "membtype_code").Enabled = true;
                            dsList.MembertypeRow(dsList.DATA[row].MEMBCAT_CODE, row);
                        }
                    }

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ" + ex);
                }
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}