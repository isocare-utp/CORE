<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_npl_reportmaster_sortyear.aspx.cs" Inherits="Saving.Criteria.u_cri_npl_reportmaster_sortyear" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=runProcess%>
    <%=JsPostRangeType%>
    <%=JsPostPost%>
    <script type="text/javascript">

        function OnClickLinkNext() {
            objdw_criteria.AcceptText();
            runProcess();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
            }
        }

        function OnDwItemChanged(s, r, c, v) {
            if (r > 0 && c != "datawindow") {
                if (c == "tyear") {
                    s.SetItem(r, "month", null);
                    s.SetItem(r, "seq_no", null);
                } else if (c == "month") {
                    s.SetItem(r, "seq_no", null);
                }
                s.SetItem(r, c, v);
                s.AcceptText();
                JsPostPost();
            }
        }

        function OnDwLawtypeItemChanged(s, r, c, v) {
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
                <table width="400">
                    <tr>
                        <td width="50%" valign="top" align="center">
                            <dw:WebDataWindowControl ID="dw_lawtype" runat="server" AutoRestoreContext="False"
                                AutoRestoreDataCache="True" ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_cri_npl_sortyear"
                                LibraryList="~/DataWindow/criteria/criteria.pbl" AutoSaveDataCacheAfterRetrieve="True"
                                ClientEventItemChanged="OnDwLawtypeItemChanged">
                            </dw:WebDataWindowControl>
                        </td>
                        <td width="50%" valign="top" align="center">
                            <dw:WebDataWindowControl ID="dw_criteria" runat="server" AutoRestoreContext="False"
                                AutoRestoreDataCache="True" ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_cri_npl_reportmaster_follow"
                                LibraryList="~/DataWindow/criteria/criteria.pbl" AutoSaveDataCacheAfterRetrieve="True"
                                ClientEventItemChanged="OnDwItemChanged">
                            </dw:WebDataWindowControl>
                        </td>
                    </tr>
                </table>
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
                            <span style="cursor: pointer" onclick="OnClickLinkNext();">ออกรายงาน &gt;</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <%=outputProcess%>
</asp:Content>
