<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.ws_dlg_sl_shareucftype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px">
            <tr>
                <td colspan="4">
                    <strong><u>ประเภทหุ้น</u></strong>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>รหัสประเภทหุ้น:</span></div>
                </td>
                <td width="10%">
                    <asp:TextBox ID="SHARETYPE_CODE" runat="server" MaxLength="2" Style="text-align:center" ></asp:TextBox>
                </td> 
                <td width="20%" align="center">
                    <div>
                        <span>ชื่อประเภทหุ้น:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="SHARETYPE_DESC" runat="server"></asp:TextBox>
                </td>
            </tr>      
            <tr>
                <td colspan="4">
                    <strong>คัดลอกข้อกำหนดจากประเภทหุ้น   </strong><asp:CheckBox ID="checkbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทเงินหุ้น:</span></div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="usepattern_shrcode" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>   
            <tr>
                <td align="right" colspan="4">
                    <br />                    
                    <asp:Button ID="b_add" runat="server" Text="ตกลง" Width="70px" />&nbsp;
                    <asp:Button ID="b_cancel" runat="server" Text="ยกเลิก" Width="70px" />
                </td>
            </tr>   
        </table>
    </EditItemTemplate>
</asp:FormView>
