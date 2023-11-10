<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_div_coopid_year_rmemno_rdate_rate.aspx.cs" Inherits="Saving.Criteria.u_cri_div_coopid_year_rmemno_rdate_rate" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=runProcess%>
    <%=popupReport%>
    <script type="text/javascript">
        function OnItemChange(s, r, c, v) {
            if (c == "year") {
                objdw_criteria.SetItem(1, "year", v);
                objdw_criteria.AcceptText();
                Gcoop.GetEl("Hddiv_year").value = v;
            }
            else if (c == "rate1") {
                objdw_criteria.SetItem(1, "rate1", v);
                objdw_criteria.AcceptText();
                Gcoop.GetEl("Hdrate1").value = v;
            }
            else if (c == "rate2") {
                objdw_criteria.SetItem(1, "rate2", v);
                objdw_criteria.AcceptText();
                Gcoop.GetEl("Hdrate2").value = v;
            }
            else if (c == "rate3") {
                objdw_criteria.SetItem(1, "rate3", v);
                objdw_criteria.AcceptText();
                Gcoop.GetEl("Hdrate3").value = v;
            }
            else if (c == "rate4") {
                objdw_criteria.SetItem(1, "rate4", v);
                objdw_criteria.AcceptText();
                Gcoop.GetEl("Hdrate4").value = v;
            }
            else if (c == "rate5") {
                objdw_criteria.SetItem(1, "rate5", v);
                objdw_criteria.AcceptText();
                Gcoop.GetEl("Hdrate5").value = v;
            }
        }

        function OnClickLinkNext() {
            objdw_criteria.AcceptText();
            runProcess();
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="text-align: center">
        <tr>
            <td align="center">
                <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
                    Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
                    Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 500px">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_cri_div_coopid_year_rmemno_rdate_rate"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventItemChanged="OnItemChange">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:LinkButton ID="LinkBack" runat="server">&lt; ย้อนกลับ</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" style="width: 100px;">
                            <span style="cursor: pointer" onclick="OnClickLinkNext();">ออกรายงาน&gt;</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="Hddiv_year" runat="server" Value="False" />
    <asp:HiddenField ID="Hdrate1" runat="server" value="False"/>
    <asp:HiddenField ID="Hdrate2" runat="server" value="False"/>
    <asp:HiddenField ID="Hdrate3" runat="server" value="False"/>
    <asp:HiddenField ID="Hdrate4" runat="server" value="False"/>
    <asp:HiddenField ID="Hdrate5" runat="server" value="False"/>
    <%=outputProcess%>
</asp:Content>
