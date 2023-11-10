<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.fund.ws_fund_cancel_payfundcoll_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">   
    <tr>  
        <th width="3%">
            
        </th>        
        <th  width="5%">
            ลำดับ
        </th>
        <th  width="10%">
            เลขสมาชิก
        </th>
        <th  width="35%">
            ชื่อ - สกุล
        </th> 
        <th width="14%">
            วันทีคืนกองทุน
        </th> 
        <th width="15%">
            เลขสัญญา
        </th>  
        <th  width="33%">
            จำนวนเงิน
        </th>  
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td style="text-align:center;">
                    <asp:CheckBox ID="choose_flag" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="running_number" runat="server" style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" style="text-align:center;"></asp:TextBox>
                </td>   
                <td>
                    <asp:TextBox ID="fullname" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td> 
                 <td>
                    <asp:TextBox ID="loancontract_no" runat="server" style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="itempay_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td> 
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>