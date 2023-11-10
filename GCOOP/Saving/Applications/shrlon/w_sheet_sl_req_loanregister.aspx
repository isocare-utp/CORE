<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_req_loanregister.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_req_loanregister" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=jsPostMember %>
    <%=jsNewClear %>
    <%=jsPostIns %>
    <%=jsCancelRequest%>
    <%=jsDeleteRequest%>
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
    </style>
    <script type="text/javascript">
        function Validate() {
            var mem_no = "";
            var approve_amt = "";

            mem_no = objDw_main.GetItem(1, "member_no");
            approve_amt = objDw_detail.GetItem(1, "loanapprove_amt");

            if (mem_no == "" || mem_no == null) {
                alert("คุณยังไม่ได้เลือกสมาชิก");
            }
            //            else if (approve_amt == 0 || approve_amt == null) {
            //                alert("คุณยังไม่ได้ใส่จำนวนเงินอนุมัติกู้");
            //            }
            else {
                return confirm("ยืนยันการบันทึกข้อมูล");
            }
        }
       function GetValueFromDlg(memberno) {
                //alter(memberno);
                objDw_main.SetItem(1, "member_no", memberno);
                objDw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objDw_main.GetItem(1, "member_no");
              //  Gcoop.GetEl("Hfmember_no").value = memberno;
                jsPostMember();


            }

        function ItemChangedmain(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
              
                objDw_main.SetItem(rowNumber, columnName, newValue);
                objDw_main.AcceptText();
                Gcoop.GetEl("Hfmember_no").value = objDw_main.GetItem(rowNumber, "member_no");
                jsPostMember();
            }
        }
        function DwdetailButtonClick(sender, rowNumber, columnName, newValue) {
            if (columnName == "b_delete") {
               
                var reqregister_docno = "";
                reqregister_docno = objDw_detail.GetItem(1, "reqregister_docno");
                if (reqregister_docno == "" || reqregister_docno == null) {
                    alert("ยังไม่มีข้อมูลไม่สามารถลบรายการได้");
                }
                else {
                    if (confirm("คุณต้องการลบรายการ รหัส : " + reqregister_docno + " ใช่หรือไม่?")) {
                        jsDeleteRequest();
                    }
                }
            }



            return 0;

        }
        function b_Click(s, r, c) {
            if (c == "b_search") {
               
                var memno = objDw_main.GetItem(1, "member_no");
                Gcoop.OpenDlg('630', '600', 'w_dlg_sl_member_search.aspx', "?memno=" + memno);
            }
        }

        function OnListClick(sender, rowNumber, objectName) {
            Gcoop.GetEl("HfSlipNo").value = rowNumber;
            jsPostIns();
            return 0;
        }

        function OnDwDetailChanged(s, r, c, v) {
            if (c == "loantype_code_1" || c == "loantype_code") {
                objDw_detail.SetItem(1, "loantype_code_1", v);
                objDw_detail.SetItem(1, "loantype_code", v);
            }
            objDw_detail.AcceptText();
            return 0;
        }

        function MenubarNew() {
            if (confirm("บันทึกข้อมูลเรียบร้อย")) {

                jsNewClear();
            }
        }
        function Reqnew_Click() {
            var member_no = "?member_no=" + objDw_main.GetItem(1, "member_no");
            membno = objDw_main.GetItem(1, "member_no");
            if (membno == '' || membno == null) {
                alert('กรุณากรอกเลขสมาชิก');
            } else {
                Gcoop.OpenIFrame('600', '500', 'w_dlg_sl_req_new.aspx', member_no);
                //                jsPostMember();                
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="Hfmember_no" runat="server" />
    <asp:HiddenField ID="HfSlipNo" runat="server" />
    <table style="width: 100%;">
        <tr>
            <td colspan="2" class="style1">
                <strong>รายละเอียดสมาชิก </strong>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" Height="70px">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_sl_lnreqregister_main"
                        LibraryList="~/DataWindow/shrlon/sl_req_loanregister.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        Width="720px" ClientEventItemChanged="ItemChangedmain" TabIndex="1" ClientFormatting="True"
                        ClientEventButtonClicked="b_Click">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <span onclick="Reqnew_Click()" class="NewRowLink">เพิ่มข้อมูล</span>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong>รายการใบคำขอกู้ </strong>
            </td>
            <td class="style1">
                <strong>รายละเอียดใบคำขอกู้ </strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" Height="200px" Width="300px">
                    <dw:WebDataWindowControl ID="Dw_list" runat="server" DataWindowObject="d_sl_lnreqregister_list"
                        LibraryList="~/DataWindow/shrlon/sl_req_loanregister.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="OnListClick" ClientFormatting="True" TabIndex="40">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="200px" Width="500px">
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_sl_lnreqregister_detail"
                        LibraryList="~/DataWindow/shrlon/sl_req_loanregister.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientFormatting="True" ClientEventItemChanged="DwdetailClick" ClientEventButtonClicked="DwdetailButtonClick"
                        TabIndex="60">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
