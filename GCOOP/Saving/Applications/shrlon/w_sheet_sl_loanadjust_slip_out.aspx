<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_loanadjust_slip_out.aspx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_sl_loanadjust_slip_out" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postMemberNo%>
    <%=postPost%>
    <%=setAMT  %>

    <script type="text/javascript">
    function MenubarNew(){
        window.location.reload();
    }
    function GetContFromDlg(contNo)
    { 
    //รับค่าเลขสัญญาจาก dlg
        objDwMain.SetItem( 1, "ref_slipno", contNo);
        objDwMain.AcceptText();
        postMemberNo();
    }
    
    function OnDwDetailClicked(s, r, c){
        Gcoop.CheckDw(s, r, c, "operate_flag", 1, 0);
        if(r <= 0) return 0;
        if(c == "operate_flag"){
            var adjType = objDwMain.GetItem( 1, "adjtype_code");
            var operateFlag = s.GetItem(r, "operate_flag");
            var prnAmt = s.GetItem( r, "bfmthpay_prnamt");
            var intAmt = s.GetItem(r, "bfmthpay_intamt");
            var itemAmt = s.GetItem( r , "bfmthpay_itemamt");
//            alert (itemAmt);
            var signFlag = s.GetItem( r , "sign_flag");
                 if(signFlag == null) {signFlag = 1;}
           var slipitemtype = s.GetItem(r, "slipitemtype_code");
           var totalAMT = s.GetItem( r, "item_balance");
           var slipAMT = objDwMain.GetItem(1, "slip_amt");
//           alert (slipAMT);
            var flagAmt  = 0;
            if ( adjType == "MTH"){
                
                
                if(operateFlag == 1){
                     if(slipitemtype == "SHR"){
                            flagAmt  = itemAmt* signFlag ;
                            totalAMT = totalAMT +flagAmt ;
                        }else if ( slipitemtype == "LON"){
                            itemAmt = prnAmt + intAmt ;
                            flagAmt =  prnAmt * signFlag ;
                            totalAMT = totalAMT +flagAmt ;
                        }
                         slipAMT =slipAMT +itemAmt ;
                }else{
                     if(slipitemtype == "SHR"){
                        flagAmt  = itemAmt* signFlag ;
                        totalAMT = totalAMT -flagAmt ;
                    }else if ( slipitemtype == "LON"){
                        itemAmt = prnAmt + intAmt ;
                        flagAmt =  prnAmt * signFlag ;
                        totalAMT = totalAMT -flagAmt ;
                    }
                    slipAMT =slipAMT - itemAmt ;
                    prnAmt = 0;
                    intAmt = 0;
                    itemAmt = 0;
                }
                   

                if ( adjType == "MTH"){
                    s.SetItem( r, "principal_adjamt", prnAmt );
                    s.SetItem( r, "interest_adjamt", intAmt );
                    s.SetItem( r, "item_adjamt", itemAmt );
                    s.SetItem( r, "item_balance",totalAMT );
                    s.AcceptText();
                    objDwMain.SetItem(1, "slip_amt", slipAMT);
                    objDwMain.AcceptText();

                }
            }else{
                itemAmt = s.GetItem( r , "item_adjamt");
                if (operateFlag == "1"){
                }else{
                   if(slipitemtype == "SHR"){
                        flagAmt  = itemAmt* signFlag ;
                        totalAMT = totalAMT -flagAmt ;
                    }else if ( slipitemtype == "LON"){
                        flagAmt =  prnAmt * signFlag ;
                        totalAMT = totalAMT -flagAmt ;
                    }
                    slipAMT =slipAMT - itemAmt ;
                    s.SetItem( r, "principal_adjamt", 0 );
                    s.SetItem( r, "interest_adjamt", 0 );
                    s.SetItem( r, "item_adjamt", 0 );
                    s.SetItem( r, "item_balance",totalAMT );
                    s.AcceptText();
                    objDwMain.SetItem(1, "slip_amt", slipAMT);
                    objDwMain.AcceptText();
                }
                Gcoop.GetEl("HdRow").value = r ;
                setAMT();
            }
            
        }
    }
    
    function OnDwDetailItemChanged(s, r, c, v){
        if(c == "principal_adjamt" || c == "interest_adjamt" || c == "item_adjamt"){
            var principalAdjamtOld = Gcoop.ParseFloat( s.GetItem(r, "principal_adjamt") + "");
            s.SetItem(r, c, v);
            s.AcceptText();
            var slipitemtypeCode = s.GetItem(r, "slipitemtype_code");
            var totalSum = 0;
            var adjType = objDwMain.GetItem(1, "adjtype_code");
            slipitemtypeCode = slipitemtypeCode != null && slipitemtypeCode != "" && slipitemtypeCode != undefined ? Gcoop.Trim(slipitemtypeCode) : "";
            
            if(slipitemtypeCode == "LON"){            
                var principalAdjamt = Gcoop.ParseFloat( s.GetItem(r, "principal_adjamt") + "");
                var interestAdjamt = Gcoop.ParseFloat( s.GetItem(r, "interest_adjamt") + "");
                var totalAMT = Gcoop.ParseFloat( s.GetItem(r, "item_balance") + "");
                var signFlag = s.GetItem( r , "sign_flag");
                    if(signFlag == null) {signFlag = 1;}
                var flagAmt  = 0;  
                if(c== "principal_adjamt"){
                     flagAmt = (principalAdjamt - principalAdjamtOld) *signFlag;
                     totalAMT = totalAMT+ flagAmt ;
                }
                totalSum = principalAdjamt + interestAdjamt;
                
                s.SetItem(r, "item_balance", totalAMT);
                s.SetItem(r, "item_adjamt", totalSum);
                s.AcceptText();
            }else {
                totalSum = Gcoop.ParseFloat( s.GetItem(r, "item_adjamt") + "");
            }
            
            var itemBalance = Gcoop.ParseFloat( s.GetItem(r, "item_balance") + "");
            var fullTotal = 0;
            for(i = 1; i <= s.RowCount(); i++){
                fullTotal += Gcoop.ParseFloat( s.GetItem(i, "item_adjamt") + "");
            }
            objDwMain.SetItem(1, "slip_amt", fullTotal);
            objDwMain.AcceptText();
            if(totalSum > itemBalance){
                alert("ยอดรวมปรับปรุง มากกว่า ยอดคงเหลือ จะไม่สามารถบันทึกข้อมูลได้");
            }
        }
        return 0;
    }
    function OnDwMainClicked(sender, rowNumber, objectName)
    {
        if (objectName== "b_refsearch")
        {
            var adjType = objDwMain.GetItem(1, "adjtype_code");
            if( (adjType == "CPL")||(adjType == "CMN"))
            {
                var memberNoVal = objDwMain.GetItem( 1, "member_no");
                Gcoop.OpenDlg(630,650, "w_dlg_sl_loancontract_search_memno.aspx", "?memno="+memberNoVal);
            }
        }
    }
    function OnDwMainItemChanged(s, r, c, v){
        if(c == "member_no"){
            s.SetItem(r, c, Gcoop.StringFormat(v, "000000"));
            s.AcceptText();
            postMemberNo();
        } else if(c == "ref_slipno"){
            var membNo = s.GetItem(1, "member_no");
            if(membNo != null && membNo != "" && membNo != undefined){
                s.SetItem(r, c, v);
                s.AcceptText();
                postMemberNo();
            }
        }else if ( c == "adjtype_code"){
             s.SetItem(r, c, v);
             s.SetItem(r, "ref_slipno", "");
             s.AcceptText();
             postMemberNo();
        }else if (c == "slipretcause_code"){ 
             s.SetItem(r, c, v);
             s.AcceptText();
        }
    }
    
    function SheetLoadComplete(s, r, c, v){
        if(Gcoop.GetEl("HdIsPostBack").value == "false"){
            Gcoop.Focus("member_no_0");
        }
    }
    
    function Validate(){
        var adjustCause = objDwMain.GetItem(1, "slipretcause_code");
        adjustCause = adjustCause == null || adjustCause == undefined ? "" : Gcoop.Trim(adjustCause);
        if(adjustCause == ""){
            alert("กรุณาใส่ข้อความเหตุผลที่ปรับปรุง");
            return false;
        }
        var detailRow = -1;
        try{
            detailRow = objDwDetail.RowCount();
        }catch(err){
            detailRow = -2;
        }
        if(detailRow <= 0){
            alert("ไม่มีข้อมูลหุ้นหนี้");
            return false;
        }
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server" />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
        DataWindowObject="d_loansrv_lnadjust_slip" LibraryList="~/DataWindow/shrlon/sl_loanadjust_slip_out.pbl"
        ClientEventItemChanged="OnDwMainItemChanged"  ClientEventClicked="OnDwMainClicked">
    </dw:WebDataWindowControl>
    <br />
    <br />
    <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
        ClientScriptable="True" DataWindowObject="d_loantest_lnadjust_slipdet" LibraryList="~/DataWindow/shrlon/sl_loanadjust_slip_out.pbl"
        ClientEventClicked="OnDwDetailClicked" ClientEventItemChanged="OnDwDetailItemChanged">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdIsPostBack" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content>
