<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_procdeptuptran.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_procdeptuptran" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=PostCutProcess%>  
    <%=PostRetriveDepttrans%>  
    <%=jsSetMemberFormat%>
    <%=jsResetMemberno%>

    <script type="text/javascript">
        function CloseFinsh() {
            Gcoop.RemoveIFrame();
            //window.location = Gcoop.GetUrl() + "flash/index.aspx?cmd=logout";
            //http://localhost/GCOOP/Saving/Exit.aspx
            window.location = Gcoop.GetUrl() + "Exit.aspx";
        }

        function OnDwMainButtonClick(sender, row, bName) {
            if (bName == "btn_process") {
                var isConfirm = confirm("ยืนยันการผ่านรายการ");
                if (isConfirm) {
                    sender.AcceptText()
                    if (objDw_Main.GetItem(1, "account_id") == null) {
                        alert("กรุณาเลือกคู่บัญชี"); return;
                    } else {
                        PostCutProcess();
                    }    
                }
            }
            if (bName == "btn_reteieve") {
                sender.AcceptText()
                PostRetriveDepttrans();
            }
            
            return 0;
        }

        function OnDwMainOnChange(s, r, c, v) {
            s.SetItem(r, c, v);
            s.AcceptText();
            if (c == "member_flag") {
                if (v == 1) {
                    jsSetMemberFormat();
                }
                else if (v == 0) {
                    jsResetMemberno();
                }
            }
            else if (c == "member_no") {
                jsSetMemberFormat();
            }
        }

        function SheetLoadComplete() {
            //  alet(Gcoop.GetEl("HdMountCut").value);
            //if (Gcoop.GetEl("HdMountCut").value == "true") {
                //Gcoop.OpenIFrame(450, 200, "w_dlg_dp_closeday.aspx", "");
              //  Gcoop.OpenProgressBar("ประมวลผลตัดยอดเข้าฝาก", true, true, PostRetriveDepttrans);
            //}
            //else if (Gcoop.GetEl("HdSkipError").value == "true") {
             //   confirm("มีข้อผิดพลาดในการโอนเงินกู้เข้าฝาก ต้องการข้ามไปทำรายการถัดไปหรือไม่")
            //}
        }

        function Validate() {
            alert("หน้าจอผ่านรายการ ไม่มีคำสั่งเซฟ");
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
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" ClientEventButtonClicked ="OnDwMainButtonClick" 
                    ClientEventItemChanged="OnDwMainOnChange" ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
              <td>
              <dw:WebDataWindowControl ID="Dw_Detail" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" DataWindowObject="d_dp_deptmountcut_depttrans_detail"
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" RowsPerPage="20" 
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
    <asp:HiddenField ID="HdSkipError" runat="server" />
    <%=outputProcess%>
</asp:Content>
