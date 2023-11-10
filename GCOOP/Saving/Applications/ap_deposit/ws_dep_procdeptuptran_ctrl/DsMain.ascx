<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_procdeptuptran_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>วันที่ทำรายการ :<span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="process_date" runat="server" Style="text-align:center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ประเภทรายการ :<span>
                    </div>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="system_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="10%">
                    <asp:Button ID="b_retrive" runat="server" Text="ตรวจสอบรายการโอน" />
                </td>
                <td width="10%">
                    <asp:Button ID="b_process" runat="server" Text="ผ่านรายการโอน" />
                </td>
            </tr>
            <tr>
                 <td>
                    <div>
                        <span><asp:CheckBox ID="member_flag" runat="server" Text="ทะเบียน"/><span>
                    </div>
                </td>
                 <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
