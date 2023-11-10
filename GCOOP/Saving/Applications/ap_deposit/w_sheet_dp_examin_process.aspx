<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_examin_process.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_examin_process" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=xmlCalIntEsimate %>
    <%=postEstimateDate%>
    <%=CheckCoop%>
    <script type="text/javascript">

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDwMainItemChange(s, row, c, v) {
            if (c == "cal_to_tdate") {
                s.SetItem(row, c, v);
                s.AcceptText();
                s.SetItem(1, "cal_to_date", Gcoop.ToEngDate(v));
                s.AcceptText();
//                postEstimateDate();

            }
            return 0;  
        }
        function OnDwHeadButtonClick(sender,row,btnName) {
            if (btnName == "btn_process") {
                xmlCalIntEsimate();
            }
        }
        function SheetLoadComplete() {
            //ctl00_ContentPlace_Chk_all
            Gcoop.GetEl("Chk_all").onclick = function () {
                var isChecked = Gcoop.GetEl("Chk_all").checked;
                var i = 0;
                //dw_menu
                var chkFlag;
                if (isChecked == true) {
                    chkFlag = 1;
                }
                else {
                    chkFlag = 0;
                }
                for (i = 1; i <= objdw_menu.RowCount(); i++) {
                    objdw_menu.SetItem(i, "ai_flag", chkFlag);
                }
            }
        }

//        function ProcessClick() {
//            alert("xxxx")
//            xmlCalIntEsimate();
//        }
        function OnDwListCoopClick(s, r, c) {
            if (c == "cross_coopflag") {
                Gcoop.CheckDw(s, r, c, "cross_coopflag", 1, 0);
                CheckCoop();
            }
        }
        function OnDwListCoopItemChanged(s, r, c, v) {
            if (c == "dddwcoopname") {
                s.SetItem(r, c, v);
                s.AcceptText();
                var coopid = s.GetItem(r, "dddwcoopname");
                Gcoop.GetEl("HfCoopid").value = coopid + "";
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwListCoop" runat="server" DataWindowObject="d_dp_dept_cooplist"
        LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="OnDwListCoopItemChanged" ClientFormatting="True" TabIndex="1"
        ClientEventClicked="OnDwListCoopClick">
    </dw:WebDataWindowControl>
    <table style="width: 100%;">
        <tr>
            <td td align=left>
               <%--<asp:Label ID="Label3" runat="server" Text="วันที่เข้าดอกเบี้ยถึง"></asp:Label>--%>
               <dw:WebDataWindowControl ID="dw_head" runat="server" DataWindowObject="d_dlg_intestimate_head"
                                        LibraryList="~/DataWindow/ap_deposit/dp_examin_process.pbl" AutoRestoreContext="False"
                                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" AutoRestoreDataCache="True"
                                        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked ="OnDwHeadButtonClick">
               </dw:WebDataWindowControl>
            </td>
            <td align=left>
               
               
            </td>
          
        </tr>
        <tr>
            <td valign=top >
                <asp:Label ID="Label2" runat="server" Text="<< ประเภทที่ต้องการประมาณการ >>" Font-Size="Medium" BackColor="White" ForeColor="Red"></asp:Label>
                <br>
                <asp:CheckBox ID="Chk_all" runat="server" Text=" เลือกทั้งหมด" Font-Size="Small" />
                <%--<asp:Label ID="Label1" runat="server" Text="ทั้งหมด"></asp:Label>--%>
                <dw:WebDataWindowControl ID="dw_menu" runat="server" DataWindowObject="d_dlg_intestimate_depttylist"
                                         LibraryList="~/DataWindow/ap_deposit/dp_examin_process.pbl" ClientScriptable="True"
                                         AutoRestoreContext="False" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True">
                </dw:WebDataWindowControl>
            </td>
            <td align=left>
            
               
            
            </td>
           
        </tr>
        <tr>
            <td align =right>
                <%-- <asp:Button ID="cb_process" runat="server" Text="ประมวลผล" 
                     onclick="cb_process_Click" />
                <asp:Button ID="cb_cancle" runat="server" Text="ยกเลิก" />--%>
            </td>
            <td>
                
            </td>
        </tr>
       
    </table>
    
    
   
    <asp:HiddenField ID="HfCoopid" runat="server" />
     <%=outputProcess%>
</asp:Content>
