<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_transloan_collateral.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_transloan_collateral" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postAccountNo%>
    <%=postTrnPrnAmt%>

    <script type="text/javascript">
        
        function OnDwMainItemChange(s, row, c, v){
            if(c == "trncollreq_tdate"){
                objdw_main.SetItem(row,c,v);
                objdw_main.AcceptText();
            }
            else if (c == "trncollreq_code" || c == "loancontract_no"){
                var trncollreqCode = "";
                var loanContract = "";
                objdw_main.SetItem(row,c,v);
                try{
                    trncollreqCode = objdw_main.GetItem(row, "trncollreq_code");
                    if(trncollreqCode == null) throw "";
                }catch(Err){ trncollreqCode = ""; }

                try{
                    loanContract = objdw_main.GetItem(row, "loancontract_no");
                    if(loanContract == null) throw "";
                }catch(Err){ loanContract = ""; }
                
                if(trncollreqCode != "" && loanContract != ""){
                    postAccountNo();   
                }
                objdw_main.AcceptText();
            }
        }
        
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){
            if(buttonName == "cb_search")
            {
                Gcoop.OpenDlg('600','590','w_dlg_sl_loancontract_search.aspx','');
            }
        }
        
        function OnDetailItemChange(sender, rowNumber, columnName, newValue){   
            if(columnName=="operate_flag"){
                objdw_detail.SetItem(rowNumber,columnName,newValue);
                postTrnPrnAmt();
                objdw_detail.AcceptText();
            }
        }
        function OnDwDetailItemClicked(sender, rowNumber, objectName)
        {
            Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", 1, 0);
            if(objectName == "operate_flag"){
                objdw_detail.SetItem(sender,rowNumber,objectName);
                postTrnPrnAmt();
                objdw_detail.AcceptText();
            }
        }
        
        function MenubarNew()
        {
            window.location = Gcoop.GetUrl() + "Applications/shrlon/w_sheet_sl_transloan_collateral.aspx";
        }
        
        function Validate(){
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        
        function GetValueFromDlg(loancontract_no){
            objdw_main.SetItem(1,"loancontract_no",loancontract_no);
            var trncollreqCode = "";
            try{
                trncollreqCode = objdw_main.GetItem(1, "trncollreq_code");
                if(trncollreqCode == null) throw "";
            }catch(Err){ trncollreqCode = ""; }
           
            if(trncollreqCode != ""){
                postAccountNo();
            }
            objdw_main.AcceptText();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="dw_main" runat="server" AutoRestoreContext="False" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="False" ClientScriptable="True" DataWindowObject="d_sl_trans_loan_collateral"
        LibraryList="~/DataWindow/shrlon/sl_transloan_collateral.pbl" AutoRestoreDataCache="True"
        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwMainButtonClicked">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="dw_detail" runat="server" AutoRestoreContext="False"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="False" ClientScriptable="True"
        LibraryList="~/DataWindow/shrlon/sl_transloan_collateral.pbl" DataWindowObject="d_sl_trans_loan_collateral_detail"
        AutoRestoreDataCache="True" ClientEventClicked="OnDwDetailItemClicked">
    </dw:WebDataWindowControl>
</asp:Content>
