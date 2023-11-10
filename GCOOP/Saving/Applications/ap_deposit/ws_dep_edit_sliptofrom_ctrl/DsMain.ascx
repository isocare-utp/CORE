<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_edit_sliptofrom_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="100%">
    <EditItemTemplate>
        <table class="DataSourceFormView" width="700px" >
            <tr>
                <td width="20%">
                    <div>
                        <span>เลขบัญชี:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="DEPTACCOUNT_NO" runat="server" Style="text-align: center;"></asp:TextBox>                    
                </td>
                <td width="20%">
                    <div>
                        <span>ชื่อบัญชี: <span>
                    </div>
                </td>
                <td  colspan="3">
                    <asp:TextBox ID="DEPTACCOUNT_NAME" runat="server" Style="width:100%;"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td>
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="MEMBER_NO" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                         <span>วันที่ทำรายการ: <span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="START_DATE" runat="server" style="text-align:center;width:100%;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="MEMB_NAME" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สกุล:  <span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="MEMB_SURNAME" runat="server"  Style="width:100%;"></asp:TextBox>
                </td>                
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
