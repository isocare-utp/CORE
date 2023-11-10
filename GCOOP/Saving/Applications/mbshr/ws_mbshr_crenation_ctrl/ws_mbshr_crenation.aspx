<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mbshr_crenation.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_crenation_ctrl.ws_mbshr_crenation" %>
<%@ Register Src="DsFund.ascx" TagName="DsFund" TagPrefix="uc2" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register src="DsInsurance.ascx" tagname="DsInsurance" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsFund = new DataSourceTool;
        var dsInsurance = new DataSourceTool;


        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMember();
            }
        }
        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2("630", "610", "w_dlg_mbshr_mem_search.aspx", "");
                
            }
        }
        function GetMembNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }
        
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }


        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno);
            PostMember();
        }
        function OnDsInsuranceClicked(s, r, c) {
            if (c == "b_del") {
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }
            }
        }       
        function OnClickInsertRow() {
            PostInsertRow();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br/>
    <%--<uc2:DsFund ID="dsFund" runat="server" />--%>
    <span onclick="OnClickInsertRow()" style="float:right;margin-right:5%;cursor: pointer;">
      <asp:Label ID="LbInsert2" runat="server" Text="เพิ่มแถว" Font-Bold="False" Font-Names="Tahoma"
            Font-Size="14px" Font-Underline="True" ForeColor="#006600" />
    </span>
    <uc3:DsInsurance ID="dsInsurance" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content>
