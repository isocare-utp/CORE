<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_reprint_receipt_ry.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_reprint_receipt_ry" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%=initJavaScript%>
<%=jsFind%>
<%=jsPrint%>
<%=jsPostMember%>
<%=jsFilter%>
<%=postcheckAll %>
<%=newClear%>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function ItemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsFind();
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

        function ButtonClickedSearch() {
            var allrow = objdw_main.RowCount();
            if (allrow > 0) {
                jsFind();
            }
        }

        function b_filter_onclick() {
            var allrow = objdw_detail.RowCount();
            if (allrow > 0) {
                jsFilter();
            }
        }

        function OnDwDetailClicked(s, r, c) {
            if (c == "operate_flag") {
                Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
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
        LibraryList="~/DataWindow/app_finance/reprint_receipt.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        Width="720px" ClientEventButtonClicked ="ButtonClickedSearch" ClientEventItemChanged = "ItemChanged" TabIndex="1"  Visible="true"
        ClientFormatting="True" >
    </dw:WebDataWindowControl>
    
    <div style="margin-top: 0px; margin-left: 10px; position: absolute; z-index: 90;">
    <asp:CheckBox ID="CheckAll" runat="server" Text="เลือกทั้งหมด" onclick="ClickCheckAll()" />&nbsp&nbsp&nbsp
          <%--  <input style="height: 30px; width:100px" id="Button1" type="button" value="ค้นหา" onclick="b_find_onclick()" />--%>
        </div>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_loansrv_prn_payinsliplist"
        LibraryList="~/DataWindow/app_finance/reprint_receipt.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientFormatting="True"  Width ="400px" TabIndex="60" RowsPerPage="20" 
        ClientEventClicked="OnDwDetailClicked">
        <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>

      <dw:WebDataWindowControl ID="dw_retrieve" runat="server" DataWindowObject="d_loansrv_prn_payinslipdetail"
        LibraryList="~/DataWindow/app_finance/reprint_receipt.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientFormatting="True"  Width ="400px" TabIndex="60" RowsPerPage="20" 
        ClientEventClicked="OnDwDetailClicked">
    </dw:WebDataWindowControl>

    <div style="margin-top: 0px; margin-left: 540px; position: absolute; z-index: 90;">
          <%--  <input style="height: 30px; width:100px" id="b_wait" type="button" value="ดึงข้อมูล" onclick="b_filter_onclick()" />--%>
             <input style="height: 30px; width:100px" id="Button2" type="button" value="สั่งพิมพ์" onclick="b_print_onclick()" />
        </div>
       
</asp:Content>

