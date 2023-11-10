<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_finconfrim_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>        
        <table class="DataSourceFormView" style="width: 720px;">
            <tr>
                <td width="10%">
                    <div>
                        <span style="font-size: 11px;">ทะเบียน:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" ></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span style="font-size: 11px;">ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="full_name" runat="server"  ></asp:TextBox>
                </td> 
                 <td width="10%">
                    <div>
                        <span style="font-size: 11px;">รายการ:</span>
                    </div>
                </td>   
                 <td width="20%">
                    <asp:DropDownList ID="PAY_RECV_STATUS" runat="server" style="background-color:#ffa366">
                        <asp:ListItem Value="">ทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="0">จ่าย</asp:ListItem>
                        <asp:ListItem Value="1">รับ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="10%">
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" />
                </td>            
            </tr>
        </table>
            
    </EditItemTemplate>
</asp:FormView>
