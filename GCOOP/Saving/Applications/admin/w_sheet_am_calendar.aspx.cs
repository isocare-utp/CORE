using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary.WcfNCommon;
using CoreSavingLibrary.WcfNAdmin;
using DataLibrary;

namespace Saving.Applications.admin
{
    public partial class w_sheet_am_calendar : PageWebSheet, WebSheet
    {
        protected String jsPostMonth;
        protected String jsPostYear;
        protected String jsPostChangeColor;
        protected String jsPostClickDetail;
        private String pbl = "am_amappstatus.pbl";
        public String CellName = "";
        public void InitJsPostBack()
        {
            jsPostMonth = WebUtil.JsPostBack(this, "jsPostMonth");
            jsPostYear = WebUtil.JsPostBack(this, "jsPostYear");
            jsPostChangeColor = WebUtil.JsPostBack(this, "jsPostChangeColor");
            jsPostClickDetail = WebUtil.JsPostBack(this, "jsPostClickDetail");
        }
        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                try
                {
                    HdCellName.Value = "";
                    DwMain.InsertRow(0);
                    DwMain.SetItemString(1, "year", (state.SsWorkDate.Year + 543).ToString());
                    TextBox1.Text = (state.SsWorkDate.Year + 543).ToString();
                    JsPostYear();
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
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
                case "jsPostMonth":
                    JsPostMonth();
                    break;
                case "jsPostYear":
                    JsPostYear();
                    break;
                case "jsPostChangeColor":
                    JsPostChangeColor();
                    break;
                case "jsPostClickDetail":
                    JsPostClickDetail();
                    break;
            }
        }
        public void SaveWebSheet()
        {
            try
            {
                int result = SaveCalendar();
                if (result == 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกสำเร็จ");
                 
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ");
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถบันทึกได้เนื่องจาก :" + ex); }
        }
        public void WebSheetLoadEnd()
        {
            DwMain.SaveDataCache();
            DwDetail.SaveDataCache();
        }
        public void JsPostChangeColor()
        {
            String cell_no = "", flag = "0", flag_num = "1", number = "1";

            try
            {
                //ตำแหน่ง cell
                cell_no = HdClick.Value;
                number = cell_no.Substring(4, cell_no.Length - 4);
                //เก็บไว้ว่า cell นี้มีการ modify
                HdCellName.Value = HdCellName.Value + "," + cell_no;
                //เช็คว่าเป็นสีอะไรอยู่ 
                flag = DwMain.GetItemString(1, "flag" + number);
                if (flag == "0")
                {
                    DwMain.SetItemString(1, "flag" + number, "1");
                }
                else
                {
                    DwMain.SetItemString(1, "flag" + number, "0");
                }


                String[] CellModify = HdCellName.Value.Split(',');
                for (int i = 0; i < CellModify.Length; i++)
                {
                    if (CellModify[i] != "" && CellModify[i] != null)
                    {
                        flag_num = CellModify[i].Substring(4, CellModify[i].Length - 4);
                        flag = DwMain.GetItemString(1, "flag" + flag_num);
                        if (flag == "1")
                        {
                            DwMain.Modify(CellModify[i] + ".color=255");
                        }
                        else
                        {
                            DwMain.Modify(CellModify[i] + ".color=0");
                        }
                    }
                    else
                    {
                        if (i > 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch { }

        }
        public void JsPostYear()
        {
            string year = "", sqlck = "";
            try
            {
                HdCellName.Value = "";
                DwDetail.Reset();
                year = DwMain.GetItemString(1, "year");
                sqlck = "select * from amworkcalendar where year =" + year + "";
                Sdt ck = WebUtil.QuerySdt(sqlck);
                if (ck.Next())
                {
                    DwUtil.RetrieveDataWindow(DwDetail, pbl, null, Convert.ToDecimal(year));
                    //LtServerMessage.Text = WebUtil.WarningMessage("ระบบมีข้อมูลปฏิทินประจำปี " + year);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ระบบยังไม่มีข้อมูลปฏิทินประจำปี " + year);
                }
            }
            catch { }
        }
        public void JsPostMonth()
        {
            string sqlck = "";
            int month = 0, year = state.SsWorkDate.Year, daycount = 0, column_start = 1;
            String day = "";
            DateTime start_date = state.SsWorkDate, end_date = state.SsWorkDate;
            try
            {
                HdCellName.Value = "";
                //เช็คให้ทุกช่องเป็นค่าว่างก่อน
                SetEmtry();
                //ดึงค่า ปี และ เดือน
                year = Convert.ToInt32(DwMain.GetItemString(1, "year"));
                month = Convert.ToInt32(DwMain.GetItemString(1, "month"));
                //จำนวนวันในเดือนและปีนั้นๆ
                daycount = DateTime.DaysInMonth(year - 543, month);
                //วันที่ 1 เป็นวันอะไร
                //string fristday = "01/" + month + "/" + (year - 543);
                start_date = new DateTime(year - 543, month, 1);
                day = start_date.DayOfWeek.ToString();

                //หาจุดเริ่มต้น
                switch (day)
                {
                    case "Sunday":
                        column_start = 1;
                        break;
                    case "Monday":
                        column_start = 2;
                        break;
                    case "Tuesday":
                        column_start = 3;
                        break;
                    case "Wednesday":
                        column_start = 4;
                        break;
                    case "Thursday":
                        column_start = 5;
                        break;
                    case "Friday":
                        column_start = 6;
                        break;
                    case "Saturday":
                        column_start = 7;
                        break;
                }
                HdCellStart.Value = column_start.ToString();
                //วน for เชตเลขวันที่
                int num = 1, cell = 1;
                cell = column_start;
                while (num <= daycount)
                {
                    DwMain.SetItemString(1, "cell" + cell, num.ToString());
                    num++;
                    cell++;
                }
                year = Convert.ToInt32(DwMain.GetItemString(1, "year"));
                sqlck = "select * from amworkcalendar where year =" + year + " and month='" + month + "'";
                Sdt ck = WebUtil.QuerySdt(sqlck);
                if (ck.Next())
                {
                    DwMain.SetItemString(1, "postingdate", ck.GetString("postingdate"));
                    DwMain.SetItemString(1, "processdate", ck.GetString("processdate"));
                    HdCellName.Value = ck.GetString("workdays");
                    Char[] Workday = HdCellName.Value.ToCharArray();
                    HdCellName.Value = "";
                    cell = column_start;
                    for (int i = 0; i < Workday.Length; i++)
                    {
                        if (Workday[i] == 'H')
                        {
                            DwMain.SetItemString(1, "flag" + cell, "1");
                            HdCellName.Value = HdCellName.Value + ",cell" + cell;
                        }
                        else
                        {
                            DwMain.SetItemString(1, "flag" + cell, "0");
                            HdCellName.Value = HdCellName.Value + ",cell" + cell;
                        }
                        cell++;
                    }
                    String[] CellModify = HdCellName.Value.Split(',');
                    String flag = "0", flag_num = "1";
                    for (int i = 0; i < CellModify.Length; i++)
                    {
                        if (CellModify[i] != "" && CellModify[i] != null)
                        {
                            flag_num = CellModify[i].Substring(4, CellModify[i].Length - 4);
                            flag = DwMain.GetItemString(1, "flag" + flag_num);
                            if (flag == "1")
                            {
                                DwMain.Modify(CellModify[i] + ".color=255");
                            }
                            else
                            {
                                DwMain.Modify(CellModify[i] + ".color=0");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }
        public void SetEmtry()
        {
            for (int i = 1; i <= 42; i++)
            {
                DwMain.SetItemString(1, "cell" + i, "");
            }
        }
        //--------------------
        protected void Button1_Click(object sender, EventArgs e)
        {

            string year = TextBox1.Text;
            try
            {
                int check = Convert.ToInt32(year);
                DwMain.SetItemString(1, "year", TextBox1.Text);
                JsPostYear();
            }
            catch
            {
                year = "";
                DwMain.SetItemString(1, "year", "");
                LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกตัวเลข");
            }
            DwMain.SetItemString(1, "month", "");
            SetEmtry();
        }
        protected void Button1_Click2(object sender, EventArgs e)
        {
            //int daycount = 0;
            string year = "", day = "", sqlinsert = "", branch_id = "";
            int status = 0, year_now = 0, firstworkdate = 0, lastworkdate = 0;
            DateTime check_date = state.SsWorkDate;
            //เพิ่มปีปฏิทิน
            try
            {
                try
                {
                    year = TextBox1.Text;
                    int check = Convert.ToInt32(year);
                    DwMain.SetItemString(1, "year", year.ToString());
                    status = 1;
                    string sqlcheck = "select year from amworkcalendar where year=" + year;
                    Sdt ck = WebUtil.QuerySdt(sqlcheck);
                    if (ck.Next())
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("มีปีบัญชี :" + year + " แล้ว");
                        status = 0;
                    }
                }
                catch
                {
                    status = 0;
                    year = "";
                    DwMain.SetItemString(1, "year", "");
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณากรอกตัวเลข");
                }


                if (status == 1)
                {
                    //for วน 12 เดือน
                    branch_id = state.SsCoopId;
                    String[] WorkDays = new String[13];
                    int[] daycount = new int[13];
                    year_now = Convert.ToInt32(year) - 543;
                    for (int i = 1; i <= 12; i++)
                    {
                        daycount[i] = DateTime.DaysInMonth(year_now, i);
                        //for วนทีละวัน
                        for (int j = 1; j <= daycount[i]; j++)
                        {
                            check_date = new DateTime(year_now, i, j);
                            day = check_date.DayOfWeek.ToString();

                            if (day == "Sunday" || day == "Saturday")
                            {
                                WorkDays[i] += "H";
                            }
                            else
                            {
                                WorkDays[i] += "W";
                            }
                        }
                    }
                    //

                    //-----insert-----
                    for (int i = 1; i <= 12; i++)
                    {
                        firstworkdate = WorkDays[i].IndexOf('W') + 1;
                        lastworkdate = WorkDays[i].LastIndexOf('W') + 1;

                        sqlinsert = @"INSERT INTO AMWORKCALENDAR  
                        ( COOP_ID,YEAR,MONTH,   
                        FIRSTWORKDATE,LASTWORKDATE,
                        DAYSINMONTH,WORKDAYS)  
                        VALUES ( '" + branch_id + "'," + year + "," + i + @",
                        " + firstworkdate + "," + lastworkdate + "," + daycount[i] + ",'" + WorkDays[i] + "')";
                        Sdt ins = WebUtil.QuerySdt(sqlinsert);
                    }//end for insert

                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถเพิ่มปีปฏิทินได้เนื่องจาก :" + ex);
            }
        }
        public void JsPostClickDetail()
        {
            int r = 0;
            r = Convert.ToInt32(HdRow.Value);
            DwMain.SetItemString(1, "year", TextBox1.Text);
            DwMain.SetItemString(1, "month", DwDetail.GetItemString(r, "month"));
            JsPostMonth();
        }
        public int SaveCalendar()
        {
            int result = 0, column_start = 0, year = 0, month = 0, daycount = 0, firstworkdate = 0, lastworkdate = 0, processdate = 0, postingdate = 0;
            string WorkDays = "", flag = "0";
            //วันที่หนึ่งเริ่มที่ Cell ไหน
            column_start = Convert.ToInt32(HdCellStart.Value);
            //ดึงข้อมูล year , month
            year = Convert.ToInt32(DwMain.GetItemString(1, "year"));
            month = Convert.ToInt32(DwMain.GetItemString(1, "month"));

            try
            {
                processdate = Convert.ToInt32(DwMain.GetItemString(1, "processdate"));
            }
            catch
            {
                processdate = 0;
            }

            try
            {
                postingdate = Convert.ToInt32(DwMain.GetItemString(1, "postingdate"));
            }
            catch
            {
                postingdate = 0;

            }
           
            //จำนวนวันในเดือนและปีนั้นๆ
            daycount = DateTime.DaysInMonth(year - 543, month);

            for (int i = column_start; i < daycount + column_start; i++)
            {
                flag = DwMain.GetItemString(1,"flag"+i);
                if (flag == "1")
                {
                    WorkDays += "H";
                }
                else
                {
                    WorkDays += "W";
                }
            }

            firstworkdate = WorkDays.IndexOf('W') + 1;
            lastworkdate = WorkDays.LastIndexOf('W') + 1;
            string sqlupdatecalendar = @"  UPDATE AMWORKCALENDAR  
            SET FIRSTWORKDATE = "+firstworkdate+",LASTWORKDATE = "+lastworkdate + ", processdate = " + processdate +@",   
            postingdate = " + postingdate + ", DAYSINMONTH = " + daycount + ", WORKDAYS = '" + WorkDays + @"'   
            WHERE COOP_ID = '" + state.SsCoopId + "' and YEAR = " + year + " and MONTH = " + month + "";//
            Sdt up = WebUtil.QuerySdt(sqlupdatecalendar);
            DwUtil.RetrieveDataWindow(DwDetail, pbl, null, Convert.ToDecimal(year));
            JsPostClickDetail();
            result = 1;
            
            return result;
        }
    }
}