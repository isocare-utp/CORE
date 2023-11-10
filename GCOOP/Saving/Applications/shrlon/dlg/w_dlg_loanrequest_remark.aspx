<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_loanrequest_remark.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_loanrequest_remark" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>เพิ่มเติมหมายเหตุ</title>
    <script type="text/javascript">

        function OnDwMainButtonClick(s, r, c) {

            var remark_desc = objdw_detail.GetItem(r, "remark");
            //window.opener.GetValueReamrk(remark_desc);
            window.opener.objdw_main.SetItem(1, "remark", remark_desc);
            window.close();

        }

        function DialogLoadComplete() {
            //alert("TEst");
            Gcoop.AddDisEnter("remark_0");
            var re = window.opener.objdw_main.GetItem(1, "remark");
            //alert(re);
            objdw_detail.SetItem(1, "remark", re);
        }

    </script>
    <style type="text/css">
        #TextArea1
        {
            height: 243px;
            width: 514px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <panel>
        <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_loanreq_remark"
            LibraryList="~/DataWindow/Shrlon/sl_loan_requestment_cen.pbl" Width="600px" Style="top: 0px;
            left: 0px" RowsPerPage="10" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="Auto" ClientScriptable="True" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClick">
           
        </dw:WebDataWindowControl>
       </panel>
        <br />
    </div>
    </form>
</body>
</html>
