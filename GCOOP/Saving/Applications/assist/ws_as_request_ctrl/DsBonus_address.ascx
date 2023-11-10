<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsBonus_address.ascx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.DsBonus_address" %>


<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="100%">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:100%">
            <tr style="width: 15%"> 
                <td >
                    <span>ที่อยู่:</span>
                </td>
                <td   style="width: 85%">
                    <asp:TextBox ID="dis_addr" runat="server" Style="width: 90%; "></asp:TextBox>
                    <asp:Button ID="b_linkaddress" runat="server" Text="..." Style="width: 8%;height:25px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
