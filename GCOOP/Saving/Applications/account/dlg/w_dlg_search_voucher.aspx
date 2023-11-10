<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_search_voucher.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_search_voucher" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">
    
     function OnDwlistClick(s, row, n){
        if (n=="voucher_no" || n == "voucher_type"){
            var vc_no = objDw_list.GetItem(row, "voucher_no");
            var vc_type = objDw_list.GetItem(row, "voucher_type");
            var isConfirm = confirm("ต้องการเลือกรายการ Voucher เลขที่  " + vc_no + "  ใช่หรือไม่");
            if(isConfirm){
//            window.parent.W_dlg_Click(vc_no);
//            parent.RemoveIFrame();
            //parent.RemoveIFrame();
            window.opener.W_dlg_Click(vc_no);
            window.close();
            }
        }
        
    }
    
    
    </script>

    <style type="text/css">
        .style6
        {
            width: 537px;
        }
.objDw-mainEC{;background-color:transparent;OVERFLOW:hidden}
.objDw-mainED{;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
.objDw-mainF0{;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-mainF1{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-mainF4{;background-color:#e6e6e6;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-listF6{;background-color:#ffffff}
.objDw-listF7{;background-color:transparent;BORDER-STYLE:outset}
.objDw-listF9{;background-color:#d3e7ff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-list100{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-list102{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
    .objDw-listADB{;background-color:#ffffff}
.objDw-listADC{;background-color:transparent;BORDER-STYLE:outset}
.objDw-listADE{;background-color:#d3e7ff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-listAE5{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-listAE7{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        .style7
        {
            width: 656px;
            font-size: small;
        }
.objDw-list185D{;background-color:#ffffff}
.objDw-list185E{;background-color:transparent;BORDER-STYLE:outset}
.objDw-list1860{;background-color:#d3e7ff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-list1867{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-list1869{;background-color:#ffffff;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
        #form1
        {
            height: 418px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: Tahoma; font-size: small">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td class="style7">
                    <b>ค้นหาข้อมูล Voucher</b>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="39px" 
                        Width="655px">
                        <table style="width:97%;">
                            <tr>
                                <td class="style6">
                                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                                        ClientScriptable="True" DataWindowObject="d_vc_vcno_search" 
                                        LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" Style="top: 0px; left: 0px;
                        width: 495px">
                                    </dw:WebDataWindowControl>
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="B_search" runat="server" onclick="B_search_Click" 
                                        Text="ค้นข้อมูล" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <b>รายละเอียด Voucher</b></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <b>
                    <asp:Panel ID="Panel2" runat="server" Height="300px" BorderStyle="Ridge" 
                        Width="655px" >
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" 
                        DataWindowObject="d_vc_vcedit_vclist_search" 
                        LibraryList="~/DataWindow/account/vc_voucher_edit.pbl" RowsPerPage="10" 
                        ClientEventClicked="OnDwlistClick" style="top: 0px; left: 0px; height: 90px">
                            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                            </PageNavigationBarSettings>
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                    </b>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:HiddenField ID="HSqlTemp" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
