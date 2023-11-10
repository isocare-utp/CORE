<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_collredeem_pawn_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="16px" 
    Height="16px">

    <EditItemTemplate>
        <table class="DataSourceFormView">
        
            <tr>
                <td width="16%">
                    <div>
                        <span>ทะเบียนจำนอง:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mrtgmast_no" runat="server" Style="width: 100px; text-align: center;"></asp:TextBox>
                        <asp:Button ID="b_sh_pawn" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />
                    </div>
                </td>                             
                <td width="16%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="full_name" runat="server" Style="width: 345px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>                
                <td width="16%">
                    <div>
                        <span>ทะเบียนสมาชิก:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="width: 100px; text-align: center;"></asp:TextBox>
                        <!--<asp:Button ID="b_sh_member_no" runat="server" Text="..." Style="width: 30px; margin-left: 2px;" />-->                        
                    </div>
                </td>                
            </tr>            
            <tr>
                <td width="16%">
                    <div>
                        <span>วันที่จำนอง:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mortgage_date" runat="server" Style="width: 110px; text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>จำนวนจำนอง:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="mortgagesum_amt" runat="server" Style="width: 110px; text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>       
             </tr>
             <tr>
                <td width="16%">
                    <div>
                        <span>วันที่บันทึกไถ่ถอน:</span></div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="redeem_date" runat="server" Style="width: 110px; text-align: center;"></asp:TextBox>
                    </div>
                </td>
             </tr>         
         </table>
         <br>         
    </EditItemTemplate>
</asp:FormView>