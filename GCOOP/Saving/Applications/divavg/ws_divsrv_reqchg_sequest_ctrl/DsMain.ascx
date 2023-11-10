<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_reqchg_sequest_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span>ปีปันผล:</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="div_year" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="13%">
                    <div>
                        <span>เลขทำรายการ:</span>
                    </div>
                </td>
                <td width="22%">
                    <asp:TextBox ID="reqchg_docno" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>วันที่ทำการ:</span>
                    </div>
                </td>
                <td width="23%">
                    <asp:TextBox ID="reqchg_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                </td>
                <td>
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_membname" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_membgroup" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div>
                        <span>หมายเหตุ:</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" Width="635px" Height="80px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
