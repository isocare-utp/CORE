<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_reprint_pea_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="9%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="24%">
                    <asp:TextBox ID="member_no" runat="server" Width="166px"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>วันที่ใบเสร็จ:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="slip_date_s" runat="server" Width="90px"></asp:TextBox>
                </td>
                <td width="1%">
                    -
                </td>
                <td width="13%">
                    <asp:TextBox ID="slip_date_e" runat="server" Width="90px"> </asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>ผู้ทำรายการ:</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="entry_id" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รายการ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="sliptype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ช่วงใบเสร็จ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="payinslip_no_s" runat="server" Width="90px"></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="payinslip_no_e" runat="server" Width="90px"></asp:TextBox>
                </td>
                <td align="right" colspan="2">
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" Width="55px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
