<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_sixline.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
         <tr>
                <td>
                    <div>
                        <span>ที่อยู่แถวที่1 :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="line1" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่แถวที่2 :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="line2" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่แถวที่3 :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="line3" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่แถวที่4 :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="line4" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่แถวที่5 :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="line5" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>ที่อยู่แถวที่6 :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="line6" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
