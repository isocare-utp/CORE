<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_edit_int.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_edit_int" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postPost%>
    <%=postNewAccount%>
    <%=postCalint%>
    <script type="text/javascript">

        function OnDwListCoopItemChanged(s, row, c, v) {

            if (c == "deptaccount_no") {
                s.SetItem(row, c, v);
                s.AcceptText();
                objDwMain.SetItem(row, c, v);
                objDwMain.AcceptText();
                Gcoop.GetEl("HdNewAccountNo").value = v;
                //     alert(Gcoop.GetEl("HdNewAccountNo").value);

                postNewAccount();

            }
        }

        function OnButtonClicked(sender, row, buttonname) {

            if (buttonname == "calint") {
                   postCalint();
           }
        }

        function Validate() {
            var newint = Gcoop.GetEl("Hdnewint").value;
            var oldint = Gcoop.GetEl("Hdoldint").value;
            var diffint = newint - oldint;
            objDwMain.AcceptText();
            objDwItem.AcceptText();
            return confirm("save แล้วนะ \nดอกเบี้ยเดิม = " + oldint + " ดอกเบี้ยใหม่ = " + newint + "\nnewintผลต่างดอกเบี้ย = " + diffintnum.toFixed(2));
        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div id="testDiv1">
    </div>
    <asp:Panel ID="Panel1" runat="server" TabIndex="1">
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dept_master"
            LibraryList="~/DataWindow/ap_deposit/dept_genintmasdue.pbl" ClientScriptable="True"
            ClientEventButtonClicked="OnButtonClicked" AutoSaveDataCacheAfterRetrieve="True"
            AutoRestoreDataCache="True" ClientEventItemChanged="OnDwListCoopItemChanged"
            ClientEventItemError="OnError" AutoRestoreContext="False" ClientFormatting="True">
        </dw:WebDataWindowControl>
        <dw:WebDataWindowControl ID="DwItem" runat="server" DataWindowObject="d_dp_dept_estint_fixed"
            LibraryList="~/DataWindow/ap_deposit/dept_genintmasdue.pbl" ClientEventButtonClicked="OnButtonClicked"
            ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True"
            ClientEventItemChanged="OnDwMainItemChanged" ClientEventItemError="OnError" AutoRestoreContext="False"
            ClientEventClicked="OnDwMainClick" ClientFormatting="True">
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="HdLastFocus" runat="server" />   
    <asp:HiddenField ID="HdNewAccountNo" runat="server" Value="false" />
    <asp:HiddenField ID="HdProcessId" runat="server" />
    <asp:HiddenField ID="HdAvpCode" runat="server" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="Hdas_apvdoc" runat="server" />
    <asp:HiddenField ID="hfSave" runat="server" />
    <asp:HiddenField ID="HfCoopid" runat="server" />
    <asp:HiddenField ID="Hdoldint" runat="server"  />
    <asp:HiddenField ID="Hdnewint" runat="server" />
</asp:Content>
