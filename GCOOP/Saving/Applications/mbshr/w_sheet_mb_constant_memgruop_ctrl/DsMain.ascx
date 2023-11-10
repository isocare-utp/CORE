<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_constant_memgruop_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 350px">
            <tr>
                <td width="30%">
                    <div>
                        <span>รหัสสังกัด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="membgroup_codet" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
               <%-- <td width="50px">
                    <div>
                        <asp:Button ID="b_search" runat="server" Text="ค้นหา" Style="width: 50px; height: 25px"  Visible="false"/>
                    </div>
                </td>--%>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
