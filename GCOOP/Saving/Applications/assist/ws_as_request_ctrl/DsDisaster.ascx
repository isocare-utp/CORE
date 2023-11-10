<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDisaster.ascx.cs"
    Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsDisaster" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">
    function chkNumber(ele) {
        var vchar = String.fromCharCode(event.keyCode);
        if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
        ele.onKeyPress = vchar;
    }
</script>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 20%">
                    <span>วันที่เริ่มประสบภัย:</span>
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="dis_disdate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <span>สถานะในทะเบียนบ้าน:</span>
                </td>
                <%--<td>
                    <asp:DropDownList ID="population_house_status" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value="1">เจ้าบ้าน</asp:ListItem>
                        <asp:ListItem Value="2">ผู้อาศัย</asp:ListItem>
                        <asp:ListItem Value="3">ผู้อาศัยไม่มีชื่อในทะเบียนบ้าน</asp:ListItem>
                        <asp:ListItem Value="4">บ้านพักราชการ</asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                <td>
                    <asp:DropDownList ID="dis_house_status" runat="server" Style="font-size: 12px;">
                    </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td>
                    <span>ค่าเสียหาย:</span>
                </td>
                <td>
                    <asp:TextBox ID="dis_disamt" runat="server" ToolTip="#,##0.00" Style="font-size: 12px;
                        text-align: right;" OnKeyPress="return chkNumber(this)"></asp:TextBox>
                </td>
                 <td style="width: 20%">
                    <span>ประเภทการจ่าย:</span>
                </td>
                <td style="width: 45%">
                    <asp:DropDownList ID="assistpay_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <span>เลขที่บ้าน:</span>
                </td>
                <td>
                    <asp:TextBox ID="dis_homedoc" runat="server" Style="width: 96%; text-align: center;" maxlength="15"></asp:TextBox>
                </td>
                <td>
                    <span>ประเภทภัยพิบัติ:</span>
                </td>
                <td>
                    <asp:DropDownList ID="disaster_code" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >
                    <span>ที่อยู่ที่ประสบภัย:</span>
                </td>
                <td colspan="3">
                    <%--<asp:TextBox ID="TextBox2" runat="server" Style="width: 90%; text-align: center;"></asp:TextBox>--%>
                    <asp:TextBox ID="dis_addr" runat="server" Style="width: 90%; "></asp:TextBox>
                    <asp:Button ID="b_linkaddress" runat="server" Text="..." Style="width: 8%;" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
