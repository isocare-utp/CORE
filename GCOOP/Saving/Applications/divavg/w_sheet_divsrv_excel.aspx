<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_divsrv_excel.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_excel" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>    
    
    <script type="text/javascript">
      

        function MenubarNew() {
            postNewClear();
        }


        //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <br />
        <table style="width: 100%;">
            <tr>
                <td valign="top">
                        <dw:WebDataWindowControl ID="Dw_choice" runat="server" 
        AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" 
        ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_divsrv_reportid" LibraryList="~/DataWindow/divavg/divsrv_reportexcel.pbl"
                          >
                        </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
                        <dw:WebDataWindowControl ID="Dw_option" runat="server" 
        AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" 
        ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_divsrv_cri_excel_bank" LibraryList="~/DataWindow/divavg/divsrv_reportexcel.pbl"
                          >
                        </dw:WebDataWindowControl>
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Button ID="B_report" runat="server" Text="แสดงข้อมูล" 
                        UseSubmitBehavior="False" onclick="B_report_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;<asp:Button ID="B_excel" runat="server" Text="ออกไฟล์ Excel" 
                        UseSubmitBehavior="False" onclick="B_excel_Click" />
&nbsp;</td>
            </tr>
            <tr>
                <td>
                   <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto">
                       <dw:WebDataWindowControl ID="Dw_report" runat="server" 
        AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" 
        ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="r_divsrv_tobank_excel" 
    LibraryList="~/DataWindow/divavg/divsrv_reportexcel.pbl"
                          >
                       </dw:WebDataWindowControl>
                    </asp:Panel>
                    &nbsp;
                    &nbsp;
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </p>
</asp:Content>


