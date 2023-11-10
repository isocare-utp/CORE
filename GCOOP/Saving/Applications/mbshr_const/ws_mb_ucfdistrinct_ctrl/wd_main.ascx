<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_main.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_ucfdistrinct_ctrl.wd_main" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>จังหวัด :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="province_code" runat="server" Style="width: 550px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
