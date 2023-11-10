<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ln_collredeem_detail.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_collredeem_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%=initJavaScript %>
 <%=jsPostMember %>
 <%=jsPostcollmast %>
 <script type="text/javascript">
        function Validate(){
            var mem_no = "";
            var coll_no = "";
           

            mem_no = objDw_main.GetItem(1, "member_no");
            coll_no = objDw_main.GetItem(1, "collmast_no");
           
            if (mem_no == "" || mem_no == null) {
                alert("คุณยังไม่ได้เลือกสมาชิก");
            }
            else if (coll_no == "" || coll_no == null) {
                alert("คุณยังไม่ได้เลือกทะเบียนอ้างอิง");
            }
            else {
                return confirm("ยืนยันการบันทึกข้อมูล");
            }
        }

        function ItemChangedMain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                objDw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                objDw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objDw_main.GetItem(rowNumber, "member_no");
                jsPostMember();

            }
            else if (columnName == "collmast_no") {
                //objdw_detail.SetItem(rowNumber, columnName, newValue);
                objDw_main.SetItem(rowNumber, columnName, newValue);
                objDw_main.AcceptText();
                Gcoop.SetLastFocus("collmast_no");
                Gcoop.GetEl("Hfcoll_no").value = objDw_main.GetItem(rowNumber, "collmast_no");
                jsPostcollmast();

            }
        }

        function MenubarOpen(s, r, c) {
            if (c == "b_member") {
                var memno = objDw_main.GetItem(1, "member_no");
                Gcoop.OpenDlg('630', '600', 'w_dlg_sl_loancontract_search_memno.aspx', "?memno=" + memno);
            }
//            else if (c == "b_collmast") {
//                var collno = objDw_main.GetItem(1, "collmast_no");
//                Gcoop.OpenDlg('630', '600', 'w_dlg_sl_.aspx', "?collno=" + collno);
//            }
        }

        function MenubarNew() {
            if (confirm("บันทึกข้อมูลเรียบร้อย")) {
                jsNewClear();
            }
        }   
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="Hfcoll_no" runat="server" />
 <div>
        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_sl_collredeem_detail"
            LibraryList="~/DataWindow/shrlon/sl_collredeem_detail.pbl" ClientScriptable="True" ClientEventButtonClicked="MenubarOpen"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" ClientEventItemChanged="ItemChangedMain">
        </dw:WebDataWindowControl>
 </div>
</asp:Content>
