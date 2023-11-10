<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_constant.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_constant" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<script type ="text/javascript" >
 function OnDwMainItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if (c == "begind_tdate") {
            s.SetItem(1, "begind_tdate", v);
            s.AcceptText();
            s.SetItem(1, "begind", Gcoop.ToEngDate(v));
            s.AcceptText();
        } else if (c == "endd_tdate") {
            s.SetItem(1, "endd_tdate", v);
            s.AcceptText();
            s.SetItem(1, "endd", Gcoop.ToEngDate(v));
            s.AcceptText();
        }
        else if (c == "begin_year_t") 
        {
            s.SetItem(1, "begin_year_t",v);
            s.AcceptText();
            var begin_year = v;
            begin_year = begin_year - 543;
            s.SetItem(1, "begin_year", begin_year);
            s.AcceptText();
        }
        return 0;  
    }
    
    function Validate() 
    {
       return confirm("ยืนยันการบันทึกข้อมูล");
    }
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width:100%;">
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_acc_start_constant" 
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
                        ClientEventItemChanged="OnDwMainItemChange">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
