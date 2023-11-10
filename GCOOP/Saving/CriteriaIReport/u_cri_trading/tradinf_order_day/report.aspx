<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="report.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_trading.tradinf_order_day.report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">
        $(function () {
            AutoSlash('input[name="ctl00$ContentPlace$start_tdate"]');
            AutoSlash('input[name="ctl00$ContentPlace$end_tdate"]');
            validatefrom();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium">[TRADING03] - รายงานวันซื้อ </asp:Label><br />
        <asp:Label ID="lbError" runat="server" Style="color: Red"></asp:Label>
    </center>
    <br />
    <table class="iReportDataSourceFormView">
        <tr>
            <td width="30%">
                <div>
                    <span>ตั้งแต่วันที่ :</span></div>
            </td>
            <td>
                <asp:TextBox ID="startdate" runat="server" ToolTip="dd/MM/yyyy" onkeyup="ValidateDate(this, event.keyCode)"
                    onkeydown="return DateFormat(this, event.keyCode)"></asp:TextBox>
            </td>
            <td width="5">
                <div>
                    <span>ถึง :</span></div>
            </td>
            <td align="left">
                <asp:TextBox ID="enddate" runat="server" ToolTip="dd/MM/yyyy" onkeyup="ValidateDate(this, event.keyCode)"
                    onkeydown="return DateFormat(this, event.keyCode)"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="validdate" runat="server" Text="0" Style="display: none"></asp:TextBox>
    <script type="text/javascript">

        var isShift = false;
        var seperator = "/";

        function DateFormat(txt, keyCode) {
            //var lblmesg = document.getElementById("<%=lbError.ClientID%>");
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
        function ValidateDate(txt, keyCode) {
            var val = txt.value;
            var lblmesg = document.getElementById("<%=lbError.ClientID%>");
            if (keyCode == 16)
                isShift = false;
            if (keyCode == 9 || keyCode == 13) {
                lblmesg.innerHTML = "";
                return;
            }
            if (txt.value.length > 10) {               
                alert("รูปแบบวันที่ไม่ถูกต้อง\n\n***กรุณาป้อนวันที่ใหม่ คะ***");
                var textv = txt.value;
                txt.value = textv.substring(0, textv.length - 1);
                lblmesg.innerHTML = "";
                return;
            }
            if (document.getElementById("<%=validdate.ClientID %>").value == "1") {              
                alert("รูปแบบวันที่ไม่ถูกต้อง\n\n***กรุณาป้อนวันที่ใหม่ คะ***");
                var d = new Date();
                var curr_date = d.getDate();
                var curr_month = d.getMonth() + 1; //Months are zero based
                var curr_year = d.getFullYear();
                document.getElementById("<%=startdate.ClientID%>").value = curr_date + '/' + curr_month + '/' + (Number(curr_year) + 543).toString();
                document.getElementById("<%=enddate.ClientID%>").value = curr_date + '/' + curr_month + '/' + (Number(curr_year) + 543).toString();
                document.getElementById("<%=validdate.ClientID %>").value = "0";
                lblmesg.innerHTML = "";
                return;
            }
            if (val.length == 10) {
                var splits = val.split("/");
                var dt = new Date(splits[1] + "/" + splits[0] + "/" + splits[2]);

                //Validation for Dates
                if (dt.getDate() == splits[0] && dt.getMonth() + 1 == splits[1]
            && dt.getFullYear() == splits[2]) {
                    //lblmesg.style.color = "green";
                    lblmesg.innerHTML = "";
                    document.getElementById("<%=validdate.ClientID %>").value = "0";
                }
                else {
                    document.getElementById("<%=validdate.ClientID %>").value = "1";
                    lblmesg.style.color = "red";
                    lblmesg.innerHTML = "*รูปแบบวันที่ไม่ถูกต้อง";
                    return;
                }
            }
            else if (val.length < 10) {
                lblmesg.style.color = "blue";
                lblmesg.innerHTML = "รูปแบบวันที่ 00/00/0000 จะใส่เครื่องหมาย / ให้โดยอัตโนมัติ";
            } //*/
        }

    </script>
</asp:Content>
