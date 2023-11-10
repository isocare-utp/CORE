<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_atmdept.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_atmdept" Title="Untitled Page"
    ValidateRequest="false" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postAccountNo%>
    <%=postTrnDiv %>
    <%=jsCardChange%>
    <script type="text/javascript">
        function MenubarOpen() {
            Gcoop.OpenDlg(610, 550, "w_dlg_dp_account_search.aspx", "");
        }

        function OnDwMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                NewAccountNo(v);
                return 0;
            }
            else if (c == "atmcard_id") {
                if (Gcoop.GetEl("HdAtmcard_ID").value == v) {
                    return;
                }
                Gcoop.GetEl("HdAtmcard_ID").value = v;
                s.SetItem(r, c, v);
                objDwMain.AcceptText();
                jsCardChange();
            }
        }

        function MenubarNew() {
            window.location = Gcoop.GetUrl() + "Applications/ap_deposit/w_sheet_dp_atmdept.aspx";
        }

        function NewAccountNo(accNo) {
            objDwMain.SetItem(1, "deptaccount_no", Gcoop.Trim(accNo));
            objDwMain.AcceptText();
            Gcoop.GetEl("HdDeptaccount_no").value = Gcoop.Trim(accNo);
            postAccountNo();
        }

        function OnDwTab1Click(s, r, c) {
            if (c == "datawindow")
                return 0;
            //alert(c != "datawindow");
            if (c == "entry_tdate") {
                alert("C  = " + objDwTab1.GetItem(r, c));
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกรายการ ATM เลขที่บัญชี = [" + objDwMain.GetItem(1, "deptaccount_no") + "] เลขที่บัญชี KTB = [" + objDwMain.GetItem(1, "atmcard_id") + "]");
        }

        function PostTrnDiv() {

            postTrnDiv();
        }
    </script>
    <style type="text/css">
        .tabTypeDefault
        {
            width: 100%;
            border-spacing: 2px;
        }
        .tabTypeTdDefault
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #777777;
            border: solid 1px #55A9CD;
            background-color: rgb(200,235,255);
            cursor: pointer;
        }
        .tabTypeTdSelected
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #660066;
            border: solid 1px #77CBEF;
            background-color: #76EFFF;
            cursor: pointer;
            text-decoration: underline;
        }
        .tabTypeTdDefault:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTypeTdSelected:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTableDetail
        {
            width: 99%;
        }
        .tabTableDetail td
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server" Text=""></asp:Literal>
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_dept_atm_master"
        LibraryList="~/DataWindow/atm_online/dp_atmdept.pbl" ClientEventItemChanged="OnDwMainItemChanged"
        ClientFormatting="True">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HSelect" runat="server" Value="01" />
    <asp:HiddenField ID="HdAtmcard_ID" runat="server" Value="" />
    <asp:HiddenField ID="HdDeptaccount_no" runat="server" Value="" />

</asp:Content>
