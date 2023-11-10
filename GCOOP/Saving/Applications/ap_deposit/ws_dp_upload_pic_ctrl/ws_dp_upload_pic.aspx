<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_dp_upload_pic.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dp_upload_pic_ctrl.ws_dp_upload_pic_ctrl" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "deposit_no") {
                PostDeposit();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_del") {
                var r = confirm("ต้องการลบรูปใช่หรือไม่");
                if (r == true) {
                    PostDelDeposit();
                } else {
                    x = "You pressed NO!";
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessege" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <table class="DataSourceFormView">
        <tr>
            <td>
                <div>
                    <span>รูปลายเซ็นต์บัญชีเงินฝาก :</span>
                </div>
            </td>
            <td>
                <div>
                    <asp:TextBox ID="DeptAcc" runat="server" Style="width: 100px"></asp:TextBox>
                    <asp:FileUpload ID="UploadDept" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <asp:Image ID="path_deposit" runat="server" Height="200px" Width="150px" />
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" Style="width: 100px" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
