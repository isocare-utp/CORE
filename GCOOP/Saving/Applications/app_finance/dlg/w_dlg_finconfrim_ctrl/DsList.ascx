<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_finconfrim_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 700px;">
        <tr align="center">  
           
            <th width="10%">
                <span>รายการ</span>
            </th>
            <th width="10%">
                <span>ทะเบียน</span>
            </th>
            <th width="25%">
                <span>ชื่อ -สกุล</span>
            </th>   
            <th width="30%">
                <span>รายการ</span>
            </th>
            <th width="15%">
                <span>จำนวนเงิน</span>
            </th>
            <th width="10%">
                <span>ผู้ทำรายการ</span>
            </th>
        </tr>
   </table>
</asp:Panel>
 <asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                 <tr>
                    
                     <td width="10%">
                        <asp:TextBox ID="PAY_RECV_DESC" runat="server" style="text-align:center"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" style="text-align:center"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="fullname" runat="server" ></asp:TextBox>
                    </td>   
                    <td width="30%">
                        <asp:TextBox ID="payment_desc" runat="server" ></asp:TextBox>
                    </td>
                    <td width="15%" >
                        <asp:TextBox ID="itempay_amt" runat="server" ToolTip="#,##0.00" style="text-align:right"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="entry_id" runat="server" style="text-align:center"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
