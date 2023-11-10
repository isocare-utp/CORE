<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_slip_mthkeep_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td colspan="4">
                    <b><u>ประจำงวด</u></b>
                </td>
            </tr>
            <tr>
                <td width="27%">
                    <div>
                        <span>ปี:</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="year" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="18%">
                    <div>
                        <span>เดือน:</span></div>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="month" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">มกราคม</asp:ListItem>
                        <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                        <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                        <asp:ListItem Value="4">เมษายน</asp:ListItem>
                        <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                        <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                        <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                        <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                        <asp:ListItem Value="9">กันยายน</asp:ListItem>
                        <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                        <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                        <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b><u>รูปแบบการพิมพ์</u></b>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่สังกัด:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="smembgroup_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="smembgroup_desc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงสังกัด:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="emembgroup_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="emembgroup_desc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่ใบเสร็จ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="receipt_no" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>