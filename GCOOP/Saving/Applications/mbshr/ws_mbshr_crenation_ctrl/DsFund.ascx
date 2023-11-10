<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsFund.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_crenation_ctrl.DsFund" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span class="TitleSpan">ข้อมูลกองทุน</span>
        <table class="DataSourceFormView" style="width:600px">
           <tr>
                <td width="20%">
                    <div>
                        <span>วงเงิน:</span>
                    </div>
                </td>
                <td width="30%">
                    <div>
                        <asp:TextBox ID="cremation_amt" runat="server" Style="text-align:right" ToolTip="#,##0.00" onfocus="this.select()"></asp:TextBox>
                    </div>
                </td>  
                <td  width="10%">บาท</td>  
                 <td width="20%">
                    <div>
                        <span>วันที่ปรับปรุงล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="APPLY_DATE" runat="server" Style="text-align:center" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>              
            </tr>           
        </table>
    </EditItemTemplate>
</asp:FormView>
