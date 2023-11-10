<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fund_ucf_fundcollkeeprate.aspx.cs" Inherits="Saving.Applications.fund.ws_fund_ucf_fundcollkeeprate_ctrl.ws_fund_ucf_fundcollkeeprate" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool();
        function chkNumber(ele) {
            var vchar = String.fromCharCode(event.keyCode);
            if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
            ele.onKeyPress = vchar;
        }
        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "member_type") {
                dsList.SetItem(r, "member_type", v);
                //alert(v);
            } else if (c == "assisttype_pay") {
                dsList.SetItem(r, "assisttype_pay", v);
            } else if (c == "num_pay") {
                if (v > 1 ) {
                   dsList.GetElement(r, 'first_payamt').disabled = false;
                    dsList.GetElement(r, 'first_payamt').style.background = '#FFFFFF';
                    dsList.GetElement(r, 'next_payamt').disabled = false;
                    dsList.GetElement(r, 'next_payamt').style.background = '#FFFFFF'
                    dsList.GetElement(r, 'first_typepay').disabled = false;
                    dsList.GetElement(r, 'first_typepay').style.background = '#FFFFFF'
                    dsList.GetElement(r, 'next_typepay').disabled = false;
                    dsList.GetElement(r, 'next_typepay').style.background = '#FFFFFF'
                    dsList.GetElement(r, 'max_nextpayamt').disabled = false;
                    dsList.GetElement(r, 'max_nextpayamt').style.background = '#FFFFFF'
                } else if (v = 1 ) {
                    dsList.SetItem(r, "next_payamt", '0.00');
                    dsList.GetElement(r, 'next_payamt').disabled = true;
                    dsList.GetElement(r, 'next_payamt').style.background = '#CCCCCC'
                    dsList.GetElement(r, 'next_typepay').disabled = true;
                    dsList.GetElement(r, 'next_typepay').style.background = '#CCCCCC'
                    dsList.GetElement(r, 'max_nextpayamt').disabled = true;
                    dsList.GetElement(r, 'max_nextpayamt').style.background = '#CCCCCC'
                }
            } else if (c == "first_typepay") {
                if (v = 2) {
                    var chk_fpay = dsList.GetItem(r, "first_payamt");
                    if (chk_fpay > '100') {
                        dsList.SetItem(r, "first_payamt",'100');
                    }                    
                }
            }  
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "fundkeeptype") {
                JsPostRetriveData();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }  
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
//            var alertstr = "";
//            for (var i = 0; i < dsList.GetRowCount() - 1; i++) {
//                var rate = dsList.GetItem(i, "rate");
//                var max_payamt = dsList.GetItem(i, "max_payamt");
//                if (max_payamt < 1) {
//                    alertstr = alertstr + "กรุณากรอกยอดเงินที่จ่าย!!\n";
//                }else if (rate < 100) {
//                    var next_payamt = dsList.GetItem(i, "next_payamt");
//                    if (next_payamt < 1) {
//                        alertstr = alertstr + "กรุณากรอกการจ่ายครั้งต่อไป!!\n";
//                    }
//                }
//            }
//            
//            if (alertstr == "") {
//                return confirm("ยืนยันการบันทึกข้อมูล");
//            } else {
//                alert(alertstr);
//            }          
        }
        function SheetLoadComplete() {
        }

        function OnClickNewRow() {
            PostNewRow();
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   <center>
        <uc1:DsMain ID="dsMain" runat="server" />
    </center>
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span> 
    <uc2:DsList ID="dsList" runat="server" />   
</asp:Content>

