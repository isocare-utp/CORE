<%@ Page Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_coop_id_firstperiodloan.aspx.cs"
    Inherits="Saving.Criteria.u_cri_coop_id_firstperiodloan" Title="Report Criteria" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=runProcess%>
    <%=popupReport%>
    <%=retrievedata %>
    <%=check_date %>
    <%=check_money %>
    <%=check_id %>
    <%=check_all%>
    <script type="text/javascript">
		    function OnClickLinkNext(){
		        objdw_main.AcceptText();
		        objdw_nec.AcceptText();
		        runProcess();
		    }
		    function SheetLoadComplete(){
		        if( Gcoop.GetEl("HdOpenIFrame").value == "True" ){
		            Gcoop.OpenIFrame("220","200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>","");
		        }
		    }


		    function DwEvClicked(sender, rowNumber, objectName) {

		        Gcoop.CheckDw(sender, rowNumber, objectName, "chk_date", 1, 0);
		        Gcoop.CheckDw(sender, rowNumber, objectName, "chk_money", 1, 0);
		        Gcoop.CheckDw(sender, rowNumber, objectName, "chk_id", 1, 0);
		        if (objectName == "chk_date") {

		            check_date();
		        }
		        else if (objectName == "chk_money") {

		            check_money();
		        }
		        else if (objectName == "chk_id") {

		            check_id();
		        }
		        else if (objectName == "chk_all") {

		            check_all();
		        }

		    }


		    function DwBtClicked(sender, rowNumber, objectName) {
		        if (objectName == "check_doc") {
		          
		            
		            retrievedata();
                }
		        return 0;

            }
//		    function DwEvItemChange(sender, rowNumber, columnName, newValue) {
//		        if (columnName == "req_tdate") {
//		            objdw_criteria.SetItem(rowNumber, columnName, newValue);
//		            objdw_criteria.AcceptText();
//		            retrievedata();
//		        }
//		        return 0;

//                
//		    }

		   
    </script>
    <style type="text/css">
        .style1
        {
            width: 571px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="text-align: center; width: 746px;">
        <tr>
            <td align="center" class="style1">
                <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
                    Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
                    Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" class="style1">
                <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_cri_list_date_main"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="DwEvItemChange" ClientEventClicked="DwEvClicked" ClientEventButtonClicked="DwBtClicked">
                </dw:WebDataWindowControl>
                <br />
                <dw:WebDataWindowControl ID="dw_nec" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" ClientScriptable="True" DataWindowObject="d_cri_coopid_firstperiodloan"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="DwEvItemChange">
                </dw:WebDataWindowControl>
               
            </td>
        </tr>
        <tr>
            <td align="center" class="style1">
                <table style="width: 93%;">
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
                            <%--<asp:LinkButton ID="LinkNext" runat="server" onclick="LinkNext_Click">ออกรายงาน &gt;</asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
</asp:Content>
