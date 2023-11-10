<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsBding.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl.DsBding" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle">
            <tr>
                <td width="14%">
                    <div>
                        <span>บ้านที่อยู่/หมู่บ้าน</span></div>
                </td>
                <td width="36%">
                    <asp:TextBox ID="bd_village" runat="server"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>เลขที่</span></div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="bd_addrno" runat="server"></asp:TextBox>
                </td>
                <td width="5%">
                    <div>
                        <span>หมู่ที่</span></div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="bd_addrmoo" runat="server" Width="97px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตรอก/ซอย</span></div>
                </td>
                <td>
                    <asp:TextBox ID="bd_soi" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ถนน</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="bd_road" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตำบล/แขวง</span></div>
                </td>
                <td>
                    <asp:TextBox ID="bd_tambol" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>อำเภอ/เขต</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="bd_amphur" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จังหวัด</span></div>
                </td>
                <td>
                    <asp:TextBox ID="bd_province" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภท</span></div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="bd_type" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งอยู่บน</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="collmasttype_code" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>เลขที่</span></div>
                </td>
                <td>
                    <asp:TextBox ID="bd_onlandno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปลูกสร้าง</span></div>
                </td>
                <td>
                    <asp:TextBox ID="bd_age" runat="server" ToolTip="#,##0" Style="text-align: center;"
                        Width="130px"></asp:TextBox>
                    ปี
                </td>
                <td>
                    <div>
                        <span>ค่าเสื่อมร้อยละ</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="bd_depreciation" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
