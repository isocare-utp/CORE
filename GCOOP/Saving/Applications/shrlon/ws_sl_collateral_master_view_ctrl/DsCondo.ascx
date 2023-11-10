<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCondo.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_view_ctrl.DsCondo" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle">
            <tr>
                <td width="12%">
                    <div>
                        <span>ชื่ออาคารชุด</span></div>
                </td>
                <td width="34%">
                    <asp:TextBox ID="condo_name" runat="server"></asp:TextBox>
                </td>
                <td width="19%">
                    <div>
                        <span>อาคารเลขที่</span></div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="condo_towerno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ห้องชุดเลขที่</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_roomno" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ทะเบียนอาคารชุดเลขที่</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_registno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชั้นที่</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_floor" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ตำบล/แขวง</span></div>
                </td>
                <td>
                    <asp:TextBox ID="pos_tambol" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ/เขต</span></div>
                </td>
                <td>
                    <asp:TextBox ID="pos_amphur" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จังหวัด</span></div>
                </td>
                <td>
                    <asp:TextBox ID="pos_province" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เนื้อที่ประมาณ</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_roomsize" runat="server" Width="195px" ToolTip="#,##0" Style="text-align: center;" ></asp:TextBox>
                    ตร. ม.
                </td>
                <td>
                    <div>
                        <span>ราคา ตร. ม. ละ</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_pricesquare" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปลูกสร้าง</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_age" runat="server" ToolTip="#,##0" Style="text-align: center;"
                        Width="195px"></asp:TextBox>
                   ปี
                </td>
                <td>
                    <div>
                        <span>ค่าเสื่อม ร้อยละ</span></div>
                </td>
                <td>
                    <asp:TextBox ID="condo_depreciation" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
