﻿<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_yearclosegl.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_yearclosegl" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCloseYear%>
    <%=postNewClear%>

    <script type="text/javascript">
   
    function CloseYear(){
        var present_account_ye = objDw_main.GetItem(1, "account_year_t");
        var isconfirm = confirm("ยืนยันการปิดสิ้นปีบัญชีประจำ ปี  " + present_account_ye);
        if (isconfirm){
            postCloseYear();      
        }
        return 0;
    }  
    
     
    function MenubarNew(){
        if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
            postNewClear();
        }
    }
    
    
    
    
    </script>

    <style type="text/css">
        .style1
        {
            width: 563px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <b>
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" DataWindowObject="d_acc_closeyear_current"
                            LibraryList="~/DataWindow/account/close_year.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input id="B_closeyear" type="button" value="ปิดสิ้นปี" onclick="CloseYear()" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>
