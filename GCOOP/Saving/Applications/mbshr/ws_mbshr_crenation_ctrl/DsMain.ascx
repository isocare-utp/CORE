<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_crenation_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span class="TitleSpan">ข้อมูลสมาชิก</span>
        <table class="DataSourceFormView" style="width: 770px;">            
            <tr>
                <td width="13%">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 100px;" onfocus="this.select()"></asp:TextBox>
                        <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td width="56%">
                    <div>
                        <asp:TextBox ID="FULLNAME" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBTYPE_DESC" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="MEMBGROUP_DESC" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>            
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" ReadOnly="true" Style="width: 754px;
                        height: 50px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
