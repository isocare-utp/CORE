<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
CodeBehind="w_sheet_sl_loanreq_register_update.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loanreq_register_update" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%=initJavaScript %>
 <%=jsPostMember %>
 <%=jsCancelRequest%>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

     <script type="text/javascript">
         function Validate() {
             var mem_no = "";
             var receive_date = "";
             var meeting_date = "";
             var type_code = "";
             var request_amt = "";

             mem_no = objDw_main.GetItem(1, "member_no");
             receive_date = objDw_detail.GetItem(1, "lnreqreceive_tdate");
             meeting_date = objDw_detail.GetItem(1, "lnmeeting_tdate");
             type_code = objDw_detail.GetItem(1, "loantype_code");
             request_amt = objDw_detail.GetItem(1, "loanrequest_amt");

             if (mem_no == "" || mem_no == null) {
                 alert("คุณยังไม่ได้เลือกสมาชิก");
             }
             else if (receive_date == "" || receive_date == null) {
                 alert("คุณใส่วันที่วันลงรับไม่ถูกต้อง");
             }
             else if (meeting_date == "" || meeting_date == null) {
                 alert("คุณใส่วันที่เข้าประชุมไม่ถูกต้อง");
             }
             else if (type_code == "" || type_code == null) {
                 alert("คุณยังไม่ได้เลือกประเภทเงินกู้");
             }
             else if (request_amt == "" || request_amt == null) {
                 alert("คุณยังไม่ได้ใส่จำนวนเงินขอกู้");
             }
             else {
                 return confirm("ยืนยันการบันทึกข้อมูล");
             }
         }

         function ItemChangedmain(sender, rowNumber, columnName, newValue) {
             if (columnName == "member_no") {
                 objDw_main.SetItem(rowNumber, columnName, Gcoop.StringFormat(newValue, "00000000"));
                 objDw_main.AcceptText();
                 //objDw_detail.AcceptText();
                 Gcoop.GetEl("Hfmember_no").value = objDw_main.GetItem(rowNumber, "member_no");
                 jsPostMember();
                 
             }
         }
         function DwmainClick(sender, rowNumber, columnName, newValue) {
             if (columnName == "b_cancel") {
                  Gcoop.GetEl("Hdselectrow").value = r ;
                  alert("cancel");
                  jsCancelRequest();

             }

         }
         function b_Click(s, r, c) {
             if (c == "b_search") {
                 var memno = objDw_main.GetItem(1, "member_no");
                 Gcoop.OpenDlg('630', '600', 'w_dlg_sl_loancontract_search_memno.aspx', "?memno=" + memno);
             }
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
    <asp:HiddenField ID="HfSlipNo" runat="server" />

    <br />
    <table class="style1">
        <tr>
            <td>
                <strong>รายละเอียดสมาชิก
            </strong></td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="70px">
                <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_sl_lnreqregister_main"
                    LibraryList="~/DataWindow/shrlon/sl_req_loanregister.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    Width="720px" ClientEventItemChanged="ItemChangedmain" TabIndex="1" ClientFormatting="True"
                     ClientEventButtonClicked="b_Click">
                </dw:WebDataWindowControl>
                </asp:Panel></td>
        </tr>
        <tr>
            <td>
                เพิ่มใบลงรับหนังสือกู้</td>
        </tr>
        <tr>
            <td>
                 <asp:Panel ID="Panel3" runat="server" Height="200px" Width="500px">
                <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_sl_lnreqregister_update"
                    LibraryList="~/DataWindow/shrlon/sl_req_loanregister.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientFormatting="True" ClientEventItemChanged="OnDwDetailChanged" ClientEventButtonClicked="DwmainClick" TabIndex="60">
                </dw:WebDataWindowControl>
                </asp:Panel></td>
        </tr>
    </table>
     <asp:HiddenField ID="Hdselectrow" runat="server" />
</asp:Content>
