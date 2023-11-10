<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_controlcash.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_controlcash_ctrl.ws_fin_controlcash" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     function check_number() {
         e_k = event.keyCode//46 = .
         if (e_k != 13 && e_k != 46 && (e_k < 48) || (e_k > 57)) {
             event.returnValue = false;
             alert("กรุณากรอกเฉพาะตัวเลขเท่านั้น!");
         }
     }
     //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
     var dsMain = new DataSourceTool();
     var dsList = new DataSourceTool();
     // get xml cashdetail
     function GetXmlFromDlg(XmlCashDetail) {
        dsMain.SetItem(0, "xml_cashdetail", XmlCashDetail);
     }
     function GetDataFromDlg(remain, cashdetail) {
         Gcoop.GetEl("HdColumn").value = "operate_amt";
         Gcoop.GetEl("HdCashdetail").value = cashdetail;
         dsMain.SetItem(0, "operate_amt", remain);
         Gcoop.GetEl("HdItemtype").value = 0;
         PostChangeTeller();
     }
     function OnDsMainClicked(s, r, c, v) {
         if (c == "b_user") {
             Gcoop.OpenIFrame2(500, 550, 'wd_fin_showuser.aspx', '');
         } else if (c == "b_cashdetail") {
             if (parseFloat(dsMain.GetItem(0, "operate_amt")) < 0.01) {
                 alert("- กรุณากรอกจำนวนเงิน"); return false;
             } else {
                 var entry_id = dsMain.GetItem(0, "entry_id");
                 if (entry_id == null) {
                     alert("- กรุณากรอกผู้ร้องขอ"); return false;
                 }
                 Gcoop.GetEl("HdBtCash").value = "true";
                 //Gcoop.OpenIFrame2(510, 470, "wd_fin_cashdetail.aspx", '');
             }
         } else if (c == "operate_amt") {
             if (dsMain.GetItem(0, "entry_id") == null) {
                 alert("กรุณากรอกผู้ร้องขอ");
                 dsMain.Focus(0, "entry_id");
                 dsMain.GetElement(0, "operate_amt").disabled = true; return false;
             } else {
                 dsMain.GetElement(0, "operate_amt").disabled = false;
             }
         } else if (c == "item_type") {
             if (dsMain.GetItem(0, "entry_id") == null) {                
                dsMain.Focus(0, "entry_id");
                dsMain.GetElement(0, "operate_amt").disabled = true;
                alert("กรุณากรอกผู้ร้องขอ"); return false;
            } else {
                 dsMain.GetElement(0, "operate_amt").disabled = false;
            }
         }
     }
     //ประกาศฟังก์ชันสำหรับ event ItemChanged
     function OnDsMainItemChanged(s, r, c, v) {
         if (c == "entry_id") {
             PostInitUser();
         } else if (c == "item_type") {
             dsMain.SetItem(0, "t_moneyreturn", "");            
             var alertstr = "";
             var status = dsMain.GetItem(0, "status");
             if (status == v) {
                 if (v == "11") {
                     dsMain.SetItem(0, "item_type", 15);
                     alertstr = "มีสถานะเปิดอยู่แล้ว ไม่สามารถเปิดได้อีก";
                 } else if (v == "14") {
                     alertstr = "มีสถานะปิดอยู่แล้ว";
                 }
             }
             if (alertstr == "") {
                 Gcoop.GetEl("HdColumn").value = c;
                 Gcoop.GetEl("HdItemtype").value = 1;
                 PostChangeTeller();
             } else {
                 alert(alertstr);
                 return false;
             }
         } else if (c == "operate_amt") {
             Gcoop.GetEl("HdColumn").value = c;
             Gcoop.GetEl("HdItemtype").value = 2;
             PostChangeTeller();
         } 
     }   
     function OnDsListClicked(s, r, c ) {
         var username = dsList.GetItem(r, "user_name");
         dsMain.SetItem(0,"entry_id",username);
         PostInitUser();
     }
     function GetUsesNameDlg(username) {
         dsMain.SetItem(0, "entry_id", username);
         PostInitUser();
     }
     function Validate() {
         var alertstr = "";
         var entry_id = dsMain.GetItem(0, "entry_id");
         var item_type = dsMain.GetItem(0, "item_type");
         var operate_amt = dsMain.GetItem(0, "operate_amt");
         var money_remain = dsMain.GetItem(0, "money_remain");

         if (entry_id == null) {
             alertstr += "- โปรดเลือกผู้ร้องขอ\n";
         }
         if (item_type == null) {
             alertstr += "- โปรดเลือกประเภทรายการ\n";
         }
         if (parseFloat(operate_amt) < 0) {
             alertstr += "- จำนวนเงินสดน้อยกว่าศูนย์ไม่ได้\n";
         }
         if (parseFloat(money_remain) != 0 && item_type == "14") {
             alertstr += "- กรณีปิดลิ้นชัก จำนวนเงินต้อง clear เป็น ศูนย์\n";
         }
         if (item_type == "16" || item_type == "14") {
             if (parseFloat(operate_amt) > (parseFloat(money_remain) + parseFloat(operate_amt))) {
                 dsMain.SetItem(0, "operate_amt", 0);
                 alertstr += "- จำนวนเงินที่ส่งคืน มากกว่าจำนวนเงินที่เหลือครับ\n";
            }
         }
         if (alertstr == "") {
             return confirm("ยืนยันการบันทึกข้อมูล");
         } else {
             alert(alertstr);
             return false;
         }
     }

     function SheetLoadComplete() {
         if (Gcoop.GetEl("HdShowDisplay").value = "true") {
             document.getElementById('F_dsShow').style.display = 'block';
         } else {
             document.getElementById('F_dsShow').style.display = 'none';
         }
         for (var i = 0; i < dsList.GetRowCount(); i++) {
             var status = dsList.GetItem(i, "status");
             if (status == "11") {
                 dsList.GetElement(i, "box").style.background = "#00FF00";
             } else {
                 dsList.GetElement(i, "box").style.background = "#ff0000";
             }
         }
//         if (Gcoop.GetEl("HdPrint").value == "true") {
//             Gcoop.GetEl("HdPrint").value = "fase";
//             if (confirm("ยืนยันการพิมพ์ใบทำรายการเบิก-จ่ายเงินสด")) {
//                PostPrintSlip();
//            }
//         }
     }     
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div id="F_dsShow" style="display: none;">
       <table width="100%">
            <tr>
                <td width="20%" style="height:100%;" valign="top">
                    <uc2:DsList ID="dsList" runat="server" />
                </td>
                <td width="80%"  style="height:100%;" align="center" valign="top">
                    <uc1:DsMain ID="dsMain" runat="server" />
                </td>                        
            </tr>   
       </table>
   </div>
    <asp:HiddenField ID="HdColumn" runat="server" />
    <asp:HiddenField ID="HdItemtype"  Value="0" runat="server" />
    <asp:HiddenField ID="HdCashdetail" runat="server" />
    <asp:HiddenField ID="HdPrint" runat="server" />
    <asp:HiddenField ID="HdSlipno" runat="server"   Value=""/>
    <asp:HiddenField ID="HdBtCash" Value="fase" runat="server" />
    <asp:HiddenField ID="HdOperateAmt" Value="fase" runat="server" />
    <asp:HiddenField ID="HdShowDisplay" runat="server" />
</asp:Content>