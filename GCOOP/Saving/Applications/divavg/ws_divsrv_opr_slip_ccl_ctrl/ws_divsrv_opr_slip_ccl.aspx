<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_divsrv_opr_slip_ccl.aspx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_opr_slip_ccl_ctrl.ws_divsrv_opr_slip_ccl" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'w_dlg_divsrv_member_search.aspx', '')
            }
        }

        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            PostMemberNo();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }

        function SheetLoadComplete() {
            var ls_member_status = dsMain.GetItem(0, "resign_status");
            if (ls_member_status == 0) {
                $('#text_resign_status').hide();
                $('#text_resign_date').hide();
                $('#text_free').show();
               
                dsMain.GetElement(0, "resign_status").style.display = "none";
                dsMain.GetElement(0, "resign_date").style.display = "none";

            } else {
                $('#text_resign_status').show();
                $('#text_resign_date').show();
                $('#text_free').hide();
              
                dsMain.GetElement(0, "resign_status").style.display = "block";
                dsMain.GetElement(0, "resign_date").style.display = "block";
            }

            dsMain.GetElement(0, "member_status").disabled = true;
            dsMain.GetElement(0, "resign_status").disabled = true;
        }             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="2" width="100%">
                <uc1:DsMain ID="dsMain" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td width="40%">
                <uc2:DsList ID="dsList" runat="server" />
            </td>
            <td width="60%">
                <uc3:DsDetail ID="dsDetail" runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hddiv_year" runat="server" />
</asp:Content>
