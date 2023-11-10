<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_search_accmaster.aspx.cs" Inherits="Saving.Applications.account.dlg.w_dlg_search_accmaster" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

.objDw-detail1E8{;background-color:transparent;OVERFLOW:hidden}
.objDw-detail1E9{;background-color:transparent;OVERFLOW:hidden;COLOR:#000000;FONT:12pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;WORD-BREAK:break-all;BORDER-STYLE:none}
.objDw-detail1EA{;background-color:transparent;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:center;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-detail1EB{;COLOR:#000000;FONT:12pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:bold;TEXT-DECORATION:none;TEXT-ALIGN:center}
.objDw-detail1EC{;background-color:#d3e7ff;OVERFLOW:hidden;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:right;WORD-BREAK:break-all;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
.objDw-detail1EE{;background-color:transparent;COLOR:#000000;FONT:10pt "Tahoma", sans-serif;FONT-STYLE:normal;FONT-WEIGHT:normal;TEXT-DECORATION:none;TEXT-ALIGN:left;BORDER-STYLE:solid;BORDER-WIDTH:1px;BORDER-COLOR:#000000}
    </style>
</head>
<%=postFindAccmaster%>
<script type ="text/javascript" >
    function OnDwFindClickButton(s,r,b)
    {
        if(b=="b_search")
        {
            postFindAccmaster();
        }
        return 0;
    }
    
//     function OnDwlistClick(s, row, n){
//        if (n=="voucher_no" || n == "voucher_type"){
//            var vc_no = objDw_list.GetItem(row, "voucher_no");
//            var vc_type = objDw_list.GetItem(row, "voucher_type");
//            var isConfirm = confirm("ต้องการเลือกรายการ Voucher เลขที่  " + vc_no + "  ใช่หรือไม่");
//            if(isConfirm){
////            window.parent.W_dlg_Click(vc_no);
////            parent.RemoveIFrame();
//            //parent.RemoveIFrame();
//            window.opener.W_dlg_Click(vc_no);
//            window.close();
//            }
//        }
//        
//    }
    
    function OnDwListClick(s,r,c)
    {
        if(c == "account_id" || c == "account_name")
        {
            var acc_id = objDw_detail.GetItem(r,"account_id");
            var isConfirm = confirm("ต้องการเลือกรหัสบัญชี : " + acc_id + " ใช่หรือไม่ ?");
            if (isConfirm) {
                parent.OnFindShow(acc_id);
                parent.RemoveIFrame();
//                window.opener.OnFindShow(acc_id);
//                window.close();
            }
        }
        return 0;
    }
</script> 
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%; font-size: small;">
            <tr>
                <td>
                    ค้นหาข้อมูล</td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Height="35px" BorderStyle="Ridge" 
                        Width="545px">
                        <dw:WebDataWindowControl ID="Dw_find" runat="server" 
    AutoRestoreContext="False" AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
    ClientScriptable="True" DataWindowObject="d_accmaster_find" 
    LibraryList="~/DataWindow/account/accmaster.pbl" 
    style="top: 0px; left: 0px" ClientEventButtonClicked="OnDwFindClickButton"></dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    รายละเอียด</td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Height="334px" BorderStyle="Ridge" 
                        ScrollBars="Vertical" Width="545px">
                        <asp:Panel ID="Panel3" runat="server" Height="101px">
                            <dw:WebDataWindowControl ID="Dw_detail" runat="server" 
                                AutoRestoreContext="False" AutoRestoreDataCache="True" 
                                AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                                ClientScriptable="True" DataWindowObject="d_accmaster_list" 
                                LibraryList="~/DataWindow/account/accmaster.pbl" 
                                ClientEventClicked="OnDwListClick">
                            </dw:WebDataWindowControl>
                        </asp:Panel>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HSqlTemp" runat="server" />
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
