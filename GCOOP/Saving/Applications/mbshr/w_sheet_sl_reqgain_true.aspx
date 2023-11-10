<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_reqgain_true.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_sl_reqgain_true" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postMemberno%>
    <%=DetailAddRow%>
    <%=DetailDelRow%>
    <%=postSalaryId%>
    <script type="text/javascript">
        function Validate() {
            objdw_gain.AcceptText();
            objdw_gaindetail.AcceptText();

            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            window.location = Gcoop.GetUrl() + "Applications/mbshr/w_sheet_sl_reqgain_true.aspx";
        }
        function ItemChange(s, r, c, v) {
            if (c == "member_no") {
                s.SetItem(1, "member_no", v);
                s.AcceptText();
                postMemberno();
            }
            else if (c == "salary_id") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                //window.location = str_arr[0] + "?strvalue=" + Gcoop.StringFormat(newValue, "00000000");
                s.SetItem(r, c, v);
                s.AcceptText();
                postSalaryId();
            }
        }
        function OnDwDetailButtonClick(s, r, c) {
            if (c == "b_add") {
                DetailAddRow();
            } else if (c == "b_del") {
                Gcoop.GetEl("HRow").value = r;
                DetailDelRow();
            }
        }
        function ItemChange_detail(s, r, c, v) {
            if (c == "gain_relation") {
                s.SetItem(r, "gain_relation", v);
            }
            s.AcceptText();
            return 0;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="dw_gain" runat="server" DataWindowObject="d_mb_gain_master_new"
        LibraryList="~/DataWindow/mbshr/sl_reqgain_true.pbl" ClientScriptable="True"
        ClientEvents="true" ClientEventItemChanged="ItemChange" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
    </dw:WebDataWindowControl>

    <dw:WebDataWindowControl ID="dw_gaindetail" runat="server" DataWindowObject="d_mb_gain_detail_new"
        LibraryList="~/DataWindow/mbshr/sl_reqgain_true.pbl" ClientScriptable="True"
        ClientEventItemChanged="ItemChange_detail" ClientEvents="true" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        ClientEventButtonClicked="OnDwDetailButtonClick" Height="300" Width="745">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HRow" runat="server" />
    <asp:HiddenField ID="HfMemberNo" runat="server" />
    <asp:HiddenField ID="HfRowDetail" runat="server" />
</asp:Content>
