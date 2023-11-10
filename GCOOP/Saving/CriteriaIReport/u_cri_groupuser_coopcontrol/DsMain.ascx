<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_groupuser_coopcontrol.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<center>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium">รายงานกำหนดสิทธ์การเข้าใช้งานหน้าจอของระบบต่างๆ </asp:Label>
</center>
<br />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>รหัสผู้ใช้งาน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="user_name" runat="server" Style="width: 325px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>