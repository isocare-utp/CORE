<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_cmdocumentcontrol_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>                
                <td width="20%">
                    <div>
                        <span>เลขที่ล่าสุด :</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="last_documentno" runat="server" ReadOnly="true" ToolTip="#,##0"></asp:TextBox>
                    </div>
                </td>  
                 <td>
                    <div>
                        <asp:Button ID="b_save" runat="server" Text="อัพเดทเลขเอกสาร"  />
                    </div>
                </td>              
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
