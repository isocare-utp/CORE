<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_constant_ucfdfmethpay.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_constant_ucfdfmethpay" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postDeleteRow %>
    <%=postAddRow %>
    <script type="text/javascript">
        function AddRow() {
            postAddRow();
        }

        function OnDwButtonClick(s, r, b) {
            if (b == "b_del") {
                if (confirm = "ต้องการลบแถวข้อมูลที่ " + r + " ใช่หรือไม่") {
                    Gcoop.GetEl("Hd_row").value = r + "";
                    postDeleteRow();
                }
            }
        }



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
        <span class="linkSpan" onclick="AddRow()" style="font-family: Tahoma; font-size: small;
            float: left; color: #3333CC;">เพิ่มแถว </span>

    <input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />

        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_constant_ucfdfmethpay" LibraryList="~/DataWindow/divavg/divsrv_constant.pbl"
                        ClientEventButtonClicked="OnDwButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
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
        </table>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="Hd_row" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
