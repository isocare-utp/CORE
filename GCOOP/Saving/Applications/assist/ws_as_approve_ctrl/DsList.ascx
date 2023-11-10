<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_approve_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<asp:Panel ID="Panel"  runat="server" >
<table cellspacing="0" rules="all" class="DataSourceRepeater" style="width: 900px;
            border-collapse: collapse;">
        <tr>
        <%--<td width="4%">

        </td>--%>
        <td  width="28%" style="text-align: right; background-color:#FFF5EE	;" colspan="3">
            รวมจำนวนคำขอ :
        </td>
        <td width="26%" style="text-align: center ; background-color:#FFF5EE;">
            <label id="sum_req"  runat="server" ></label>
        </td>
         <td width="18%" style="text-align: right;background-color:#FFF5EE;">
            รวมเงิน :
        </td>
        <td width="16%" style="text-align: right;background-color:#FFF5EE;">
            <label id="sum_balance"  runat="server" ></label>
        </td>
        <td width="8%" style="border:0px; ">

        </td>
    </tr>
    </table>

 <table cellspacing="0" rules="all" class="DataSourceRepeater" style="width: 900px;
            border-collapse: collapse;">
    <tr>
        <th width="3%">
        </th>
        <th width="25%">
            ชื่อ - สกุล
        </th>
        <th width="26%">
            ประเภท
        </th>
        <th width="8%">
            <asp:Label ID="text_reqdate" Text="วันที่ขอ" runat="server"></asp:Label>
        </th>
        <th width="10%">
             <asp:Label ID="text_edulevel" Text="สิทธิสวัสดิการ" runat="server"></asp:Label>
        </th>
        <th width="8%">
             <asp:Label ID="text_salary" Text="รายการหัก" runat="server"></asp:Label>
        </th>
        
        <th width="8%">
            เงินสวัสดิการ
        </th>
        <th width="8%">
            สถานะ
        </th>
    </tr>
</table>
<div style="overflow-y: scroll; overflow-x: hidden; height: 450px; width: 900px">
   <table cellspacing="0" rules="all" class="DataSourceRepeater" style="width: 900px;
            border-collapse: collapse;">
   
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
          
           <tr>
                    <td width="3%" align="center">
                        <asp:CheckBox ID="choose_flag" runat="server" />
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="cp_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="26%">
                        <asp:TextBox ID="assistpay_desc" runat="server" Style="text-align: left" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="ass_date" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="edulevel_desc" runat="server" Style="text-align: right" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="salary_amount" runat="server" Style="text-align: right" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    
                    <td width="8%">
                        <asp:TextBox ID="assistnet_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:DropDownList ID="req_status" runat="server" Width="90%">
                            <asp:ListItem Value="8" Text="รออนุมัติ"></asp:ListItem>
                            <asp:ListItem Value="1" Text="อนุมัติ"></asp:ListItem>
                            <asp:ListItem Value="0" Text="ไม่อนุมัติ"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
</div>
</asp:Panel>
