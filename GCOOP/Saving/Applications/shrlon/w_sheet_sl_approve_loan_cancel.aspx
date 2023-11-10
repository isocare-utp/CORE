<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_approve_loan_cancel.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_approve_loan_cancel" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsgenReqDocNo%>
    <%=initJavaScript %>
    <script type="text/javascript">

        function genlnreqdocno() {
            objdw_master.AcceptText();
            var count = objdw_master.RowCount();
            var selectedRow = "";
            for (var i = 0; i < count; i++) {
                var temp = objdw_master.GetItem(i + 1, "loanrequest_status");
                if (temp == 1) {
                    jsgenReqDocNo();
                    return;
                }
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function selectRow(sender, rowNumber, objectName) {
            if (objectName == "loanrequest_status") {
                var appl_status = objdw_master.GetItem(rowNumber, "loanrequest_status");
                if (appl_status == "8") {
                   // objdw_master.SetItem(rowNumber, "loancontract_no", "");
                }
            }
        }

        function b_wait_onclick() {
            var allrow = objdw_master.RowCount();
            for (var i = 1; i <= allrow; i++) {
                objdw_master.SetItem(i, "loanrequest_status", "8");
              //  objdw_master.SetItem(i, "loancontract_no", "");
            }
        }
        function b_reject_onclick() {
            var allrow = objdw_master.RowCount();
            for (var i = 1; i <= allrow; i++) {
                objdw_master.SetItem(i, "loanrequest_status", "0");
            }
        }
        function b_approve_onclick() {
            var allrow = objdw_master.RowCount();
            for (var i = 1; i <= allrow; i++) {
                objdw_master.SetItem(i, "loanrequest_status", "1");
            }
        }
        function b_approve_no_onclick() {
            var allrow = objdw_master.RowCount();
            for (var i = 1; i <= allrow; i++) {
                objdw_master.SetItem(i, "loanrequest_status", "2");
            }
        }
        //        function newMemnoDlg() {
        //            opendlg("600", "350", "w_dlg_sl_edit_loan_contractno.aspx", "");
        //        }
       
    </script>
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
        .style2
        {
            font-size: small;
            font-family: Tahoma;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:HiddenField ID="HfRowSelectedCode1" runat="server" />
    <%-- <asp:TextBox ID="TextBox1" runat="server" Width="500px"></asp:TextBox>--%>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <%-- <table style="width: 100%;">
        <tr>
            <td style="width: 33.33%; height: 50px;" align="center">
            </td>
            <td style="width: 33.33%;" align="center">
                <input id="b_setrunno" type="button" value="ทะเบียนล่าสุด" style="width: 120px; height: 35px;"
                    onclick="newMemnoDlg();" disabled="disabled" />
            </td>
            <td style="width: 33.33%;" align="right">
                <input id="Button2" type="button" value="สร้างเลขสัญญา" style="width: 100px;
                    height: 35px;" onclick="genlnreqdocno();" />
            </td>
        </tr>
    </table>--%>
    <table style="width: 100%;" border="1">
        <tr>
            <td>
                <table style="width: 100%;" border="1" width="80px" bgcolor="#D3E7FF">
                    <tr>
                        <td align="center" width="70px" class="style1">
                            ใบคำร้อง
                        </td>
                        <td align="center" width="55px" class="style1">
                            ประเภท
                        </td>
                        <td align="center" width="210px" class="style1">
                            ทะเบียน ชื่อ-สกุล
                        </td>
                        <td align="center" width="110px" class="style1">
                            เงินขอกู้
                        </td>
                        <td align="center" width="150px">
                            <input style="height: 35px; width: 75px; background-color: Yellow; color: Black;"
                                id="b_wait" type="button" value="ยกเลิก" onclick="b_wait_onclick()" class="style2" />
                       <%--     <input style="height: 35px; width: 45px; background-color: Red; color: Black;" id="b_reject"
                                type="button" value="ไม่" onclick="b_reject_onclick()" class="style2" />--%>
                            <input style="height: 35px; width: 75px; background-color: #33CC33; color: Black;"
                                id="b_approve" type="button" value="อนุุมัติ" onclick="b_approve_onclick()" class="style2" />
                            <%-- <input style="height: 35px; width: 50px; background-color: Gray; color: Black;" id="Button1"
                                type="button" value="ไม่มีเลข" onclick="b_approve_no_onclick()" class="style2" />--%>
                        </td>
                        <td align="center" width="100px" class="style1">
                            <input id="b_buildrunno" type="button" value="สร้างเลขสัญญา" style="width: 100px;
                                height: 35px;" onclick="genlnreqdocno();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="table_list" style="height: 400px;" valign="top">
                <dw:WebDataWindowControl ID="dw_master" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    DataWindowObject="d_sl_apvloan_list_cancel" LibraryList="~/DataWindow/shrlon/sl_approve_loan.pbl"
                    ClientEventClicked="selectRow" ClientFormatting="True" Height="500px" Width="760px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
