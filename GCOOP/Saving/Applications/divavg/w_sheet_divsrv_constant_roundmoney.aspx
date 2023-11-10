<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_constant_roundmoney.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_dirsrv_constant_roundmoney" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postInsertRow%>
<%=postDeleteRow%>
<%=postNewClear %>
<%=postRefresh%>
<script type ="text/javascript">
    function OnDwMainItemChange(s, r, c, v) {
        if (c == "function_code") {
            objDw_main.SetItem(r, "function_code", v);
            objDw_main.AcceptText();
            postRefresh();
        }
        else if (c == "use_flag") {
            Gcoop.CheckDw(s, r, c, "use_flag", 1, 0);
            objDw_main.SetItem(r, "use_flag", v);
            objDw_main.AcceptText();
            postRefresh();
        }
        return 0;
    }
    function MenubarNew() {
        postNewClear();
    }

    // ฟังก์ชันการเพิ่มแถวข้อมูล
    function AddRow_DwMain() {
        postInsertRow();
    }

    // ฟังก์ชันการลบข้อมูล
    function DeleteRow_DwMain(sender, row, bName) {
        if (bName == "b_del") {
            var function_code = objDw_main.GetItem(row, "function_code");

            if (function_code == "" || function_code == null) {
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow();
            } else {
                var isConfirm = confirm("ต้องการลบข้อมูลประเภท : " + function_code + " ใช่หรือไม่ ?");
                if (isConfirm) {
                    Gcoop.GetEl("Hd_row").value = row + "";
                    postDeleteRow();
                }
            }
        }
        return 0;
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

    <input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />

        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_cm_constant_roundmoney" 
                        LibraryList="~/DataWindow/divavg/divsrv_constant.pbl" 
                        ClientEventButtonClicked="DeleteRow_DwMain" 
                        ClientEventItemChanged="OnDwMainItemChange">
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
