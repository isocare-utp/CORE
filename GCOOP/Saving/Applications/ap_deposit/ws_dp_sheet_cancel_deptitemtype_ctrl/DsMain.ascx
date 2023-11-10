<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dp_sheet_cancel_deptitemtype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" valign="top">
                    <div>
                        <span>วันที่:</span>
                    </div>
                </td>
                <td width="20%" valign="top">
                    <asp:TextBox ID="tran_date" runat="server" Style="text-align: center"></asp:TextBox> 
                </td>
                <td width="17%" valign="top">
                    <div>
                        <span>รหัสรายการ:</span>
                    </div>
                </td>                
                <td width="25%" valign="top">
                    <asp:DropDownList ID="system_code" runat="server">
                            <asp:ListItem Value="DTR" >DTR : ฝากด้วยการโอน</asp:ListItem>
                            <asp:ListItem Value="LON" >DTL : โอนเงินกู้</asp:ListItem>
                            <asp:ListItem Value="WTI" >WTI : ถอนเพื่อการโอนแบบกลุ่ม</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td  valign="top">
                    <asp:Button ID="b_save" runat="server" Text="ยกเลิกรหัสรายการ" Width="110px" />   
                </td>
            </tr>            
        </table>    
    </EditItemTemplate>
</asp:FormView>
