<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_vc_voucher_edit_detail.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_vc_voucher_edit_detail" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>แก้ไขรายละเอียด Voucher</title>
    <style type="text/css">
        #B_ok
        {
            width: 70px;
        }
        .objDw-main3C86
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-main3C89
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3C8A
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3C8D
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-main3C92
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-search3CAA
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-search3CAB
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-search3CAD
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-search3CAF
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-search3CB0
        { ;background-color:transparent;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-search3CB1
        { ;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center}
        .objDw-accid3CB3
        { ;background-color:#ffffff}
        .objDw-accid3CB4
        { ;background-color:transparent;BORDER-STYLE:outset}
        .objDw-accid3CB6
        { ;background-color:#d3e7ff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:none}
        .objDw-accid3CBA
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:none}
        .objDw-accid3CBB
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:none}
        .objDw-detail3C6B
        { ;background-color:transparent;OVERFLOW:hidden}
        .objDw-detail3C6C
        { ;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C71
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-detail3C76
        { ;background-color:#e6e6e6;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C77
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C78
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C7A
        { ;background-color:#ffffff;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C7B
        { ;background-color:#d4d0c8;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C7C
        { ;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center}
        .objDw-detail3C7D
        { ;background-color:#e6e6e6;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C80
        { ;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:none}
        .objDw-detail3C81
        { ;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .objDw-detail3C83
        { ;background-color:#ffffff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt"Tahoma",sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:none}
        #B_add
        {
            width: 100px;
        }
        #B_insert
        {
            width: 100px;
        }
        #B_save
        {
            width: 100px;
        }
        #B_cancel
        {
            width: 100px;
        }
        *
        {
            padding: 0;
            margin: 0 0 0 2px;
        }
        .linkSpan
        {
            float: right;
            cursor: pointer;
        }
        .style9
        {
            width: 391px;
        }
        .style10
        {
            width: 214px;
        }
    </style>
</head>
<%=postInsertDwDetail%>
<%=postVoucherEdit%>
<%=postAddnewUpdateVoucher%>
<%=postDeleteRowDetail %>
<%=postRefresh%>


<script type="text/javascript">
    //ฟังก์ชั่นการเพิ่มแถว
    function Insert_Dw_detail() {
        postInsertDwDetail();
    }

    //ฟังก์ชั่นการลบแถว
    function OnDwListClick(s, r, c) {
        if (c == "b_del") {
            if (confirm("ยืนยันการลบแถวข้อมูล")) {
                var row = r;
                Gcoop.GetEl("HdDetailRow").value = row + "";
                postDeleteRowDetail();
            }
        }
    }

    //ฟังก์ชันในการกดปุ่ม ตกลง
    function OnOkClick() {
        if (confirm("ยืนยันการบันทึกข้อมูล ")) {
            postAddnewUpdateVoucher();
        }
    }

    //ฟังก์ชันในการปิด dialog
    function OnCloseDialog() {
        if (confirm("ยืนยันการออกจากหน้าจอ ")) {
            window.close();
        }
    }



</script>
<body>
    <form id="form1" runat="server">
    <div style="font-size: small; font-family: Tahoma">
        <asp:Literal ID="LtVoucerMessage" runat="server"></asp:Literal>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
           
           
            <tr>
                <td colspan="3" valign="top">
                    <asp:Panel ID="Panel6" runat="server" BorderStyle="Ridge" Height="53px" Width="900px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_vc_vcupdate_main_detail"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" Style="top: 0px; left: 0px; width: 715px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                </tr>
            <tr>
                 <td colspan="3" valign="top">
                    <asp:Panel ID="Panel9" runat="server" BorderStyle="Ridge" Height="315px" ScrollBars="Vertical"
                        Width="900px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_vc_vcupdate_detail_det"
                            LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" Style="top: 0px; left: 0px; height: 75px;width: 728px" ClientEventClicked="OnDwListClick" 
                            ClientEventItemChanged="Dw_detailItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
  </tr>
  <tr>

                <td valign="top" class="style9">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <input id="B_add" type="button" value="เพิ่มแถว" onclick="Insert_Dw_detail()" />&nbsp;
                     <%--<input id="B_insert" type="button" value="แทรกแถว" onclick="InsertAfter_Dw_detail()" />--%>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td valign="top">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="B_save" type="button" value="ตกลง" onclick="OnOkClick()" />
                    <input id="B_cancel" type="button" value="ยกเลิก" onclick="OnCloseDialog()" />
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:HiddenField ID="HdAccname" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdRefresh" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdCurrentrow" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HdAccid" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HSqlTemp" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="Hd_accid" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="HdDetailRow" runat="server" />
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
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>

