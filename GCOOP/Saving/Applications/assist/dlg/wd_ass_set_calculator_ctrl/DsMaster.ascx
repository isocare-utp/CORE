<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMaster.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_ass_set_calculator_ctrl.DsMaster" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<table class="DataSourceRepeater" style="width:600px">
    <tr>
        <th width="8%" align="center">
           
        </th>
        <th width="10%">
            รหัส
        </th>
        <th width="50%">
            ชื่อ
        </th>
    </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="8%" align="center">
                        <asp:CheckBox ID="check_flag" runat="server" />
                    </td>
                    <td width="10%" align="center">
                        <asp:TextBox ID="CALCULATOR_TYPE" ReadOnly="true" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                     <td width="50%">
                        <asp:TextBox ID="CALCULATOR_DESC" ReadOnly="true" runat="server" Style="text-indent: 20px;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>


