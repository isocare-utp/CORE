using System;
using CoreSavingLibrary;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Saving.Applications.account
{
    public class accFunction
    {
        /* function หาวันสุดท้ายของเดือน ค.ศ.
         * 
         * month = "1","2","3",.......,"11","12"
         * year = ปี ค.ศ. "1999"
         * 
         * ex: GetFindToLDayOfM("1","1999");
         * then: return "31 ";
         */
        public string GetFindToLDayOfM(string month, string year)
        {
            return GetFindToLDayOfM(month, year, "us");
        }

        /* function หาวันสุดท้ายของเดือน พ.ศ.
         * 
         * month = "1","2","3",.......,"11","12"
         * year = ปี พ.ศ. "2552"
         * type_year = "th"
         * 
         * ex: GetFindToLDayOfM("1","2552","th");
         * then: return "31 ";
         */
        public string GetFindToLDayOfM(string month, string year, string type_year)
        {
            if (type_year.Equals("th"))
            {
                year = Convert.ToString(Convert.ToInt32(year) - 543);
            }
            switch (Convert.ToInt32(month))
            {
                case 2:
                    if ((Convert.ToInt32(year) % 4) == 0)
                    {
                        return "29 ";
                    }
                    else { return "28 "; }
                //break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return "31 ";
                //break;
                case 4:
                case 6:
                case 9:
                case 11:
                    return "30 ";
                //break;
            }
            return null;
        }

        /* function เปลี่ยนเดือนที่ เป็นชื่อเดือน แบบ สั่น
         * 
         * i_mth = "1","2","3",.......,"11","12"
         * 
         * ex: CnvStrToThaiM("1");
         * then: return "ม.ค. ";
         */
        public string CnvStrToThaiM(string i_mth)
        {
            return CnvStrToThaiM(i_mth, 1);
        }

        /* function เปลี่ยนเดือนที่ เป็นชื่อเดือน แบบ สั้น/ยาว
         * 
         * i_mth = "1","2","3",.......,"11","12"
         * หรือ
         * i_mth = "01","02","03",.......,"11","12"
         * 
         * ex: CnvStrToThaiM("1",0);
         * then: return "มกราคม ";
         * ex: CnvStrToThaiM("1",1);
         * then: return "ม.ค. ";
         */
        public string CnvStrToThaiM(string i_mth, int s_option)
        {
            string s_mth = "";
            switch (i_mth)
            {
                case "1":
                case "01": 
                    if (s_option == 0)
                    {
                        s_mth = " มกราคม ";
                    }
                    else
                    {
                        s_mth = " ม.ค. ";
                    } break;
                case "2":
                case "02": 
                    if (s_option == 0)
                    {
                        s_mth = " กุมภาพันธุ์ ";
                    }
                    else
                    {
                        s_mth = " ก.พ. ";
                    } break;
                case "3":
                case "03": 
                if (s_option == 0)
                    {
                        s_mth = " มีนาคม ";
                    }
                    else
                    {
                        s_mth = " มี.ค. ";
                    } break;
                case "4":
                case "04": 
                    if (s_option == 0)
                    {
                        s_mth = " เมษายน ";
                    }
                    else
                    {
                        s_mth = " เม.ย. ";
                    } break;
                case "5":
                case "05": 
                    if (s_option == 0)
                    {
                        s_mth = " พฤษภาคม ";
                    }
                    else
                    {
                        s_mth = " พ.ค. ";
                    } break;
                case "6":
                case "06": 
                    if (s_option == 0)
                    {
                        s_mth = " มิถุนายน ";
                    }
                    else
                    {
                        s_mth = " มิ.ย. ";
                    } break;
                case "7":
                case "07": 
                    if (s_option == 0)
                    {
                        s_mth = " กรกฏาคม ";
                    }
                    else
                    {
                        s_mth = " ก.ค. ";
                    } break;
                case "8":
                case "08": 
                    if (s_option == 0)
                    {
                        s_mth = " สิงหาคม ";
                    }
                    else
                    {
                        s_mth = " ส.ค. ";
                    } break;
                case "9":
                case "09": 
                    if (s_option == 0)
                    {
                        s_mth = " กันยายน ";
                    }
                    else
                    {
                        s_mth = " ก.ย. ";
                    } break;
                case "10": if (s_option == 0)
                    {
                        s_mth = " ตุลาคม ";
                    }
                    else
                    {
                        s_mth = " ต.ค. ";
                    } break;
                case "11": if (s_option == 0)
                    {
                        s_mth = " พฤษจิกายน ";
                    }
                    else
                    {
                        s_mth = " พ.ย. ";
                    } break;
                case "12": if (s_option == 0)
                    {
                        s_mth = " ธันวาคม ";
                    }
                    else
                    {
                        s_mth = " ธ.ค. ";
                    } break;
            }

            return s_mth;
        }

        /* function เปลี่ยน string วันที่ เป็น วันที่ แบบเต็ม
         * 
         * str_date = "01/01/2553"
         * คือ  วัน/เดือน/ปี
         * type_year = "th" or "us"
         * คือ ประเภทปี พ.ศ. หรือ ค.ศ.
         * 
         * ex:  CnvStrDateToStrFDate("01/01/2553","th")
         * then: return "1 มกราคม พ.ศ. 2553 "
         */
        public string CnvStrDateToStrFDate(string str_date,string type_year)
        {
            string[] str_tmp = str_date.Split('/');
            str_tmp[0] = Convert.ToString(Convert.ToInt32(str_tmp[0]))+" ";
            str_tmp[1] = CnvStrToThaiM(Convert.ToString(Convert.ToInt32(str_tmp[1])), 0);
            if (type_year.Equals("th"))
            {
                return str_tmp[0] + str_tmp[1] + "พ.ศ. " + str_tmp[2] + " ";
            }
            else
            {
                return str_tmp[0] + str_tmp[1] + "ค.ศ. " + str_tmp[2] + " ";
            }
        }
    }
}
