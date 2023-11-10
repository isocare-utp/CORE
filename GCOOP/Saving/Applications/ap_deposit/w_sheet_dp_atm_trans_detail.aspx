<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_atm_trans_detail.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_atm_trans_detail" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsSearch%>

    <script type="text/javascript">

//        function Validate() {
//            return confirm("ต้องการยืนยัน ผ่านรายการหรือไม่?");
//        }

        function DwMainItemChanged(s, r, c, v) {
            s.SetItem(r, c, v);
            switch (c) {
                case "st_memno":
                    s.SetItem(r, "ed_memno", v);
                    break;
                case "st_tdate":
                    s.SetItem(r, "end_tdate", v);
                    break;
            }
            s.AcceptText();
        }

        function DwMainButtonClicked(s, r, c) {
            if (c == "b_search") {
                jsSearch();
            }
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_atmtrans_header"
        LibraryList="~/DataWindow/ap_deposit/dp_atm_memberdetail.pbl" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True" ClientFormatting="True"
        ClientEventItemChanged="DwMainItemChanged" ClientEventButtonClicked="DwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_atmtrans_all"
        LibraryList="~/DataWindow/ap_deposit/dp_atm_memberdetail.pbl" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
        ClientEventItemChanged="DwDetailItemChanged" ClientEventButtonClicked="DwDetailButtonClicked" ClientFormatting="True" Width="742px" Height="400px">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdRowPass" runat="server" Value="" />
    <asp:HiddenField ID="HdOperate_Date" runat="server" Value="" />
</asp:Content>
