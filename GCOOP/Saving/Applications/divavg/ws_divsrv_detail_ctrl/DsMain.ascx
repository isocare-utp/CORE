<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_detail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span>เลขที่สมาชิก:</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center; width: 90px;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px;" />
                </td>
                <td width="12%">
                    <div>
                        <span>ชื่อสมาชิก:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="cp_name_thai" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="13%">
                    <div>
                        <span>วันเป็นสมาชิก:</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="member_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทุนเรือนหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharestk" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_membgroup" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="membtype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td width="8%">
                    <font color="#FF0000"><strong>ปีปันผล: </strong></font>
                </td>
                <td>
                    <asp:DropDownList ID="div_year" runat="server" Style="width: 130px;" BackColor="#FF3333"
                        ForeColor="White">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
