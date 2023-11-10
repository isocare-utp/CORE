<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_shrwtd_cancel.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_sl_shrwtd_cancel" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsPostCancelSwd%>
    <%=jsPostCancelSwdDet%>
    <%=newClear%>
    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objdw_main.GetItem(rowNumber, "member_no");
                jsPostCancelSwd();
            }

        }
        function ListClick(s, r, c) {
            Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
            Gcoop.GetEl("HfSlipNo").value = objdw_list.GetItem(r, "payoutslip_no");
            jsPostCancelSwdDet();
        }
        function Checkbox(s, r, c) {
            Gcoop.CheckDw(s, r, c, "cmshrwithdraw_setshrarr_flag", 1, 0);

        }
        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            newClear();
        }
        function CnvNumber(num) {
            if (IsNum(num)) {
                return parseFloat(num);
            }
            return 0;
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                // alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="HfSlipNo" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <table style="width: 100%;">
        <tr>
            <td colspan="2" valign="top">
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_lnccl_detailmem_tks"
                    LibraryList="~/DataWindow/mbshr/sl_slipall.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" ClientEventItemChanged="ItemChangedMain">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td rowspan="2" valign="top">
                <dw:WebDataWindowControl ID="dw_list" runat="server" DataWindowObject="d_sl_lnccl_sliplist_shrwtd_tks"
                    LibraryList="~/DataWindow/mbshr/sl_slipall.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventClicked="ListClick">
                </dw:WebDataWindowControl>
            </td>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_sl_lnccl_shrwtd"
                    LibraryList="~/DataWindow/mbshr/sl_slipall.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventClicked="Checkbox">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_lnccl_payin_detail"
                    LibraryList="~/DataWindow/mbshr/sl_slipall.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
