<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_transfer_new_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขสมาชิก :</span></div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="MEMBER_NO" runat="server" Width="150px" Style="text-align: center"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Width="25px" />
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อ - นามสกุล :</span></div>
                </td>
                <td width="45%">
                    <asp:TextBox ID="MEMB_NAME" runat="server" Width="230px" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด :</span></div>
                </td>
                <td width="45%">
                    <asp:TextBox ID="MEMBGROUP_DESC" runat="server" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
