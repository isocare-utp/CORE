<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_post_chgfixperiodpay.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_post_chgfixperiodpay_ctrl.ws_sl_post_chgfixperiodpay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Post() {
            Post();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table class="DataSourceFormView" style="width: 500px">
            <tr>
                <td width="25%">
                    <div>
                        <span>วันที่ผ่านรายการ :</span></div>
                </td>
                <td width="50%">
                    <asp:TextBox ID="post_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="25%">
                    <input type="button" value="ผ่านรายการ" style="width: 80px" onclick="Post()" />
                </td>
            </tr>
        </table>    
</asp:Content>
