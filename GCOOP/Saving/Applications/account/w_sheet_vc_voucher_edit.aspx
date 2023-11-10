<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_vc_voucher_edit.aspx.cs"
    Inherits="Saving.Applications.account.w_sheet_vc_voucher_edit" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  protected String postSelectList;
        protected String postVoucherDate;
        protected String postW_dlg_Click;
        protected String postNewClear;
        protected String postSearchVoucher;--%>
    <%=initJavaScript%>
    <%=postSelectList%>
    <%=postVoucherDate%>
    <%=postW_dlg_Click%>
    <%=postNewClear%>
    <%=postSearchVoucher%>
    <%=postPrint %>
    <%=postMoneyCode%>
    <script type="text/javascript">

        function OnDwfindItemChange(s, r, c, v) {
            if (c == "voucher_no") {
                s.SetItem(1, "voucher_no", v);
                s.AcceptText();
                Gcoop.GetEl("vcno").value = v;
                postSearchVoucher();
            }
        }

        function MenubarNew() {
            if (confirm("Â×¹ÂÑ¹¡ÒÃÅéÒ§¢éÍÁÙÅº¹Ë¹éÒ¨Í")) {
                postNewClear();
            }
        }

        function OnDwListClick(s, r, c) {
            if (c == "voucher_type" || c == "voucher_desc") {
                var vcNo = objDw_list.GetItem(r, "voucher_no");
                if (vcNo == null || vcNo == "") {
                    alert("¡ÃØ³Òà¾ÔèÁÃÒÂ¡ÒÃ Voucher ¡èÍ¹")
                }
                else {
                    Gcoop.GetEl("HdVoucherNo").value = Gcoop.Trim(vcNo);
                    Gcoop.GetEl("HdRowListClick").value = r + "";
                    postSelectList();
                }
            }
        }

        // opendialog
        //    function B_NewVc_Click() 
        //    {
        //        var vcDate = "";
        //        var b_type = "";
        //        vcDate = objDw_date.GetItem(1, "voucher_tdate"); // Êè§ª×èÍ¿ÔÇÊìÇÑ¹·Õèä·Âà¢éÒä»
        //        b_type = objDw_date.GetItem(row, "cash_type"); // Êè§ª×èÍ¿ÔÇÊìÇÑ¹·Õèä·Âà¢éÒä»
        //            
        //            if (vcDate == "" || vcDate == null )
        //            {
        //                alert ("¡ÃØ³Ò¡ÃÍ¡ÇÑ¹·Õè Voucher")
        //            }
        //            else if (b_type == "" || b_type == null) {
        //                alert("¡ÃØ³ÒàÅ×Í¡»ÃÐàÀ· Voucher")
        //            }
        //            else
        //            {
        //                var B_status = "New";
        //                //Gcoop.OpenDlg(950,550,"w_dlg_vc_voucher_edit.aspx", "?vcDate="+ vcDate +"&B_status=" + B_status);
        //                Gcoop.OpenIFrame(1000, 600, "w_dlg_vc_voucher_edit.aspx", "?vcDate=" + vcDate + "&B_status=" + B_status + "&b_type=" + b_type);
        //                 
        //            }    
        //    }


        // Print ãº voucher ¨Ò¡Ë¹éÒÅ§ÃÒÂÇÑ¹
        function Dw_mainButtonclick(sender, rowNumber, buttonName) {
            //if (buttonName == "b_print") {

            postPrint();
            // }
        }


        function OnDWDateItemChange(s, r, c, v)//à»ÅÕèÂ¹ÇÑ¹·Õè
        {
            if (c == "voucher_tdate") {
                if (v == "" || v == null) {
                    alert("กรุณาใส่วันที่ Voucher")
                }
                else {
                    s.SetItem(1, "voucher_tdate", v);
                    s.AcceptText();
                    s.SetItem(1, "voucher_date", Gcoop.ToEngDate(v));
                    s.AcceptText();
                    postVoucherDate();
                }
            }
            return 0;
        }


        function OnDwDateClicked(s, row, c, v) {
            //        if (c == "b_newvc") {
            //            B_NewVc_Click();
            //            
            //        }
            //         return 0;
            //    }

            if (c == "b_newvc") {
                var vcDate = "";
                var b_type = "";
                vcDate = objDw_date.GetItem(1, "voucher_tdate"); // Êè§ª×èÍ¿ÔÇÊìÇÑ¹·Õèä·Âà¢éÒä»
                b_type = objDw_date.GetItem(row, "cash_type"); // Êè§ª×èÍ¿ÔÇÊìÇÑ¹·Õèä·Âà¢éÒä»

                if (vcDate == "" || vcDate == null) {
                    alert("กรุณาใส่วันที่ Voucher")
                }
                else if (b_type == "" || b_type == null) {
                    alert("กรุณาเลือกประเภท Voucher")
                }
                else {
                    var B_status = "New";
                    //Gcoop.OpenDlg(950,550,"w_dlg_vc_voucher_edit.aspx", "?vcDate="+ vcDate +"&B_status=" + B_status);
                    Gcoop.OpenDlg(1055, 600, "w_dlg_vc_voucher_edit.aspx", "?vcDate=" + vcDate + "&B_status=" + B_status + "&b_type=" + b_type);

                }
            }
            else if (c == "b_search") {
                var vcDate = "";
                vcDate = objDw_date.GetItem(1, "voucher_tdate");

                // Gcoop.OpenIFrame(1055, 600, "w_dlg_search_voucher_full.aspx", "?vcDate=" + vcDate);
                Gcoop.OpenDlg(1055, 600, "w_dlg_search_voucher_full.aspx", "?vcDate=" + vcDate);
            }
            return 0;
        }

        function GetValueDlg(voucher_no) {
            Gcoop.GetEl("voucher_no_input").value = Gcoop.Trim(voucher_no);
            postMoneyCode();
        }

        function Validate() {
            var alertstr = "";
            var voucher_date = "";
            voucher_date = objDw_date.GetItem(1, "voucher_tdate");
            if (voucher_date == "" || voucher_date == null) {
                alertstr = alertstr + "_¡ÃØ³Ò¡ÃÍ¡ÇÑ¹·Õè Voucher \n";
            }

            if (alertstr == "") {
                return true;
            }
            else {
                alert(alertstr);
                return false;
            }
        }

        function OnDwListEditClick(s, r, c) {
            //HdVoucherNo
            //"ctl00_ContentPlace_HfChequeRowCurrent"
            if (c == "b_edit") {
                var PosttoaccFlag = "";
                PosttoaccFlag = objDw_list.GetItem(r, "posttoacc_flag");
                if (PosttoaccFlag == 1) {
                    alert("กรุณาเลือกประเภท Voucher")
                } else {
                    var vcNo = "";
                    var vcDate = "";
                    vcNo = objDw_list.GetItem(r, "voucher_no");
                    vcDate = objDw_date.GetItem(1, "voucher_tdate");
                    Gcoop.GetEl("HdRowListClick").value = r + "";
                    
                    var B_status = "Edit";
                    //Gcoop.OpenDlg(950,550,"w_dlg_vc_voucher_edit.aspx", "?vcNo=" + vcNo +"&vcDate=" + vcDate +"&B_status=" + B_status);
                    Gcoop.OpenDlg(1000, 600, "w_dlg_vc_voucher_edit.aspx", "?vcNo=" + vcNo + "&vcDate=" + vcDate + "&B_status=" + B_status);
                    SetFocusDWListClick("Dw_list");
                }
            }
        }


        function W_dlg_Click(vc_no) {
            Gcoop.GetEl("HdVoucherNo").value = vc_no;
            postW_dlg_Click();
        }

        function SheetLoadComplete() {
            SetFocusDWListClick("Dw_list");
        }

        function SetFocusDWListClick(Dwobj) {
            var idx = Number(Gcoop.GetEl("HdRowListClick").value) - 1;
            if (idx < 1) {
                idx = 0;
            }
            var sel = "#obj" + Dwobj + "_datawindow input[name='voucher_desc_" + idx + "']";
            $(sel).focus();
        }

    </script>
    <style type="text/css">
        .style3
        {
            font-size: small;
        }
        .style4
        {
            font-size: small;
            font-weight: bold;
        }
        .style5
        {
            width: 177px;
        }
        .style6
        {
            width: 177px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <span class="style3">
            <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
            <table style="width: 100%;">
                <tr>
                    <td class="style5">
                        <b>ประจำวันที่</b>
                    </td>
                    <td class="style4">
                        รายละเอียด Voucher <span class="linkSpan" id="print2" runat="server" onclick="postPrint()"
                            style="font-family: Tahoma; font-size: medium; float: right; color: #0000CC;">Print</span>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <asp:Panel ID="Panel3" runat="server" Height="81px" Width="200px" BorderStyle="Ridge">
                            <span class="style3">
                                <dw:WebDataWindowControl ID="Dw_date" runat="server" DataWindowObject="d_vc_vcedit_vcdate_cash_type"
                                    LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwDateClicked"
                                    ClientScriptable="True" ClientEventItemChanged="OnDWDateItemChange" ClientFormatting="True">
                                </dw:WebDataWindowControl>
                        </asp:Panel>
                    </td>
                    <td rowspan="2" valign="top">
                        <asp:Panel ID="Panel4" runat="server" BorderStyle="Ridge" Height="81px" Width="575px">
                            <span class="style3">
                                <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_vc_vcedit_vchead_tks"
                                    LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                                    ClientScriptable="True" ClientEventClicked="Dw_mainClick" ClientEventButtonClicked="Dw_mainButtonclick">
                                </dw:WebDataWindowControl>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <span class="style3">
                            <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="30px" Width="200px">
                                <dw:WebDataWindowControl ID="Dw_find" runat="server" DataWindowObject="d_vcno_find"
                                    LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwfindItemChange"
                                    ClientFormatting="True" ClientScriptable="True">
                                </dw:WebDataWindowControl>
                            </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        รายการ Voucher
                    </td>
                    <td>
                        <b>&nbsp; รายการ Voucher</b>
                    </td>
                </tr>
                <tr>
                    <td class="style5" valign="top">
                        <span class="style3">
                            <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Vertical" BorderStyle="Ridge">
                                <span class="style3">
                                    <dw:WebDataWindowControl ID="Dw_list" runat="server" DataWindowObject="d_vc_vcedit_vclist"
                                        LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                                        ClientEventClicked="OnDwListClick" ClientEventButtonClicked="OnDwListEditClick"
                                        Width="180px">
                                    </dw:WebDataWindowControl>
                            </asp:Panel>
                    </td>
                    <td>
                        <span class="style3">
                            <asp:Panel ID="Panel5" runat="server" BorderStyle="Ridge" Height="300px " Width="575px"
                                ScrollBars="Vertical">
                                <span class="style3">
                                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_vc_vcedit_vcdetail"
                                        LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                                    </dw:WebDataWindowControl>
                            </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <span class="style3">
                            <asp:Panel ID="Panel7" runat="server" Height="60px" Width="200px">
                                <span class="style3">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="font-size: 14px;">
                                                <span class="style4">เงินสดยกมา</span><span class="style3"> </span>
                                            </td>
                                            <td align="right" style="font-size: 14px;">
                                                <b>&nbsp;<span class="style3"><asp:Label ID="lbl_moneybg" runat="server" Text="0.00"
                                                    ForeColor="#006600"></asp:Label>
                                                </span></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-size: 14px;" width="100px">
                                                <span class="style4">เงินสดยกไป</span><span class="style3"> </span>
                                            </td>
                                            <td align="right" class="style4">
                                                <asp:Label ID="lbl_moneyfw" runat="server" Text="0.00" ForeColor="Maroon"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                            </asp:Panel>
                    </td>
                    <td valign="top">
                        <span class="style3">
                            <asp:Panel ID="Panel6" runat="server" Height="30px">
                                <span class="style3">
                                    <dw:WebDataWindowControl ID="Dw_footer" runat="server" DataWindowObject="d_vc_vcedit_vcdetail_tail"
                                        LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                                        ClientScriptable="True">
                                    </dw:WebDataWindowControl>
                            </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HdRowListClick" runat="server" />
            <asp:HiddenField ID="Hdrow" runat="server" />
            <asp:HiddenField ID="HdVoucherNo" runat="server" Value="" />
            <asp:HiddenField ID="Hdvcdate" runat="server" />
            <asp:HiddenField ID="vcno" runat="server" />
            <asp:HiddenField ID="HdButton" runat="server" />
            <asp:HiddenField ID="HSqlTemp" runat="server" />
            <asp:HiddenField ID="HdVoucherDate" runat="server" Value="" />
            <asp:HiddenField ID="HdBranchId" runat="server" />
            <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
            <asp:HiddenField ID="voucher_no_input" runat="server" />
    </p>    
    <%=outputProcess%>
</asp:Content>
