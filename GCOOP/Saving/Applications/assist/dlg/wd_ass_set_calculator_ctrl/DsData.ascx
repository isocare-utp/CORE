<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsData.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_ass_set_calculator_ctrl.DsData" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width:600px" >
    <tr>
        <th width="5%" align="center">

        </th>
        <th width="5%">
            รหัส
        </th>
        <th width="50%">
            ชื่อ
        </th>
         <th width="10%">
            การกระทำ
        </th>
    </tr>
           <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%" align="center">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>

                    <td width="10%" align="center">
                        <asp:TextBox ID="CALCULATOR_TYPE" ReadOnly="true" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                     <td width="25%">
                        <asp:TextBox ID="calculator_name" ReadOnly="true" runat="server" Style="text-indent: 20px;"></asp:TextBox>
                    </td>
                  <td width="10%">
                        <asp:DropDownList ID="OPERATION_TYPE" runat="server">
                        <asp:ListItem Value="1" Selected="True">บวก</asp:ListItem>
                        <asp:ListItem Value="2">ลบ</asp:ListItem>
                        <asp:ListItem Value="3">คูณ</asp:ListItem>
                        <asp:ListItem Value="4">หาร</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
