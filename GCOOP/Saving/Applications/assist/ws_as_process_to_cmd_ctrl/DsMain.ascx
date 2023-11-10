<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_process_to_cmd_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>วันที่ทำรายการ:</span>
                    </div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="operate_date" runat="server" ReadOnly="True" Style="text-align:center"></asp:TextBox>
                </td>
                <td width="10%">
                    <span>ปีสวัสดิการ:</span>
                </td>
                <td width="15%">
                    <asp:DropDownList ID="assist_year" runat="server" Style="text-align:center">
                    </asp:DropDownList>
                </td>               
            </tr>
             <tr>
                <td align = "center" colspan = "4">
                    <br />
                    <asp:Button ID="b_process" runat="server" Text="ดึงข้อมูล" />  
                </td>
            </tr>
         </table>
    </EditItemTemplate>
</asp:FormView>