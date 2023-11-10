<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_upnewbook.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_upnewbook"
    Culture="th-TH" %>

<%@ Register Src="w_sheet_dp_upnewbook_ctrl/SlipMaster.ascx" TagName="SlipMaster"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDeptAccountNo%>
    <%=postDeptAccountNoHd%>
    <%=postReset%>
    <%=postBarcode%>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกหน้าจอ");
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(900, 600, "w_dlg_dp_account_search.aspx", "");
        }

        function MenubarNew() {
            window.location = "";
        }

        function NewAccountNo(coopid, dept_no) {
            Gcoop.GetEl("HdDumCoopId").value = coopid;
            Gcoop.GetEl("HdDumAccountNo").value = dept_no;
            postDeptAccountNoHd();
        }

        function SheetLoadComplete() {
        }

        function PrintNewBook() {
            var deptAccountNo = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptaccount_no').value;
            var deptPassBookNo = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptslip_amt').value;
            Gcoop.OpenIFrame(450, 110, "w_iframe_dp_printfirstpage.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo);
        }

        function PrintBook() {
            var deptAccountNo = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptaccount_no').value;
            if (deptAccountNo != "") {
                Gcoop.OpenIFrame(900, 400, "w_dlg_dp_printbook.aspx", "?deptAccountNo=" + deptAccountNo);
            }
        }

        function DialBookNew() {
            var deptAccountNo = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptaccount_no').value;
            var deptPassBookNo = "";
            try {
                deptPassBookNo = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptslip_amt').value;
                //sdeptPassBookNo = Gcoop.StringFormat(Gcoop.ParseInt(deptPassBookNo), "0000000000");
            } catch (err) { deptPassBookNo = ""; }
            if (deptAccountNo != "") {
                var deptTypeCode = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_depttype_code').value;
                Gcoop.OpenDlg(450, 150, "w_dlg_dp_booknew.aspx", "?deptAccountNo=" + deptAccountNo + "&deptPassBookNo=" + deptPassBookNo + "&deptTypeCode=" + deptTypeCode );
            }
        }

        function CommitPrintFirstPage() {
            Gcoop.RemoveIFrame();
        }
    </script>
    <%
        try
        {
            String accNo = SlipMaster1.GetItemString(1, "deptaccount_no");
            if (accNo != "" && SlipMaster1.GetItemString(1, "deptslip_amt") == "" && SlipMaster1.GetItemString(1, "recppaytype_code") != "PRINT")
            {
                String sql = "select deptpassbook_no from dpdeptmaster where coop_id='" + state.SsCoopControl + "' and deptaccount_no='" + accNo + "'";
                DataLibrary.Sdt dt = Saving.WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    String bookNo = dt.GetString(0);
                    SlipMaster1.SetItem(1, "deptslip_amt", bookNo);
                    SlipMaster1.SetItem(1, "deptslip_amt", bookNo);
                }
            }
        }
        catch
        {
        }
    %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:SlipMaster ID="SlipMaster1" runat="server" />
    <input id="ShifF5" type="button" value="พิมพ์ปกสมุด" onclick="PrintNewBook()" style="height: 40px;
        width: 100px;" />
    <input id="ShifF7" type="button" value="พิมพ์ BOOK" onclick="PrintBook()" style="height: 40px;
        width: 100px;" />
    <input id="ShifF11" type="button" value="ออกสมุดใหม่" onclick="DialBookNew()" style="height: 40px;
        width: 100px;" />
    <asp:HiddenField ID="HdDumAccountNo" Value="" runat="server" />
    <asp:HiddenField ID="HdDumCoopId" Value="" runat="server" />
    
</asp:Content>
