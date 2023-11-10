<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_acc_amortized.aspx.cs" Inherits="Saving.Applications.account.w_acc_amortized" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostAssetDoc%>
    <%=jsPostAmortized%>
    <script type="text/javascript">
        function OnDwMainItemChange(sender, row, col, value) {
            sender.SetItem(row, col, value);
            sender.AcceptText();
            if (col == "asset_doc" && value != "") {
                jsPostAssetDoc();
            }
        }

        function OnDwMainButtonClicked(sender, row, bName) {
            if (bName == "b_amortized") {
                var asset_doc = objDwMain.GetItem(1, "asset_doc");
                var sale_date = objDwMain.GetItem(1, "sale_tdate");
                var exe_text = objDwMain.GetItem(1, "exe_text");
                if (asset_doc == "" || asset_doc == null) {
                    alert("กรุณาระบุเลขทะเบียนสินทรัพย์ก่อน");
                }
                else if (sale_date == "00000000") {
                    alert("กรุณาระบุวันที่จำหน่ายก่อน");
                }
                else if (exe_text == "" || exe_text == null) {
                    alert("กรุณาระบุเหตุผลการตัดจำหน่าย");
                }
                else {
                    if (confirm("ต้องการตัดจำหน่ายสินทรัพย์ เลขทะเบียน " + asset_doc + " ใช่หรือไม่")) {
                        jsPostAmortized();
                    }
                }
            }
            else if (bName == "b_search") {
                Gcoop.OpenIFrame(670, 400, "w_dlg_del.aspx", "");
            }
        }

        function SelectAssetDoc(asset_doc) {
            objDwMain.SetItem(1, "asset_doc", asset_doc);
            jsPostAssetDoc();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_acc_amortized"
        LibraryList="~/DataWindow/account/asset.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwMainButtonClicked"
        ClientScriptable="True" ClientEventItemChanged="OnDwMainItemChange" ClientFormatting="True">
    </dw:WebDataWindowControl>
</asp:Content>
