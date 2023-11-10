using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_mb_detail_keepdatadet_ctrl
{
    public partial class w_dlg_mb_detail_keepdatadet : PageWebDialog, WebDialog
    {

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void InitJsPostBack()
        {
            dsDetail.InitDsDetail(this);
        }

        public void WebDialogLoadBegin()
        {
            try
            {
                string member_no = Request["memno"];
                string recvperiod = Request["recvperiod"];
                dsDetail.Retrieve(member_no, recvperiod);

                string recv_period = "";
                decimal sum_prin = 0, sum_int = 0, sum_item = 0;
                for (int i = 0; i < dsDetail.RowCount; i++)
                {
                    sum_prin += dsDetail.DATA[i].PRINCIPAL_PAYMENT * dsDetail.DATA[i].SIGN_FLAG;
                    sum_int += dsDetail.DATA[i].INTEREST_PAYMENT * dsDetail.DATA[i].SIGN_FLAG;
                    sum_item += dsDetail.DATA[i].ITEM_PAYMENT * dsDetail.DATA[i].SIGN_FLAG;
                }

                switch (recvperiod.Substring(4))
                {
                    case "01": recv_period = recv_period = "มกราคม"; break;
                    case "02": recv_period = "กุมภาพันธ์"; break;
                    case "03": recv_period = "มีนาคม"; break;
                    case "04": recv_period = "เมษายน"; break;
                    case "05": recv_period = "พฤษภาคม"; break;
                    case "06": recv_period = "มิถุนายน"; break;
                    case "07": recv_period = "กรกฎาคม"; break;
                    case "08": recv_period = "สิงหาคม"; break;
                    case "09": recv_period = "กันยายน"; break;
                    case "10": recv_period = "ตุลาคม"; break;
                    case "11": recv_period = "พฤศจิกายน"; break;
                    case "12": recv_period = "ธันวาคม"; break;
                }



                lbl_title.Text = "ประจำเดือน" + recv_period + " " + recvperiod.Substring(0, 4) + " ใบเสร็จ : " + dsDetail.DATA[0].RECEIPT_NO;
                txt_sum_prin.Text = string.Format("{0:n}", sum_prin);
                txt_sum_int.Text = string.Format("{0:n}", sum_int);
                txt_sum_item.Text = string.Format("{0:n}", sum_item);
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูล");
                Panel1.Visible = false;
            }
        }

        public void WebDialogLoadEnd()
        {

        }
    }
}