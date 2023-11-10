<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_processsltrnpayin.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_processsltrnpayin" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=callGendisk%>
    <script type="text/javascript">
        function OnItemChange(s, r, c, v) {
            if (c == "coop_id") {
                objdw_cri.SetItem(1, "coop_id", v);
                objdw_cri.AcceptText();
            }
        }
        function OnButtonClick(s, r, c) {
            if (c == "b_accept") {
                callGendisk();
            }
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdRunProcess").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลเรียกเก็บ", true, true, getLineText);
            }
            var saveFlag = Gcoop.GetEl("HdSavedisk").value;
            if (saveFlag == "true") {
                document.getElementById("spanSave").style.visibility = "visible";
            } else {
                document.getElementById("spanSave").style.visibility = "hidden";
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="dw_cri" runat="server" DataWindowObject="d_cutloan_sltrnpayin"
                    LibraryList="~/DataWindow/shrlon/sl_cutloan_sltrnpayin.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientFormatting="True" ClientEventButtonClicked="OnButtonClick" ClientEventItemChanged="OnItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdRunProcess" runat="server" />
    <asp:HiddenField ID="HdSavedisk" runat="server" />
    <asp:HiddenField ID="HdReturnText" runat="server" />
</asp:Content>
