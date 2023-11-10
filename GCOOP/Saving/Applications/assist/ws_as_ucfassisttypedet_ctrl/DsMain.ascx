<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttypedet_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                 <td width="15%">
                    <div>
                        <span>ปีสวัสดิการ:</span>
                    </div>
                </td>
                <td width="45%">
                    <asp:DropDownList ID="process_year" runat="server" Style="text-align: center;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td width="45%">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <div>
                        <span>เงื่อนไขการคำนวณ:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="calculate_flag" runat="server" ReadOnly="true" BackColor="#DDDDDD" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>