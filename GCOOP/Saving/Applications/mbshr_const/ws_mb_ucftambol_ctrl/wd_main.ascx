<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_main.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_ucftambol_ctrl.wd_main" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 730px;">
            <tr>
                <td width="20%" >
                    <div>
                        <span>จังหวัด :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="province_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <div>
                        <span>อำเภอ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:DropDownList ID="district_code" runat="server">
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
