<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_rmembgroup_memtype.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่สังกัด:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="smembgroup_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="smembgroup_desc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงสังกัด:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="emembgroup_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="emembgroup_desc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="member_status" runat="server">
                    <asp:ListItem Value="0" Text="เป็นสมาชิก"></asp:ListItem>
                    <asp:ListItem Value="1" Text="ไม่เป็นสมาชิก"></asp:ListItem>
                  
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
