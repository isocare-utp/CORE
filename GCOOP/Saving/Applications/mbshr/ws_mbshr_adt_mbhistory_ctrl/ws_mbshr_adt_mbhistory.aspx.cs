using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.mbshr.ws_mbshr_adt_mbhistory_ctrl
{
    public partial class ws_mbshr_adt_mbhistory : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String PostSearch { get; set; }
        [JsPostBack]
        public String PostDetail { get; set; }


        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
            dsDetail.InitDsDetail(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.Visible = false;
                dsDetail.Visible = false;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostSearch")
            {
                String search = "";


                int sdate = dsMain.DATA[0].START_DATE.Year;
                int edate = dsMain.DATA[0].END_DATE.Year;
                //1/1/2043 0:00:00
                if ((sdate > 1900) && (edate > 1900))
                {
                    search += "and trunc(sys_logmodtb.entry_date) between to_date('" + dsMain.DATA[0].START_DATE.ToString("dd/MM/yyyy", WebUtil.EN) + @"','dd/MM/yyyy') 
                               and to_date('" + dsMain.DATA[0].END_DATE.ToString("dd/MM/yyyy",WebUtil.EN) + "','dd/MM/yyyy')";
                }

                if (dsMain.DATA[0].DOC_NO != "")
                {
                    search += "and sys_logmodtb.clmkey_desc like '%" + dsMain.DATA[0].DOC_NO + "%' ";
                }

                if (dsMain.DATA[0].USER_ID != "")
                {
                    search += "and sys_logmodtb.entry_id like '%" + dsMain.DATA[0].USER_ID + "%' ";
                }

                dsList.Visible = true;
                dsList.RetrieveList(search);
            }
            else if (eventArg == "PostDetail")
            {
                dsDetail.Visible = true;
                int row = Convert.ToInt32(HdCheckRow.Value);
                String doc_no = dsList.DATA[row].MODTBDOC_NO;
                dsDetail.RetrieveDetail(doc_no);
            }
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
            for (int i = 0; i < dsDetail.RowCount; i++)
            {
                if (dsDetail.DATA[i].CLM_NAME == "MEMB_NAME")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนชื่อ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMB_SURNAME")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนนามสกุล";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMBTYPE_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนประเภทสมาชิก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMBER_DATE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนวันที่เป็นสมาชิก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "RETRY_DATE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนวันที่เกษียณ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "RESIGN_STATUS")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนสถานะลาออก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "RESIGN_DATE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนวันที่ลาออก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_NO")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนที่อยู่";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_VILLAGE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนหมู่บ้าน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_NO")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนที่อยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_VILLAGE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนหมู่บ้านที่อยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CARD_PERSON")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเลขที่บัตรประชาชน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "WORK_DATE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนวันที่วันบรรจุ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_POSTCODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนรหัสไปรษณีย์";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_POSTCODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนรหัสไปรษณีย์ที่อยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "SEX")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเพศ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "PRENAME_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนคำนำหน้าชื่อ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MARIAGE_STATUS")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนสถานภาพสมรส";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MATE_NAME")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนชื่อคู่สมรส";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MATE_SALARYID")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเลขพนักงานคู่สมรส";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "POSITION_DESC")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนตำแหน่ง";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "SALARY_ID")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเลขที่พนักงาน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "SALARY_AMOUNT")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเงินเดือน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CLOSE_DATE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนวันที่ปิดบัญชี";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMBER_REF")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนอ้างอิงสมาชิก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "RESIGNCAUSE_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนสาเหตุลาออก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "KLONGTOON_FLAG")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนกองทุนสำรองเลี้ยงชีพ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "TRANSRIGHT_FLAG")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนหนังสือโอนสิทธ์เรียกร้อง";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ALLOWLOAN_FLAG")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนใบยินยอมคู่สมรส";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "DROPLOANALL_FLAG")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนงดกู้เงินทุกประเภท";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "DROPGURANTEE_FLAG")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนงดค้ำประกันเงินกู้";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "PAUSEKEEP_FLAG")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนงดออกใบเสร็จ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "REMARK")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนหมายเหตุ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_MOO")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนหมู่";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_SOI")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนซอย";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_ROAD")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนถนน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_EMAIL")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนอีเมล";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "PROVINCE_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนจังหวัด";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "AMPHUR_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเขต/อำเภอ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "TAMBOL_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนแขวง/ตำบล";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_PHONE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเบอร์โทรศัพท์";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "ADDR_MOBILEPHONE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเบอร์มือถือ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_MOO")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนหมู่ที่อาศัยอยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_SOI")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนซอยที่อาศัยอยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_ROAD")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนถนนที่อาศัยอยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRPROVINCE_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนจังหวัดที่อาศัยอยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRAMPHUR_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเขต/อำเภอที่อาศัยอยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRTAMBOL_CODE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนแขวง/ตำบลที่อาศัยอยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "CURRADDR_PHONE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเบอร์โทรศัพท์ที่ใช้อยู่ปัจจุบัน";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMB_ENAME")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนชื่ออังกฤษ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMB_ESURNAME")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนนามสกุลอังกฤษ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MEMBER_TYPE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนสถานะสมาชิก";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "MATE_CARDPERSON")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนบัตรฯคู่สมรส";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "INCOMEETC_AMT")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนเงินได้อื่นๆ";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "BIRTH_DATE")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนวันเกิด";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "DVAV1")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนการรับเงินปันผล";
                }
                else if (dsDetail.DATA[i].CLM_NAME == "KEEP1")
                {
                    dsDetail.DATA[i].CLM_NAME = "เปลี่ยนการเรียกเก็บ";
                }
                
                    
                
                
            }
        }
    }
}