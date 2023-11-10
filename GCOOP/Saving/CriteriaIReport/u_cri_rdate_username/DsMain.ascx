<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_rdate_username.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<center>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium">รายการตรวจสอบการเปลี่ยนแปลงข้อมูลของระบบสมาชิก (Audit Member)</asp:Label>
</center>
<br />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่วันที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="start_date" runat="server" Style="width: 130px;"></asp:TextBox>
                        <asp:TextBox ID="TextBox1" runat="server" Style="width: 10px; border:0 none black;" Text="-"></asp:TextBox>
                        <asp:TextBox ID="end_date" runat="server" Style="width: 130px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
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
