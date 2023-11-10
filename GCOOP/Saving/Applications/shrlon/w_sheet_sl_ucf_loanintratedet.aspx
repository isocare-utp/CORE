<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_ucf_loanintratedet.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_ucf_loanintratedet" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postInsertRow%>
    <%=postDeleteRow%>
    <%=postLoanintrate%>
    <%=PostEffDate %>
    <script type="text/javascript">
        function OnClickInsertRow() { //function เพิ่มแถวข้อมูล
            postInsertRow();
        }

        function OnDwHeadItemChanged(sender, row, col, Value) {
            if (col == "loanintrate_code") {
                sender.SetItem(row, col, Value);
                sender.AcceptText();
                postLoanintrate();
            }
        }


        function Delete_ucf_district(sender, row, bName) { //function ลบแถวข้อมูล
            if (bName == "b_del") {
                var seq_no = objDwMain.GetItem(row, "seq_no");

                if (seq_no == "" || seq_no == null) {
                    Gcoop.GetEl("Hd_row").value = row + "";
                    postDeleteRow();
                }
                else {
                    var isConfirm = confirm("ต้องการลบข้อมูลใช่หรือไม่ ?");
                    if (isConfirm) {
                        Gcoop.GetEl("Hd_row").value = row + "";
                        postDeleteRow();
                    }
                }
            }
            return 0;
        }
        function OnDwMainItemChanged(s, r, c, v) {
            
            if (c == "effective_tdate") {
                Gcoop.GetEl("Hd_row").value = r;
                s.SetItem(r, c, v);
                s.AcceptText();
                s.SetItem(r, "effective_date", Gcoop.ToEngDate(v));
                s.AcceptText();
                PostEffDate();

            }

        }

        function Validate() { //function เช็คค่าข้อมูลก่อน save
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");

            //            if (!isconfirm) {
            //                return false;
            //            }
            //            var RowDetail = objDwMain.RowCount();
            //            var alertstr = "";

            //            for (var i = 1; i <= RowDetail; i++) {
            //                var effective_tdate = objDwMain.GetItem(i, "effective_tdate");
            //                var lower_amt = objDwMain.GetItem(i, "lower_amt");
            //                var upper_amt = objDwMain.GetItem(i, "upper_amt");
            //                var interest_rate = objDwMain.GetItem(i, "interest_rate");

            //                if (effective_tdate == "" || effective_tdate == null || effective_tdate == "00000000" || lower_amt == "" || lower_amt == null || upper_amt == "" ||
            //                 upper_amt == null || interest_rate == "" || interest_rate == null) {
            //                    alert("กรุณาระบุข้อมูลให้ครบถ้วน");
            //                    return false;
            //                }
            //            }
            return true;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 34%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

    <input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />

    <span style="color: red; font-size: 15px;">***
        <br />
        1. วันที่เปลี่ยนอัตราดอกเบี้ย ต้องมากกว่าวันที่สิ้นสุดในลำดับก่อนหน้าเช่น
        <br />
    </span><span style="color: red; font-size: 12px;">
        <table class="style1">
            <tr>
                <td width="50%">
                    วันที่เปลี่ยนอัตราดอกเบี้ย
                </td>
                <td align="center">
                    วันที่สิ้นสุด
                </td>
            </tr>
            <tr>
                <td align="center">
                    01/01/2556
                </td>
                <td align="center">
                    31/03/2556
                </td>
            </tr>
            <tr>
                <td align="center">
                    01/04/2556
                </td>
                <td align="center">
                    31/12/9999
                </td>
            </tr>
        </table>
    </span><span style="color: red; font-size: 15px;">2. การเซ็ทค่ายอดเงิน(ยอดเงินตั้งแต่,ยอดเงินถึง)ยอดเงินตั้งแต่
        ต้องมากกว่ายอดเงินถึงในลำดับก่อนหน้าเช่น</span> <span style="color: red; font-size: 12px;">
            <table class="style1">
                <tr>
                    <td width="50%" align="center">
                        ยอดเงินตั้งแต่
                    </td>
                    <td align="center">
                        ยอดเงินถึง
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        1.00
                    </td>
                    <td align="right">
                        100,000.00
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        100,000.01
                    </td>
                    <td align="right">
                        999,999.00
                    </td>
                </tr>
            </table>
        </span>
    <br />
    <dw:webdatawindowcontrol id="DwHead" runat="server" datawindowobject="dw_lc_lccfloanintratedet_head"
        librarylist="~/DataWindow/shrlon/sl_cfinttable.pbl" clientscriptable="True" clienteventitemchanged="OnDwHeadItemChanged"
        autorestorecontext="False" autorestoredatacache="True" autosavedatacacheafterretrieve="True">
    </dw:webdatawindowcontrol>
    <span onclick="OnClickInsertRow()" style="cursor: pointer;">
        <asp:Label ID="LbInsert1" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="Blue" /></span>
    <dw:webdatawindowcontrol id="DwMain" runat="server" datawindowobject="dw_lc_lccfloanintratedet"
        librarylist="~/DataWindow/shrlon/sl_cfinttable.pbl" clientscriptable="True" clienteventitemchanged="OnDwMainItemChanged"
        clienteventbuttonclicked="Delete_ucf_district" autorestorecontext="False" autorestoredatacache="True"
        autosavedatacacheafterretrieve="True" rowsperpage="20" clientformatting="True"
        style="top: 1px; left: 0px">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:webdatawindowcontrol>
    <asp:HiddenField ID="Hd_row" runat="server" />
</asp:Content>
