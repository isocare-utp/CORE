<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_syslogtrans.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<center>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
</center>
<br />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>ตาราง:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="tabname" runat="server" Style="width: 200px;">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ช่วงวันที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="startdate" runat="server" Style="width: 100px;"></asp:TextBox>
                        
                        <asp:TextBox ID="enddate" runat="server" Style="width: 100px;"></asp:TextBox>
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
                        <asp:TextBox ID="user_name" runat="server" Style="width: 200px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
