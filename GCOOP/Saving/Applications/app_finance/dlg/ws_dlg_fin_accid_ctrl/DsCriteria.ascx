<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.ws_dlg_fin_accid_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 480px;">
            <tr>
                <td width="40%">
                    <div>
                        <span>รหัส:</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="slipitemtype_code" runat="server"></asp:TextBox>
                    </div>
                </td>   
                <td width="10%" rowspan="2" >
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" Style="width:100px;height:53px" />
                </td>  
            </tr>
            <tr>
                 <td>
                    <div>
                        <span>คำอธิบาย:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="item_desc" runat="server"></asp:TextBox>
                    </div>
                </td>
             </tr>           
        </table>
    </EditItemTemplate>
</asp:FormView>
