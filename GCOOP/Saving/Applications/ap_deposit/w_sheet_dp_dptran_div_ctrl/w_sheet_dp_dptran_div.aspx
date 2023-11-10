<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_dptran_div.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_dptran_div_ctrl.w_sheet_dp_dptran_div" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        dsMain = DataSourceTool();
        dsList = DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function SheetLoadComplete() {
        }
        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "tran_year") {
                PostInitDiv();
            } else if (c == "member_no") {
                PostMember();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsMainClicked(s, r, c) {

            if (c == "btn_memsreach") {
                PostMember();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsListClicked(s, r, c) {
            if (r >= 0) {
                if (c == "member_no" || c == "deptaccount_no") {
                    dsList.SetRowFocus(r);
                    PostDetail();
                }
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <hr align="left" style="width: 96%;" />
    <br />
    <table height="450">
        <tr>
            <td valign="top" width="317">
                <p>
                    สมาชิก</p>
            </td>
            <td valign="top" width="3%">
            </td>
            <td valign="top">
                <p>
                    รายละเอียด</p>
            </td>
        </tr>
        <tr>
            <td valign="top" width="317">
                <uc2:DsList ID="dsList" runat="server" />
            </td>
            <td valign="top" width="3%">
            </td>
            <td valign="top">
                <uc3:DsDetail ID="dsDetail" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
