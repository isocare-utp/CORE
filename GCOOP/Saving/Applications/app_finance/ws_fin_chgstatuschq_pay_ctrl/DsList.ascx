<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_chgstatuschq_pay_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width:100%;">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                       <div style="text-decoration: underline; text-align:left; font-size:15px; font-style:inherit; color:#191970" >
                            <span>รายละเอียด :</span>
                  </div> 
            </tr>
            </table>
            <table class="DataSourceFormView">
            <tr>
                <td width="5%" >
                        <div>
                            <span>ธนาคาร :<span>
                       </div>
                </td>
                <td width="11%">
                            <asp:TextBox ID="BANK_DESC" runat="server" Style="text-align:center; b" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="5%" >
                        <div>
                            <span>สาขา :<span>
                       </div>
                </td>
                <td width="15%">
                            <asp:TextBox ID="BRANCH_NAME" runat="server" Style="text-align:center;" ReadOnly="True"></asp:TextBox>
                            
                </td>
            </tr>
            <tr >
                <td >
                        <div>
                            <span>สั่งจ่าย :<span>
                       </div>
                </td>
                <td colspan = "3">
                        <asp:TextBox ID="TO_WHOM" runat="server" Style="text-align:left; width:98%" ReadOnly="True"></asp:TextBox>
                        
                </td>
            </tr>
            <tr>
                <td>
                       <div>
                            <span>วันที่ทำรายการ :<span>
                       </div>
                </td>
                <td >
                        <asp:TextBox ID="ENTRY_DATE" runat="server" Style="text-align:center;" ReadOnly="True"></asp:TextBox>
                </td>
                <td rowspan ="2">
                      <span style = "height:50px">
                            จำนวนเงิน :
                      </span>     
                       
                </td>
                <td rowspan ="2">
                        <asp:TextBox ID="MONEY_AMT" runat="server" style="background-color:Black; color:#00FF00; font-size:40px; height:50px; text-align:right" ReadOnly="True" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                       <div>
                            <span>ผู้ทำรายการ :<span>
                       </div>
                </td>
                <td >
                        <asp:TextBox ID="ENTRY_ID" runat="server" Style="text-align:center;" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>