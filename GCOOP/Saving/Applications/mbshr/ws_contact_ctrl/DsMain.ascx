<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_contact_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="27%">
                    <div>
                        <span>เลขสมาชิก :</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="MEMBER_NO" runat="server" Width="280px" Style="text-align: center"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Width="25px" />
                </td> 
            </tr>
            <tr>
            <td width="10%">
                    <div>
                        <span>ชื่อ - นามสกุล :</span></div>
                </td>
                <td width="10%">
                    <asp:TextBox ID="MEMB_NAME" runat="server" Width="350px" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สังกัด :</span></div>
                </td>
                <td width="45%">
                    <asp:TextBox ID="MEMBGROUP_DESC" runat="server" Width="350px" BackColor="#DDDDDD" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
