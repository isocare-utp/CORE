<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_divsrv_constant_ucfmethpay.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_constant_ucfmethpay" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postNewClear%>
<%=postRefresh%>
<script type ="text/javascript">
    function OnDwMainClick(s, r, c) {
        if (c == "showlist_flag") {
            Gcoop.CheckDw(s, r, c, "showlist_flag", 1, 0);
            postRefresh();
        }
    }

    function OnDwMainItemChange(s, r, c, v) {
        if (c == "methpaytype_sort") {
            objDw_main.SetItem(r, "methpaytype_sort", v);
            objDw_main.AcceptText();
        }
        return 0;
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

    <input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />

        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_divsrv_constant_ucfmethpay" 
                        LibraryList="~/DataWindow/divavg/divsrv_constant.pbl" 
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
