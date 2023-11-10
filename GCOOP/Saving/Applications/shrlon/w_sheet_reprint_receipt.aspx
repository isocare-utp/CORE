<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_reprint_receipt.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_reprint_receipt" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=jsFind%>
    <%=jsPrint%>
    <%=jsPostMember%>
    <%=jsFilter%>
    <%=postcheckAll %>
    <%=newClear%>
    <%=jsPostEntry%>
    <%=jsPostcoopId%>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function ItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsPostMember();
            } else if (columnName == "entry_id") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsPostEntry();
            } else if (columnName == "coop_id") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsPostcoopId();
            }
        }

        //        function ListClick(s, r, c) 
        //        {
        //            Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
        //            Gcoop.GetEl("HfSlipNo").value = objdw_list.GetItem(r, "payinslip_no");
        //            jsPostPayInList();
        //        }

        function CnvNumber(num) {
            if (IsNum(num)) {
                return parseFloat(num);
            }
            return 0;
        }

        function b_print_onclick() {
            var allrow = objdw_detail.RowCount();
            if (allrow > 0) {
                jsPrint();
            }
            else {
                alert("ไม่พบข้อมูลที่จะพิมพ์");
            }
        }

        function ClickCheckAll() {
            if (objdw_detail.RowCount() > 0) {
                postcheckAll();
            }
        }

        function b_find_onclick() {
            var allrow = objdw_main.RowCount();
            if (allrow > 0) {
                jsFind();
            }
        }

        function b_filter_onclick() {
            //            var allrow = objdw_detail.RowCount();
            //            if (allrow > 0) {
            //                jsFilter();
            //            }
            jsFilter();
        }

        function OnDwDetailClicked(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
                //                alert(objdw_detail.GetItem(r, "operate_flag"));
                objdw_detail.AcceptText();
                // newClear();
            }
        }
        function b_seachClick(s, r, c) {
            if (c == "b_search") {

                if (objdw_main.GetItem(r, "entry_id") != null) {
                    jsPostEntry();
                }
                else if (objdw_main.GetItem(r, "member_no") != null) {
                    jsPostMember();
                }
            }
        }

        function MenubarNew() {

            newClear();

        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="HfSlipNo" runat="server" />
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_loansrv_prn_payinslipcriteria"
        LibraryList="~/DataWindow/shrlon/reprint_receipt.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        Width="720px" ClientEventItemChanged="ItemChanged" ClientEventButtonClicked="b_seachClick"
        TabIndex="1" ClientFormatting="True">
    </dw:WebDataWindowControl>
   <div style="margin-top: 0px; margin-left: 540px; position: absolute; z-index: 90;">
        <input style="height: 30px; width: 100px" id="Button1" type="button" value="ดึงข้อมูล"
            onclick="b_filter_onclick()" />
        <input style="height: 30px; width: 100px" id="Button3" type="button" value="สั่งพิมพ์"
            onclick="b_print_onclick()" />
    </div>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_loansrv_prn_payinsliplist_loan"
        LibraryList="~/DataWindow/shrlon/reprint_receipt.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientFormatting="True" Width="700px" Height="600px" ClientEventClicked="OnDwDetailClicked">
    </dw:WebDataWindowControl>
    <div style="margin-top: 0px; margin-left: 540px; position: absolute; z-index: 90;">
        <input style="height: 30px; width: 100px" id="b_wait" type="button" value="ดึงข้อมูล"
            onclick="b_filter_onclick()" />
        <input style="height: 30px; width: 100px" id="Button2" type="button" value="สั่งพิมพ์"
            onclick="b_print_onclick()" />
    </div>
</asp:Content>
