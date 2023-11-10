<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.fund.ws_fund_ucf_fundcollkeeprate_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<table class="DataSourceRepeater">    
    <tr>  
        <th width="20%">
            ประเภทสินเชื่อ
        </th>        
        <th  width="5%">
            ลำดับ
        </th>
        <th  width="7%">
            LOAN_STEPMIN
        </th>
        <th  width="7%">
            LOAN_STEP
        </th> 
        <th width="5%">
            KEEPMIN_AMT
        </th>  
        <th  width="10%">
            KEEPMAX_AMT
        </th>   
        <th  width="10%">
            ยอดหักกองทุน(%)
        </th>   
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:DropDownList ID="LOANTYPE_CODE" runat="server" >
                    </asp:DropDownList>        
                </td>
                <td>
                    <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="LOAN_STEPMIN" runat="server" Style="text-align: right;" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>   
                <td>
                    <asp:TextBox ID="LOAN_STEP" runat="server" Style="text-align: right;" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="KEEPMIN_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td> 
                <td>
                    <asp:TextBox ID="KEEPMAX_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>    
                 <td>
                    <asp:TextBox ID="KEEP_PERCENT" runat="server" Style="text-align: right;" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>                   
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
