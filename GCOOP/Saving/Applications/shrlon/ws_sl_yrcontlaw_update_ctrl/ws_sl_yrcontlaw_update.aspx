<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_yrcontlaw_update.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_yrcontlaw_update_ctrl.ws_sl_yrcontlaw_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Post() {
            Post();
        }
        function txtChanged() {
            PostCheck();
        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hdperiod" runat="server" />
    <table class="DataSourceFormView" style="width: 400px">
        <tr>
            <td width="30%">
                <div>
                    <span>ปี :</span></div>
            </td>
            <td width="30%">
                <asp:TextBox ID="year" runat="server" Style="text-align: center;" onChange="txtChanged()"></asp:TextBox>
            </td>
            <td width="40%">
                <input type="button" value="ปรับปรุง" style="width: 80px" onclick="Post()" />
            </td>
        </tr>
    </table>
</asp:Content>
