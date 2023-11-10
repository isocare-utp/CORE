<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_req_decrepitude_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">

    <br /> 
    <div><u>ประวัติการขอสวัสดิการของสมาชิก</u></div>
    <tr align="center">  
        <th width="15%">
            <span>เลขเอกสาร</span>
        </th>   
        <th width="15%">
            <span>วันที่ขอสวัสดิการ</span>
        </th>
        <th width="40%">
            <span>ประเภทสวัสดิการ</span>
        </th>
        <th width="15%">
            <span>จำนวนเงิน</span>
        </th>
        <th width="15%">
            <span>สถานะ</span>
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">      
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>    
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="asscontract_no" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="assist_date" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:TextBox ID="assisttype_code" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="approve_amt" runat="server" Style="text-align:center;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="status_desc" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
