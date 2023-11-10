<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_procdepttrancut_loan.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_procdepttrancut_loan" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=PostCutProcess%>  
    <%=PostRetriveDepttrans%>  

    <script type="text/javascript">
        function CloseFinsh() {
            Gcoop.RemoveIFrame();
            //window.location = Gcoop.GetUrl() + "flash/index.aspx?cmd=logout";
            //http://localhost/GCOOP/Saving/Exit.aspx
            window.location = Gcoop.GetUrl() + "Exit.aspx";
        }
        function OnDwMainItemChanged(sender, row, col, val) {
            if (col == "system_code") {
                sender.AcceptText();
                sender.SetItem(row, col, val);
                PostRetriveDepttrans();
            }
          
            return 0;
        }
        function OnDwMainButtonClick(sender, row, bName) {
            if (bName == "btn_process") {
                var isConfirm = confirm("ยืนยันการโอนชำระหนี้");
                if (isConfirm) {
                    sender.AcceptText();
                    PostCutProcess();
                }
            } else if (bName == "b_retrieve") {
            alert("11");
                sender.AcceptText();    
                PostRetriveDepttrans();
           
            
             }
           
            return 0;
        }


        function SheetLoadComplete() {
            //  alet(Gcoop.GetEl("HdMountCut").value);
            if (Gcoop.GetEl("HdMountCut").value == "true") {
                //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
                Gcoop.OpenProgressBar("ประมวลผลตัดยอดเข้าฝาก", true, true, PostRetriveDepttrans);
            }
        }

        function Validate() {
            alert("หน้าจอประมวลผลตัดยอดเข้าฝาก ไม่มีคำสั่งเซฟ");
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="Dw_Main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_deptmountcut_depttrans_main"
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans_loan.pbl" ClientEventButtonClicked ="OnDwMainButtonClick"
                    ClientEventItemChanged="OnDwMainItemChanged" 
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
              <td>
              <dw:WebDataWindowControl ID="Dw_Detail" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dep_tran_cut_lon"
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans_loan.pbl" RowsPerPage="20" 
                    ClientScriptable="True" ClientFormatting="True">
                     <PageNavigationBarSettings Position="Bottom" Visible="True" NavigatorType="Numeric">
                        <BarStyle HorizontalAlign="Center" />
                        <NumericNavigator FirstLastVisible="True" />
                     </PageNavigationBarSettings>
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label> 
              
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdMountCut" runat="server" />
    <asp:HiddenField ID="HdGroupStatus" runat="server" />
</asp:Content>
