<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_imp_excal_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormViewMain" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
        <tr>
            <td width="180px">
                <span>วันที่ : </span>
            </td>
            <td>
                <asp:TextBox ID="entry_date" runat="server" Width="120px" Style="text-align: center;"></asp:TextBox>
            </td>
            <td width="150px">
                <span>ประเภทรายการ : </span>
            </td>
            <td>
                <asp:DropDownList ID="type_code" runat="server" Width="150px" >
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="b_process" runat="server" Text="Import ข้อมูล" Style="width:120px;"/>
            </td>
            <td>
                <asp:Button ID="b_delete" runat="server" Text="ลบข้อมูลที่ Import" Style="width:120px;"/>
            </td>
        </tr>
    </table>
    </EditItemTemplate>
</asp:FormView>