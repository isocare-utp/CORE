<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_add_newgroup.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_mb_add_newgroup" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsRefresh%>
    <%=jsPostGroup%>
    <%=initJavaScript%>
    <%=newClear%>
    <%=jsmembgroup_code%>
    <%=jsCoopSelect %>
    <%=jsChangmidgroupcontrol%>
    <%=jsRetreivemidgroup %>
    <%=jsRetreivemidgroup2 %>
    <%=jsConfirmDelete%>
    <script type="text/javascript">
        function Validate() {
            objdw_main.AcceptText();
            objdw_detail.AcceptText();

            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "add_type") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsRefresh();
            }
            if (columnName == "memb_group1") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsRetreivemidgroup();
            }
            if (columnName == "membgroup_2") {
                objdw_main.SetItem(rowNumber, columnName, newValue);
                objdw_main.AcceptText();
                jsRetreivemidgroup2();
            }
            return 0;
        }

        function OnInsert() {
            Gcoop.GetEl("HdCheckRow").value = "TRUE";
            objdw_detail.InsertRow(0);

        }

        function ConfirmDelete(s, r, c) {            
            if (c == "b_del1") {              
                if (r > 0) {
                    var detail = "รหัส " + objdw_detail.GetItem(r, "membgroup_code");
                    var confirm_del = confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")
                    if (confirm_del) {                        
                        Gcoop.GetEl("HdRowDelete").value = r;
                        jsConfirmDelete();                        
                    }
                }
            }
        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hidmembsection" runat="server" />
    <asp:HiddenField ID="Hidgroup" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <asp:TextBox ID="TextDwmain" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="Textdwhistory" runat="server" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="HdCheckRow" runat="server" />
    <asp:HiddenField ID="HdRowDelete" runat="server" />
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_mb_addgroup_main"
        LibraryList="~/DataWindow/mbshr/mb_add_newgroup.pbl" ClientScriptable="True"
        ClientEventItemChanged="itemChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventButtonClicked="Click_search"
        ClientEventClicked="checkMain" TabIndex="1" RowsPerPage="0">
    </dw:WebDataWindowControl>
    <span class="linkSpan" onclick="OnInsert()" style="font-size: small; color: Green;
        float: right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
            - หลังจาก เพิ่มแถว/ลบแถว แล้วกดปุ่ม save อีกครั้ง )</span>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_mb_addgroup_detail"
        LibraryList="~/DataWindow/mbshr/mb_add_newgroup.pbl" ClientScriptable="True"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" ClientEventButtonClicked="ConfirmDelete" TabIndex="500" Width="350">
    </dw:WebDataWindowControl>
</asp:Content>
