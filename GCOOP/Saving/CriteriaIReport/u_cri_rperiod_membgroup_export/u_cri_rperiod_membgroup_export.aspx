<%@ Page Language="C#" MasterPageFile="~/Report.Master"  AutoEventWireup="true" CodeBehind="u_cri_rperiod_membgroup_export.aspx.cs"
    Inherits="Saving.CriteriaIReport.u_cri_rperiod_membgroup_export" Title="Report Criteria" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
		<script type="text/javascript">
		    function ShowReport() {
		       // postShowReport();
            }
		    function OnClickLinkNext(){
		        objdw_criteria.AcceptText();
		       // runProcess();
		    }
		    function SheetLoadComplete(){
		       
		    }
		    
		    function OnDwItemChanged( sender, row, column, value){
		        if (column == "start_membgroup" || column == "end_membgroup") {
		            objdw_criteria.SetItem(row, column, value);
		            objdw_criteria.AcceptText();
		            post();
		        } else if (column == "start_membgroup_1" ) {
		            objdw_criteria.SetItem(row, "start_membgroup", value);
		            objdw_criteria.AcceptText();
		            post();
		        } else if (  column == "end_membgroup_1") {
		            objdw_criteria.SetItem(row, "end_membgroup", value);
		            objdw_criteria.AcceptText();
		            post();
		        }
		        else if (column == "year") {
		            objdw_criteria.SetItem(row, column, value);
		            objdw_criteria.AcceptText();
		        }
		        else if (column == "month") {
		            objdw_criteria.SetItem(row, column, value);
		            objdw_criteria.AcceptText();
                }
		    }

		    function OnDwClick(s, r, c) {
		        if (c == "report_choice") {
		            Gcoop.CheckDw(s, r, c, "report_choice", 1, 0);
                }
            }
		</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="text-align:center">
        <tr>
            <td align=center>
                <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
                    Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                    Font-Size="Large" Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="width:500px">
                <dw:WebDataWindowControl ID="dw_criteria" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" 
                    ClientScriptable="True" DataWindowObject="d_cri_rperiod_membgroup_prc"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="OnDwItemChanged" ClientEventClicked="OnDwClick">
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
                        <td align=left style="width:100px;">
                            <asp:LinkButton ID="LinkBack" runat="server">&lt; ย้อนกลับ</asp:LinkButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align=left style="width:100px;">
                               <span style="cursor: pointer" onclick="OnClickLinkNext();"> ออกรายงาน &gt;</span>
                            <%--<asp:LinkButton ID="LinkNext" runat="server" onclick="LinkNext_Click">ออกรายงาน &gt;</asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" value="False"/>
    <asp:HiddenField ID="HdRunProcess" runat="server" value="False"/>
</asp:Content>
