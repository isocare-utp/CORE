<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dep_procdeptuptran.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_procdeptuptran_ctrl.ws_dep_procdeptuptran" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function SheetLoadComplete() {
            if (parseFloat(dsMain.GetItem(0, "member_flag")) > 0) {
                dsMain.GetElement(0, "member_no").disabled = false;
            } else {
                dsMain.GetElement(0, "member_no").disabled = true;
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_flag") {
                if (v == "0") {
                    dsMain.GetElement(0, "member_no").disabled = true;
                    dsMain.SetItem(0, "member_no", "");
                } else {
                    dsMain.SetItem(0, "member_no", "00000000");
                    dsMain.GetElement(0, "member_no").disabled = false;
                }
            } else if (c == "member_no") {
                JsPostFormatMemno();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_retrive") {
                var alertstr = "";
                if (dsMain.GetItem(0, "member_flag") == 1) {
                    if (dsMain.GetItem(0, "member_no") == null ) {
                        alertstr+="กรุณากรอกเลขสมาชิก!\n"; 
                    }
                }
                if (dsMain.GetItem(0, "system_code") == null) {                    
                    alertstr+="กรุณาเลือกประเภทรายการ!\n";
                }
                if (alertstr == "") {
                    JsPostRetriveData();
                } else {
                    alert(alertstr);
                    return false;
                }
            }
            else if (c == "b_process") {
                var alertstr = "";
                var count_detail = document.getElementById('<%=COUNT_MEM.ClientID%>').value;
                if (parseInt(count_detail) < 1) {
                    alertstr = "ไม่มีรายการที่จะผ่านรายการ";
                }
               
                if (alertstr == "") {
                    var isConfirm = confirm("ยืนยันการผ่านรายการ");
                    if (isConfirm) {
                        JsPostProcess();
                    }  
                } else {
                    alert(alertstr);
                    return false;
                }
                              
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
     <br />
    <table  width="100%" style="font-size:14px">          
        <tr>
            <td style="width: 50px">จำนวน  :</td>
            <td>
                <asp:TextBox ID="COUNT_NUM" runat="server" ReadOnly="true" Style="width: 80px;height:25px;text-align: right;font-weight:bold;font-size:14px;" BackColor="Black" ForeColor="Red" ToolTip="#,##0"></asp:TextBox>
            </td>
            <td><div style="float:left;width:100px;">รายการ</div></td>
            <td style="width: 50px">จำนวน  :</td>
            <td style="width: 50px">
                <asp:TextBox ID="COUNT_MEM" runat="server" ReadOnly="true" Style="width: 80px;height:25px;text-align: right;font-weight:bold;font-size:14px;" BackColor="Black" ForeColor="Red" ></asp:TextBox>
            </td>
            <td style="width: 50px">ราย</td>
            <td >
                <span style="width: 50px;float:right">รวม  :</span>
            </td>
            <td style="width: 50px">
                <asp:TextBox ID="SUM_TOTAL" runat="server" ReadOnly="true" Style="width:205px;height:25px;text-align: right;font-weight:bold;font-size:14px;" BackColor="Black" ForeColor="Red" ></asp:TextBox>
            </td>
            <td style="width: 50px">
                <span>บาท</span>
            </td>
        </tr>
   </table>
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <%=outputProcess%>
</asp:Content>
