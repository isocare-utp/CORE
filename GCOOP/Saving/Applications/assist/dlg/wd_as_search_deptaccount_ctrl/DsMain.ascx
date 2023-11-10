<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_search_deptaccount_ctrl.DsMain" %>


<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="100%">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 100%;">
            <tr>
                <td style="width:11%">
                    <div>
                        <span>เลขสมาชิก :</span>
                    </div>
                </td>
                <td style="width:10%">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                <td style="width:5%">
                    <div>
                        <span>ชื่อบัญชี :</span>
                    </div>
                </td>
                <td style="width:10%">
                    <asp:TextBox ID="deptaccount_name" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width:10%">
                    <div>
                        <span>เลขบัญชี :</span>
                    </div>
                </td >
                <td>
                    <asp:TextBox ID="deptaccount_no" runat="server"></asp:TextBox>
                </td>
                <td style="width:8%" rowspan="2"><asp:Button ID="b_search" runat="server" Text="ค้นหา" style="width:100%; padding:4px;"/></td>
            </tr>
            
        </table>
    </EditItemTemplate>
</asp:FormView>
