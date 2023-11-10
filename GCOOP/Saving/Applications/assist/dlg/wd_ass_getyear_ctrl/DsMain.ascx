<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_ass_getyear_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 300px;">
           <tr>
                <td width="25%">
                    <div>
                        <span>คัดลอกข้อมูลเงื่อนไขจากปี(พ.ศ.) :</span>
                    </div>
                </td>
                <td width="10%">
                    <asp:TextBox ID="year" runat="server"></asp:TextBox>
                </td>

                 <td width="6%">
                    <asp:Button ID="b_1" runat="server" width="100%" Height="100%" Text="คัดลอก" />
                </td>

  
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>