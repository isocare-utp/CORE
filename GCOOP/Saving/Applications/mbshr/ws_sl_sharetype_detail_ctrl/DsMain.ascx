<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">            
            <tr>
                <%--<td width="15%">
                    <div>
                        <span>รหัสประเภทหุ้น:</span></div>
                </td>--%>
                <%--<td width="10%">
                    <asp:TextBox ID="sharetype_code" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                        Style="text-align: center"></asp:TextBox>
                </td>--%>
                <td width="15%">
                    <div>
                        <span>ประเภทหุ้น:</span></div>
                </td>
                <td width="60%">
                    <asp:DropDownList ID="sharetype_code" runat="server">
                    </asp:DropDownList> 
                </td>   
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
