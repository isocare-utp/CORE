<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_dpgetpb_lastpageline.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_dlg_dp_dpgetpb_lastpageline" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ระบบเงินฝาก</title>

    <script type="text/javascript">
    function OnDetailClick(s, row, n){
        var dept_no = objdw_detail.GetItem(row, "deptaccount_no");
        var acc = objdw_detail.GetItem(row, "deptaccount_name");
        //var isConfirm = confirm("ต้องการเลือกบัญชี " + dept_no + ": " + acc + "  ใช่หรือไม่");
        window.opener.NewAccountNo(dept_no);
        window.close();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table>
            <tr>
                <td valign="top" width="550">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_dppbk_prompt_print"
                        LibraryList="~/DataWindow/ap_deposit/dp_deptedit.pbl" Width="530px" HorizontalScrollBar="NoneAndClip"
                        VerticalScrollBar="NoneAndClip" UseCurrentCulture="True" AutoRestoreContext="False"
                        AutoSaveDataCacheAfterRetrieve="True" style="top: 0px; left: 0px">
                    </dw:WebDataWindowControl>
                </td>

            </tr>
                        <tr>
                <td valign="top" width="550">

                </td>
                <td valign="top">
                    
                </td>
            </tr>
                        <tr>
                <td valign="top" width="550">

                    <input id="change" type="button" value="เปลี่ยนค่าการพิมพ์" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="ok" type="button" value="ตกลง " />&nbsp;
                    <input id="cancel" type="button" value="ยกเลิก" /></td>
  
            </tr>
        </table>
       

        <br />
    </div>
    </form>
</body>
</html>
