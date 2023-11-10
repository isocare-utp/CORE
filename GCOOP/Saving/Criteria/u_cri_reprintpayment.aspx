<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_reprintpayment.aspx.cs" Inherits="Saving.Criteria.u_cri_reprintpayment" %>
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
        function OnClickSubmit() {
            objdw_main.AcceptText();
            objdw_nec.AcceptText();
            runProcess();
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
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


        function DwBtnClicked(sender, rowNumber, objectName) {
            if (objectName == "btnretive") {
                objdw_main.AcceptText();
                retrievedata();
            }
            return 0;

        }   
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
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" >
                <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" 
                    ClientScriptable="True" DataWindowObject="d_sl_reprint_criteria"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" 
                    AutoSaveDataCacheAfterRetrieve="True" 
                    ClientEventButtonClicked="DwBtnClicked">
                </dw:WebDataWindowControl>
                <br />
                <asp:Panel ID="Panel1" runat="server" ScrollBars= "Horizontal" Width = "650px">
                <dw:WebDataWindowControl ID="dw_nec" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" ClientFormatting="True" 
                    ClientScriptable="True" DataWindowObject="d_sl_slip_reprint"
                    LibraryList="~/DataWindow/criteria/criteria.pbl" 
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="DwEvItemChange">
                </dw:WebDataWindowControl>
               </asp:Panel>
            </td>
        </tr>
        <tr>
        <td align="center" >
            <input id="Submit1" type="submit" value="ส่งข้อมูลสู่กาารพิมพ์" onclick = "OnClickSubmit();" 
                size="3cm" style="background-color: #99CCFF" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
</asp:Content>
