<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_divsrv_detail_day.aspx.cs" 
Inherits="Saving.Applications.divavg.ws_divsrv_detail_day_ctrl.ws_divsrv_detail_day" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsMaster.ascx" TagName="DsMaster" TagPrefix="uc2" %>
<%@ Register Src="DsMethodpayment.ascx" TagName="DsMethodpayment" TagPrefix="uc3" %>
<%@ Register Src="DsStatement.ascx" TagName="DsStatement" TagPrefix="uc4" %>
<%@ Register Src="DsShrday.ascx" TagName="DsShrday" TagPrefix="uc5" %>
<%@ Register Src="DsLoan.ascx" TagName="DsLoan" TagPrefix="uc7" %>
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
            } else if (c == "div_year") {
                PostDivYear();
            }
        }

        function SheetLoadComplete() {
            $('.DataSourceRepeater').find('input,select,button').attr('readOnly', true);
            //            $('.ctl00_ContentPlace_dsLoan_Panel2').find('input,select,button').attr('disabled', true); 
        }             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsMaster ID="dsMaster" runat="server" />
    <uc3:DsMethodpayment ID="dsMethodpayment" runat="server" />
    <br />
    <uc4:DsStatement ID="dsStatement" runat="server" />
    <br />
    <uc5:DsShrday ID="dsShrday" runat="server" />
    <br />
    <uc7:DsLoan ID="dsLoan" runat="server" />
    <br />
</asp:Content>
