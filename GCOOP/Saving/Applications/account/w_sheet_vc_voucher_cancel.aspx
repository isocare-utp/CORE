<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_vc_voucher_cancel.aspx.cs"
    Inherits="Saving.Applications.account.w_sheet_vc_voucher_cancel" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  protected String postVcDate;
        protected String postClickVoucher;
        protected String postNewClear;
        protected String postSelectVoucher;--%>
    <%=initJavaScript%>
    <%=postVcDate%>
    <%=postClickVoucher%>
    <%=postNewClear%>
    <%=postSelectVoucher%>
    <script type="text/javascript">
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }

        function OnDwListClick(s, r, c) {
            Gcoop.CheckDw(s, r, c, "voucher_status", -9, 1);
            if (c == "voucher_desc") {
                vc_no = objDw_list.GetItem(r, "voucher_no");
                Gcoop.GetEl("Hd_vcno").value = vc_no;
                Gcoop.GetEl("Hd_rowclick").value = r + "";
                postClickVoucher();
            }
            return 0
        }

        function OnDWDateItemChange(s, r, c, v)//เปลี่ยนวันที่
        {
            if (v == "" || v == null) {
                alert("กรุณากรอกข้อมูลวันที่ Voucher")
            } else {
                s.SetItem(1, "voucher_tdate", v);
                s.AcceptText();
                s.SetItem(1, "voucher_date", Gcoop.ToEngDate(v));
                s.AcceptText();
                Gcoop.GetEl("Hdvcdate").value = v; //Hiden Field เก็บข้อมูลวันที่
                postVcDate();
            }
            return 0;
        }

        function Validate() {
            var isconfirm = confirm("ยืนยันการยกเลิกการคีย์รายวัน ?");
            if (!isconfirm) {
                return false;
            }

            var alertstr = "";
            var voucher_date = objDw_date.GetItem(1, "voucher_tdate");
            if (voucher_date == "" || voucher_date == null) {
                alertstr = alertstr + "_กรุณากรอกวันที่ Voucher \n";
            }

            if (alertstr == "") {
                return true;
            }
            else {
                alert(alertstr);
                return false;
            }
        }
        function SheetLoadComplete() {
            SetFocusDWListClick("Dw_list");
        }

        function SetFocusDWListClick(Dwobj) {
            var idx = Number(Gcoop.GetEl("Hd_rowclick").value) - 1;
            if (idx < 1) {
                idx = 0;
            }
            var sel = "#obj" + Dwobj + "_datawindow input[name='voucher_desc_" + idx + "']";
            $(sel).focus();
        }
    </script>
    <style type="text/css">
        .style1
        {
            font-size: x-small;
        }
        .style2
        {
            font-size: small;
        }
        .style3
        {
            font-size: small;
            width: 184px;
        }
        .style4
        {
            font-size: small;
            font-weight: bold;
        }
        .style5
        {
            font-size: small;
            width: 184px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr class="style1">
                <td class="style5">
                    ประจำวันที่
                </td>
                <td class="style4">
                    รายละเอียด Voucher
                </td>
            </tr>
            <tr class="style1">
                <td class="style3" valign="top">
                    <asp:Panel ID="Panel2" runat="server" Width="190px" BorderStyle="Ridge" Height="85px">
                        <dw:WebDataWindowControl ID="Dw_date" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_vc_vcedit_vcdate_cancel"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" ClientEventItemChanged="OnDWDateItemChange"
                            ClientFormatting="True" Width="180px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td class="style2">
                    <asp:Panel ID="Panel3" runat="server" BorderStyle="Ridge" Height="85px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_vc_vcedit_vchead"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" ClientFormatting="True"
                            Width="550px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="style1">
                <td class="style5">
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                        oncheckedchanged="CheckBox1_CheckedChanged" style="font-size: small" 
                        Text="ยกเลิกทั้งหมด" />
                </td>
                <td class="style4">
                    รายละเอียด Voucher
                </td>
            </tr>
            <tr class="style1">
                <td class="style3" valign="top">
                    <asp:Panel ID="Panel4" runat="server" BorderStyle="Ridge" Height="300px" ScrollBars="Vertical"
                        Width="190px">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_vc_vcedit_vclist_cancel"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" ClientEventClicked="OnDwListClick"
                            ClientFormatting="True" Width="180px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td class="style2" valign="top">
                    <asp:Panel ID="Panel5" runat="server" BorderStyle="Ridge" Height="300px" ScrollBars="Vertical">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_vc_vcedit_vcdetail"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            Width="550px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="style1">
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style2">
                    <asp:Panel ID="Panel6" runat="server">
                        <dw:WebDataWindowControl ID="Dw_footer" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_vc_vcedit_vcdetail_tail" LibraryList="~/DataWindow/account/vc_voucher_edit.pbl"
                            Width="550px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="style1">
                <td class="style3">
                    <asp:HiddenField ID="Hd_row" runat="server" />
                    <asp:HiddenField ID="HdBranchId" runat="server" />
                    <asp:HiddenField ID="Hdvcdate" runat="server" />
                    <asp:HiddenField ID="HdVoucherNo" runat="server" />
                </td>
                <td class="style2" valign="top">
                    <asp:HiddenField ID="Hd_vcno" runat="server" />
                    <asp:HiddenField ID="Hd_rowclick" runat="server" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
