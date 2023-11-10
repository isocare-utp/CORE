<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_set_formula.aspx.cs" Inherits="Saving.Applications.account.w_acc_set_formula" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postDwChoose%>
<%=postNewClear%>




<script type ="text/javascript" >
    
    function MenubarNew(){
        if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
            postNewClear();
        }
    }
    
     function OnRefresh(){
        postDwChoose();
     }
    
//    function MenubarOpen() {
//      Gcoop.OpenDlg(1100,500,"w_acc_dlg_setformula_det_sm.aspx", "");
//     }
   
    function OnDwChooseItemChange(s, r, c, v){
        if(c == "section_id"){
            objDw_choose.SetItem(r, c, v);
            objDw_choose.AcceptText();
            Gcoop.GetEl("Hd_sectionid").value = v;
            postDwChoose();
        }
        return 0;
    }
    
    
    function OnDwDataButtonClick(s,r,c)
    {
        if(c == "b_formula")
        {
            var data_desc = "";
            var data_type = "";
            var cnt_moneydet = "";
            var moneysheet_code = "";
            var moneysheet_seq = "";
            //mai ประกาศตัวแปร coopid
            var coop_id = "";
            //var data_type = "";
            
            data_desc = objDw_data.GetItem(r,"data_desc");
            data_type = objDw_data.GetItem(r,"data_type");
            cnt_moneydet = objDw_data.GetItem(r,"cnt_moneydet");
            moneysheet_code = objDw_data.GetItem(r,"moneysheet_code");
            moneysheet_seq = objDw_data.GetItem(r, "moneysheet_seq");
            
            
            if (cnt_moneydet > 0) {
                     Gcoop.OpenIFrame(600, 400, "w_acc_dlg_set_formula_cntmoney.aspx", "?moneysheet_code=" + moneysheet_code + "&moneysheet_seq=" + moneysheet_seq + "&data_desc=" + data_desc );
                }
                else
                {
                    if(data_type == "FC") {
                        Gcoop.OpenIFrame(900, 550, "w_acc_dlg_setformula_det.aspx", "?moneysheet_code=" + moneysheet_code + "&moneysheet_seq=" + moneysheet_seq);
                    }
                    else if(data_type == "SM") {
                        Gcoop.OpenIFrame(900, 550, "w_acc_dlg_setformula_det_sm.aspx", "?moneysheet_code=" + moneysheet_code + "&moneysheet_seq=" + moneysheet_seq);
                    }
                }
         }
    }
    
    function Validate()
    {
    
    }
          
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_choose" runat="server" ClientScriptable="True" 
        DataWindowObject="d_acc_section_sheetchoose" 
        LibraryList="~/DataWindow/account/acc_set_formula.pbl" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientEventItemChanged="OnDwChooseItemChange" 
                        ClientEventButtonClicked="OnDwChooseClick">
    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Vertical" 
                        BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_data" runat="server" 
        DataWindowObject="d_acc_set_formmula" 
        LibraryList="~/DataWindow/account/acc_set_formula.pbl" 
        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" 
    ClientEventButtonClicked="OnDwDataButtonClick">
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
                    <asp:HiddenField ID="Hd_sectionid" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
    </p>
    <p>
    </p>
</asp:Content>
